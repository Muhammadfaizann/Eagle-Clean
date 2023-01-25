namespace Custodian.Pages;

public partial class Training : ContentPage
{
	public Training()
	{
		InitializeComponent();
	}
    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void btnCTCVideoClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CTCTrainingVideo));
    }
    private async void btnJobAidClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CTCJobAidsPage));
    }
    private async void btnMonthlyCTCClicked(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CTCMonthlyTraining));
    }
}