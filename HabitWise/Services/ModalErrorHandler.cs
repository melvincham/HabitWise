using HabitWise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Services
{
    public class ModalErrorHandler: IErrorHandler
    {
        private readonly IDailogService _dailogService;

        public ModalErrorHandler(IDailogService dailogService)
        {
            _dailogService = dailogService;
        }
        public void HandleError(Exception ex)
        {
            _dailogService.DisplayAlertAsync(ex).FireAndForgetSafeAsync();
        }
    }
}
