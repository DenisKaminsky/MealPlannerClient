using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.MarkupExtensions
{
    public class GlobalFontSizeExtension : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return 15;
        }
    }
}
