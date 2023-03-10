using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.Pages;
using Custodian.Screens;

namespace Custodian;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();
        MainPage = new AppShell();
        _ = InitializeApp(); 
        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (sender, args) => 
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new AppShell();
                });
            }
            catch(Exception ex) 
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        });


        

    }
    protected override void OnStart()
    {
       Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTExNjMwM0AzMjMwMmUzNDJlMzBVMkJaZDBDdHdVYnFBOFcrOFlrYStVVFRUZzByNjNqWXZjci9iaUNGVkZVPQ==");
       Utils.createConfigFile();

        Logger.Log("2", "Info", "App launched.");
    }
   
    private async Task InitializeApp()
	{
        MainPage = new SplashScreen();
            await Task.Delay(9000);
        MainPage = new NavigationPage(new UserAgreement());
    }

   
    
}
