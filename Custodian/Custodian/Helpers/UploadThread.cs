using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.ProofOfWork;
using Org.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Helpers
{
    public class UploadThread
    {
        static IProofOfWorkService _proofOfWorkSerive;
        public UploadThread(IProofOfWorkService proofOfWorkSerive)
        {
            Init();
            _proofOfWorkSerive =proofOfWorkSerive;
            Thread thread1 = new Thread(RunUploadBackendThread);
            thread1.Start();
        }

        private async void Init()
        {
            try { 
            // Loading all the records from local storage

            IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync("/storage/emulated/0/Custodian/Database/Routes");
            var files = await folder.GetFilesAsync();

                foreach (var file in files)
                {
                    using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonString = await reader.ReadLineAsync();
                        MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(jsonString);
                        if (!record.IsUploaded)
                        {
                            string guid = file.Name.Split("_")[0];
                            Utils.OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid), json = jsonString });
                        }
                        
                    }
                }
               
            }
            catch(Exception ex)
            {

            }
        }
        public async void RunUploadBackendThread()
        {
            while (true)
            {
                try
                {
                    foreach (var record in Utils.OfflineRecords.ToList())
                    {
                        // send the record to the server
                        var result = await _proofOfWorkSerive.SendWorkRecord(record);
                        if (result) // if record is successfully uploaded change the IsUploaded to true, update the local record and remove it from offlineRecords
                        {

                            Utils.OfflineRecords.Remove(record);
                            MergeRecord mergeRecord = JsonSerializer.Deserialize<MergeRecord>(record.json);
                            mergeRecord.IsUploaded = true;
                            await DatabaseService.Write(record.json);
                        }
                        // Sleep while there are no records
                        while(Utils.OfflineRecords.Count == 0) Thread.Sleep(1000);

                    }
                }
                catch(Exception ex)
                { 
                
                }
            }
        }
       
    }
}
