using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Services.Facility
{
    public interface IFacilityService
    {
        Task<List<Models.Facility>> GetAllFacilities(double Lat, double Long, int radius);
    }
}
