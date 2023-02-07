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
            var itemToRemove = Utils.ongoingAssigments.Single(r => r.Title == Utils.activeAssigment.Title);
            Utils.ongoingAssigments.Remove(itemToRemove);
            Utils.completedAssigments.Add(new Models.CompletedAssignment() { Title = Utils.activeAssigment.Title, IsOverTime = false });
        }
        else
        {
            var obj = Utils.ongoingAssigments.FirstOrDefault(r => r.Title == Utils.activeAssigment.Title);
            if (obj != null) obj.IsStarted = true;
        }

        WeakReferenceMessenger.Default.Unregister<EndRouteMessage>(this);
        await Shell.Current.GoToAsync("..");

    } 
    
}