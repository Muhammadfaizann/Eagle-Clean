

using Android.Content.PM;
using Android.Graphics.Pdf;
using Android.OS;
using PCLStorage;
using System.Text;
using static Android.Icu.Util.LocaleData;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.ActivityLog
{
    public class app_activity_logger
    {
        static string path = "/storage/emulated/0/";
        public static async void write(string activity)
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("d");
            string time = now.ToString("T");
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(path);
                IFolder folder = await rootFolder.CreateFolderAsync("Custodian", CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync("activity-log.txt", CreationCollisionOption.OpenIfExists);
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

       
    }
}
