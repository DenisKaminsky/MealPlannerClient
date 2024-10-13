using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Helpers;

namespace MealPlannerClient.App.ViewModels
{
    public partial class SettingsPageViewModel: ObservableObject
    {
        [RelayCommand]
        public void SetSmallFontSize()
        {
            FontPreferencesManager.SetFontSize(FontSize.Small);
        }

        [RelayCommand]
        public void SetMidFontSize()
        {
            FontPreferencesManager.SetFontSize(FontSize.Medium);
        }

        [RelayCommand]
        public void SetBigFontSize()
        {
            FontPreferencesManager.SetFontSize(FontSize.Big);
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
