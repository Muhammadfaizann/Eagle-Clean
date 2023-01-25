using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;


namespace Custodian;



[Activity(Theme = "@style/MainTheme.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait,MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize  | ConfigChanges.Orientation  | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.AddFlags(WindowManagerFlags.Fullscreen);
        Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
        base.OnCreate(savedInstanceState);
        
    }
}
