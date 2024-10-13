using MealPlannerClient.App.Resources;
using System.Globalization;

namespace MealPlannerClient.App.UserPreferences
{
    public static class PreferencesManager
    {
        private const string BUTTON_FONT_SIZE_KEY = "ButtonFontSize";
        private const string LANGUAGE_KEY = "Language";

        public static void SaveUIPreferencesToResources()
        {
            //this.Resources["LabelColor"] = Colors.Green;
            InitializeResourceFromPreferences(BUTTON_FONT_SIZE_KEY, AppConstants.ButtonFontSizeDefault);

            InitializeLanguage();
        }

        public static void UpdateFontSize(double fontSize)
        {
            UpdateResource(BUTTON_FONT_SIZE_KEY, fontSize);
        }

        public static void SetUSEnglishLanguage() => UpdateLanguage(AppConstants.LanguageUSEnglish);

        public static void SetRussianLanguage() => UpdateLanguage(AppConstants.LanguageRussian);


        #region private methods

        private static void UpdateResource(string resourceKey, double value)
        {
            Application.Current!.Resources[resourceKey] = value;
            Preferences.Set(resourceKey, value);
        }

        private static void UpdateLanguage(string lang)
        {
            var culture = new CultureInfo(lang);
            LocalizationResourceManager.Instance.SetCulture(culture);
            Preferences.Set(LANGUAGE_KEY, lang);
        }


        private static void InitializeResourceFromPreferences(string resourceKey, double defaultValue)
        {
            var value = Preferences.Get(resourceKey, defaultValue);
            Application.Current!.Resources[resourceKey] = value;
        }

        private static void InitializeLanguage()
        {
            var lang = Preferences.Get(LANGUAGE_KEY, AppConstants.LanguageDefault);

            var culture = new CultureInfo(lang);
            LocalizationResourceManager.Instance.SetCulture(culture);
        }

        #endregion
    }
}
