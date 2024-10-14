using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Models;
using MealPlannerClient.App.Enums;

namespace MealPlannerClient.App.ViewModels
{
    public partial class InventoryPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty]
        private bool isPageEmpty = false;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private bool isDirty = false;

        [ObservableProperty] 
        private ObservableCollection<Product> myProducts;

        public InventoryPageViewModel()
        {
            myProducts = new ObservableCollection<Product>();
        }

        [RelayCommand]
        public void AddNewItem()
        {
            IsDirty = true;
            MyProducts.Add(new Product()
            {
                Id = "5",
                CategoryId = "5",
                CategoryName = "Category5",
                ProductId = "5",
                ProductName = "Product5",
                ProductUnitOfMeasurement = ProductUnitOfMeasurement.Milliliter,
                Quantity = 1000
            });
        }

        [RelayCommand]
        public void RemoveLastItem()
        {
            IsDirty = true;
            MyProducts.RemoveAt(MyProducts.Count-1);
        }

        [RelayCommand]
        public void ClearList()
        {
            IsPageEmpty = true;
            MyProducts.Clear();
        }

        private void MyProductsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            var a = 1;
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
                IsDirty = false;

                await InitializeProductsAsync();
            }
            finally
            {
                IsBusy = false;
            }

            if (MyProducts.Count == 0)
            {
                IsPageEmpty = true;
            }
        }

        public void CleanupAsync()
        {
            MyProducts.CollectionChanged -= MyProductsOnCollectionChanged;
            MyProducts.Clear();
        }


        private async Task InitializeProductsAsync()
        {
            try
            {
                var products = GetMyProducts();
                if (products != null && products.Any())
                {
                    MyProducts = new ObservableCollection<Product>(products);

                    IsPageEmpty = false;
                }
                else
                {
                    MyProducts = new ObservableCollection<Product>();
                }
            }
            catch (Exception ex)
            {
                await LogAndShowError("Error loading products", ex);
            }
        }

        private async Task LogAndShowError(string message, Exception ex)
        {
            await Toast.Make(message, ToastDuration.Short, 15).Show();
            Debug.WriteLine($"{message}: {ex.Message}");
        }






        private List<Product> GetMyProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = "1",
                    CategoryId = "1",
                    CategoryName = "Category1",
                    ProductId = "1",
                    ProductName = "Product1",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Milliliter,
                    Quantity = 1000
                },
                new Product()
                {
                    Id = "2",
                    CategoryId = "2",
                    CategoryName = "Category2",
                    ProductId = "2",
                    ProductName = "Product2",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Gram,
                    Quantity = 500
                },
                new Product()
                {
                    Id = "3",
                    CategoryId = "3",
                    CategoryName = "Category3",
                    ProductId = "3",
                    ProductName = "Product3",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Unit,
                    Quantity = 2
                },
                new Product()
                {
                    Id = "4",
                    CategoryId = "4",
                    CategoryName = "Category4",
                    ProductId = "4",
                    ProductName = "Product4",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Unit,
                    Quantity = 5
                },
                new Product()
                {
                    Id = "5",
                    CategoryId = "5",
                    CategoryName = "Category5",
                    ProductId = "5",
                    ProductName = "Product5",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Unit,
                    Quantity = 9
                }
            };
        }
    }
}
