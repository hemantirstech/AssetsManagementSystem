using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterBusinessVerticleInterface<MasterBusinessVerticleResult>
    {
        IEnumerable<MasterBusinessVerticleResult> GetAllADMasterBusinessVerticle();

        MasterBusinessVerticleResult GetADMasterBusinessVerticleByID(long MasterBusinessVerticleId);

        Task InsertADMasterBusinessVerticle(ADMasterBusinessVerticle objADMasterBusinessVerticle);
        Task UpdateADMasterBusinessVerticle(ADMasterBusinessVerticle objADMasterBusinessVerticle);
        Task DeleteADMasterBusinessVerticle(long MasterBusinessVerticleId);
        bool ADMasterBusinessVerticleExists(long id);
    }
}
