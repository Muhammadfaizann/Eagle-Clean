using Custodian.ActivityLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Helpers
{
    public class SignatureService
    {
        static string root = Utils.ROOT_PATH;
        static string mainFolder = "Custodian";
        static string sigFolder = "Signatures";

        static string filename = string.Empty;

        public SignatureService()
        {
            try
            {

                string dirCustodian = Path.Combine(root, mainFolder);
                if (!Directory.Exists(dirCustodian))
                    Directory.CreateDirectory(dirCustodian);
                string dirDebug = Path.Combine(dirCustodian, sigFolder);
                if (!Directory.Exists(dirDebug))
                    Directory.CreateDirectory(dirDebug);

                filename = Path.Combine(root, mainFolder, sigFolder, Utils.BadgeID + ".png");
               

            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }

        public async void SaveSignature(IScreenshotResult signature)
        {
            
                try
                {

                    Stream sourceStream = await signature.OpenReadAsync(ScreenshotFormat.Png, 100);
                    using (var memoryStream = new MemoryStream())
                    {
                        sourceStream.CopyTo(memoryStream);
                        File.WriteAllBytes(filename,  memoryStream.ToArray());
                    }

                   
                }
                catch (Exception ex)
                {
                    Logger.Log("1", "Exception", ex.Message);
                }
            
        }
    }
}
