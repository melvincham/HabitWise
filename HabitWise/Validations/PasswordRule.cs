using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HabitWise.Validations
{
    public class PasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value) {
            
            if (value is string password) {
                if (!password.Any(char.IsUpper))
                {
                    ValidationMessage = "Password must contain at least one uppercase letter.";
                    return false;
                }

                if (!password.Any(char.IsLower))
                {
                    ValidationMessage = "Password must contain at least one lowercase letter.";
                    return false;
                }

                if (!password.Any(char.IsDigit))
                {
                    ValidationMessage = "Password must contain at least one numeric character.";
                    return false;
                }

                if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    ValidationMessage = "Password must contain at least one special character.";
                    return false;
                }

                ValidationMessage = null;
                return true;
            }
            return false;
        }
            
    }
}
