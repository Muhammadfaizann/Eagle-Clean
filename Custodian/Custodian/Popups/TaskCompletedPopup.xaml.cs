using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Messages;

namespace Custodian.Popups;

public partial class TaskCompletedPopup : Popup
{
	public TaskCompletedPopup()
	{
        InitializeComponent();
    }
    private void cancel_Clicked(object sender, EventArgs e)
    {
        Close();
    } 
    private void complete_Clicked(object sender, EventArgs e)
    {
        try
        {
            WeakReferenceMessenger.Default.Send(new TaskCompletedMessage("Task Completed"));
            Close();
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}