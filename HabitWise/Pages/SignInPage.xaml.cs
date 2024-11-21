using HabitWise.PageModels;

namespace HabitWise.Pages;

public partial class SignInPage : ContentPage
{
	private readonly SignInPageModel _signInPageModel; 
    public SignInPage(SignInPageModel signInPageModel)
	{
		InitializeComponent();
		BindingContext = _signInPageModel = signInPageModel;
	}
}