using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Web
{
    public interface ICopilotWebService
    {
        Task<List<SuggestedRecipe>> SuggestRecipesAsync(int numberOfRecipes);
    }
}
