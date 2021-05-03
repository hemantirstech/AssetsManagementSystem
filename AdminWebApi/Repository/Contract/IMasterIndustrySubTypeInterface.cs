using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterIndustrySubTypeInterface<MasterIndustrySubTypeResult>
    {
        IEnumerable<MasterIndustrySubTypeResult> GetAllADMasterIndustrySubType();

        MasterIndustrySubTypeResult GetADMasterIndustrySubTypeByID(long MasterIndustrySubTypeId);

        Task InsertADMasterIndustrySubType(ADMasterIndustrySubType objADMasterIndustrySubType);
        Task UpdateADMasterIndustrySubType(ADMasterIndustrySubType objADMasterIndustrySubType);
        Task DeleteADMasterIndustrySubType(long MasterIndustrySubTypeId);
        bool ADMasterIndustrySubTypeExists(long id);
    }
}
