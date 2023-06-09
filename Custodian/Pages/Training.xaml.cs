namespace Custodian.Pages;

public partial class Training : ContentPage
{
	public Training()
	{
		InitializeComponent();
	}

    private async void btnCTCVideoClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(CTCTrainingVideo));
        loader.IsRunning = loader.IsVisible = false;
    }
    private async void btnJobAidClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(CTCJobAidsPage));
        loader.IsRunning = loader.IsVisible = false;
    }
    private async void btnMonthlyCTCClicked(object sender, TappedEventArgs e)
    {
        loader.IsRunning = loader.IsVisible = true;
        await Shell.Current.GoToAsync(nameof(CTCMonthlyTraining));
        loader.IsRunning = loader.IsVisible = false;
    }
}