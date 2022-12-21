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
            MainPage = new SplashScreen();
            await Task.Delay(12000);
            MainPage = new AppShell();
    }
}
