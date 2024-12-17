using HabitWise.Resources.Themes;
using System.Reflection;

namespace HabitWise
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.InnerException?.StackTrace);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.InnerException?.StackTrace);
                throw;
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            ApplyTheme(AppInfo.RequestedTheme == AppTheme.Dark ? "DarkTheme" : "LightTheme");
            return new Window(new AppShell());
        }

        public void ApplyTheme(string themeKey)
        {
            if (themeKey == "DarkTheme")
                Application.Current?.Resources.MergedDictionaries.Add(new DarkTheme());
            else
                Application.Current?.Resources.MergedDictionaries.Add(new LightTheme());
        }
    }
}
