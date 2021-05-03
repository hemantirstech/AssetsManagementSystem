using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterCountryInterface<MasterCountryResult>
    {
        IEnumerable<MasterCountryResult> GetAllADMasterCountry();

        MasterCountryResult GetADMasterCountryByID(long MasterCountryId);

        Task InsertADMasterCountry(ADMasterCountry objADMasterCountry);
        Task UpdateADMasterCountry(ADMasterCountry objADMasterCountry);
        Task DeleteADMasterCountry(long MasterCountryId);
        bool ADMasterCountryExists(long id);
    }
}
