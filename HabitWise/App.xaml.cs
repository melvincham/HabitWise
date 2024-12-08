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
            }
            ApplyTheme(AppInfo.RequestedTheme == AppTheme.Dark ? "DarkTheme" : "LightTheme");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        public void ApplyTheme(string themeKey)
        {
            if (themeKey == "DarkTheme")
                Resources.MergedDictionaries.Add(new DarkTheme());
            else
                Resources.MergedDictionaries.Add(new LightTheme());
        }
    }
}
