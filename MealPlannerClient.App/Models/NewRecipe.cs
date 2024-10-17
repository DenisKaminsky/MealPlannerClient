namespace MealPlannerClient.App.Models
{
    public class NewRecipe
    {
        public string Name { get; set; } = string.Empty;
        public string PreparationSteps { get; set; }
        public string Ingredients { get; set; } 
        public string Instructions { get; set; }
        public int CookTimeInMinutes { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
