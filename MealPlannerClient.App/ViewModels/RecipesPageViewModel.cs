using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Models;
using MealPlannerClient.App.Interfaces.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.ViewModels
{
    public partial class RecipesPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IRecipesService _recipesService;

        [ObservableProperty]
        private bool _hasMyRecipes = false;

        [ObservableProperty]
        private bool _isMyRecipesLoading = false;

        [ObservableProperty]
        private bool _isMyRecipesSaving = false;

        [ObservableProperty]
        private bool _createNewRecipePopupIsOpen = false;

        [ObservableProperty]
        private NewRecipe _newRecipe;

        public ObservableCollection<Recipe> MyRecipes { get; set; }

        public RecipesPageViewModel(IRecipesService recipesService)
        {
            _recipesService = recipesService;

            _newRecipe = new NewRecipe();
            MyRecipes = new ObservableCollection<Recipe>();
            MyRecipes.CollectionChanged += MyRecipesOnCollectionChanged;
        }

        public async Task InitializeAsync()
        {
            CreateNewRecipePopupIsOpen = false;

            await InitializeMyRecipesAsync();
        }

        public void CleanupAsync()
        {
            CreateNewRecipePopupIsOpen = false;
            IsMyRecipesLoading = false;
            IsMyRecipesSaving = false;
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
                IsMyRecipesSaving = true;

                var savedRecipe = await _recipesService.SaveAsync(NewRecipe);

                CloseCreateNewRecipeModal();

                await ShowToastMessageAsync("RecipeSaved");
            }
            catch (Exception)
            {
                await ShowToastMessageAsync("ErrorLoadingRecipe");
            }
            finally
            {
                IsMyRecipesSaving = false;
            }
        }

        private void MyRecipesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            HasMyRecipes = sender != null && ((IList)sender).Count > 0;
        }

        private async Task InitializeMyRecipesAsync()
        {
            try
            {
                if (IsMyRecipesLoading)
                {
                    return;
                }

                IsMyRecipesLoading = true;
                HasMyRecipes = false;

                var myRecipes= await _recipesService.GetAllAsync();
                MyRecipes.Clear();
                if (myRecipes != null)
                {
                    foreach (var item in myRecipes)
                    {
                        MyRecipes.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                await ShowToastMessageAsync("ErrorLoadingRecipe");
            }
            finally
            {
                IsMyRecipesLoading = false;
            }
        }

        private async Task ShowToastMessageAsync(string resourceKey)
        {
            var message = (string)LocalizationResourceManager.Instance[resourceKey];
            await Toast.Make(message, ToastDuration.Short, 15).Show();
        }
    }
}
