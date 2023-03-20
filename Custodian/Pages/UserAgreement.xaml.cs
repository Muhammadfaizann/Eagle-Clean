

using Custodian.ActivityLog;
using Custodian.Helpers;

namespace Custodian.Pages;

public partial class UserAgreement : ContentPage
{
   

    public UserAgreement()
	{

		InitializeComponent();
		lbl1.Text = "This Mutual Non-Disclosure Agreement (\"Agreement\"), dated as of ________ (\"Effective Date\"), is agreed by and between [COMPANY 1], with primary offices located at [STREET ADDRESS] (“Company 1”), and [COMPANY 2], with primary offices located at [STREET ADDRESS] (\"Company 2\").";
		lbl2.Text = "Company 1 and Company 2 are individually referred to herein as, the “Party”, and collectively as, the “Parties”.";
		lbl3.Text = "RECITALS";
        lbl4.Text = "WHEREAS, the Parties desire to explore and engage in discussions regarding a potential business opportunity of mutual interest (“Business Discussion”);";

        PrepareLocalEnvironment();
       
    }

    

    private async void btnIAgree_Clicked(object sender, EventArgs e)
    {
        try 
        {
            Navigation.PushAsync(new SignaturePad());
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