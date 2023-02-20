using Custodian.Helpers;
using PCLStorage;
using System.Text;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Services
{
    internal class DatabaseService
    {
        public static async void write(string record)
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder custodianFolder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFolder debugFolder = await custodianFolder.CreateFolderAsync("Database", CreationCollisionOption.OpenIfExists);
                IFile file = await debugFolder.CreateFileAsync("offline-db.json", CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        writer.WriteLine(record);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
