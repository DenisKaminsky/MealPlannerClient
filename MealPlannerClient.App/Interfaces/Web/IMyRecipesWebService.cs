using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Web
{
    public interface IMyRecipesWebService
    {
        Task<List<Recipe>> GetAllAsync();

        Task<string> SaveAsync(SaveNewRecipeDTO newRecipe);

        Task DeleteAsync(string recipeId);

        Task FavoriteAsync(string recipeId);

        Task UnfavoriteAsync(string recipeId);
    }
}
