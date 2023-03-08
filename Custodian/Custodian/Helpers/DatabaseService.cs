using Custodian.ActivityLog;
using PCLStorage;
using System.IO.Enumeration;
using System.Text;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Helpers
{
    internal class DatabaseService
    {
        public static async Task<string> Write(string record)
        {
            try
            {
                DateTime now = DateTime.Now;

                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder routeFolder = await rootFolder.CreateFolderAsync("Custodian/Database/Routes", CreationCollisionOption.OpenIfExists);
                string fileName;
                if (!string.IsNullOrEmpty(Utils.activeRouteFileName))
                {
                     fileName = Utils.activeRouteFileName;
                }
                else
                {
                     Guid guidID = Guid.NewGuid();
                     Utils.currentGuid = guidID;
                     fileName = guidID.ToString() + "_" + now.ToString("yyyymmdd") + ".json";
                }
                IFile file = await routeFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        await writer.WriteLineAsync(record);
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                Logger.Log("Exception", ex.ToString());
            }
            return string.Empty;
        }
        public static async Task<string> read(string fileName)
        {
            try
            {
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/Database/completed-routes.json");


                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var FileText = await reader.ReadLineAsync();
                    return FileText;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception", ex.ToString());
                return string.Empty;
            }
        }
    }
}
