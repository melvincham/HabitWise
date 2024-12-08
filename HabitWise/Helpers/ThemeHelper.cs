using HabitWise.Resources.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Helpers
{
    public static class ThemeHelper
    {
        public static void ChangeTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;

                var darkThemeInstance = mergedDictionaries.FirstOrDefault(d => d is DarkTheme);
                mergedDictionaries.Remove(darkThemeInstance);
                mergedDictionaries.Add(new LightTheme());
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;

                var lightThemeInstance = mergedDictionaries.FirstOrDefault(d => d is LightTheme);
                mergedDictionaries.Remove(lightThemeInstance);
                mergedDictionaries.Add(new DarkTheme());
            }
        }
    }
}
