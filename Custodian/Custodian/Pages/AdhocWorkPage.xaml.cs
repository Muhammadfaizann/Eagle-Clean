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
        DateTime dateTime = DateTime.ParseExact("00:03:00", "HH:mm:ss", null);
        var seconds = dateTime.TimeOfDay.TotalSeconds;
        var progressPerSec = (1 / seconds) * 100;
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
    private void OpenFlyoutMenu(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        Shell.Current.FlyoutIsPresented = true;
    }

}