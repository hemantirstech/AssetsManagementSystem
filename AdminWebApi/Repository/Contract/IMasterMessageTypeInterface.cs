using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
   public interface IMasterMessageTypeInterface<MasterMessageTypeResult>
    {
        IEnumerable<MasterMessageTypeResult> GetAllADMasterMessageType();
        MasterMessageTypeResult GetADMasterMessageTypeByID(long MasterMessageTypeId);
        Task InsertADMasterMessageType(ADMasterMessageType objADMasterMessageType);
        Task UpdateADMasterMessageType(ADMasterMessageType objADMasterMessageType);
        Task DeleteADMasterMessageType(long MasterMessageTypeId);
        bool ADMasterMessageTypeExists(long id);
    }
}
