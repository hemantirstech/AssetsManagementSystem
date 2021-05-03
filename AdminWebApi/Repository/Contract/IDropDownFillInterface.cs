using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Repository.Contract
{
    public interface IDropDownFillInterface<SPDropDownFillResult>
    {
         IEnumerable<SPDropDownFillResult> GetDropDownFill(string TableName, long MasterId, string Type);      
    }
}
