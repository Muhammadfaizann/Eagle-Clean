

using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class UserAgreement : ContentPage
{

    public UserAgreement()
	{

		InitializeComponent();
		lbl1.Text = "This is a US Government computer system and is intended for official and other authorized use only. Unauthorized access or use of the system may subject violators to administrative action, civil, and/or criminal prosecution under  the US Criminal Code (Title 18 USC 1030). All information on this computer system may be monitored, intercepted, recorded, read, copied, or captured and disclosed by and to authorized personnel for official purposes, including criminal prosecution.  You have no expectations of privacy regarding monitoring of this system. Any authorized or unauthorized use of this computer system signifies consent to and compliance with Postal Service policies and their terms.";
		
        PrepareLocalEnvironment();
       
    }

    private void btnIAgree_Clicked(object sender, EventArgs e)
    {
        try 
        {
            Navigation.PushAsync(new Login(new LoginViewModel()));

        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
            throw ;
        }
    }

    private async void PrepareLocalEnvironment()
    {
        await CheckAndAskPermissionsAsync();
        Logger.init();
        RecordLogger.init();
        Utils.createConfigFile();

        Logger.Log("2", "Info", "App launched.");
    }

    private async Task CheckAndAskPermissionsAsync()
    {
        try
        {
            var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (statusRead != PermissionStatus.Granted)
            {
                if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                {
                    // Prompt the user with additional information as to why the permission is needed
                }

                statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (statusRead == PermissionStatus.Granted)
            {
                Logger.Log("2", "Info", "Storage Read Permission granted");
            }

            var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (statusWrite != PermissionStatus.Granted)
            {
                if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
                {
                    // Prompt the user with additional information as to why the permission is needed
                }

                statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (statusWrite == PermissionStatus.Granted)
            {
                Logger.Log("2", "Info", "Storage Write Permission granted");
            }
            var statusLocation = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (statusLocation != PermissionStatus.Granted)
            {
                if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
                {
                    // Prompt the user with additional information as to why the permission is needed
                }

                statusLocation = await Permissions.RequestAsync<Permissions.LocationAlways>();
            }

            if (statusLocation == PermissionStatus.Granted)
            {
                Logger.Log("2", "Info", "Location Permission granted");
            }
        }
        catch (Exception e)
        {

        }
    }

}