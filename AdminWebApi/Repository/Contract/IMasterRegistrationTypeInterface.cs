using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
     public interface IMasterRegistrationTypeInterface <MasterRegistrationTypeResult>
    {
        IEnumerable<MasterRegistrationTypeResult> GetAllADMasterRegistrationType();
        MasterRegistrationTypeResult GetADMasterRegistrationTypeByID(long MasterRegistrationTypeId);
        Task InsertADMasterRegistrationType(ADMasterRegistrationType objADMasterRegistrationType);
        Task UpdateADMasterRegistrationType(ADMasterRegistrationType objADMasterRegistrationType);
        Task DeleteADMasterRegistrationType(long MasterRegistrationTypeId);
        bool ADMasterRegistrationTypeExists(long id);
    }
}
