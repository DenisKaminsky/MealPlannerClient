using CommunityToolkit.Mvvm.ComponentModel;
using MealPlannerClient.App.Enums;

namespace MealPlannerClient.App.Models
{
    public partial class MyProduct: CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public ProductUnitOfMeasurement ProductUnitOfMeasurement { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }
        
        [ObservableProperty] 
        private double quantity;
    }
}
