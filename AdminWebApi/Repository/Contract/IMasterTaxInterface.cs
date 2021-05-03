using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;


namespace AdminWebApi.Repository.Contract
{
   public interface IMasterTaxInterface<MasterTaxResult>
    {
        IEnumerable<MasterTaxResult> GetAllADMasterTax();

        MasterTaxResult GetADMasterTaxByID(long MasterTaxId);

        Task InsertADMasterTax(ADMasterTax objADMasterTax);
        Task UpdateADMasterTax(ADMasterTax objADMasterTax);
        Task DeleteADMasterTax(long MasterTaxId);
        bool ADMasterTaxExists(long id);

    }
}
