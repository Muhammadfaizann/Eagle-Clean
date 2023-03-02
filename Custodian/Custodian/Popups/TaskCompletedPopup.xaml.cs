using Bumptech.Glide.Util;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Messages;
using Custodian.Models;
using static Android.Renderscripts.ScriptGroup;

namespace Custodian.Popups;

public partial class TaskCompletedPopup : Popup
{
    static TimeSpan secondPrevTime = TimeSpan.Zero;
    static TimeSpan prevTime=TimeSpan.Zero;
	public TaskCompletedPopup(Step step, TimeSpan timeSpan)
	{
        InitializeComponent();

        TimeSpan difference = timeSpan.Subtract(prevTime);
        secondPrevTime = prevTime;
        prevTime = timeSpan;

        lblEstimated.Text = "Estimated Time : " + step.PlannedTimeInMint + "  Minutes";
        lblActual.Text = "Actual Time: " + String.Format("%.2f", difference.TotalMinutes)  + " Minutes";

    }
    private void cancel_Clicked(object sender, EventArgs e)
    {
        prevTime = secondPrevTime;
        Close();
    } 
    private void complete_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new TaskCompletedMessage("Task Completed"));
        Close();
       
    }
}