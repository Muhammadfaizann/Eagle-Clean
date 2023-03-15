using CommunityToolkit.Maui.Views;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.ProofOfWork;
using System.Text.Json;
using Custodian.ActivityLog;
using static System.Net.Mime.MediaTypeNames;
using Application = Microsoft.Maui.Controls.Application;
using GoogleGson;

namespace Custodian.Popups;

public partial class EndRoutePopup : Popup
{
    ILocationService _locationService;
	public EndRoutePopup(bool IsComplete, ILocationService locationService)
	{
		InitializeComponent();
        _locationService = locationService;
        if (IsComplete)
        {
            lblButton.Text = "Complete";
            lbl.Text = "Complete Route?";
            lblButton.Background = (Brush) Application.Current.Resources["GreenGradient"];
        }

        lblRoute.Text = Utils.activeRouteRecord.route;
        estimatedTime.Text = "Estimated Time : " + TimeSpan.FromSeconds(int.Parse(Utils.activeRouteRecord.estimatedTime)) ;
        if (IsComplete)
            actualTime.Text = "Actual Time : " + TimeSpan.FromSeconds(int.Parse(Utils.activeRouteRecord.actualTime));
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
                var completedRoute = Utils.partialRoutes.First(r => r.filename == Utils.activeRouteFileName);
                Utils.partialRoutes.Remove(completedRoute);
                var completedMergeRecord = JsonSerializer.Deserialize<MergeRecord>(completedRoute.json);
                Utils.completedRoutes.Add(completedMergeRecord);

                Utils.activeRouteRecord.seq = "4";
                Utils.activeRouteRecord.endDate = DateTime.Now.ToString("MM/dd/yyyy");
                Utils.activeRouteRecord.endTime = DateTime.Now.ToString("HH:mm:ss");
                Location currentLocation = await _locationService.GetCurrentLocation();
                Utils.activeRouteRecord.endLatitude = currentLocation.Latitude.ToString();
                Utils.activeRouteRecord.endLongitude = currentLocation.Longitude.ToString();
                Utils.activeRouteRecord.status = "Complete";


                
                string jsonRecord = JsonSerializer.Serialize(Utils.activeRouteRecord);
                Utils.activeRouteFileName = await FileSystemService.Write(jsonRecord);

                string guid = Utils.activeRouteFileName.Split("_")[0];
                Utils.OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid), filename = Utils.activeRouteFileName, json = jsonRecord });
               

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
                Utils.activeRouteFileName = await FileSystemService.Write(jsonRecord);

                string guid = Utils.activeRouteFileName.Split("_")[0];
                Utils.OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid), filename = Utils.activeRouteFileName, json = jsonRecord });
                var found = Utils.partialRoutes.FirstOrDefault(x => x.id == Guid.Parse(guid));
                found.json = jsonRecord;
            }

            await Shell.Current.GoToAsync("..");
        }
        catch(Exception ex)
        {
            Logger.Log("1", "Exception", ex.Message);
        }
    } 
    
}