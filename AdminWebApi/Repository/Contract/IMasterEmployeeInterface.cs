using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterEmployeeInterface<MasterEmployeeResult>
    {
        IEnumerable<MasterEmployeeResult> GetAllADMasterEmployee();

        MasterEmployeeResult GetADMasterEmployeeByID(long MasterEmployeeId);

        Task InsertADMasterEmployee(ADMasterEmployee objADMasterEmployee);
        Task UpdateADMasterEmployee(ADMasterEmployee objADMasterEmployee);
        Task DeleteADMasterEmployee(long MasterEmployeeId);
        bool ADMasterEmployeeExists(long id);
    }
}
