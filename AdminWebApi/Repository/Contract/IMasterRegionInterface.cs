using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterRegionInterface <MasterRegionResult>
    {
        IEnumerable<MasterRegionResult> GetAllADMasterRegion();
        MasterRegionResult GetADMasterRegionByID(long MasterRegionId);
        Task InsertADMasterRegion(ADMasterRegion objADMasterRegion);
        Task UpdateADMasterRegion(ADMasterRegion objADMasterRegion);
        Task DeleteADMasterRegion(long MasterRegionId);
        bool ADMasterRegionExists(long id);
    }
}
