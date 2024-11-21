using HabitWise.PageModels;

namespace HabitWise.Pages;

public partial class SignUpPage : ContentPage
{
	private readonly SignUpPageModel _signUpPageModel;
	public SignUpPage(SignUpPageModel signUpPageModel)
	{
		InitializeComponent();
		BindingContext = _signUpPageModel = signUpPageModel;
	}
}