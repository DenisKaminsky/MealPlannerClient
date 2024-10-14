using MealPlannerClient.App.Resources;
using MealPlannerClient.App.Resources.Styles;
using MealPlannerClient.App.Resources.Themes;

namespace MealPlannerClient.App.Helpers
{
    public static class ThemePreferencesManager
    {
        private const string PREFERENCE_KEY = "AppTheme";
        
        public static void Initialize()
        {
            var loadedFontSize = LoadTheme();
            SetTheme(loadedFontSize);
        }

        public static void SetTheme(Enums.CustomTheme theme)
        {
            ResourceDictionary newTheme = theme switch
            {
                Enums.CustomTheme.Main => new MainTheme(),
                Enums.CustomTheme.Navy => new NavyTheme(),
                Enums.CustomTheme.Hacker => new HackerTheme(),
                Enums.CustomTheme.Danger => new DangerTheme(),
                _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
            };

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current!.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                //Add new theme
                mergedDictionaries.Add(newTheme);
                //Add base styles
                mergedDictionaries.Add(new MealPlannerStylesBase());
            }

            Preferences.Set(PREFERENCE_KEY, theme.ToString());
        }

        private static Enums.CustomTheme LoadTheme()
        {
            var defaultValue = AppConstants.ThemeDefault;

            var savedValue = Preferences.Get(PREFERENCE_KEY, defaultValue.ToString());
            if (!Enum.TryParse(savedValue, out Enums.CustomTheme result))
            {
                result = defaultValue;
            }

            return result;
        }
    }
}
