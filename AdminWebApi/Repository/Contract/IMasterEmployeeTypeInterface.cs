using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterEmployeeTypeInterface<MasterEmployeeTypeResult>
    {
        IEnumerable<MasterEmployeeTypeResult> GetAllADMasterEmployeeType();

        MasterEmployeeTypeResult GetADMasterEmployeeTypeByID(long MasterEmployeeTypeId);

        Task InsertADMasterEmployeeType(ADMasterEmployeeType objADMasterEmployeeType);
        Task UpdateADMasterEmployeeType(ADMasterEmployeeType objADMasterEmployeeType);
        Task DeleteADMasterEmployeeType(long MasterEmployeeTypeId);
        bool ADMasterEmployeeTypeExists(long id);
    }
}
