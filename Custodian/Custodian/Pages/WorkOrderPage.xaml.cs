
using Android.Content;
using Android.OS;
using Android.Widget;
using Custodian.Models;

namespace Custodian.Pages;

public partial class WorkOrderPage : ContentPage, IQueryAttributable
{
    IDispatcherTimer timer;
    public WorkOrderPage()
	{
		InitializeComponent();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Workorder obj = query["param"] as Workorder;
        lblTitle.Text = obj.Title+ " - "+ obj.Subject;
    }

    private void btnStartTimer_Clicked(object sender, EventArgs e)
    {
        var button=sender as Microsoft.Maui.Controls.Button;
        if (button.Text == "Start Timer")
        {
            btnEndRoute.Text = "Finish Work Order";
            btnEndRoute.BackgroundColor = Color.FromArgb("#E71921");
            btnEndRoute.Background = Brush.Default;
            btnEndRoute.CornerRadius = 10;
            StartTimerCountDown();
        }
        else
        {
            timer.Stop();
            DateTime dateTime = DateTime.ParseExact(plannedTime.Text, "HH:mm:ss", null);
            DateTime Timer = DateTime.ParseExact(lblTime.Text, "HH:mm:ss", null);
            TimeSpan timeDifference = dateTime.Subtract(Timer);
            DateTime dateTime1 = new DateTime() + timeDifference;
            actualTime.Text = dateTime1.ToString("HH:mm:ss");
        }
        
    }

    private void StartTimerCountDown()
    {
        
        DateTime dateTime = DateTime.ParseExact(plannedTime.Text, "HH:mm:ss", null) ;
        var seconds = dateTime.TimeOfDay.TotalSeconds;
        var progressPerSec =  (1 / seconds )* 100;
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        lblTime.IsVisible = true;
        timer.Tick += (s, e) =>
        {
            lblTime.Dispatcher.Dispatch(() =>
            {
                lblTime.Text = dateTime.ToString("HH:mm:ss");
                dateTime = dateTime.AddSeconds(-1);
                timerProgressBar.Progress = timerProgressBar.Progress - progressPerSec;
            });

        };
        timer.Start();

    }
}