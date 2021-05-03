using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterColorInterface<MasterColorResult>
    {
        IEnumerable<MasterColorResult> GetAllADMasterColor();

        MasterColorResult GetADMasterColorByID(long MasterColorId);

        Task InsertADMasterColor(ADMasterColor objADMasterColor);
        Task UpdateADMasterColor(ADMasterColor objADMasterColor);
        Task DeleteADMasterColor(long MasterColorId);
        bool ADMasterColorExists(long id);
    }
}
