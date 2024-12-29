using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public interface INavigationService
    {
        public Task GoToAsync(string page, bool animate = true);
    }
}
