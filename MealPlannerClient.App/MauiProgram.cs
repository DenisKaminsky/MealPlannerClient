using CommunityToolkit.Maui;
using MealPlannerClient.App.Pages;
using MealPlannerClient.App.ViewModels;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddTransient<SettingsPageViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddTransient<InventoryPageViewModel>();
            builder.Services.AddTransient<InventoryPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
