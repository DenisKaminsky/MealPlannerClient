namespace MealPlannerClient.App.Models
{
    public class ProductGroup : List<Product>
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public ProductGroup(string id, string name, List<Product> products) : base(products)
        {
            Id = id;
            Name = name;
        }
    }
}
