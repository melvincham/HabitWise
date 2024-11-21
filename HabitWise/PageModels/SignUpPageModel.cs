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

        public SignUpPageModel(FirebaseAuthService firebaseAuthService) 
        {
            _firebaseAuthService = firebaseAuthService;
        }

        [ObservableProperty]
        private SignUpModel _signUpModel;

        [RelayCommand]
        private async Task SignUp()
        { 
            var  result = await _firebaseAuthService.SignUpAsync(_signUpModel.Email, _signUpModel.Password, _signUpModel.Username);
            if (!string.IsNullOrWhiteSpace(result?.User?.Info.Email)) 
            {
                await Shell.Current.GoToAsync($"{nameof(SignInPage)}", true);
            } 
        }

        [RelayCommand]
        private async Task NavigateSignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(SignInPage)}");
        }
    }
}
