using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterLoginTypeInterface<MasterLoginTypeResult>
    {
        IEnumerable<MasterLoginTypeResult> GetAllADMasterLoginType();
        MasterLoginTypeResult GetADMasterLoginTypeByID(long MasterLoginTypeId);
        Task InsertADMasterLoginType(ADMasterLoginType objADMasterLoginType);
        Task UpdateADMasterLoginType(ADMasterLoginType objADMasterLoginType);
        Task DeleteADMasterLoginType(long MasterLoginTypeId);
        bool ADMasterLoginTypeExists(long id);
    }
}
