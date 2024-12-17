using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HabitWise.Validations;

namespace HabitWise.Models
{
    public partial class SignUpModel: ObservableObject
    {
        [ObservableProperty]
        private ValidatableObject<string> email = new ();

        [ObservableProperty]
        private ValidatableObject<string> username = new();

        [ObservableProperty]
        private ValidatableObject<string> password = new();

        public bool CanSignUp
        {
            get => Email.IsValid && Username.IsValid && Password.IsValid;
        }

        public bool ValidateAll()
        {
            return Username.Validate() && Email.Validate() && Password.Validate();
        }

        public SignUpModel()
        {
            AddValidation();

            // Subscribe to property changes for dependent properties.
            Username.PropertyChanged += OnValidatableObjectPropertyChanged;
            Email.PropertyChanged += OnValidatableObjectPropertyChanged;
            Password.PropertyChanged += OnValidatableObjectPropertyChanged;
        }
        private void OnValidatableObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ValidatableObject<string>.IsValid))
            {
                OnPropertyChanged(nameof(CanSignUp)); // Notify that CanSignUp has changed.
            }
        }
        private void AddValidation()
        {
            Username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An Email is required." });
            Email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email!" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." });
            Password.Validations.Add(new PasswordRule<string> { ValidationMessage = "Invalid Password!" });
        }

    }
}
