using Custodian.Models.ServerModels;
using Custodian.Services.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Services.ProofOfWork
{

    public class ProofOfWorkSerive : IProofOfWorkService
    {
        public IApiClientService _apiService;
        public ProofOfWorkSerive(IApiClientService apiService)
        {
            _apiService = apiService;
        }
        public async void SendWorkRecord(WorkRecord record)
        {
            bool response = await _apiService.PostAsync<WorkRecord>("workrecord/merge",record);
        }
    }
}
