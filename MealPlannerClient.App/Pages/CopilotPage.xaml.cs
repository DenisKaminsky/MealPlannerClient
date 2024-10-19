using MealPlannerClient.App.ViewModels;

namespace MealPlannerClient.App.Pages;

public partial class CopilotPage : ContentPage
{
    private readonly CopilotPageViewModel _viewModel;

    public CopilotPage(CopilotPageViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnDisappearing()
    {
        _viewModel.Cleanup();

        base.OnDisappearing();
    }
}