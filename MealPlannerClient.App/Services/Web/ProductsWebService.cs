using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;
using Newtonsoft.Json;

namespace MealPlannerClient.App.Services.Web
{
    public class ProductsWebService : IProductsWebService
    {
        private readonly BackendHttpClient _httpClient;

        public ProductsWebService(BackendHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync("Product/GetAll");
            return JsonConvert.DeserializeObject<List<Product>>(result)!;
        }
    }
}
