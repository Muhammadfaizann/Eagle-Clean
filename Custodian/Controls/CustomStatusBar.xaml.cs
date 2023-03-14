namespace Custodian.Controls;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Messages;

public partial class CustomStatusBar : Frame
{
	public CustomStatusBar()
	{
		InitializeComponent();
        Task.Run(() => {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            DateTime now = DateTime.Now;
            time.Text = now.ToString("t");
            return Task.CompletedTask;
        });
        

        Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;

        WeakReferenceMessenger.Default.Register<ShowSyncIconMessage>(this, ShowSyncIcon);

        WeakReferenceMessenger.Default.Register<HideSyncIconMessage>(this, HideSyncIcon);
    }

    private void HideSyncIcon(object recipient, HideSyncIconMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            syncIcon.IsVisible = false;
        });
    }

    private void ShowSyncIcon(object recipient, ShowSyncIconMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            syncIcon.IsVisible = true;
        });
       
    }
    
    private  void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
       
    }

    private async void Help_Icon_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "App version: " + AppInfo.Current.VersionString;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 12;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
}