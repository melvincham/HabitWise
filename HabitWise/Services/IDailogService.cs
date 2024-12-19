using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public interface IDailogService
    {
        Task DisplaySnackbarAsync(string message);
        Task DisplayToastAsync(string message);
        Task DisplayAlertAsync(Exception ex);
        Task DisplayAlertAsync(string message, string title, string buttonLabel);
    }
}
