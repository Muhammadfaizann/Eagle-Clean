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
        await Task.Delay(112*13);
        MainPage = new AppShell();
    }
}
