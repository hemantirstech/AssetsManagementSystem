using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterFinancialYearInterface<MasterFinancialYearResult>
    {
        IEnumerable<MasterFinancialYearResult> GetAllADMasterFinancialYear();

        MasterFinancialYearResult GetADMasterFinancialYearByID(long MasterFinancialYearId);

        Task InsertADMasterFinancialYear(ADMasterFinancialYear objADMasterFinancialYear);
        Task UpdateADMasterFinancialYear(ADMasterFinancialYear objADMasterFinancialYear);
        Task DeleteADMasterFinancialYear(long MasterFinancialYearId);
        bool ADMasterFinancialYearExists(long id);
    }
}
