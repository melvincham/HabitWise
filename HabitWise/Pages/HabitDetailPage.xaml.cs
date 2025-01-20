using HabitWise.PageModels;

namespace HabitWise.Pages;

public partial class HabitDetailPage : ContentPage
{
	HabitDetailPageModel _habitDetailPageModel;
	public HabitDetailPage( HabitDetailPageModel habitDetailPageModel)
	{
		InitializeComponent();
		habitDetailPageModel.InitializeAsync();
		BindingContext = _habitDetailPageModel = habitDetailPageModel;
	}
}