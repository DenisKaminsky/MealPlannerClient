using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.Helpers
{
    public static class FontPreferencesManager
    {
        private const string PREFERENCE_KEY = "AppFontSize";

        private const string BUTTON_FONT_SIZE_KEY = "ButtonFontSize";
        private const string LABEL_FONT_SIZE_KEY = "LabelFontSize";
        private const string HEADER_TITLE_FONT_SIZE_KEY = "HeaderTitleFontSize";
        private const string TEXT_FONT_SIZE_KEY = "TextFontSize";
        private const string HELP_TEXT_FONT_SIZE_KEY = "HelpTextFontSize";
        private const string INPUT_TEXT_FONT_SIZE_KEY = "InputTextFontSize";

        public static void Initialize()
        {
            var loadedFontSize = LoadFontSize();
            SetFontSize(loadedFontSize);
        }

        public static void SetFontSize(Enums.FontSize fontSize)
        {
            switch (fontSize)
            {
                case Enums.FontSize.Small:
                    SetSmallFontSize();
                    break;
                case Enums.FontSize.Medium:
                    SetMediumFontSize();
                    break;
                case Enums.FontSize.Big:
                    SetBigFontSize();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontSize), fontSize, null);
            }

            Preferences.Set(PREFERENCE_KEY, fontSize.ToString());
        }

        private static void SetSmallFontSize()
        {
            Save(BUTTON_FONT_SIZE_KEY, AppConstants.ButtonFontSizeSmall);
            Save(LABEL_FONT_SIZE_KEY, AppConstants.LabelFontSizeSmall);
            Save(HEADER_TITLE_FONT_SIZE_KEY, AppConstants.HeaderTitleFontSizeSmall);
            Save(TEXT_FONT_SIZE_KEY, AppConstants.TextFontSizeSmall);
            Save(HELP_TEXT_FONT_SIZE_KEY, AppConstants.HelpTextFontSizeSmall);
            Save(INPUT_TEXT_FONT_SIZE_KEY, AppConstants.InputTextFontSizeSmall);
        }

        private static void SetMediumFontSize()
        {
            Save(BUTTON_FONT_SIZE_KEY, AppConstants.ButtonFontSizeMid);
            Save(LABEL_FONT_SIZE_KEY, AppConstants.LabelFontSizeMid);
            Save(HEADER_TITLE_FONT_SIZE_KEY, AppConstants.HeaderTitleFontSizeMid);
            Save(TEXT_FONT_SIZE_KEY, AppConstants.TextFontSizeMid);
            Save(HELP_TEXT_FONT_SIZE_KEY, AppConstants.HelpTextFontSizeMid);
            Save(INPUT_TEXT_FONT_SIZE_KEY, AppConstants.InputTextFontSizeMid);
        }

        private static void SetBigFontSize()
        {
            Save(BUTTON_FONT_SIZE_KEY, AppConstants.ButtonFontSizeBig);
            Save(LABEL_FONT_SIZE_KEY, AppConstants.LabelFontSizeBig);
            Save(HEADER_TITLE_FONT_SIZE_KEY, AppConstants.HeaderTitleFontSizeBig);
            Save(TEXT_FONT_SIZE_KEY, AppConstants.TextFontSizeBig);
            Save(HELP_TEXT_FONT_SIZE_KEY, AppConstants.HelpTextFontSizeBig);
            Save(INPUT_TEXT_FONT_SIZE_KEY, AppConstants.InputTextFontSizeBig);
        }

        private static void Save(string resourceKey, double value)
        {
            Application.Current!.Resources[resourceKey] = value;
        }


        private static Enums.FontSize LoadFontSize()
        {
            var defaultValue = AppConstants.FontSizeDefault;

            var savedValue = Preferences.Get(PREFERENCE_KEY, defaultValue.ToString());
            if (!Enum.TryParse(savedValue, out Enums.FontSize result))
            {
                result = defaultValue;
            }

            return result;
        }
    }
}
