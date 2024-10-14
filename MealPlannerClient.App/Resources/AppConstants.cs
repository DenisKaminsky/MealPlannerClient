using MealPlannerClient.App.Enums;
using FontSize = MealPlannerClient.App.Enums.FontSize;

namespace MealPlannerClient.App.Resources
{
    public static class AppConstants
    {
        public static readonly FontSize FontSizeDefault = FontSize.Medium;
        public static readonly CustomTheme ThemeDefault = CustomTheme.Main;

        #region Button Font Size
        public static readonly double ButtonFontSizeSmall = 10;
        public static readonly double ButtonFontSizeMid = 15;
        public static readonly double ButtonFontSizeBig = 20;
        #endregion

        #region Label Font Size
        public static readonly double LabelFontSizeSmall = 17;
        public static readonly double LabelFontSizeMid = 23;
        public static readonly double LabelFontSizeBig = 30;
        #endregion

        #region Header Title Font Size
        public static readonly double HeaderTitleFontSizeSmall = 20;
        public static readonly double HeaderTitleFontSizeMid = 30;
        public static readonly double HeaderTitleFontSizeBig = 40;
        #endregion

        #region Language
        public static readonly string LanguageUSEnglish = "en-US";
        public static readonly string LanguageRussian = "ru";

        public static string LanguageDefault = LanguageUSEnglish;
        #endregion
    }
}
