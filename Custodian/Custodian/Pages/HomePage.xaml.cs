namespace Custodian.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void StartWorkingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DailySchedulePage));
    }

    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }
}