using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterIndustryTypeInterface<MasterIndustryTypeResult>
    {
        IEnumerable<MasterIndustryTypeResult> GetAllADMasterIndustryType();

        MasterIndustryTypeResult GetADMasterIndustryTypeByID(long ADMasterIndustryTypeId);

        Task InsertADMasterIndustryType(ADMasterIndustryType objADMasterIndustryType);
        Task UpdateADMasterIndustryType(ADMasterIndustryType objADMasterIndustryType);
        Task DeleteADMasterIndustryType(long MasterIndustryTypeId);
        bool ADMasterIndustryTypeExists(long id);
    }
}
