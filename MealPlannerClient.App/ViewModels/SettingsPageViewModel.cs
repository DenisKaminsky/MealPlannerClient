using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Helpers;
using FontSize = MealPlannerClient.App.Enums.FontSize;

namespace MealPlannerClient.App.ViewModels
{
    public partial class SettingsPageViewModel: ObservableObject
    {
        [RelayCommand]
        public void SetTheme(CustomTheme theme)
        {
            ThemePreferencesManager.SetTheme(theme);
            Toast.Make("Theme changed", ToastDuration.Short, 15).Show();
        }

        [RelayCommand]
        public void SetFontSize(FontSize fontSize)
        {
            FontPreferencesManager.SetFontSize(fontSize);
            Toast.Make("Font changed", ToastDuration.Short, 15).Show();
        }

        [RelayCommand]
        public void SetUSEnglish()
        {
            LanguagePreferencesManager.SetUSEnglishLanguage();
            Toast.Make("Language changed", ToastDuration.Short, 15).Show();
        }

        [RelayCommand]
        public void SetRussian()
        {
            LanguagePreferencesManager.SetRussianLanguage();
            Toast.Make("Language changed", ToastDuration.Short, 15).Show();
        }
    }
}
