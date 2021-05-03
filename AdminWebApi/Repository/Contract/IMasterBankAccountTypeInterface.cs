using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterBankAccountTypeInterface<MasterBankAccountTypeResult>
    {
        IEnumerable<MasterBankAccountTypeResult> GetAllADMasterBankAccountType();

        MasterBankAccountTypeResult GetADMasterBankAccountTypeByID(long MasterBankAccountTypeId);

        Task InsertADMasterBankAccountType(ADMasterBankAccountType objADMasterBankAccountType);
        Task UpdateADMasterBankAccountType(ADMasterBankAccountType objADMasterBankAccountType);
        Task DeleteADMasterBankAccountType(long MasterBankAccountTypeId);
        bool ADMasterBankAccountTypeExists(long id);
    }
}
