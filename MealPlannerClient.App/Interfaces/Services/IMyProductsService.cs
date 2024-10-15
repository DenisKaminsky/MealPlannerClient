using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Services
{
    public interface IMyProductsService
    {
        Task<List<Product>> GetAllAsync();
    }
}
