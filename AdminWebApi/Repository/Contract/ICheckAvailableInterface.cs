using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Repository.Contract
{
    public interface ICheckAvailableInterface<SPCheckAvailableResult>
    {
         IEnumerable<SPCheckAvailableResult> GetCheckAvailable(string TableName, string NameAvailable, long NameId);      
    }
}
