using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Web
{
    public interface IMyProductsWebService
    {
        Task<List<MyProduct>> GetAllAsync();

        Task SaveAsync(List<SaveMyProductDTO> myProducts);
    }
}
