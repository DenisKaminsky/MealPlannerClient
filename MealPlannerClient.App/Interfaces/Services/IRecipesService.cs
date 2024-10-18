using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Services
{
    public interface IRecipesService
    {
        Task<List<Recipe>> GetAllAsync();
        Task<Recipe> SaveAsync(NewRecipe newRecipe);
    }
}
