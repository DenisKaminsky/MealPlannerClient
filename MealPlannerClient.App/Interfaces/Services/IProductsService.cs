using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Services
{
    public interface IProductsService
    {
        public Task<List<Product>> GetAllAsync();
    }
}
