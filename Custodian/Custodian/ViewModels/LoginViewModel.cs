using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Login(object arg)
        {
            try
            {
                await Task.Delay(2000);
                WeakReferenceMessenger.Default.Send(new LoginMessage(""));
                app_activity_logger.write("Logged in successful!");
            }
            catch(Exception ex)
            {

            }
        }
    }
}
