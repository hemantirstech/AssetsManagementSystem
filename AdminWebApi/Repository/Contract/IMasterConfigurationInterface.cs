using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterConfigurationInterface<MasterConfigurationResult>
    {
        IEnumerable<MasterConfigurationResult> GetAllADMasterConfiguration();

        MasterConfigurationResult GetADMasterConfigurationByID(long MasterConfigurationId);

        Task InsertADMasterConfiguration(ADMasterConfiguration objADMasterConfiguration);
        Task UpdateADMasterConfiguration(ADMasterConfiguration objADMasterConfiguration);
        Task DeleteADMasterConfiguration(long MasterConfigurationId);
        bool ADMasterConfigurationExists(long id);
    }
}
