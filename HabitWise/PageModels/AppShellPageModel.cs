using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Helpers;
using HabitWise.Services;
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
        private INavigationService _navigationService;

        [ObservableProperty]
        private bool isDarkMode;

        [ObservableProperty]
        private string pageTitle;

        public ICommand ToggleThemeCommand => new Command<bool>((isToggled) =>
        {
            IsDarkMode = isToggled;
            ThemeHelper.ChangeTheme();
        });

        public ICommand FlyoutCommand => new Command(() => Shell.Current.FlyoutIsPresented = true);

        [RelayCommand]
        private async Task Profile()
        {
            await _navigationService.GoToAsync("///MainPage");
        }

        public AppShellPageModel(INavigationService navigationService)
        {
            PageTitle = "HabitWise";    
            _navigationService = navigationService;
            IsDarkMode = ThemeHelper.IsDarkTheme;
        }
    }
}
