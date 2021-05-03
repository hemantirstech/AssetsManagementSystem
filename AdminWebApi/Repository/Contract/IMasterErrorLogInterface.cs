using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
   public interface IMasterErrorLogInterface<MasterErrorLogResult>
    {
        IEnumerable<MasterErrorLogResult> GetAllADMasterErrorLog();
        MasterErrorLogResult GetADMasterErrorLogByID(long MasterErrorLogId);
        Task InsertADMasterErrorLog(ADMasterErrorLog objADMasterErrorLog);
        Task UpdateADMasterErrorLog(ADMasterErrorLog objADMasterErrorLog);
        Task DeleteADMasterErrorLog(long MasterErrorLogId);
        bool ADMasterErrorLogExists(long id);
    }
}
