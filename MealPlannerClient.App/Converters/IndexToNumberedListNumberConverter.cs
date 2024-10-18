using System.Globalization;

namespace MealPlannerClient.App.Converters
{
    public class IndexToNumberedListNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                // Return a 1-based index as a string (e.g., "1.", "2.")
                return $"{index + 1}.";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
