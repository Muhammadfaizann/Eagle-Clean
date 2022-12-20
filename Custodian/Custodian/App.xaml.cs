using Custodian.Pages;

namespace Custodian;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        
        _ = InitializeApp();
    }
	private async Task InitializeApp()
	{
        try
        {
            MainPage = new SplashScreen();
            await Task.Delay(8000);
            MainPage = new AppShell();
        }
        catch (Exception ex) 
        { 
        }
    }
}
