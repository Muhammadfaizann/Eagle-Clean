using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Messages;
using Custodian.Pages;
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
    private async void partial_Clicked(object sender, EventArgs e)
    {

        Close();
        if (lblButton.Text == "Complete")
        {
            var itemToRemove = Utils.ongoingRoutes.Single(r => r.rte == Utils.activeAssigment.rte);
            Utils.ongoingRoutes.Remove(itemToRemove);
            Utils.completedRoutes.Add(new Models.CompletedRoute() { Title = Utils.activeAssigment.rte, IsOverTime = false });
        }
        else
        {
            //var obj = Utils.ongoingRoutes.FirstOrDefault(r => r.rte == Utils.activeAssigment.rte);
            //if (obj != null) obj.IsStarted = true;
        }
        GC.Collect();
        GC.WaitForPendingFinalizers();
        await Shell.Current.GoToAsync("..");

    } 
    
}