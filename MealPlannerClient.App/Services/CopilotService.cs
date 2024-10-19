using System.Diagnostics;
using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Services
{
    public class CopilotService : ICopilotService
    {
        private readonly IMyRecipesWebService _myRecipesWebService;
        private readonly ICopilotWebService _copilotWebService;


        public CopilotService(IMyRecipesWebService myRecipesWebService, ICopilotWebService copilotWebService)
        {
            _myRecipesWebService = myRecipesWebService;
            _copilotWebService = copilotWebService;
        }

        public async Task<List<SuggestedRecipe>> SuggestRecipesAsync(int numberOfRecipes)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    return await _copilotWebService.SuggestRecipesAsync(numberOfRecipes);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            return new List<SuggestedRecipe>();
        }

        public async Task SaveAsync(SuggestedRecipe suggestedRecipe)
        {
            var saveDto = ConvertSuggestedRecipeToSaveNewRecipeDto(suggestedRecipe);
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    await SaveToBackendAsync(saveDto);
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            await SaveToLocalStorageAsync(saveDto);
        }

        private async Task SaveToBackendAsync(SaveNewRecipeDTO newRecipe)
        {
            await _myRecipesWebService.SaveAsync(newRecipe);
        }

        private async Task SaveToLocalStorageAsync(SaveNewRecipeDTO newRecipe)
        {
            await Task.Delay(1000);
        }

        private SaveNewRecipeDTO ConvertSuggestedRecipeToSaveNewRecipeDto(SuggestedRecipe suggestedRecipe)
        {
            return new SaveNewRecipeDTO
            {
                Name = suggestedRecipe.Name,
                CookTimeInMinutes = suggestedRecipe.CookTimeInMinutes,
                CreatedDateUtc = DateTime.UtcNow,
                PreparationSteps = suggestedRecipe.PreparationSteps,
                Ingredients = suggestedRecipe.Ingredients,
                Instructions = suggestedRecipe.Instructions
            };
        }
    }
}
