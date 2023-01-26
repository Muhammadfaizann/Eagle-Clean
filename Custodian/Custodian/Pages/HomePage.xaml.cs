namespace Custodian.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void StartWorkingClicked(object sender, EventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(DailySchedulePage));
        loader.IsRunning = loader.IsVisible = false;
    }

    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }
}