using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterProfileInterface<MasterProfileResult>
    {
        IEnumerable<MasterProfileResult> GetAllADMasterProfile();

        MasterProfileResult GetADMasterProfileByID(long MasterProfileId);

        Task InsertADMasterProfile(ADMasterProfile objADMasterProfile);
        Task UpdateADMasterProfile(ADMasterProfile objADMasterProfile);
        Task DeleteADMasterProfile(long MasterProfileId);
        bool ADMasterProfileExists(long id);
    }
}
