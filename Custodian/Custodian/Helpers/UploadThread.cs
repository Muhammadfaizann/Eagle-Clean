using CommunityToolkit.Mvvm.Messaging;
using Custodian.ActivityLog;
using Custodian.Controls;
using Custodian.Messages;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Custodian.Services.ProofOfWork;
using PCLStorage;
using System.Data;
using System.Text.Json;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Helpers
{
    public class UploadThread
    {
        static IProofOfWorkService _proofOfWorkSerive;
        public UploadThread(IProofOfWorkService proofOfWorkSerive)
        {
             Initialize();  
            _proofOfWorkSerive =proofOfWorkSerive;
           
        }
        private async void Initialize()
        {

            await Init();
            Thread thread1 = new Thread(RunUploadBackendThread);
            thread1.Start();
        }
        private async System.Threading.Tasks.Task Init()
        {
            try { 
            // Loading all the records from local storage

            IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync("/storage/emulated/0/Custodian/Data/ToUpload");
            var files = await folder.GetFilesAsync();

                foreach (var file in files)
                {
                    using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        var jsonString = await reader.ReadLineAsync();
                        MergeRecord record = JsonSerializer.Deserialize<MergeRecord>(jsonString);
                        if (record.employee==Utils.BadgeID)
                        {
                            string guid = file.Name.Split("_")[0];
                            string date = file.Name.Split("_")[1].Split(".")[0]; ;
                            DateTime now = DateTime.Now;
                            if(date==now.ToString("yyyyMMdd")) // only today's records
                            Utils.OfflineRecords.Add(new WorkRecord() { id = Guid.Parse(guid),filename= file.Name, json = jsonString });
                        }
                        
                    }
                }
                Utils.AllRecords = Utils.OfflineRecords;
               
            }
            catch(Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
        public async void RunUploadBackendThread()
        {
            while (true)
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        WeakReferenceMessenger.Default.Send(new ShowSyncIconMessage("Show Sync Icon"));
                    });

                    Logger.Log("3", "UploadThread", $"{Utils.OfflineRecords.Count} records found to be uploaded!");
                    foreach (var record in Utils.OfflineRecords.ToList())
                    {
                        try 
                        {
                            // send the record to the server
                            Logger.Log("3", "UploadThread"," WorkRecord with GUID : " + record.id + " Uploading!");
                            var result = await _proofOfWorkSerive.SendWorkRecord(record);
                            if (result) 
                            {
                                Logger.Log("3", "UploadThread", " WorkRecord with GUID : " + record.id + " Uploaded Successfully!");
                                Utils.OfflineRecords.Remove(record);
                                MergeRecord mergeRecord = JsonSerializer.Deserialize<MergeRecord>(record.json);

                                string updatedJson = JsonSerializer.Serialize(mergeRecord);
                                record.json = updatedJson;
                                await FileSystemService.MoveToUploaded(record);
                                await FileSystemService.Delete(record.filename);
                            }
                            else
                            {
                                Logger.Log("3", "UploadThread", " WorkRecord with GUID : " + record.id + " Upload failed!");
                            }
                        }
                        catch(Exception ex)
                        {
                            Logger.Log("3", "UploadThread", record.id +" could not upload." + ex.Message);
                        }

                    }
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        WeakReferenceMessenger.Default.Send(new HideSyncIconMessage("Hide Sync Icon"));
                    });


                    if (Utils.OfflineRecords.Count > 0) Thread.Sleep(120000);

                    // Sleep while there are no records
                    if (Utils.OfflineRecords.Count == 0)
                        Logger.Log("3", "UploadThread", "No record detected, Upload Thread going to sleep");
                    while (Utils.OfflineRecords.Count == 0) Thread.Sleep(1000);
                    Logger.Log("3", "UploadThread", "New record detected, Starting Upload Thread");
                }
                catch(Exception ex)
                {
                    Logger.Log("1", "Exception", ex.Message);
                }
            }
        }
       
    }
}
