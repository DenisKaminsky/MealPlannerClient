using System.Globalization;
using MealPlannerClient.App.Resources.Languages;

namespace MealPlannerClient.App.Pages;

public partial class TestPAgeGrid : ContentPage
{
	public TestPAgeGrid()
	{
		InitializeComponent();
    }

    private void OnNormalTip(object? sender, EventArgs e)
    {
        if (Preferences.ContainsKey("DenisTest"))
        {
            DenisLabel.Text = Preferences.Get("DenisTest", "DEF");
        }
        else
        {
            Preferences.Set("DenisTest", "This is Denis");
            DenisLabel.Text = "Not Denis";

        }
        //Toast.Make("Recipe saved successfully!", ToastDuration.Short, 15).Show();
    }

    private void OnGenerousTip(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ChangeLabelColors(object? sender, EventArgs e)
    {
        this.Resources["LabelColor"] = Colors.Green;
    }

    private void English_OnClicked(object? sender, EventArgs e)
    {
        var culture = new CultureInfo("en-US");
        LocalizationResourceManager.Instance.SetCulture(culture);
    }

    private void Russian_OnClicked(object? sender, EventArgs e)
    {
        var culture = new CultureInfo("ru");
        LocalizationResourceManager.Instance.SetCulture(culture);
    }
}