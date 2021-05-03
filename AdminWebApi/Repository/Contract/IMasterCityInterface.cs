using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterCityInterface<MasterCityResult>
    {
        IEnumerable<MasterCityResult> GetAllADMasterCity();

        MasterCityResult GetADMasterCityByID(long MasterCityId);

        Task InsertADMasterCity(ADMasterCity objADMasterCity);
        Task UpdateADMasterCity(ADMasterCity objADMasterCity);
        Task DeleteADMasterCity(long MasterCityId);
        bool ADMasterCityExists(long id);
    }
}
