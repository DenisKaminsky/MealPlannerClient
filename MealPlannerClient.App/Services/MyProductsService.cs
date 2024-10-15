using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Services
{
    public class MyProductsService: IMyProductsService
    {
        public async Task<List<MyProduct>> GetAllAsync()
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
    }
}
