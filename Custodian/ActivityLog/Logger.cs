using Custodian.Helpers;
using PCLStorage;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.ActivityLog
{

    public class Logger
    {
        static string root = Utils.ROOT_PATH;
        static string mainFolder = "Custodian";
        static string logFolder = "debug";
        static object Monitor = new object();

        public static void init()
        {
            try
            {
               
                string dirCustodian = Path.Combine(root, mainFolder);
                if(!Directory.Exists(dirCustodian))
                 Directory.CreateDirectory(dirCustodian);
                string dirDebug =Path.Combine(dirCustodian, logFolder);
                if (!Directory.Exists(dirDebug))
                    Directory.CreateDirectory(dirDebug);

               
                string filePath = Path.Combine(root, mainFolder, logFolder, DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(filePath)) { File.Create(filePath); }
            }
            catch(Exception ex)
            {

            }
        }

        protected static string GetFilename()
        {
            string name = Path.Combine(root,mainFolder,logFolder, DateTime.Now.ToString("yyyyMMdd") + ".txt");
            return name;
        }
       
        public  static void Log(string level, string category, string message)
        {
            lock(Monitor)
            {
                try
                {
                    //Get Filename, can change as date changes
                    string filename = Logger.GetFilename();
                    using (StreamWriter sw = File.AppendText(filename))
                    {
                        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "|" + level + "|" + category + "|" + message);
                    }
                }
                catch (Exception ex) {
                
                }
            }
        }
        

    }
}
