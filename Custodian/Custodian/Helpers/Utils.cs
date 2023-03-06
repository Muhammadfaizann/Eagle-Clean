using CommunityToolkit.Maui.Core.Extensions;
using Custodian.Models;
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
        public static string ROOT_PATH = "/storage/emulated/0/";
        public static Route activeAssigment { get; set; }
        public static string activeRouteFileName { get; set; }= string.Empty;

        public static Guid currentGuid { get; set; }
        public static MergeRecord activeRouteRecord { get; set; }
        public static ObservableCollection<Route> partialRoutes = new ObservableCollection<Route>();
        public static ObservableCollection<CompletedRoute> completedRoutes = new ObservableCollection<CompletedRoute>();
        public static Config config { get; set; }



        public static async void LoadCompletedRoutes()
        {
            string response = await DatabaseService.read("completed-routes.json");
            completedRoutes = JsonSerializer.Deserialize<List<CompletedRoute>>(response).ToObservableCollection();
        }

        public static async void LoadPartialRoutes()
        {
            string response = await DatabaseService.read("partial-routes.json");
            partialRoutes = JsonSerializer.Deserialize<List<Route>>(response).ToObservableCollection();
        }

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
    }
}
