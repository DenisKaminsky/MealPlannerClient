using System.Globalization;
using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.Converters
{
    public class DateTimeToLocalDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                var localDateTime = dateTime.ToLocalTime();
                
                return localDateTime.ToString(AppConstants.DateFormat, LocalizationResourceManager.Instance.GetCulture());
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
