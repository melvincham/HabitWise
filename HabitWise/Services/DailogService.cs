using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Font;

namespace HabitWise.Services
{
    public class DailogService: IDailogService
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        public async Task DisplaySnackbarAsync(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#FF3300"),
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(0),
                Font = Font.SystemFontOfSize(18),
                ActionButtonFont = Font.SystemFontOfSize(14)
            };

            var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }

        public async Task DisplayToastAsync(string message)
        {
            var toast = Toast.Make(message, textSize: 18);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await toast.Show(cts.Token);
        }

        public async Task DisplayAlertAsync(Exception ex)
        {
         await DisplayAlertAsync("Error", ex.Message, "OK");
        }

        public async Task DisplayAlertAsync(string message, string title, string buttonLabel)
        {
            try
            {
                await _semaphore.WaitAsync();
                if (Shell.Current is Shell shell)
                    await shell.DisplayAlert(title, message, buttonLabel);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
