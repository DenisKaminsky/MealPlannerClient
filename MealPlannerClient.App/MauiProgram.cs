using CommunityToolkit.Maui;
using MealPlannerClient.App.Interfaces.Services;
using MealPlannerClient.App.Pages;
using MealPlannerClient.App.Services;
using MealPlannerClient.App.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MealPlannerClient.App.Interfaces.Web;
using MealPlannerClient.App.Services.Web;

namespace MealPlannerClient.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            using var configStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("MealPlannerClient.App.appsettings.json");
            var config = new ConfigurationBuilder()
                .AddJsonStream(configStream!)
                .Build();
            builder.Configuration.AddConfiguration(config);

            builder.Services.AddTransient<IMyProductsService, MyProductsService>();
            builder.Services.AddTransient<IProductsService, ProductsService>();

            builder.Services.AddTransient<SettingsPageViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddTransient<InventoryPageViewModel>();
            builder.Services.AddTransient<InventoryPage>();
            
            builder.Services.AddSingleton(_ => new BackendHttpClient
            {
                BaseAddress = new Uri(config["Backend:BaseURI"]!),
                DefaultRequestHeaders = { { "Accept", "application/json" } }
            });

            builder.Services.AddSingleton<IProductsWebService, ProductsWebService>();
            builder.Services.AddSingleton<IMyProductsWebService, MyProductsWebService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
