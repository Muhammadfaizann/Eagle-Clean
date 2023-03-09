using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Messages;
using Microsoft.Extensions.Configuration;

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
                Logger.Log("2","Info","Logged in successful!");
            }
            catch(Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
    }
}
