using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Models;
using System.Diagnostics;
using MealPlannerClient.App.Interfaces.Services;

namespace MealPlannerClient.App.Services
{
    public class RecipesService: IRecipesService
    {
        public async Task<List<Recipe>> GetAllAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    return await GetAllFromBackend();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return await GetAllFromLocalStorage();
        }

        public async Task<Recipe> SaveAsync(NewRecipe newRecipe)
        {
            var dto = ConvertNewRecipeToSaveNewRecipeDto(newRecipe);

            string newId;
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    newId = await SaveToBackendAsync(dto);
                    return ConvertSaveNewRecipeDtoToRecipe(dto, newId, false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            newId = await SaveToLocalStorageAsync(dto);
            return ConvertSaveNewRecipeDtoToRecipe(dto, newId, false);
        }

        private async Task<List<Recipe>> GetAllFromBackend()
        {
            await Task.Delay(5000);

            throw new NotImplementedException();
        }

        private async Task<List<Recipe>> GetAllFromLocalStorage()
        {
            await Task.Delay(1000);

            var result = new List<Recipe>
            {
                new Recipe()
                {
                    Id = "1",
                    Name = "Caesar Salad",
                    CookTimeInMinutes = 15,
                    IsFavorite = true,
                    DateCreatedUtc = DateTime.UtcNow,
                    Ingredients = new []
                    {
                        "Chicken breast (x1)",
                        "Tomatoes (x10)",
                        "Parmesan Cheese (300g)"
                    },
                    PreparationSteps = new []
                    {
                        "Wash Tomatoes",
                        "Cut Cheese"
                    },
                    Instructions = new []
                    {
                        "Inst 1",
                        "Inst 2",
                        "Inst 3",
                        "Inst 4"
                    }
                },
                new Recipe()
                {
                    Id = "2",
                    Name = "Greek Salad",
                    CookTimeInMinutes = 15,
                    IsFavorite = false,
                    DateCreatedUtc = DateTime.UtcNow,
                    Ingredients = new []
                    {
                        "Chicken breast (x1)",
                        "Tomatoes (x1)",
                        "Feta Cheese (x1)"
                    },
                    PreparationSteps = new []
                    {
                        "Wash Tomatoes",
                        "Cut Cheese",
                        "Wash hands:)"
                    },
                    Instructions = new []
                    {
                        "Inst 1",
                        "Inst 2",
                        "Inst 3"
                    }
                },
            };

            return result;
        }

        private async Task<string> SaveToBackendAsync(SaveNewRecipeDTO newRecipe)
        {
            await Task.Delay(2000);

            return Guid.NewGuid().ToString();
        }

        private async Task<string> SaveToLocalStorageAsync(SaveNewRecipeDTO newRecipe)
        {
            await Task.Delay(1000);

            return Guid.NewGuid().ToString();
        }

        private SaveNewRecipeDTO ConvertNewRecipeToSaveNewRecipeDto(NewRecipe newRecipe)
        {
            return new SaveNewRecipeDTO
            {
                Name = newRecipe.Name,
                CookTimeInMinutes = newRecipe.CookTimeInMinutes,
                DateCreatedUtc = DateTime.UtcNow,
                PreparationSteps = ConvertTextToArrayOfRows(newRecipe.PreparationSteps),
                Ingredients = ConvertTextToArrayOfRows(newRecipe.Ingredients),
                Instructions = ConvertTextToArrayOfRows(newRecipe.Instructions)
            };
        }

        private Recipe ConvertSaveNewRecipeDtoToRecipe(SaveNewRecipeDTO dto, string newId, bool isFavorite)
        {
            return new Recipe
            {
                Id = newId,
                Name = dto.Name,
                IsFavorite = isFavorite,
                CookTimeInMinutes = dto.CookTimeInMinutes,
                DateCreatedUtc = dto.DateCreatedUtc,
                PreparationSteps = dto.PreparationSteps,
                Ingredients = dto.PreparationSteps,
                Instructions = dto.Instructions
            };
        }

        private IEnumerable<string> ConvertTextToArrayOfRows(string text)
        {
            return text.Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(row => row.Trim())                                      
                .Where(row => !string.IsNullOrEmpty(row))                       
                .ToArray();
        }
    }
}
