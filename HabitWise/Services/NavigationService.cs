using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    internal class NavigationService : INavigationService
    {
        public async void GoToAsync(string page, bool animate = true) 
        {
            await Shell.Current.GoToAsync(page, animate);
        }
    }
}
