using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Helpers;
using HabitWise.Models;
using HabitWise.Pages;
using HabitWise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HabitWise.PageModels
{
    public partial class SignInPageModel : BasePageModel
    {
        private FirebaseAuthService _firebaseAuthService;
        private INavigationService _navigationService;
        private IErrorHandler _errorHandler;
        private IDailogService _dailogService;
        public SignInPageModel(FirebaseAuthService firebaseAuthService,
            INavigationService navigationService,
            IErrorHandler errorHandler,
            IDailogService dailogService)
        {
            _firebaseAuthService = firebaseAuthService;
            _navigationService = navigationService;
            _errorHandler = errorHandler;
            _dailogService = dailogService;
            isDarkMode = ThemeHelper.IsDarkTheme;
        }

        [ObservableProperty]
        SignInModel _signInModel = new();

        [ObservableProperty]
        private bool isDarkMode;

        public ICommand ToggleThemeCommand => new Command<bool>((isToggled) =>
        {
            IsDarkMode = isToggled;
            ThemeHelper.ChangeTheme();
        });

        [RelayCommand]
        private async Task SignIn()
        {
            if (SignInModel.ValidateAll())
            {
                await RunWithBusyIndicator(async () =>
                {
                    try
                    {
                        var isSignedIn = await _firebaseAuthService.SignInAsync(SignInModel.Email.Value, SignInModel.Password.Value);
                        if (isSignedIn)
                        {
                            ErrorMessage = "Login successful!";
                            _navigationService.GoToAsync($"//{nameof(MainPage)}");
                            await _dailogService.DisplayToastAsync(ErrorMessage);
                        }
                        else
                        {
                            ErrorMessage = "Sign-in failed. UserCredential returned null.";
                            _errorHandler.HandleError(new Exception(ErrorMessage));
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        _errorHandler.HandleError(ex);
                    }
                });
            }
        }

        [RelayCommand]
        private  async Task NavigateSignUp()
        {
            await RunWithBusyIndicator(async () =>
            {
                await _navigationService.GoToAsync($"///{nameof(SignUpPage)}");
            });
        }

    }
}
