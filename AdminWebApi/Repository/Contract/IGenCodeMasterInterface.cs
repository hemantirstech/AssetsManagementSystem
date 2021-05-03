using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IGenCodeMasterInterface<GenCodeMasterResult>
    {
         IEnumerable<GenCodeMasterResult> GetAllADGenCodeMaster();
         GenCodeMasterResult GetADGenCodeMasterByID(long GenCodeMasterId);
         Task InsertADGenCodeMaster(ADGenCodeMaster objADGenCodeMaster);
         Task UpdateADGenCodeMaster(ADGenCodeMaster objADGenCodeMaster);
         Task DeleteADGenCodeMaster(long GenCodeMasterId);
         bool ADGenCodeMasterExists(long id);
      
    }
}
