using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterStatusInterface<MasterStatusResult>
    {
        IEnumerable<MasterStatusResult> GetAllADMasterStatus();

        MasterStatusResult GetADMasterStatusByID(long MasterStatusId);

        Task InsertADMasterStatus(ADMasterStatus objADMasterStatus);
        Task UpdateADMasterStatus(ADMasterStatus objADMasterStatus);
        Task DeleteADMasterStatus(long MasterStatusId);
        bool ADMasterStatusExists(long id);
    }
}
