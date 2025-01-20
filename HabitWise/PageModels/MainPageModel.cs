
using CommunityToolkit.Mvvm.Input;
using HabitWise.Pages;
using HabitWise.Services;

namespace HabitWise.PageModels
{
    public partial class MainPageModel : BasePageModel
    {
        INavigationService _navigationService;
        public MainPageModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task AddButton()
        {
            await RunWithBusyIndicator(async () =>
            {
                await _navigationService.GoToAsync($"///{nameof(HabitDetailPage)}");
            });
        }

    }
}
