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
        private readonly MeasurementUnitToIncrementConverter _measurementUnitToIncrementConverter;
        private readonly Dictionary<ProductUnitOfMeasurement, double> _incrementValuesBasedOnMeasurementUnit;

        private readonly IMyProductsService _myProductsService;
        private readonly IProductsService _productsService;

        [ObservableProperty]
        private bool _hasMyProducts = false;

        [ObservableProperty]
        private bool _hasAvailableProducts = false;

        [ObservableProperty]
        private bool _isMyProductsBusy = false;

        [ObservableProperty]
        private bool _isAvailableProductsLoaded = false;

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
                await InitializeMyProductsAsync();
                await InitializeAvailableProductsAsync();
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
        public void IncrementMyProductQuantity(string myProductId)
        {
            IsDirty = true;

            var item = MyProducts.First(p => p.Id == myProductId);
            var incrementValue = GetIncrementValueBasedOnProductMeasurementUnit(item.ProductUnitOfMeasurement);
            item.Quantity += incrementValue;
        }

        [RelayCommand]
        public void DecrementMyProductQuantity(string myProductId)
        {
            IsDirty = true;

            var item = MyProducts.First(p => p.Id == myProductId);
            var decrementValue = GetIncrementValueBasedOnProductMeasurementUnit(item.ProductUnitOfMeasurement);
            item.Quantity -= decrementValue;
        }

        [RelayCommand]
        public async Task AddMyProduct(Product product)
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
        public void RemoveMyProduct(string myProductId)
        {
            var item = MyProducts.First(p => p.Id == myProductId);
            MyProducts.Remove(item);
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
                    AvailableProducts = products
                        .GroupBy(x => x.CategoryId)
                        .Select(x => new ProductGroup(x.Key, x.First().CategoryName, x.ToList()))
                        .ToList();

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
