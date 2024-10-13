using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealPlannerClient.App.Resources;
using MealPlannerClient.App.UserPreferences;

namespace MealPlannerClient.App.ViewModels
{
    public partial class SettingsPageViewModel: ObservableObject
    {
        [RelayCommand]
        public void SetSmallFontSize()
        {
            PreferencesManager.UpdateFontSize(AppConstants.ButtonFontSizeSmall);
        }

        [RelayCommand]
        public void SetMidFontSize()
        {
            PreferencesManager.UpdateFontSize(AppConstants.ButtonFontSizeMid);
        }

        [RelayCommand]
        public void SetBigFontSize()
        {
            PreferencesManager.UpdateFontSize(AppConstants.ButtonFontSizeBig);
        }

        [RelayCommand]
        public void SetUSEnglish()
        {
            PreferencesManager.SetUSEnglishLanguage();
        }

        [RelayCommand]
        public void SetRussian()
        {
            PreferencesManager.SetRussianLanguage();
        }
    }
}
