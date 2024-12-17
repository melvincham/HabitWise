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

namespace HabitWise.PageModels
{
    public partial class SignUpPageModel : BasePageModel
    {
        private FirebaseAuthService _firebaseAuthService;
        private INavigationService _navigationService;

        public SignUpPageModel(FirebaseAuthService firebaseAuthService, INavigationService navigationService) 
        {
            _firebaseAuthService = firebaseAuthService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private SignUpModel signUpModel = new();

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
                        }
                        else
                        {
                            ErrorMessage = "Sign-up failed. UserCredential returned null.";
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
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
