using System.Globalization;
using MealPlannerClient.App.Enums;

namespace MealPlannerClient.App.Converters
{
    public class MeasurementUnitToIncrementConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var productUnitOfMeasurement = (ProductUnitOfMeasurement)value!;
            var result = productUnitOfMeasurement switch
            {
                ProductUnitOfMeasurement.Gram or ProductUnitOfMeasurement.Milliliter => 100,
                ProductUnitOfMeasurement.Unit => 1,
                _ => (double)1,
            };
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
