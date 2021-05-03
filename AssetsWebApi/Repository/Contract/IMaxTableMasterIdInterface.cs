using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMaxTableMasterIdInterface<SPMaxTableMasterIdResult>
    {
         IEnumerable<SPMaxTableMasterIdResult> GetMaxTableMasterId(string TableName);      
    }
}
