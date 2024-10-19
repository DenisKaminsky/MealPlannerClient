using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Interfaces.Services
{
    public interface ICopilotService
    {
        Task<List<SuggestedRecipe>> SuggestRecipesAsync(int numberOfRecipes);

        Task SaveAsync(SuggestedRecipe suggestedRecipe);
    }
}
