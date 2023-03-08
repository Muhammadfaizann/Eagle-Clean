using CommunityToolkit.Maui.Core.Extensions;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.ProofOfWork;
using Org.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Helpers
{
    public static class Utils
    {
       
        public static string BadgeID;
        public static string ROOT_PATH = "/storage/emulated/0/";
        public static Route activeAssigment { get; set; }
        public static string activeRouteFileName { get; set; }= string.Empty;
        public static Guid currentGuid { get; set; }
        public static MergeRecord activeRouteRecord { get; set; }
        public static ObservableCollection<Route> partialRoutes = new ObservableCollection<Route>();
        public static ObservableCollection<CompletedRoute> completedRoutes = new ObservableCollection<CompletedRoute>();

        public static List<WorkRecord> OfflineRecords = new List<WorkRecord>();
        public static Config config { get; set; }
        public static async void ImportConfigurations()
        {
            try
            {
                Utils.config = new Config();
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/" + "config_settings.json");
                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var jsonString = await reader.ReadToEndAsync();
                    config = JsonSerializer.Deserialize<Config>(jsonString);
                }
            }
            catch (Exception ex)
            {

            }
        }
        internal static async void LoadRoutes()
        {
           IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync("/storage/emulated/0/Custodian/Database/Routes");
           var files  = await folder.GetFilesAsync(); 

                foreach(var file in files)
                {
                    using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonString = await reader.ReadLineAsync();
                        MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(jsonString);
                        
                        if(record.seq=="3")
                        {

                            Route route = JsonSerializer.Deserialize<Route>(record.startBarcode);
                            partialRoutes.Add(route);
                        }
                        else if (record.seq == "4")
                        {

                            Route route = JsonSerializer.Deserialize<Route>(record.startBarcode);
                            completedRoutes.Add(new CompletedRoute() { Title = route.rte, IsOverTime=false }) ;
                        }
                    }
                }

        }
        public static async Task<Route> StartRoute(string startJson, double Latitude, double Longitude, bool IsScanned)
        {
            int totalSec = 0;
            Route route = JsonSerializer.Deserialize<Route>(startJson);
            route.taskList = new List<Models.Task>();
            foreach (var task in route.tasks)
            {
                var strings = task.Split('|');
                route.taskList.Add(new Models.Task { Description = strings[0], PlannedTimeInMint = strings[1], PlannedTimeInSec= (int.Parse(strings[1]) * 60).ToString() });
                totalSec = totalSec + (int.Parse(strings[1])*60);
            }
            TimeSpan estimatedTimeSpan = TimeSpan.FromSeconds(totalSec);
            route.plannedTime = estimatedTimeSpan.ToString("t");
            Utils.partialRoutes.Add(route);

            MergeRecord record = new MergeRecord();
            record.ver = "1";
            record.seq = "1";
            record.facilityid = route.fid;
            record.route = route.rte;
            record.startDate = DateTime.Now.ToString("MM/dd/yyyy");
            record.startTime = DateTime.Now.ToString("HH:mm:ss");
            record.endDate = null;
            record.endTime = null;
            record.startLatitude = Latitude.ToString();
            record.startLongitude = Longitude.ToString();
            record.endLatitude = null;
            record.endLongitude = null;
            record.employee = BadgeID;
            if(IsScanned)
            record.startBarcode = startJson;
            else
            record.startBarcode = null;
            record.endBarcode = null;
            record.estimatedTime = estimatedTimeSpan.TotalSeconds.ToString();
            record.actualTime = null;
            record.status = "Started";
            record.tasksComplete = new List<string>();
            record.tasksIncomplete = route.tasks;
            record.pics = default(List<string>);
            record.IsUploaded = false;
            string jsonRecord = JsonSerializer.Serialize<MergeRecord>(record);
            Utils.activeRouteFileName = await DatabaseService.Write(jsonRecord);
            Utils.activeRouteRecord = record;
            return route;
        }

        
    }
}
