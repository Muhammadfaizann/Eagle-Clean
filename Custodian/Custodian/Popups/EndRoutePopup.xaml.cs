using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Pages;
using Custodian.Services.ProofOfWork;
using System.Reflection;           
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Custodian.Popups;

public partial class EndRoutePopup : Popup
{
    ILocationService _locationService;
    IProofOfWorkService _proofOfWorkService;
	public EndRoutePopup(bool IsComplete, ILocationService locationService, IProofOfWorkService proofOfWorkService)
	{
		InitializeComponent();
        _locationService = locationService;
        _proofOfWorkService= proofOfWorkService;
        if (IsComplete)
        {
            lblButton.Text = lbl.Text = "Complete";
            lblButton.Background = (Brush) Application.Current.Resources["GreenGradient"];
        }
    }

    private void cancel_Clicked(object sender, EventArgs e)
    {
		Close();
    }
    private async void partial_Clicked(object sender, EventArgs e)
    {
        try
        {
            Close();
            if (lblButton.Text == "Complete")
            {
                var itemToRemove = Utils.partialRoutes.First(r => r.rte == Utils.activeAssigment.rte);
                Utils.partialRoutes.Remove(itemToRemove);
                Utils.completedRoutes.Add(new Models.CompletedRoute() { Title = Utils.activeAssigment.rte, IsOverTime = false });

                Utils.activeRouteRecord.seq = "4";
                Utils.activeRouteRecord.endDate = DateTime.Now.ToString("MM/dd/yyyy");
                Utils.activeRouteRecord.endTime = DateTime.Now.ToString("HH:mm:ss");
                Location currentLocation = await _locationService.GetCurrentLocation();
                Utils.activeRouteRecord.endLatitude = currentLocation.Latitude.ToString();
                Utils.activeRouteRecord.endLongitude = currentLocation.Longitude.ToString();
                Utils.activeRouteRecord.status = "Complete";

                string jsonRecord = JsonSerializer.Serialize<MergeRecord>(Utils.activeRouteRecord);
                await DatabaseService.write(jsonRecord);
                var workRecord = new WorkRecord() { id = Utils.currentGuid, json = jsonRecord };
                _proofOfWorkService.SendWorkRecord(workRecord);

                Utils.activeRouteFileName = string.Empty;
                
            }
            else
            {
                Utils.activeRouteRecord.seq = "3";
                Utils.activeRouteRecord.endDate = DateTime.Now.ToString("MM/dd/yyyy");
                Utils.activeRouteRecord.endTime = DateTime.Now.ToString("HH:mm:ss");
                Location currentLocation = await _locationService.GetCurrentLocation();
                Utils.activeRouteRecord.startLatitude = currentLocation.Latitude.ToString();
                Utils.activeRouteRecord.startLongitude = currentLocation.Longitude.ToString();
                Utils.activeRouteRecord.status = "Partial";

                string jsonRecord = JsonSerializer.Serialize<MergeRecord>(Utils.activeRouteRecord);
                await DatabaseService.write(jsonRecord);
                var workRecord = new WorkRecord() { id = Utils.currentGuid, json = jsonRecord };
                _proofOfWorkService.SendWorkRecord(workRecord);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch(Exception ex)
        {

        }
    } 
    
}