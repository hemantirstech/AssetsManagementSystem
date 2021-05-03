using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
   public interface IMasterDepartmentInterface<MasterDepartmentResult>
    {
        IEnumerable<MasterDepartmentResult> GetAllADMasterDepartment();

        MasterDepartmentResult GetADMasterDepartmentByID(long MasterDepartmentId);

        Task InsertADMasterDepartment(ADMasterDepartment objADMasterDepartment);
        Task UpdateADMasterDepartment(ADMasterDepartment objADMasterDepartment);
        Task DeleteADMasterDepartment(long MasterDepartmentId);
        bool ADMasterDepartmentExists(long MasterDepartmentId);
    }
}
