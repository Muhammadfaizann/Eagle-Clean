namespace Custodian.Controls;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

public partial class CustomStatusBar : Frame
{
	public CustomStatusBar()
	{
		InitializeComponent();
        DateTime now = DateTime.Now;
        time.Text=now.ToString("t");
        Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
    }

    private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        var level = e.ChargeLevel;
        if (level < 0.25)
            battery.Source = "battery1cell.png";
        else if (level < 0.5)
            battery.Source = "battery2cell.png";
        else if (level < 0.75)
            battery.Source = "battery3cell.png";
        
        flash.IsVisible = e.State switch
        {
            BatteryState.Charging => true,
            _ => false
        };
    }

    private async void Help_Icon_Tapped(object sender, TappedEventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        string text = "App version: " + AppInfo.Current.VersionString;
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 12;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}