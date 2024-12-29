using CommunityToolkit.Mvvm.ComponentModel;
using HabitWise.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Models
{
    public partial class SignInModel: ObservableObject
    {
        [ObservableProperty]
        private ValidatableObject<string> email = new();

        [ObservableProperty]
        private ValidatableObject<string> password = new();

        public bool CanSignIn
        {
            get => Email.IsValid && Password.IsValid;
        }

        public bool ValidateAll()
        {
            return Email.Validate() && Password.Validate();
        }

        public SignInModel()
        {
            AddValidation();

            // Subscribe to property changes for dependent properties.
            Email.PropertyChanged += OnValidatableObjectPropertyChanged;
            Password.PropertyChanged += OnValidatableObjectPropertyChanged;
        }

        private void OnValidatableObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ValidatableObject<string>.IsValid))
            {
                OnPropertyChanged(nameof(CanSignIn));
            }
        }

        private void AddValidation()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An Email is required." });
            Email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email!" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." });
        }
    }
}
