using MealPlannerClient.App.Pages;

namespace MealPlannerClient.App.Converters
{
    public class PageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Depending on the CurrentPage, return the appropriate view
            switch (value as string)
            {
                case "Page1":
                    return new InventoryPage();
                case "Page2":
                    return new Page2();
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
