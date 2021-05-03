using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterFunctionInterface<MasterFunctionResult>
    {
        IEnumerable<MasterFunctionResult> GetAllADMasterFunction();

        MasterFunctionResult GetADMasterFunctionByID(long MasterFunctionId);

        Task InsertADMasterFunction(ADMasterFunction objADMasterFunction);
        Task UpdateADMasterFunction(ADMasterFunction objADMasterFunction);
        Task DeleteADMasterFunction(long MasterFunctionId);
        bool ADMasterFunctionExists(long id);
    }
}
