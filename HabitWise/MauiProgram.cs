using CommunityToolkit.Maui;
using Fonts;
using HabitWise.PageModels;
using HabitWise.Pages;
using HabitWise.Services;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace HabitWise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register services
            builder.Services.AddSingleton<FirebaseAuthService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            // Register PageModels
            builder.Services.AddTransient<SignInPageModel>();
            builder.Services.AddTransient<SignUpPageModel>();

            //Register Pages
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<MainPage>();


            return builder.Build();
        }
    }
}
