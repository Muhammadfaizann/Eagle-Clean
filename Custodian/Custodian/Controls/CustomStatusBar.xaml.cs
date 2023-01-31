namespace Custodian.Controls;

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
}