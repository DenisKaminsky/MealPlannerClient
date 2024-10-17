using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Models;
using System.Diagnostics;

namespace MealPlannerClient.App.ViewModels
{
    public partial class RecipesPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty]
        private bool createNewRecipePopupIsOpen;

        [ObservableProperty]
        private NewRecipe newRecipe;

        public RecipesPageViewModel()
        {
        }

        public async Task InitializeAsync()
        {
            try
            {
                CreateNewRecipePopupIsOpen = false;
            }
            finally
            {
                
            }
        }

        public void CleanupAsync()
        {
            CreateNewRecipePopupIsOpen = false;
        }

        [RelayCommand]
        private void OpenCreateNewRecipeModal()
        {
            NewRecipe = new NewRecipe();
            CreateNewRecipePopupIsOpen = true;
        }

        [RelayCommand]
        public void CloseCreateNewRecipeModal()
        {
            CreateNewRecipePopupIsOpen = false;
        }

        [RelayCommand]
        public async Task SaveNewRecipe()
        {
            if (string.IsNullOrWhiteSpace(NewRecipe.Name) || string.IsNullOrWhiteSpace(NewRecipe.Instructions))
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            try
            {
                CloseCreateNewRecipeModal();

                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                //log error
            }
        }
    }
}
