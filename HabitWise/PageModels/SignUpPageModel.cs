using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitWise.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using HabitWise.Models;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Pages;
using System.ComponentModel;
using HabitWise.Validations;
using HabitWise.Helpers;
using System.Windows.Input;

namespace HabitWise.PageModels
{
    public partial class SignUpPageModel : BasePageModel
    {
        private FirebaseAuthService _firebaseAuthService;
        private INavigationService _navigationService;
        private IErrorHandler _errorHandler;
        private IDailogService _dailogService;

        public SignUpPageModel(
            FirebaseAuthService firebaseAuthService, 
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
        private SignUpModel signUpModel = new();

        [ObservableProperty]
        private bool isDarkMode;

        public ICommand ToggleThemeCommand => new Command<bool>((isToggled) =>
        {
            IsDarkMode = isToggled;
            ThemeHelper.ChangeTheme();
        });

        [RelayCommand]
        private async Task SignUp()
        {
            if (SignUpModel.ValidateAll())
            {
                await RunWithBusyIndicator(async () =>
                {
                    try
                    {
                        var isSignedIn = await _firebaseAuthService.SignUpAsync(SignUpModel.Email.Value, SignUpModel.Password.Value, SignUpModel.Username.Value);

                        if (isSignedIn)
                        {
                            ErrorMessage = "Sign-up successful!";
                            _navigationService.GoToAsync($"//{nameof(MainPage)}");
                            await _dailogService.DisplayToastAsync(ErrorMessage);
                        }
                        else
                        {
                            ErrorMessage = "Sign-up failed. UserCredential returned null.";
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
        private async Task NavigateSignIn()
        {
            await Shell.Current.GoToAsync($"///{nameof(SignInPage)}");
        }
    }
}
