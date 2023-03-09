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
    bool Isbusy=false;
    public ScanJob(ILocationService locationService,IProofOfWorkService proofOfWorkService)
	{
		InitializeComponent();
       _locationService=locationService;
       _proofOfWorkService= proofOfWorkService;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        WeakReferenceMessenger.Default.Register<StartRouteMessage>(this, OnStartRouteMessageReceived);
      
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
                    var route = await Utils.StartRoute(jsonString, currentLocation.Latitude, currentLocation.Longitude, true); 

                   // var workRecord = new WorkRecord() { id = Utils.currentGuid, json = jsonRecord };
                   // _proofOfWorkService.SendWorkRecord(workRecord);

                    await Navigate(route);

                    loader.IsRunning = loader.IsVisible = false;
                });

            }
            catch (Exception ex)
            {
                Logger.Log("1","Exception", ex.Message);
            }
    }

    private async System.Threading.Tasks.Task Navigate(Route route)
    {
        try
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", route }
            };
            await Shell.Current.GoToAsync(nameof(ProofOfWork), navigationParameter);
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