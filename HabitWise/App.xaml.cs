using HabitWise.Resources.Themes;

namespace HabitWise
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ApplyTheme(AppInfo.RequestedTheme == AppTheme.Dark ? "DarkTheme" : "LightTheme");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        public void ApplyTheme(string themeKey)
        {
            Resources.MergedDictionaries.Clear();

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Colors.xaml") });
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Styles.xaml") });

            if (themeKey == "DarkTheme")
                Resources.MergedDictionaries.Add(new DarkTheme());
            else
                Resources.MergedDictionaries.Add(new LightTheme());
        }
    }
}
