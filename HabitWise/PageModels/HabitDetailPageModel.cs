using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitWise.Data;
using HabitWise.Models;
using HabitWise.Pages;
using HabitWise.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;


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
        private ObservableCollection<Tag> allTags;

        [ObservableProperty]
        string pageTitle;

        [ObservableProperty]
        bool isNewHabit;

        

        public async Task InitializeAsync(int? habitId = null)
        {
            if (habitId == null)
            {
                Habit = new Habit();
                if (Application.Current.Resources.TryGetValue("IconAdd", out var iconResource) && iconResource is FontImageSource fontImageSource)
                {
                    Habit.Imoji = fontImageSource;
                }
                IsNewHabit = true;
                PageTitle = "New Habit";
            }
            else
            {
                Habit = await _habitRepository.GetHabitAsync(habitId.Value);
                IsNewHabit = false;
                PageTitle = "Edit Habit";
                
            }
            AllTags = new ObservableCollection<Tag>(await _tagRepository.GetAllTagsAsync());   
        }

        private async Task LoadData(int id)
        {
            try
            {
                await RunWithBusyIndicator(async () =>
                {
                    Habit = await _habitRepository.GetHabitAsync(id);
                    AllTags = new ObservableCollection<Tag>(await _tagRepository.GetAllTagsAsync());

                    foreach (var tag in AllTags)
                    {
                        tag.IsSelected = Habit?.Tags.Any(t => t.Id == tag.Id);
                    }
                });
            }
            catch (Exception ex) 
            {
                _errorHandler.HandleError(ex);
            }
        }

        [RelayCommand]
        private void ToggleTagSelection(Tag tag)
        {
            tag.IsSelected = !tag.IsSelected;
        }

        [RelayCommand]
        private async Task SaveHabit()
        {
            await RunWithBusyIndicator( async () =>
            {

                if (allTags.Any(t => t.IsSelected == true))
                {
                    await _dailogService.DisplayAlertAsync("Error", "Please select at least one tag", "OK");
                    return;
                }
                if (Habit is null)
                {
                    _errorHandler.HandleError(
                        new Exception("Habit is null. Cannot Save."));

                    return;
                }

                try
                {
                    await _habitRepository.RemoveAllTagsFromHabitAsync(Habit.Id);
                    foreach (var tag in AllTags)
                    {
                        await _tagRepository.SaveTagAsync(tag);
                        if (tag.IsSelected) 
                        {
                            await _habitRepository.AddTagToHabitAsync(Habit.Id, tag.Id);
                        } 
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
        private async Task DeleteHabit()
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
        private async Task Cancel()
        {
            await RunWithBusyIndicator(async () =>
            {
                await _navigationService.GoToAsync($"///Dashboard");
            });
        }
    }
}
