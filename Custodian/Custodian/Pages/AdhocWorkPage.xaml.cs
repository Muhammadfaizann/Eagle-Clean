namespace Custodian.Pages;

public partial class AdhocWorkPage : ContentPage
{
    bool stopTimer = false;
    public AdhocWorkPage()
	{
		InitializeComponent();
	}

    private void btnStartTimer_Clicked(object sender, EventArgs e)
    {
        var button = sender as Microsoft.Maui.Controls.Button;
        if (button.Text == "Start Timer")
        {
            btnEndRoute.Text = "Finish Work Order";
            btnEndRoute.BackgroundColor = Color.FromArgb("#E71921");
            btnEndRoute.Background = Brush.Default;
            btnEndRoute.CornerRadius = 10;
            Task task = StartTimerCountDownAsync();
        }
        else
        {
            stopTimer = true;
        }
    }
    private async Task StartTimerCountDownAsync()
    {
        lblTime.IsVisible = true;
        for (int progress = 100; progress >= 0; progress--)
        {
            if (stopTimer)
                break;
            await Task.Delay(1000);
            lblTime.Text = progress.ToString();
            timer.Progress = progress;

        }

    }
    
}