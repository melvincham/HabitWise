using CommunityToolkit.Maui;
using HabitWise.PageModels;
using HabitWise.Pages;
using HabitWise.Resources.Fonts;
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register services
            builder.Services.AddSingleton<FirebaseAuthService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDailogService, DailogService>();
            builder.Services.AddSingleton<IErrorHandler, ModalErrorHandler>();

            // Register PageModels
            builder.Services.AddTransient<SignInPageModel>();
            builder.Services.AddTransient<SignUpPageModel>();
            builder.Services.AddTransient<AppShellPageModel>();

            //Register Pages
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<AppShell>();


            return builder.Build();
        }
    }
}
