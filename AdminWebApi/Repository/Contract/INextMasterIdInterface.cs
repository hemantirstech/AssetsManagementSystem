using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Repository.Contract
{
    public interface INextMasterIdInterface<SPNextMasterIdResult>
    {
         IEnumerable<SPNextMasterIdResult> GetNextMasterId(string TableName);      
    }
}
