using HabitWise.PageModels;

namespace HabitWise
{
    public partial class AppShell : Shell
    {
        private readonly AppShellPageModel _appShellPageModel;
        public AppShell(AppShellPageModel appShellPageModel)
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.InnerException?.StackTrace);
                throw;
            }
            BindingContext = _appShellPageModel = appShellPageModel;
        }
    }
}
