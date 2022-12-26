using CommunityToolkit.Maui.Views;

namespace Custodian.Popups;

public partial class EndRoutePopup : Popup
{
	public EndRoutePopup()
	{
		InitializeComponent();
	}

    private void cancel_Clicked(object sender, TappedEventArgs e)
    {
		Close();
    }
    private void partial_Clicked(object sender, TappedEventArgs e)
    {
        Close();
    }
}