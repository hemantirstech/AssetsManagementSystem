using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterCompanyTypeInterface<MasterCompanyTypeResult>
    {
        IEnumerable<MasterCompanyTypeResult> GetAllADMasterCompanyType();

        MasterCompanyTypeResult GetADMasterCompanyTypeByID(long MasterCompanyTypeId);

        Task InsertADMasterCompanyType(ADMasterCompanyType objADMasterCompanyType);
        Task UpdateADMasterCompanyType(ADMasterCompanyType objADMasterCompanyType);
        Task DeleteADMasterCompanyType(long MasterCompanyTypeId);
        bool ADMasterCompanyTypeExists(long id);
    }
}
