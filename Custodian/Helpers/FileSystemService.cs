﻿using Custodian.ActivityLog;
using Custodian.Models.ServerModels;
using PCLStorage;
using System.IO.Enumeration;
using System.Text;
using FileSystem = PCLStorage.FileSystem;

namespace Custodian.Helpers
{
    internal class FileSystemService
    {
        internal static async Task<string> Write(string record)
        {
            try
            {
                DateTime now = DateTime.Now;


                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder toUploadFolder = await rootFolder.CreateFolderAsync("Custodian/Data/ToUpload", CreationCollisionOption.OpenIfExists);
                string fileName;
                if (!string.IsNullOrEmpty(Utils.activeRouteFileName))
                {
                     fileName = Utils.activeRouteFileName;
                }
                else
                {
                     Guid guidID = Guid.NewGuid();
                     Utils.currentGuid = guidID;
                     fileName = guidID.ToString() + "_" + now.ToString("yyyyMMdd") + ".json";
                }
                IFile toUploadedfile = await toUploadFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                using (var fs = await toUploadedfile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
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
                Logger.Log("1", "Exception", ex.Message);
            }
            return string.Empty;
        }
        internal static async Task<string> Read(string fileName)
        {
            try
            {
                IFile file = await FileSystem.Current.LocalStorage.GetFileAsync("/storage/emulated/0/Custodian/Data/completed-routes.json");


                using (var stream = await file.OpenAsync(PCLStorage.FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var FileText = await reader.ReadLineAsync();
                    return FileText;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1","Exception", ex.Message);
                return string.Empty;
            }
        }

        internal static async Task Delete(string fileName)
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder routeFolder = await rootFolder.CreateFolderAsync("Custodian/Data/ToUpload", CreationCollisionOption.OpenIfExists);
                IFile file = await routeFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await file.DeleteAsync();
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
        internal static async Task Update(WorkRecord record)
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder routeFolder = await rootFolder.CreateFolderAsync("Custodian/Data/ToUpload", CreationCollisionOption.OpenIfExists);
                IFile file = await routeFolder.CreateFileAsync(record.filename, CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        await writer.WriteLineAsync(record.json);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
        internal static async Task MoveToUploaded(WorkRecord record)
        {
            try
            {
                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Utils.ROOT_PATH);
                IFolder routeFolder = await rootFolder.CreateFolderAsync("Custodian/Data/Uploaded", CreationCollisionOption.OpenIfExists);
                IFile file = await routeFolder.CreateFileAsync(record.filename, CreationCollisionOption.OpenIfExists);
                using (var fs = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        await writer.WriteLineAsync(record.json);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
        }
    }
}
