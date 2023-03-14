using Custodian.ActivityLog;
using Custodian.Models;
using Custodian.Models.ServerModels;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Custodian.Services.Server
{
    public class ApiClientService : IApiClientService
    {

        private string _baseUrl = "https://eagleclean-be.azurewebsites.net";

        public async Task<T> GetAsync<T>(string endPoint)
        {
            try
            {
                string url = Path.Combine(_baseUrl, endPoint);
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            if (typeof(T) == typeof(string))
                                return (T)Convert.ChangeType(content, typeof(T));
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            T result = JsonSerializer.Deserialize<T>(content,options);
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);

            }
            return default(T);
        }

        public async Task<bool> PostAsync<T>(string endPoint, T obj)
        {
            bool status = false;
            try
            {
                string url = Path.Combine(_baseUrl, endPoint);
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, obj);
                    if (response.IsSuccessStatusCode)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("1", "Exception", ex.Message);
            }
            return status;
        }

        public Task<bool> PutAsync<T>(string endPoint, T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string endPoint)
        {
            throw new NotImplementedException();
        }
    }
}
