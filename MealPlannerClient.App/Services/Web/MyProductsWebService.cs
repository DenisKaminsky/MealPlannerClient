using MealPlannerClient.App.DTO;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Models;
using Newtonsoft.Json;

namespace MealPlannerClient.App.Services.Web
{

    public class MyProductsWebService: IMyProductsWebService
    {
        private readonly BackendHttpClient _httpClient;

        public MyProductsWebService(BackendHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MyProduct>> GetAllAsync()
        {
            var result = await _httpClient.GetStringAsync("MyProduct/GetAll");
            return JsonConvert.DeserializeObject<List<MyProduct>>(result)!;
        }

        public async Task SaveAsync(List<SaveMyProductDTO> myProducts)
        {
            using var jsonContent = new StringContent(
                JsonConvert.SerializeObject(myProducts),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("MyProduct/Save", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
