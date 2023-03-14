namespace Custodian.Pages;

public partial class TravelPage : ContentPage
{
	public TravelPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new MapNavigationPage());
    }

}