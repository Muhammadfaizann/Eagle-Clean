using Custodian.Screens;

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
            //MainPage = new SplashScreen();
            //await Task.Delay(11500);
            MainPage = new AppShell();
    }
}
