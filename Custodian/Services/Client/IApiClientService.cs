using Custodian.Models;
using Custodian.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Services.Server
{
    public interface IApiClientService
    {
        public Task<T> GetAsync<T>(string endPoint);
        public Task<bool> PostAsync<T>(string endPoint, T obj);
        public Task<bool> PutAsync<T>(string endPoint, T obj);
        public Task<bool> DeleteAsync(string endPoint);

    }
}
