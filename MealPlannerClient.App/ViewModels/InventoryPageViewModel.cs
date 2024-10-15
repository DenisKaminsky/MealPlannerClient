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

namespace MealPlannerClient.App.ViewModels
{
    public partial class InventoryPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly MeasurementUnitToIncrementConverter _measurementUnitToIncrementConverter;
        private readonly Dictionary<ProductUnitOfMeasurement, double> _incrementValuesBasedOnMeasurementUnit;

        private readonly IMyProductsService _myProductsService;

        [ObservableProperty]
        private bool _hasMyProducts = false;

        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private bool _isDirty = false;

        [ObservableProperty] 
        private ObservableCollection<Product> _myProducts;

        public InventoryPageViewModel(IMyProductsService myProductsService)
        {
            _myProductsService = myProductsService;

            _measurementUnitToIncrementConverter = new MeasurementUnitToIncrementConverter();
            _incrementValuesBasedOnMeasurementUnit = new Dictionary<ProductUnitOfMeasurement, double>();

            MyProducts = new ObservableCollection<Product>();
            MyProducts.CollectionChanged += MyProductsOnCollectionChanged;
        }

        public async Task InitializeAsync()
        {
            //Update IsBusy status
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                await InitializeProductsAsync();
            }
            finally
            {
                IsBusy = false;
                IsDirty = false;
            }
        }

        public void CleanupAsync()
        {
            MyProducts.Clear();
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
        public void AddNewItem()
        {
            MyProducts.Add(new Product()
            {
                Id = "6",
                CategoryId = "5",
                CategoryName = "Category5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                ProductId = "5",
                ProductName = "Product5",
                ProductUnitOfMeasurement = ProductUnitOfMeasurement.Milliliter,
                Quantity = 1000
            });
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

        private async Task InitializeProductsAsync()
        {
            try
            {
                var products = await _myProductsService.GetAllAsync();
                MyProducts.Clear();
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        MyProducts.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAndShowError("Error loading products", ex);
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
