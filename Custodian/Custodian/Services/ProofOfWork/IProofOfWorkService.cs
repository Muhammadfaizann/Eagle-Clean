using Custodian.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Services.ProofOfWork
{
    public interface IProofOfWorkService
    {
        Task<bool> SendWorkRecord(WorkRecord record);
    }
}
