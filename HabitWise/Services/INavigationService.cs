using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();

        public Task GoToAsync(string page, bool animate = true, IDictionary<string, object> routeParameters = null);

        Task PopAsync();
    }
}
