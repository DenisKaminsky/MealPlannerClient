using System.Globalization;
using MealPlannerClient.App.Enums;
using MealPlannerClient.App.Resources;

namespace MealPlannerClient.App.Converters
{
    public class MeasurementUnitToTranslatedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var productUnitOfMeasurement = (ProductUnitOfMeasurement)value!;
            var translatedString = (string)LocalizationResourceManager.Instance[productUnitOfMeasurement.ToString()];

            return translatedString;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
