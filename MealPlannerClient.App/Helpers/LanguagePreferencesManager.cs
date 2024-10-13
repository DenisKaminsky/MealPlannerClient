using System.Globalization;
using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.Helpers
{
    public class LanguagePreferencesManager
    {
        private const string PREFERENCE_KEY = "AppLanguage";

        public static void SetUSEnglishLanguage() => UpdateLanguage(AppConstants.LanguageUSEnglish);

        public static void SetRussianLanguage() => UpdateLanguage(AppConstants.LanguageRussian);

        public static void Initialize()
        {
            var lang = Preferences.Get(PREFERENCE_KEY, AppConstants.LanguageDefault);

            var culture = new CultureInfo(lang);
            LocalizationResourceManager.Instance.SetCulture(culture);
        }

        private static void UpdateLanguage(string lang)
        {
            var culture = new CultureInfo(lang);
            LocalizationResourceManager.Instance.SetCulture(culture);
            Preferences.Set(PREFERENCE_KEY, lang);
        }
    }
}
