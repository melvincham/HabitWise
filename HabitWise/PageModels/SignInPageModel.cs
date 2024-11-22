using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Models;
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
        public SignInPageModel(FirebaseAuthService firebaseAuthService) 
        {
            _firebaseAuthService = firebaseAuthService;
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
                    var userCredential = await _firebaseAuthService.SignInAsync(_signInModel.Email, _signInModel.Password);
                    if (!string.IsNullOrWhiteSpace(userCredential?.User?.Info.Email))
                    {
                        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
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
