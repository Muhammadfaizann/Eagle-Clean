using Custodian.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Custodian.ViewModels
{
    public class DailyScheduleViewModel : BaseViewModel
    {
        #region Commands  
        public ICommand StartCommand { get; private set; }
        #endregion

        public DailyScheduleViewModel()
        {
            StartCommand = new Command<object>((object arg) =>
            {
                try
                {
                    Shell.Current.Navigation.PushAsync(new ActiveCleaningRoutePage(arg as string));
                }
                catch (Exception ex)
                {
                   
                }
            });
        }
    }
}
