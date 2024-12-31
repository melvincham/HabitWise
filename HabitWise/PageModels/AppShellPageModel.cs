using CommunityToolkit.Mvvm.ComponentModel;
using HabitWise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HabitWise.PageModels
{
    public partial class AppShellPageModel: BasePageModel
    {
        [ObservableProperty]
        private bool isDarkMode;

        public ICommand ToggleThemeCommand => new Command<bool>((isToggled) =>
        {
            IsDarkMode = isToggled;
            ThemeHelper.ChangeTheme();
        });

        public AppShellPageModel()
        {
            IsDarkMode = ThemeHelper.IsDarkTheme;
        }
    }
}
