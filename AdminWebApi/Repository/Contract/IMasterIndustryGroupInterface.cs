using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
   public interface IMasterIndustryGroupInterface<MasterIndustryGroupResult>
    {
        IEnumerable<MasterIndustryGroupResult> GetAllADMasterIndustryGroup();

        MasterIndustryGroupResult GetADMasterIndustryGroupByID(long MasterIndustryGroupId);

        Task InsertADMasterIndustryGroup(ADMasterIndustryGroup objADMasterIndustryGroup);
        Task UpdateADMasterIndustryGroup(ADMasterIndustryGroup objADMasterIndustryGroup);
        Task DeleteADMasterIndustryGroup(long MasterIndustryGroupId);
        bool ADMasterIndustryGroupExists(long id);
    }
}
