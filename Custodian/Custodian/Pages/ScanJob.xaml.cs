using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Helpers;
using Custodian.Helpers.LocationService;
using Custodian.Messages;
using Custodian.Models;
using System.Text.Json;

namespace Custodian.Pages;

public partial class ScanJob : ContentPage
{
    ILocationService _locationService;
	public ScanJob(ILocationService locationService)
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<StartRouteMessage>(this, (sender, args) =>
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async() =>
                {
                    int totalMint = 0;
                    var jsonString = args.Value.ToString();
                    var route = JsonSerializer.Deserialize<Route>(jsonString);
                    route.steps = new List<Step>();
                    foreach (var task in route.tasks)
                    {
                        var strings = task.Split('|');
                        route.steps.Add(new Step { Description = strings[0], PlannedTimeInMint = strings[1] });
                        totalMint = totalMint + int.Parse(strings[1]);
                    }
                    TimeSpan timeSpan = TimeSpan.FromMinutes(totalMint);
                    route.plannedTime = timeSpan.ToString("t");
                    Utils.partialRoutes.Add(route);
                    MergeRecord record = new MergeRecord();
                    record.ver = "1";
                    record.seq = "1";
                    record.facilityid = "123321";
                    record.route = route.rte;
                    record.startDate = DateTime.Now.ToString("MM/dd/yyyy");
                    record.startTime = DateTime.Now.ToString("HH:mm:ss");
                    record.endDate = null;
                    record.endTime = null;
                    Location currentLocation = await locationService.GetCurrentLocation();
                    record.startLatitude = currentLocation.Latitude.ToString();
                    record.startLongitude = currentLocation.Longitude.ToString();
                    record.endLatitude = null;
                    record.endLongitude= null;
                    record.employee = "456654";
                    record.startBarcode = args.Value.ToString();
                    record.endBarcode = null;
                    record.estimatedTime = totalMint.ToString();
                    record.actualTime = null;
                    record.status = "Started";
                    record.tasksComplete = new List<string>();
                    record.tasksIncomplete = route.tasks;
                    record.pics= default(List<string>);
                    string jsonRecord = JsonSerializer.Serialize<MergeRecord>(record);
                    Utils.activeRouteFileName= await DatabaseService.write(jsonRecord);
                    Utils.activeRouteRecord = record;
                    Navigate(route);
                });

            }
            catch (Exception ex)
            {
                app_activity_logger.write("Exception", ex.ToString());
            }
        });
    }

    private async void Navigate(Route route)
    {
        try
        {
            Utils.activeAssigment = route;
            var navigationParameter = new Dictionary<string, object>
            {
                { "param", route }
            };
            await Shell.Current.GoToAsync(nameof(ProofOfWork), navigationParameter);
        }
        catch (Exception ex)
        {
            app_activity_logger.write("Exception", ex.ToString());
        }
    }
}