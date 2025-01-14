using HabitWise.PageModels;

namespace HabitWise.Pages
{
    public partial class MainPage : ContentPage
    {
        MainPageModel _mainPageModel;
        public MainPage(MainPageModel mainPageModel)
        {
            InitializeComponent();
            BindingContext = _mainPageModel = mainPageModel;
        }
    }
}
