using MealPlannerClient.App.ViewModels;

namespace MealPlannerClient.App.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();

        BindingContext = settingsPageViewModel;
    }
}