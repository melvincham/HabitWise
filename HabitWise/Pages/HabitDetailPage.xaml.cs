using HabitWise.PageModels;

namespace HabitWise.Pages;

public partial class HabitDetailPage : ContentPage
{
	HabitDetailPageModel _habitDetailPageModel;
	public HabitDetailPage(HabitDetailPageModel habitDetailPageModel)
	{
		InitializeComponent();
		BindingContext = _habitDetailPageModel = habitDetailPageModel;
	}
}