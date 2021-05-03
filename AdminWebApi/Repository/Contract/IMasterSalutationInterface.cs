using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterSalutationInterface<MasterSalutationResult>
    {
        IEnumerable<MasterSalutationResult> GetAllADMasterSalutation();

        MasterSalutationResult GetADMasterSalutationByID(long MasterSalutationId);

        Task InsertADMasterSalutation(ADMasterSalutation objADMasterSalutation);
        Task UpdateADMasterSalutation(ADMasterSalutation objADMasterSalutation);
        Task DeleteADMasterSalutation(long MasterSalutationId);
        bool ADMasterSalutationExists(long MasterSalutationId);
    }
}
