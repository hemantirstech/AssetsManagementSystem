using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterMailTemplateInterface<MasterMailTemplateResult>
    {
        IEnumerable<MasterMailTemplateResult> GetAllADMasterMailTemplate();
        MasterMailTemplateResult GetADMasterMailTemplateByID(long MasterMailTemplateId);
        Task InsertADMasterMailTemplate(ADMasterMailTemplate objADMasterMailTemplate);
        Task UpdateADMasterMailTemplate(ADMasterMailTemplate objADMasterMailTemplate);
        Task DeleteADMasterMailTemplate(long MasterMailTemplateId);
        bool ADMasterMailTemplateExists(long id);
    }
}
