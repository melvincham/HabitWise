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

namespace HabitWise.PageModels
{
    public partial class SignUpPageModel : BasePageModel
    {
        private FirebaseAuthService _firebaseAuthService;
        private INavigationService _navigationService;
        [ObservableProperty]
        private SignUpModel signUpModel;

        public SignUpPageModel(FirebaseAuthService firebaseAuthService, INavigationService navigationService) 
        {
            _firebaseAuthService = firebaseAuthService;
            _navigationService = navigationService; 
            SignUpModel = new();
        }

        [ObservableProperty]
        private string? _errorMessage;

        [RelayCommand(CanExecute = nameof(CanSignUp))]
        private async Task SignUp()
        {
            await RunWithBusyIndicator(async () =>
            {
                try
                {
                    var isSignedIn = await _firebaseAuthService.SignUpAsync(SignUpModel.Email, SignUpModel.Password, SignUpModel.Username);
                   
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

        private bool CanSignUp()
        {
            return  !string.IsNullOrWhiteSpace(SignUpModel.Email) && 
                    !string.IsNullOrWhiteSpace(SignUpModel.Password) && 
                    !string.IsNullOrWhiteSpace(SignUpModel.Username);
        }

        [RelayCommand]
        private async Task NavigateSignIn()
        {
            await Shell.Current.GoToAsync($"///{nameof(SignInPage)}");
        }
    }
}
