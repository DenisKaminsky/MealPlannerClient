using System.Diagnostics;
using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Services
{
    public class MyProductsService : IMyProductsService
    {
        private readonly IMyProductsWebService _myProductsWebService;

        public MyProductsService(IMyProductsWebService myProductsWebService)
        {
            _myProductsWebService = myProductsWebService;
        }

        public async Task<List<MyProduct>> GetAllAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    return await GetAllFromBackend();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return await GetAllFromLocalStorage();
        }

        public async Task SaveAsync(List<MyProduct> myProducts)
        {
            var mappedData = myProducts.Select(x => new SaveMyProductDTO()
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    await SaveToBackend(mappedData);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            await SaveToLocalStorage(mappedData);
        }

        private async Task<List<MyProduct>> GetAllFromBackend()
        {
            return await _myProductsWebService.GetAllAsync();
        }

        private async Task<List<MyProduct>> GetAllFromLocalStorage()
        {
            await Task.Delay(1000);

            var result = new List<MyProduct>
            {
                new MyProduct()
                {
                    Id = "1",
                    CategoryId = "1",
                    CategoryName = "Category1",
                    ProductId = "1",
                    ProductName = "Product1",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Milliliter,
                    Quantity = 1000
                },
                new MyProduct()
                {
                    Id = "2",
                    CategoryId = "2",
                    CategoryName = "Category2",
                    ProductId = "2",
                    ProductName = "Product2",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Gram,
                    Quantity = 500
                },
                new MyProduct()
                {
                    Id = "3",
                    CategoryId = "3",
                    CategoryName = "Category3",
                    ProductId = "3",
                    ProductName = "Product3",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Unit,
                    Quantity = 2
                },
                new MyProduct()
                {
                    Id = "4",
                    CategoryId = "4",
                    CategoryName = "Category4",
                    ProductId = "4",
                    ProductName = "Product4",
                    ProductUnitOfMeasurement = ProductUnitOfMeasurement.Unit,
                    Quantity = 5
                },
                new MyProduct()
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

            return result;
        }

        private async Task SaveToBackend(List<SaveMyProductDTO> myProducts)
        {
            await _myProductsWebService.SaveAsync(myProducts);
        }

        private async Task SaveToLocalStorage(List<SaveMyProductDTO> myProducts)
        {
            await Task.Delay(2000);
        }
    }
}
