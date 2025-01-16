using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Data;
using HabitWise.Models;
using HabitWise.Pages;
using HabitWise.Services;
using System.Collections.ObjectModel;


namespace HabitWise.PageModels
{
    public partial class HabitDetailPageModel : BasePageModel
    {
        private HabitRepository _habitRepository;
        private TagRepository _tagRepository;
        private INavigationService _navigationService;
        private IErrorHandler _errorHandler;
        private IDailogService _dailogService;

        public HabitDetailPageModel(HabitRepository habitRepository,
            TagRepository tagRepository,
            INavigationService navigationService,
            IErrorHandler errorHandler, 
            IDailogService dailogService) 
        {
            _habitRepository = habitRepository;
            _tagRepository = tagRepository;
            _navigationService = navigationService;
            _errorHandler = errorHandler;
            _dailogService = dailogService;
        }

        [ObservableProperty]
        private Habit habit;

        [ObservableProperty]
        private ObservableCollection<Tag> availableTags;

        [ObservableProperty]
        private ObservableCollection<Tag> selectedTags;

        [ObservableProperty]
        string pageTitle;

        [ObservableProperty]
        bool isNewHabit;

        public async Task InitializeAsync(int? habitId = null)
        {
            if (habitId == null)
            {
                Habit = new Habit();
                IsNewHabit = true;
                PageTitle = "New Habit";
                SelectedTags = new ObservableCollection<Tag>();
            }
            else
            {
                Habit = await _habitRepository.GetHabitAsync(habitId.Value);
                IsNewHabit = false;
                PageTitle = "Edit Habit";
                SelectedTags = new ObservableCollection<Tag>(await _habitRepository.GetTagsForHabitAsync(habitId.Value));
            }
            AvailableTags = new ObservableCollection<Tag>(await _tagRepository.GetAllTagsAsync());   
        }

        [RelayCommand]
        private void ToggleTagSelection(Tag tag)
        {
            if (SelectedTags.Contains(tag))
            {
                SelectedTags.Remove(tag);
                AvailableTags.Add(tag);
            }
            else
            {
                SelectedTags.Add(tag);
                AvailableTags.Remove(tag);
            }
        }

        [RelayCommand]
        private async Task SaveHabitAsync()
        {
            await RunWithBusyIndicator( async () =>
            {

                if (SelectedTags.Count == 0)
                {
                    await _dailogService.DisplayAlertAsync("Error", "Please select at least one tag", "OK");
                    return;
                }

                try
                {
                    await _habitRepository.RemoveAllTagsFromHabitAsync(Habit.Id);
                    foreach (var tag in SelectedTags)
                    {
                        await _tagRepository.SaveTagAsync(tag);
                        await _habitRepository.AddTagToHabitAsync(Habit.Id, tag.Id);
                    }

                    await _habitRepository.SaveHabitAsync(Habit);

                    await _navigationService.GoBackAsync();
                }
                catch (Exception ex)
                {
                    _errorHandler.HandleError(ex);
                }

            });
        }

        [RelayCommand]
        private async Task DeleteHabitAsync()
        {
            await RunWithBusyIndicator(async () =>
            {
                try
                {
                    await _habitRepository.RemoveAllTagsFromHabitAsync(Habit.Id);
                    await _habitRepository.DeleteHabitAsync(Habit);
                    await _navigationService.GoBackAsync();
                }
                catch (Exception ex)
                {
                    _errorHandler.HandleError(ex);
                }
            });
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
