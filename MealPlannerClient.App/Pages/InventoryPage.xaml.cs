using MealPlannerClient.App.Resources;
using MealPlannerClient.App.ViewModels;

namespace MealPlannerClient.App.Pages;

public partial class InventoryPage : ContentPage
{
    private readonly InventoryPageViewModel _viewModel;

    public InventoryPage(InventoryPageViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Shell.Current.Navigating += OnNavigating!;
        await _viewModel.InitializeAsync();
    }

    protected override void OnDisappearing()
    {
        Shell.Current.Navigating -= OnNavigating!;
        _viewModel.CleanupAsync();

        base.OnDisappearing();
    }

    private async void OnNavigating(object sender, ShellNavigatingEventArgs args)
    {
        // Pause navigation till the user makes a decision
        var deferral = args.GetDeferral();

        // Show alert
        if (_viewModel.IsDirty)
        {
            var modalHeader = (string)LocalizationResourceManager.Instance["LeavePageWithUnsavedChangesAlertTitle"];
            var modalText = (string)LocalizationResourceManager.Instance["LeavePageWithUnsavedChangesAlertText"];
            var modalYesButton = (string)LocalizationResourceManager.Instance["LeavePageWithUnsavedChangesAlertYesOption"];
            var modalNoButton = (string)LocalizationResourceManager.Instance["LeavePageWithUnsavedChangesAlertNoOption"];

            var answer = await DisplayAlert(modalHeader, modalText, modalYesButton, modalNoButton);
            if (!answer)
            {
                args.Cancel();
            }
        }

        // Un-pause navigation
        deferral.Complete();
    }
}