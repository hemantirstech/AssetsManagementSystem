using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterCurrencyInterface<MasterCurrencyResult>
    {
        IEnumerable<MasterCurrencyResult> GetAllADMasterCurrency();

        MasterCurrencyResult GetADMasterCurrencyByID(long MasterCurrencyId);

        Task InsertADMasterCurrency(ADMasterCurrency objADMasterCurrency);
        Task UpdateADMasterCurrency(ADMasterCurrency objADMasterCurrency);
        Task DeleteADMasterCurrency(long MasterCurrencyId);
        bool ADMasterCurrencyExists(long id);
    }
}
