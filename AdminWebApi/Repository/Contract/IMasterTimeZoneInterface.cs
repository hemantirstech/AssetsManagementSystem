using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterTimeZoneInterface<MasterTimeZoneResult>
    {
        IEnumerable<MasterTimeZoneResult> GetAllADMasterTimeZone();

        MasterTimeZoneResult GetADMasterTimeZoneByID(long MasterTimeZoneId);

        Task InsertADMasterTimeZone(ADMasterTimeZone objADMasterTimeZone);
        Task UpdateADMasterTimeZone(ADMasterTimeZone objADMasterTimeZone);
        Task DeleteADMasterTimeZone(long MasterTimeZoneId);
        bool ADMasterTimeZoneExists(long id);
    }
}
