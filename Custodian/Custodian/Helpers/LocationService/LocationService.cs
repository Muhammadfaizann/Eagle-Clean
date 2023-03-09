using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Custodian.ActivityLog;

namespace Custodian.Helpers.LocationService
{
    public class LocationService : ILocationService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        public async Task<string> GetCachedLocation()
        {
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                    return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }

            return "None";
        }


        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

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
            return default(Location);
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
