using Android.Content.PM;
using Android.Hardware.Camera2;
using CommunityToolkit.Maui.Core.Extensions;
using Custodian.ActivityLog;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileSystem = PCLStorage.FileSystem;
using Task = System.Threading.Tasks.Task;

namespace Custodian.Helpers
{
    public static class Utils
    {
       
        public static string BadgeID;
        public static string ROOT_PATH = "/storage/emulated/0/";
        public static bool IsFromHomePage = false;
        public static Route activeAssigment { get; set; }
        public static string activeRouteFileName { get; set; }= string.Empty;
        public static Guid currentGuid { get; set; }
        public static MergeRecord activeRouteRecord { get; set; }
        
        public static ObservableCollection<WorkRecord> partialRoutes = new ObservableCollection<WorkRecord>();
        public static ObservableCollection<MergeRecord> completedRoutes = new ObservableCollection<MergeRecord>();
        public static List<WorkRecord> AllRecords = new List<WorkRecord>();
        public static List<WorkRecord> OfflineRecords = new List<WorkRecord>();
        public static Config config { get; set; }
        public static async Task ImportConfigurations()
        {
            try
            {
                config = new Config();
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/" + "config.json");
                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var jsonString = await reader.ReadToEndAsync();
                    config = JsonSerializer.Deserialize<Config>(jsonString);
                    reader.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
            return ;
        }
        public static async void createConfigFile()
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(ROOT_PATH);
                IFolder folder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync("config.json", CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        string json = JsonSerializer.Serialize(new Config { Radius=10, APIBaseURL= "https://eagleclean-be.azurewebsites.net" });
                        await writer.WriteLineAsync(json);
                        writer.Close();
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
        internal static async Task LoadRoutes()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    if (Permissions.ShouldShowRationale<Permissions.StorageRead>()) 
                    {
                        // Prompt the user with additional information as to why the permission is needed
                    }

                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                if (status == PermissionStatus.Granted)
                {
                    IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                    IFolder toUploadFolder = await rootFolder.CreateFolderAsync("Custodian/Data/ToUpload", CreationCollisionOption.OpenIfExists);
                    var toUploadFiles = await toUploadFolder.GetFilesAsync();

                    
                    IFolder uploadedFolder = await rootFolder.CreateFolderAsync("Custodian/Data/Uploaded", CreationCollisionOption.OpenIfExists);
                    var uploadedFiles = await uploadedFolder.GetFilesAsync();
                   

                    List<MergeRecord> toUploadedRecords = new List<MergeRecord>();
                    foreach(var file in toUploadFiles.ToList() )
                    {
                        using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                        using (var reader = new StreamReader(stream))
                        {
                            var jsonString = await reader.ReadLineAsync();
                            MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(jsonString);
                            record.filename = file.Name;
                            toUploadedRecords.Add(record);
                        }
                    }
                    List<MergeRecord> uploadedRecords = new List<MergeRecord>();
                    foreach (var file in uploadedFiles.ToList())
                    {
                        using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                        using (var reader = new StreamReader(stream))
                        {
                            var jsonString = await reader.ReadLineAsync();
                            MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(jsonString);
                            record.filename = file.Name;
                            uploadedRecords.Add(record);
                        }
                    }
                    
                    foreach (var record in toUploadedRecords.ToList())
                    {
                        foreach(var uploadedFile in uploadedRecords.ToList())
                        if(record.filename==uploadedFile.filename)
                        {
                           uploadedRecords.Remove(uploadedFile);
                        }
                    }
                    foreach(var record in uploadedRecords.ToList())
                    {
                        toUploadedRecords.Add(record);
                    }
                    foreach (var file in toUploadedRecords.ToList())
                    {
                        string guid = file.filename.Split("_")[0];
                        string date = file.filename.Split("_")[1].Split(".")[0];
                        DateTime now = DateTime.Now;
                        if (date == now.ToString("yyyyMMdd")) // only today's records
                        {
                            
                                if(file.employee == BadgeID)
                                if ((file.seq == "3" || file.seq == "2" || file.seq == "1") )  // partial records
                                {
                                        partialRoutes.Add(new WorkRecord(){ id=Guid.Parse(guid), filename=file.filename,json= JsonSerializer.Serialize(file) });
                                }
                                else if (file.seq == "4" ) //completed records
                                {
                                        completedRoutes.Add(file);
                                }
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
            return ;
        }
        public static async Task<Route> StartRoute(string startJson, double Latitude, double Longitude, bool IsScanned)
        {
            try
            {
                int totalSec = 0;
                Route route = JsonSerializer.Deserialize<Route>(startJson);
                route.taskList = new List<Models.Task>();
                List<string> tasksWithEstimatedTimeInSec = new List<string>();
                foreach (var task in route.tasks)
                {
                    var strings = task.Split('|');
                    var estimatedTimeInSec = (int.Parse(strings[1]) * 60);
                    route.taskList.Add(new Models.Task { Description = strings[0], PlannedTimeInMint = strings[1], PlannedTimeInSec = estimatedTimeInSec.ToString() });
                    tasksWithEstimatedTimeInSec.Add(strings[0] + "|" + estimatedTimeInSec);
                    totalSec = totalSec + estimatedTimeInSec;
                }
                TimeSpan estimatedTimeSpan = TimeSpan.FromSeconds(totalSec);
                route.plannedTime = estimatedTimeSpan.ToString("t");
               

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
                //if(IsScanned)
                record.startBarcode = startJson;
                //else
                //record.startBarcode = null;
                record.endBarcode = null;
                record.estimatedTime = estimatedTimeSpan.TotalSeconds.ToString();
                record.actualTime = null;
                record.status = "Started";
                record.tasksComplete = new List<string>();
                record.tasksIncomplete = tasksWithEstimatedTimeInSec;
                record.pics = default(List<string>);
               
                string jsonRecord = JsonSerializer.Serialize(record);
                activeRouteFileName = await FileSystemService.Write(jsonRecord);  
                activeRouteRecord = record;
                activeAssigment = route;
                partialRoutes.Add(new WorkRecord() { id= Guid.Parse(activeRouteFileName.Split("_")[0]), filename= activeRouteFileName, json= jsonRecord });
                string guid = activeRouteFileName.Split("_")[0];
                OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid), filename= activeRouteFileName, json=jsonRecord});
               
            }
            catch(Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
            return null;
        }

        public async static Task<bool> IsBadgeValid(string badgeID)
        {
            bool result = false;
            try
            {
                var match12 = Regex.Match(badgeID, "^\\d{12}\\z", RegexOptions.IgnoreCase);
                var match7 = Regex.Match(badgeID, "^\\d{7}\\z", RegexOptions.IgnoreCase);

                Logger.Log("2", "Info", $"Checking Mod10 of Badge ID: {badgeID} , Length: {badgeID.Length}.");

                if (match12.Success || match7.Success)
                {
                    CheckSum cs = new CheckSum();
                    cs.CalculateMod10CheckDigit(badgeID.Substring(0, badgeID.Length - 1), "");
                    Logger.Log("2", "Info", $"Mod10 Check Digit: {cs.CheckDigit[0]},  Badge ID: {badgeID}.");
                    if (badgeID[badgeID.Length - 1] == cs.CheckDigit[0])
                    {
                        result = true;
                        Logger.Log("2", "Info", $"Badge ID: {badgeID} validated successfully, Length: {badgeID.Length}.");
                    }
                }
            }
            catch (Exception ex) 
            {
                Logger.Log("1", "Exception", ex.Message);
                result = false;
            }
            return result;
        }

    }
}
