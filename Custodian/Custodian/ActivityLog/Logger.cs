using Custodian.Helpers;
using PCLStorage;
using System.Text;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.ActivityLog
{
    public class Logger
    {
        public static async void Log(string level,string category, string message)
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.ToString("HH:mm:ss:fff");
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
                    {
                        // Prompt the user with additional information as to why the permission is needed
                    }
                    status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }

                if (status == PermissionStatus.Granted)
                {
                    IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                    IFolder custodianFolder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                    IFolder debugFolder = await custodianFolder.CreateFolderAsync("debug", CreationCollisionOption.OpenIfExists);
                    IFile file = await debugFolder.CreateFileAsync("debug_log_"+ now.ToString("yyyy_MM_dd") + ".txt", CreationCollisionOption.OpenIfExists);
                    
                        using (StreamWriter writer = new StreamWriter(file.Path,true))
                        {
                            writer.WriteLine("["+ timeStamp +"|"+ category + "|"+ level + "] " + message);
                            writer.Close();
                        }
                    
                }
                
            }
            catch (Exception ex)
            {
               
            }
        }

        /*
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
        }*/
        
    }
}
