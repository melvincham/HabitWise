using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HabitWise.Models
{
    public partial class SignUpModel:ObservableObject
    {
        [ObservableProperty]
        public string? email;
        [ObservableProperty]
        public string? username;
        [ObservableProperty]
        public string? password; 
    }
}
