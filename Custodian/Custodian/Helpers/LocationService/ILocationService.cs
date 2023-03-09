using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Helpers.LocationService
{
    public interface ILocationService
    {
        Task<Location> GetLastKnownLocation();

        Task<Location> GetCurrentLocation();

    }
}
