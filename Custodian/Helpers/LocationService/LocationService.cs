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

        //it gets Last known location 
        public async Task<Location> GetLastKnownLocation()
        {
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                return location;
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }

            return default(Location);
        }

        //it gets current location from GPS!
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
