using MealPlannerClient.App.ViewModels;

namespace MealPlannerClient.App.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();

        BindingContext = settingsPageViewModel;
    }

    private void OnGridTapped(object? sender, TappedEventArgs e)
    {
        var a = 1;
    }
}