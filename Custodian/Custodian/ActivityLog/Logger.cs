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
                    //Following make sure if the folders do not exist create them or use the existing ones 

                    IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                        IFolder custodianFolder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                        IFolder debugFolder = await custodianFolder.CreateFolderAsync("debug", CreationCollisionOption.OpenIfExists);
                        IFile file = await debugFolder.CreateFileAsync("debug_log_"+ now.ToString("yyyy_MM_dd") + ".txt", CreationCollisionOption.OpenIfExists);
                        //var fileName = Path.Combine(Utils.ROOT_PATH, "Custodian/debug/" + "debug_log_" + now.ToString("yyyy_MM_dd") + ".txt");  -->> this wouldnt work if the folders or file is not there
                        using (StreamWriter writer = new StreamWriter(file.Path, true))
                        {
                            await writer.WriteLineAsync("["+ timeStamp +"|"+ category + "|"+ level + "] " + message);
                            writer.Flush();
                            writer.Close();
                        }
                    
                }
                
            }
            catch (Exception ex)
            {
               
            }
        }
        
    }
}
