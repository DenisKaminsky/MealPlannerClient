using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Web
{
    public interface IProductsWebService
    {
        Task<List<Product>> GetAllAsync();
    }
}
