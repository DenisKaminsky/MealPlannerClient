using MealPlannerClient.App.ViewModels;

namespace MealPlannerClient.App.Pages;

public partial class RecipesPage : ContentPage
{
    private readonly RecipesPageViewModel _viewModel;

    public RecipesPage(RecipesPageViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await _viewModel.InitializeAsync();
    }

    protected override void OnDisappearing()
    {
        _viewModel.CleanupAsync();

        base.OnDisappearing();
    }
}