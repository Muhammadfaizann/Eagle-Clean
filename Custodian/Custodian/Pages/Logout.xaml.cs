using Android.Service.Voice;

namespace Custodian.Pages;

public partial class Logout : ContentPage
{
	public Logout()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        await Shell.Current.GoToAsync(nameof(Login));
    }
}