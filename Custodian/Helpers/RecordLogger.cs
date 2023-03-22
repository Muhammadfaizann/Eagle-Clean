using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Helpers
{
    public class RecordLogger
    {
        static string root = Utils.ROOT_PATH;
        static string mainFolder = "Custodian";
        static string logFolder = "debug";
        static object Monitor = new object();

        static string filePath;

        public static void init()
        {
            try
            {

                string dirCustodian = Path.Combine(root, mainFolder);
                if (!Directory.Exists(dirCustodian))
                    Directory.CreateDirectory(dirCustodian);
                string dirDebug = Path.Combine(dirCustodian, logFolder);
                if (!Directory.Exists(dirDebug))
                    Directory.CreateDirectory(dirDebug);


                filePath = Path.Combine(root, mainFolder, logFolder, "Record-"+DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(filePath)) { File.Create(filePath); }
            }
            catch (Exception ex)
            {

            }
        }

        

        public static void LogRcord(string type, string record)
        {
            lock (Monitor)
            {
                try
                {
                   
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "|" + type + "|" + record);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
