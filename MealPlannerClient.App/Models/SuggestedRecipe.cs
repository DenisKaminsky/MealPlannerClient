using CommunityToolkit.Mvvm.ComponentModel;

namespace MealPlannerClient.App.Models
{
    public partial class SuggestedRecipe : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public string Name { get; set; }

        public IEnumerable<string> PreparationSteps { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public IEnumerable<string> Instructions { get; set; }

        public int CookTimeInMinutes { get; set; }

        [ObservableProperty]
        private bool isSaved = false;
    }
}
