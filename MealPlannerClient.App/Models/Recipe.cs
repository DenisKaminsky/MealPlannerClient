using CommunityToolkit.Mvvm.ComponentModel;

namespace MealPlannerClient.App.Models
{
    public partial class Recipe: CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [ObservableProperty] 
        private bool isFavorite;

        public IEnumerable<string> PreparationSteps { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public IEnumerable<string> Instructions { get; set; }

        public int CookTimeInMinutes { get; set; }

        public DateTime CreatedDateUtc { get; set; }
    }
}
