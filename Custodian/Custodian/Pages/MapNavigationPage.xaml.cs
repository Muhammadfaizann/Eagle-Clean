namespace Custodian.Pages;

public partial class MapNavigationPage : ContentPage
{
	public MapNavigationPage()
	{
		InitializeComponent();
	}
    private async void NavigateBack(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}