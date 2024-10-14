using System.Globalization;
using MealPlannerClient.App.Enums;

namespace MealPlannerClient.App.Converters
{
    public class MeasurementUnitToMaxValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var productUnitOfMeasurement = (ProductUnitOfMeasurement)value!;
            double result = productUnitOfMeasurement switch
            {
                ProductUnitOfMeasurement.Gram or ProductUnitOfMeasurement.Milliliter => 1000 * 1000,
                ProductUnitOfMeasurement.Unit => 1000,
                _ => 1000,
            };

            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
