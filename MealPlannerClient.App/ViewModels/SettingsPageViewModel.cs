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
            LanguagePreferencesManager.SetRussianLanguage();
        }

        [RelayCommand]
        public void SetFontSize(FontSize fontSize)
        {
            FontPreferencesManager.SetFontSize(fontSize);
        }

        [RelayCommand]
        public void SetUSEnglish()
        {
            LanguagePreferencesManager.SetUSEnglishLanguage();
        }

        [RelayCommand]
        public void SetRussian()
        {
            LanguagePreferencesManager.SetRussianLanguage();
        }
    }
}
