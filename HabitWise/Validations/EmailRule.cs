using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HabitWise.Validations
{
    public class EmailRule<T>: IValidationRule<T>
    {
        private readonly Regex _regex = new(@"^([\w.-]+)@([\w-]+)(\.[a-zA-Z]{2,})$");

        public string ValidationMessage { get; set; }

        public bool Check(T value) =>
            value is string str && _regex.IsMatch(str);
    }
}
