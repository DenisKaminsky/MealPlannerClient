using MealPlannerClient.App.Interfaces.Web;
using Newtonsoft.Json;
using MealPlannerClient.App.Models;

namespace MealPlannerClient.App.Services.Web
{ 
    public class CopilotWebService: ICopilotWebService
    {
        private readonly BackendHttpClient _httpClient;

        public CopilotWebService(BackendHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SuggestedRecipe>> SuggestRecipesAsync(int numberOfRecipes)
        {
            var parameter = new SuggestRecipesRequestTemp(numberOfRecipes);

            using var jsonContent = new StringContent(
                JsonConvert.SerializeObject(parameter),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("Copilot/Suggest", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SuggestedRecipe>>(responseBody)!;
        }

        private class SuggestRecipesRequestTemp
        {
            public int NumberOfRecipes { get; }

            public SuggestRecipesRequestTemp(int numberOfRecipes)
            {
                NumberOfRecipes = numberOfRecipes;
            }
        }
    }
}
