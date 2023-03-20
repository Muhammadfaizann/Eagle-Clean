using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Custodian.ActivityLog;
using Custodian.Models;
using System.Text.Json;
using Android.Hardware.Camera2;

namespace Custodian.Helpers.LocationService
{
    public class LocationService : ILocationService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        static string root = Utils.ROOT_PATH;
        static string mainFolder = "Custodian";
        static string locationFolder = "Location";
        static object Monitor = new object();

        static string filename = string.Empty;

        public LocationService()
        {
            try
            {

                string dirCustodian = Path.Combine(root, mainFolder);
                if (!Directory.Exists(dirCustodian))
                    Directory.CreateDirectory(dirCustodian);
                string dirLocation = Path.Combine(dirCustodian, locationFolder);
                if (!Directory.Exists(dirLocation))
                    Directory.CreateDirectory(dirLocation);


                string filePath = Path.Combine(root, mainFolder, locationFolder, "User_Location.json");
                if (!File.Exists(filePath)) { File.Create(filePath); }

                filename = filePath;
            }
            catch (Exception ex)
            {

            }
        }
      

        //it gets current location GPS!
        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                StoreLocation(location);

                return location;
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
            finally
            {
                _isCheckingLocation = false;
            }
            return RestorePreviousLocation();
        }

        public static void StoreLocation(Location location)
        {
            lock (Monitor)
            {
                try
                {
                    
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        string locationString = JsonSerializer.Serialize(location);
                        sw.WriteLine(locationString);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("1", "Exception", ex.Message);
                }
            }
        }

        public static Location RestorePreviousLocation()
        {
            try
            {

                string locationString = File.ReadAllText(filename);
                
                Location location = JsonSerializer.Deserialize<Location>(locationString);

                return location;
               
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
                return null;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
