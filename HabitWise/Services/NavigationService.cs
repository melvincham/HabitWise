using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public class NavigationService : INavigationService
    {
        public async Task InitializeAsync()
        {

        }

        public Task GoToAsync(string page, bool animate = true, IDictionary<string, object> routeParameters = null) 
        {
            var shellNavigation = new ShellNavigationState(page);

            return routeParameters != null
                ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
