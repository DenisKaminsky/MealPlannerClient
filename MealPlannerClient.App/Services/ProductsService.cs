using System.Diagnostics;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsWebService _productsWebService;

        public ProductsService(IProductsWebService productsWebService)
        {
            _productsWebService = productsWebService;
        }

        public async Task<List<Product>> GetAllAsync()
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

        private async Task<List<Product>> GetAllFromBackend()
        {
            return await _productsWebService.GetAllAsync();
        }

        private async Task<List<Product>> GetAllFromLocalStorage()
        {
            await Task.Delay(1000);

            var result = new List<Product>
            {
                new Product()
                {
                    Id = "6",
                    CategoryId = "6",
                    CategoryName = "Category6",
                    Name = "Product6",
                    UnitOfMeasurement = ProductUnitOfMeasurement.Milliliter
                },
                new Product()
                {
                    Id = "7",
                    CategoryId = "6",
                    CategoryName = "Category6",
                    Name = "Product7",
                    UnitOfMeasurement = ProductUnitOfMeasurement.Gram
                },
                new Product()
                {
                    Id = "8",
                    CategoryId = "6",
                    CategoryName = "Category6",
                    Name = "Product8",
                    UnitOfMeasurement = ProductUnitOfMeasurement.Unit
                },
                new Product()
                {
                    Id = "9",
                    CategoryId = "7",
                    CategoryName = "Category7",
                    Name = "Product9",
                    UnitOfMeasurement = ProductUnitOfMeasurement.Unit
                },
                new Product()
                {
                    Id = "10",
                    CategoryId = "8",
                    CategoryName = "Category8",
                    Name = "Product10",
                    UnitOfMeasurement = ProductUnitOfMeasurement.Unit
                }
            };

            return result;
        }
    }
}
