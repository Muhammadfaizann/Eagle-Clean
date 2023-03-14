using Android.Service.Voice;
using Custodian.ActivityLog;
using Custodian.ViewModels;

namespace Custodian.Pages;

public partial class Logout : ContentPage
{
	public Logout()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        Logger.Log("2", "INFO", "Logged out!!");
        await Navigation.PushAsync(new Login(new LoginViewModel()));
        //await Shell.Current.GoToAsync(nameof(Login));
    }
}