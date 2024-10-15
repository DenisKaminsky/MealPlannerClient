using MealPlannerClient.App.Enums;

namespace MealPlannerClient.App.Models
{
    public partial class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ProductUnitOfMeasurement UnitOfMeasurement { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
