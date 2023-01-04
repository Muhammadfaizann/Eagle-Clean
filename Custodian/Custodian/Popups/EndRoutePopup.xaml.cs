using CommunityToolkit.Maui.Views;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Custodian.Popups;

public partial class EndRoutePopup : Popup
{
	public EndRoutePopup(bool IsComplete)
	{
		InitializeComponent();
        if (IsComplete)
        {
            lblButton.Text = lbl.Text = "Complete";
            lblButton.Background = (Brush) Application.Current.Resources["GreenGradient"];
        }
    }

    private void cancel_Clicked(object sender, EventArgs e)
    {
		Close();
    }
    private void partial_Clicked(object sender, EventArgs e)
    {
        Close();
    }
    
}