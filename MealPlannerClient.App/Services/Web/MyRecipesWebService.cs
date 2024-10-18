using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;
using Newtonsoft.Json;
using System.Text;

namespace MealPlannerClient.App.Services.Web
{
    public class MyRecipesWebService : IMyRecipesWebService
    {
        private readonly BackendHttpClient _httpClient;

        public MyRecipesWebService(BackendHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync("MyRecipes/GetAll");

            return JsonConvert.DeserializeObject<List<Recipe>>(result)!;
        }

        public async Task<string> SaveAsync(SaveNewRecipeDTO newRecipe)
        {
            using var jsonContent = new StringContent(
                JsonConvert.SerializeObject(newRecipe),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("MyRecipes/Save", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<SaveNewRecipeResponse>(responseBody)!;

            return parsedResponse.NewId;
        }

        public async Task DeleteAsync(string recipeId)
        {
            var response = await _httpClient.DeleteAsync($"MyRecipes/{recipeId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task FavoriteAsync(string recipeId)
        {
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"MyRecipes/Favorite/{recipeId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UnfavoriteAsync(string recipeId)
        {
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"MyRecipes/Unfavorite/{recipeId}", content);
            response.EnsureSuccessStatusCode();
        }

        private class SaveNewRecipeResponse
        {
            public string NewId { get; set; }
        }
    }
}
