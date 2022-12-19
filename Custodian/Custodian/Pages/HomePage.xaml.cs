namespace Custodian.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void StartWorkingClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new DailySchedulePage());
    }
}