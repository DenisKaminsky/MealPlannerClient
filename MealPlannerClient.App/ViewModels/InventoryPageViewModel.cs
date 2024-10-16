using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Converters;
using MealPlannerClient.App.Models;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.ViewModels
{
    public partial class InventoryPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private const int AVAILABLE_PRODUCTS_PAGE_SIZE = 10;

        private readonly MeasurementUnitToIncrementConverter _measurementUnitToIncrementConverter;
        private readonly Dictionary<ProductUnitOfMeasurement, double> _incrementValuesBasedOnMeasurementUnit;

        private readonly IMyProductsService _myProductsService;
        private readonly IProductsService _productsService;

        [ObservableProperty]
        private bool _hasMyProducts = false;

        [ObservableProperty]
        private bool _isMyProductsBusy = false;

        [ObservableProperty]
        private bool _hasAvailableProducts = false;

        [ObservableProperty]
        private bool _isAvailableProductsLoaded = false;
        
        private string _availableProductsSearchString = String.Empty;

        [ObservableProperty]
        private int _availableProductsPageNumber = 0;

        [ObservableProperty]
        private int _availableProductsTotalPages = 0;

        [ObservableProperty]
        private bool _availableProductsIsFirstPage = true;

        [ObservableProperty]
        private bool _availableProductsIsLastPage = true;

        [ObservableProperty]
        private bool _isDirty = false;
        
        public ObservableCollection<MyProduct> MyProducts { get; set; }

        private List<Product> _allAvailableProducts;
        [ObservableProperty] 
        private List<ProductGroup> _availableProducts;

        public InventoryPageViewModel(IMyProductsService myProductsService, IProductsService productsService)
        {
            _myProductsService = myProductsService;
            _productsService = productsService;

            _measurementUnitToIncrementConverter = new MeasurementUnitToIncrementConverter();
            _incrementValuesBasedOnMeasurementUnit = new Dictionary<ProductUnitOfMeasurement, double>();

            MyProducts = new ObservableCollection<MyProduct>();
            MyProducts.CollectionChanged += MyProductsOnCollectionChanged;

            _allAvailableProducts = new List<Product>();
            _availableProducts = new List<ProductGroup>();
        }

        public async Task InitializeAsync()
        {
            IsDirty = false;

            try
            {
                await Task.WhenAll(
                    InitializeMyProductsAsync(),
                    InitializeAvailableProductsAsync()
                );
            }
            finally
            {
                IsDirty = false;
            }
        }

        public void CleanupAsync()
        {
            MyProducts.Clear();
            IsMyProductsBusy = false;
        }
        
        [RelayCommand]
        private void IncrementMyProductQuantity(string myProductId)
        {
            IsDirty = true;

            var item = MyProducts.First(p => p.Id == myProductId);
            var incrementValue = GetIncrementValueBasedOnProductMeasurementUnit(item.ProductUnitOfMeasurement);
            item.Quantity += incrementValue;
        }

        [RelayCommand]
        private void DecrementMyProductQuantity(string myProductId)
        {
            IsDirty = true;

            var item = MyProducts.First(p => p.Id == myProductId);
            var decrementValue = GetIncrementValueBasedOnProductMeasurementUnit(item.ProductUnitOfMeasurement);
            item.Quantity -= decrementValue;
        }

        [RelayCommand]
        private async Task AddMyProduct(Product product)
        {
            if (MyProducts.Any(p => p.ProductId == product.Id))
            {
                var message = (string)LocalizationResourceManager.Instance["MyProductAlreadyExists"];
                await Toast.Make(message, ToastDuration.Short, 15).Show();
            }
            else
            {
                var myProduct = new MyProduct()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName,
                    ProductUnitOfMeasurement = product.UnitOfMeasurement,
                    Quantity = 0
                };
                MyProducts.Add(myProduct);
            }
        }

        [RelayCommand]
        private void RemoveMyProduct(string myProductId)
        {
            var item = MyProducts.First(p => p.Id == myProductId);
            MyProducts.Remove(item);
        }

        [RelayCommand]
        private async Task SaveMyProducts()
        {
            IsMyProductsBusy = true;
            await _myProductsService.SaveAsync(MyProducts.ToList());

            IsMyProductsBusy = false;
            IsDirty = false;

            var successMessage = (string)LocalizationResourceManager.Instance["MyProductsSaved"];
            await Toast.Make(successMessage, ToastDuration.Short, 15).Show();
        }

        [RelayCommand]
        private void PerformAvailableProductsSearch(string searchString)
        {
            _availableProductsSearchString = searchString ?? string.Empty;
            AvailableProductsPageNumber = 1;

            FilterAvailableProducts();
        }

        [RelayCommand]
        private void GoToNextAvailableProductsPage()
        {
            if (AvailableProductsIsLastPage)
                return;

            AvailableProductsPageNumber++;

            FilterAvailableProducts();
        }

        [RelayCommand]
        private void GoToPreviousAvailableProductsPage()
        {
            if (AvailableProductsIsFirstPage)
                return;

            AvailableProductsPageNumber--;

            FilterAvailableProducts();
        }

        private void FilterAvailableProducts()
        {
            AvailableProductsIsFirstPage = AvailableProductsPageNumber == 1;

            var queryable = string.IsNullOrWhiteSpace(_availableProductsSearchString)
                ? _allAvailableProducts.AsQueryable()
                : _allAvailableProducts.Where(x =>
                    x.Name.Contains(_availableProductsSearchString, StringComparison.InvariantCultureIgnoreCase));

            var filtered = queryable.ToList();

            AvailableProductsTotalPages = (int)Math.Ceiling((double)filtered.Count / AVAILABLE_PRODUCTS_PAGE_SIZE);
            AvailableProductsIsLastPage = AvailableProductsTotalPages == AvailableProductsPageNumber;

            var filteredAndPaged = filtered
                .Skip(AVAILABLE_PRODUCTS_PAGE_SIZE * (AvailableProductsPageNumber - 1))
                .Take(AVAILABLE_PRODUCTS_PAGE_SIZE)
                .ToList();

            AvailableProducts = filteredAndPaged
                .GroupBy(x => x.CategoryId)
                .Select(x => new ProductGroup(x.Key, x.First().CategoryName, x.OrderBy(y => y.Name).ToList()))
                .ToList(); ;
        }

        private void MyProductsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            HasMyProducts = sender != null && ((IList)sender).Count > 0;
            IsDirty = true;
        }

        private async Task InitializeMyProductsAsync()
        {
            try
            {
                if (IsMyProductsBusy)
                {
                    return;
                }

                IsMyProductsBusy = true;
                HasMyProducts = false;

                var myProducts = await _myProductsService.GetAllAsync();
                MyProducts.Clear();
                if (myProducts != null)
                {
                    foreach (var item in myProducts)
                    {
                        MyProducts.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAndShowError("Error loading my products", ex);
            }
            finally
            {
                IsMyProductsBusy = false;
            }
        }

        private async Task InitializeAvailableProductsAsync()
        {
            try
            {
                if (IsAvailableProductsLoaded)
                {
                    return;
                }

                var products = await _productsService.GetAllAsync();
                if (products != null && products.Any())
                {
                    _allAvailableProducts = products;
                    AvailableProductsPageNumber = 1;
                    FilterAvailableProducts();

                    HasAvailableProducts = true;
                }
                else
                {
                    HasAvailableProducts = false;
                }

                IsAvailableProductsLoaded = true;
            }
            catch (Exception ex)
            {
                await LogAndShowError("Error loading all available products", ex);
            }
        }

        #region Helpers
        private double GetIncrementValueBasedOnProductMeasurementUnit(ProductUnitOfMeasurement measurementUnit)
        {
            if (_incrementValuesBasedOnMeasurementUnit.ContainsKey(measurementUnit))
            {
                return _incrementValuesBasedOnMeasurementUnit[measurementUnit];
            }
            else
            {
                var increment = (double)_measurementUnitToIncrementConverter.Convert(measurementUnit, typeof(double), null, default!)!;
                _incrementValuesBasedOnMeasurementUnit.Add(measurementUnit, increment);
                return increment;
            }
        }

        private async Task LogAndShowError(string message, Exception ex)
        {
            await Toast.Make(message, ToastDuration.Short, 15).Show();
            Debug.WriteLine($"{message}: {ex.Message}");
        }
        #endregion
    }
}
