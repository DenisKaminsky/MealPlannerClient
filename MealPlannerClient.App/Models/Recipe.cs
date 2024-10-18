namespace MealPlannerClient.App.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsFavorite { get; set; }
        public IEnumerable<string> PreparationSteps { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        public int CookTimeInMinutes { get; set; }
        public DateTime DateCreatedUtc { get; set; }
    }
}
