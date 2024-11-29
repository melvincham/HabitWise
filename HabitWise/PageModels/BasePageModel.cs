using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HabitWise.PageModels
{
    public partial class BasePageModel: ObservableObject
    {
        // Property to manage loading state
        [ObservableProperty]
        private bool isBusy;

        // Property for error messages (if needed for error handling)
        [ObservableProperty]
        private string? errorMessage;
       
        // Command to handle common functionality, such as refreshing the page
        public virtual Task LoadDataAsync()
        {
            // Override this method in derived ViewModels to load specific data for each page
            return Task.CompletedTask;
        }

        // A simple method to handle the busy state and simulate an async task
        protected async Task RunWithBusyIndicator(Func<Task> action)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    await action();
                }
                }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
