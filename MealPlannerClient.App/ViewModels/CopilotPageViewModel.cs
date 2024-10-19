using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using MealPlannerClient.App.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;
using MealPlannerClient.App.Resources;
using System.Collections.Specialized;
using System.Collections;
using MealPlannerClient.App.Interfaces.Services;

namespace MealPlannerClient.App.ViewModels
{
    public partial class CopilotPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly ICopilotService _copilotService;

        [ObservableProperty]
        private bool _hasSuggestedRecipes = false;

        [ObservableProperty]
        private bool _isSuggestedRecipesLoading = false;

        [ObservableProperty]
        private bool _isSuggestedRecipeSaving = false;

        public ObservableCollection<SuggestedRecipe> SuggestedRecipes { get; set; }

        public CopilotPageViewModel(ICopilotService copilotService)
        {
            _copilotService = copilotService;

            SuggestedRecipes = new ObservableCollection<SuggestedRecipe>();
            SuggestedRecipes.CollectionChanged += SuggestedRecipesOnCollectionChanged;
        }

        public void Cleanup()
        {
            IsSuggestedRecipesLoading = false;
            IsSuggestedRecipeSaving = false;
        }

        [RelayCommand]
        private async Task SuggestRecipesAsync()
        {
            try
            {
                if (IsSuggestedRecipesLoading)
                {
                    return;
                }

                IsSuggestedRecipesLoading = true;
                HasSuggestedRecipes = false;

                var suggestedRecipes = await _copilotService.SuggestRecipesAsync(3);
                SuggestedRecipes.Clear();
                if (suggestedRecipes != null)
                {
                    foreach (var item in suggestedRecipes)
                    {
                        SuggestedRecipes.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                await ShowToastMessageAsync("ErrorLoadingRecipe");
            }
            finally
            {
                IsSuggestedRecipesLoading = false;
            }
        }

        [RelayCommand]
        public async Task SaveSuggestedRecipeAsync(SuggestedRecipe suggestedRecipe)
        {
            try
            {
                IsSuggestedRecipeSaving = true;

                await _copilotService.SaveAsync(suggestedRecipe);
                suggestedRecipe.IsSaved = true;

                await ShowToastMessageAsync("RecipeSaved");
            }
            catch (Exception)
            {
                await ShowToastMessageAsync("ErrorSavingRecipe");
            }
            finally
            {
                IsSuggestedRecipeSaving = false;
            }
        }

        private void SuggestedRecipesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            HasSuggestedRecipes = sender != null && ((IList)sender).Count > 0;
        }

        private async Task ShowToastMessageAsync(string resourceKey)
        {
            var message = (string)LocalizationResourceManager.Instance[resourceKey];
            await Toast.Make(message, ToastDuration.Short, 15).Show();
        }
    }
}
