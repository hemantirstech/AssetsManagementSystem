using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Repository.Contract
{
    public interface IValidateAccountInterface<SPValidateAccountResult>
    {
         IEnumerable<SPValidateAccountResult> GetValidateAccount(string UserName, string Password, string VerificationCode, string MacId, string SessionId, string Mode, int ModeVersion, string SearchCondition);      
    }
}
