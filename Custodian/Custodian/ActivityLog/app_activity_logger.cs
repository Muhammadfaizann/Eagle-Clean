

using Android.Content.PM;
using Android.Graphics.Pdf;
using Android.OS;
using Custodian.Helpers;
using Custodian.Models;
using Newtonsoft.Json;
using PCLStorage;
using System.Text;
using static Android.Icu.Util.LocaleData;
using static AndroidX.Navigation.Navigator;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.ActivityLog
{
    public class app_activity_logger
    {
        public static async void write(string activity)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("d");
            string time = now.ToString("T");
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder custodianFolder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFolder debugFolder = await custodianFolder.CreateFolderAsync("debug", CreationCollisionOption.OpenIfExists);
                IFile file = await debugFolder.CreateFileAsync("activity-log.txt", CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        writer.WriteLine("["+date+"]"+","+"["+time+"] "+activity);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static async void createConfigFileTXT()
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder folder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync("config.txt", CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        writer.WriteLine("IsStartRouteButtonsVisible=false");
                    }
                }
                 
            }
            catch (Exception ex)
            {

            }
        }
        public static async void createConfigFile()
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder folder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync("config.json", CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                       // string jsonContents = JsonConvert.SerializeObject(new Config { IsStartRouteButtonVisible = false });
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static async void readConfigFile()
        {
            try
            {
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/" + "config.json");


                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var FileText = await reader.ReadToEndAsync();
                   // Config config = JsonConvert.DeserializeObject<Config>(FileText);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static async void importConfigurations()
        {
            try
            {
                Utils.config = new Config();
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/" + "config.txt");
                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var FileText = await reader.ReadToEndAsync();
                    if (FileText.Contains("true"))
                        Utils.config.IsStartRouteButtonVisible = true;
                    else if(FileText.Contains("false"))
                        Utils.config.IsStartRouteButtonVisible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
