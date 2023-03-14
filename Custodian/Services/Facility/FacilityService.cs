using Custodian.Services.Server;
using Custodian.Models;

namespace Custodian.Services.Facility
{
    public class FacilityService : IFacilityService
    {
        public IApiClientService _apiService;
        public FacilityService(IApiClientService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Models.Facility>> GetAllFacilities(double Lat, double Long, int radius)
        {
            var facilities = await _apiService.GetAsync<List<Models.Facility>>("facilities/" + Lat+"/"+Long+"/"+radius);
            return facilities ?? new List<Models.Facility>();
        }
    }
}
