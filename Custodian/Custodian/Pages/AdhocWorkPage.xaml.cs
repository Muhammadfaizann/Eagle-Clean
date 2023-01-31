namespace Custodian.Pages;

public partial class AdhocWorkPage : ContentPage
{
    IDispatcherTimer timer;
    public AdhocWorkPage()
	{
		InitializeComponent();
	}

    private void btnStartTimer_Clicked(object sender, EventArgs e)
    {
        var button = sender as Microsoft.Maui.Controls.Button;
        if (button.Text == "Start Timer")
        {
            btnEndRoute.Text = "Stop Timer";
            btnEndRoute.CornerRadius = 10;
            btnEndRoute.Background = Brush.Default;
            btnEndRoute.BackgroundColor = Color.Parse("#E71921");
            StartTimerCountDown();
        }
        else
        {
            timer.Stop();
        }
    }
    private void StartTimerCountDown()
    {
        DateTime dateTime = DateTime.ParseExact("00:01:00", "HH:mm:ss", null);
        var seconds = dateTime.TimeOfDay.TotalSeconds;
        var progressPerSec = (1 / seconds) * 100;
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        lblTime.IsVisible = true;
        DateTime timer_date_time = new DateTime();
        timer.Tick += (s, e) =>
        {
            lblTime.Dispatcher.Dispatch(() =>
            {
                
                lblTime.Text = timer_date_time.ToString("HH:mm:ss");
                timer_date_time = timer_date_time.AddSeconds(1);
                timerProgressBar.Progress = timerProgressBar.Progress + progressPerSec;
            });

        };
        timer.Start();

    }
    

}