using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.ProofOfWork;
using System.Text.Json;

namespace Custodian.Pages;

public partial class ScanJob : ContentPage
{
    ILocationService _locationService;
    IProofOfWorkService _proofOfWorkService;

    public ScanJob(ILocationService locationService, IProofOfWorkService proofOfWorkService)
    {
        try
        {
            InitializeComponent();
            _locationService = locationService;
            _proofOfWorkService = proofOfWorkService;
        }
        catch (Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }
    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            WeakReferenceMessenger.Default.Register<StartRouteMessage>(this, OnStartRouteMessageReceived);
            Logger.Log("2", "Info", "ScanJob Page Loaded!");
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    }

    private void OnStartRouteMessageReceived(object recipient, StartRouteMessage message)
    {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    loader.IsRunning = loader.IsVisible = true;
                    
                    var jsonString = message.Value.ToString();

                    Location currentLocation = await _locationService.GetCurrentLocation();
                    if(currentLocation != null)  
                    await Utils.StartRoute(jsonString, currentLocation.Latitude, currentLocation.Longitude, true); 
                    else
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        string text = "Location record is not available";
                        ToastDuration duration = ToastDuration.Short;
                        double fontSize = 12;
                        var toast = Toast.Make(text, duration, fontSize);
                        await toast.Show(cancellationTokenSource.Token);
                    }

                    await Navigate();

                    loader.IsRunning = loader.IsVisible = false;
                });

            }
            catch (Exception ex)
            {
                Logger.Log("1","Exception", ex.Message);
            }
    }

    private async System.Threading.Tasks.Task Navigate()
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(ProofOfWork));
        }
        catch (Exception ex)
        {
            Logger.Log("1","Exception", ex.Message);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<StartRouteMessage>(this);
    }
}