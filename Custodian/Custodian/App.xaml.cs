using CommunityToolkit.Mvvm.Messaging;
using Custodian.Pages;
using Custodian.Screens;

namespace Custodian;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
        //_ = InitializeApp();
        WeakReferenceMessenger.Default.Register<string>(this, (sender, arg) => {
            MainPage = new AppShell();
        });
    }
	private async Task InitializeApp()
	{
            MainPage = new SplashScreen();
            await Task.Delay(13000);
            MainPage = new NavigationPage(new UserAgreement());
    }
}
