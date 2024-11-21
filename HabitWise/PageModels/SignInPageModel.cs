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

        [RelayCommand]
        private async Task SignIn()
        {
            var result = await _firebaseAuthService.SignInAsync(_signInModel.Email, _signInModel.Password);
            if (!string.IsNullOrWhiteSpace(result?.User?.Info.Email)) 
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
        }
    }
}
