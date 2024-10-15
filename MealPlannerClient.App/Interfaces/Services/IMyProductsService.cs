using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Services
{
    public interface IMyProductsService
    {
        Task<List<MyProduct>> GetAllAsync();

        Task SaveAsync(List<MyProduct> myProducts);
    }
}
