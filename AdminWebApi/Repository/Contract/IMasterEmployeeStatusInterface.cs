using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterEmployeeStatusInterface<MasterEmployeeStatusResult>
    {
        IEnumerable<MasterEmployeeStatusResult> GetAllADMasterEmployeeStatus();

        MasterEmployeeStatusResult GetADMasterEmployeeStatusByID(long MasterEmployeeStatusId);

        Task InsertADMasterEmployeeStatus(ADMasterEmployeeStatus objADMasterEmployeeStatus);
        Task UpdateADMasterEmployeeStatus(ADMasterEmployeeStatus objADMasterEmployeeStatus);
        Task DeleteADMasterEmployeeStatus(long MasterEmployeeStatusId);
        bool ADMasterEmployeeStatusExists(long id);
    }
}
