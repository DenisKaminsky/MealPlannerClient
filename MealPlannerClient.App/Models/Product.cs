using MealPlannerClient.App.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MealPlannerClient.App.Models
{
    public partial class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductUnitOfMeasurement UnitOfMeasurement { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
