using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Models;
using HabitWise.Pages;
using HabitWise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.PageModels
{
    public partial class SignInPageModel : BasePageModel
    {
        private FirebaseAuthService _firebaseAuthService;
        private INavigationService _navigationService;
        public SignInPageModel(FirebaseAuthService firebaseAuthService, INavigationService navigationService) 
        {
            _firebaseAuthService = firebaseAuthService;
            _navigationService = navigationService;

        }

        [ObservableProperty]
        SignInModel _signInModel = new();

        [ObservableProperty]
        private string _errorMessage;

        [RelayCommand(CanExecute = nameof(CanSignIn))]
        private async Task SignIn()
        {
            await RunWithBusyIndicator(async () =>
            {
                try
                {
                    var isSignedIn = await _firebaseAuthService.SignInAsync(_signInModel.Email, _signInModel.Password);
                    if (isSignedIn)
                    {
                        ErrorMessage = "Login successful!";
                        _navigationService.GoToAsync($"//{nameof(MainPage)}"); ;
                    }
                    else {
                        ErrorMessage = "Sign-in failed. UserCredential returned null.";
                    }
                }
                catch (Exception ex) 
                {
                    ErrorMessage = ex.Message;
                }
            });
        }

        private bool CanSignIn() 
        {
            return !string.IsNullOrWhiteSpace(_signInModel?.Email) && !string.IsNullOrWhiteSpace(_signInModel?.Password);
        }
    }
}
