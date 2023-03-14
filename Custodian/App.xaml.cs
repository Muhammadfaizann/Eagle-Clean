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
        
        //AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnHandledException;
        InitializeComponent();
        //MainPage = new AppShell();
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

    private void CurrentDomain_UnHandledException(object sender, UnhandledExceptionEventArgs e)
    {
         Logger.Log("1", "CRASH", "Application Crashing");
         Logger.Log("1", "Exception", e.ExceptionObject.ToString());
    }
    /*
    private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        Logger.Log("1", "CRASH", "====================Application Crashing===================");
        Logger.Log("1", "Exception", e.Exception.StackTrace);

    }*/
    protected override void OnStart()
    {
       Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTExNjMwM0AzMjMwMmUzNDJlMzBVMkJaZDBDdHdVYnFBOFcrOFlrYStVVFRUZzByNjNqWXZjci9iaUNGVkZVPQ==");
     
    }
    
    private async Task InitializeApp()
	{
        MainPage = new SplashScreen();
        await Task.Delay(10000);
        MainPage = new NavigationPage(new UserAgreement());
       
    }

}
