using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterRegistrationInterface <MasterRegistrationResult>
    {
        IEnumerable<MasterRegistrationResult> GetAllADMasterRegistration();
        MasterRegistrationResult GetADMasterRegistrationByID(long MasterRegistrationId);
        Task InsertADMasterRegistration(ADMasterRegistration objADMasterRegistration);
        Task UpdateADMasterRegistration(ADMasterRegistration objADMasterRegistration);
        Task DeleteADMasterRegistration(long MasterRegistrationId);
        bool ADMasterRegistrationExists(long id);
    }
}
