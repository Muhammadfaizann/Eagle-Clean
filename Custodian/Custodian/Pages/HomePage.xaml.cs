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
}