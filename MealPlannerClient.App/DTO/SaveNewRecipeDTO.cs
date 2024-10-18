namespace MealPlannerClient.App.DTO
{
    public class SaveNewRecipeDTO
    {
        public string Name { get; set; }
        public IEnumerable<string> PreparationSteps { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        public int CookTimeInMinutes { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
