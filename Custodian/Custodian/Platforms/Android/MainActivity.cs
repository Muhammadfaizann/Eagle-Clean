using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Messages;
using Custodian.Platforms.Android;

namespace Custodian;



[Activity(Theme = "@style/MainTheme.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait,MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize  | ConfigChanges.Orientation  | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private static string ACTION_DATAWEDGE_FROM_6_2 = "com.symbol.datawedge.api.ACTION";
    private static string EXTRA_CREATE_PROFILE = "com.symbol.datawedge.api.CREATE_PROFILE";
    private static string EXTRA_SET_CONFIG = "com.symbol.datawedge.api.SET_CONFIG";
    private static string EXTRA_PROFILE_NAME = "Eagle Clean";
    private DataWedgeReceiver _broadcastReceiver = null; 
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        Window.AddFlags(WindowManagerFlags.Fullscreen);
        Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
        _broadcastReceiver = new DataWedgeReceiver();
        

        _broadcastReceiver.scanDataReceived += (s, scanData) =>
        {
            WeakReferenceMessenger.Default.Send(new BarcodeScanMessage(scanData));
        };

        CreateProfile();
    }
    protected override void OnResume()
    {
        base.OnResume();

        if (null != _broadcastReceiver)
        {
            // Register the broadcast receiver
            IntentFilter filter = new IntentFilter(DataWedgeReceiver.IntentAction);
            filter.AddCategory(DataWedgeReceiver.IntentCategory);
            Android.App.Application.Context.RegisterReceiver(_broadcastReceiver, filter);
        }
    }
    protected override void OnPause()
    {
        if (null != _broadcastReceiver)
        {
            // Unregister the broadcast receiver
            Android.App.Application.Context.UnregisterReceiver(_broadcastReceiver);
        }
        base.OnStop();
    }
    private void CreateProfile()
    {
        String profileName = EXTRA_PROFILE_NAME;
        SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_CREATE_PROFILE, profileName);

        //  Now configure that created profile to apply to our application
        Bundle profileConfig = new Bundle();
        profileConfig.PutString("PROFILE_NAME", EXTRA_PROFILE_NAME);
        profileConfig.PutString("PROFILE_ENABLED", "true"); //  Seems these are all strings
        profileConfig.PutString("CONFIG_MODE", "UPDATE");
        Bundle barcodeConfig = new Bundle();
        barcodeConfig.PutString("PLUGIN_NAME", "BARCODE");
        barcodeConfig.PutString("RESET_CONFIG", "true"); //  This is the default but never hurts to specify
        Bundle barcodeProps = new Bundle();
        barcodeConfig.PutBundle("PARAM_LIST", barcodeProps);
        profileConfig.PutBundle("PLUGIN_CONFIG", barcodeConfig);
        Bundle appConfig = new Bundle();
        appConfig.PutString("PACKAGE_NAME", this.PackageName);      //  Associate the profile with this app
        appConfig.PutStringArray("ACTIVITY_LIST", new String[] { "*" });
        profileConfig.PutParcelableArray("APP_LIST", new Bundle[] { appConfig });
        SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
        //  You can only configure one plugin at a time, we have done the barcode input, now do the intent output
        profileConfig.Remove("PLUGIN_CONFIG");
        Bundle intentConfig = new Bundle();
        intentConfig.PutString("PLUGIN_NAME", "INTENT");
        intentConfig.PutString("RESET_CONFIG", "true");
        Bundle intentProps = new Bundle();
        intentProps.PutString("intent_output_enabled", "true");
        intentProps.PutString("intent_action", DataWedgeReceiver.IntentAction);
        intentProps.PutString("intent_category", DataWedgeReceiver.IntentCategory);
        intentProps.PutString("intent_delivery", "2");
        intentConfig.PutBundle("PARAM_LIST", intentProps);
        profileConfig.PutBundle("PLUGIN_CONFIG", intentConfig);
        SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
    }
    private void SendDataWedgeIntentWithExtra(String action, String extraKey, Bundle extras)
    {
        Intent dwIntent = new Intent();
        dwIntent.SetAction(action);
        dwIntent.PutExtra(extraKey, extras);
        SendBroadcast(dwIntent);
    }
    private void SendDataWedgeIntentWithExtra(String action, String extraKey, String extraValue)
    {
        Intent dwIntent = new Intent();
        dwIntent.SetAction(action);
        dwIntent.PutExtra(extraKey, extraValue);
        SendBroadcast(dwIntent);
    }
}
