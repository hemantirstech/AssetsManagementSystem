using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterCompanyInterface<MasterCompanyResult>
    {
        IEnumerable<MasterCompanyResult> GetAllADMasterCompany();

        MasterCompanyResult GetADMasterCompanyByID(long MasterCompanyId);

        Task InsertADMasterCompany(ADMasterCompany objADMasterCompany);
        Task UpdateADMasterCompany(ADMasterCompany objADMasterCompany);
        Task DeleteADMasterCompany(long MasterCompanyId);
        bool ADMasterCompanyExists(long id);
    }
}
