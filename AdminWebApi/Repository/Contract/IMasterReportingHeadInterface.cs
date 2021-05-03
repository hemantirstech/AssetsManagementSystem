using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterReportingHeadInterface <MasterReportingHeadResult>
    {
        IEnumerable<MasterReportingHeadResult> GetAllADMasterReportingHead();
        MasterReportingHeadResult GetADMasterReportingHeadByID(long MasterReportingHeadId);
        Task InsertADMasterReportingHead(ADMasterReportingHead objADMasterReportingHead);
        Task UpdateADMasterReportingHead(ADMasterReportingHead objADMasterReportingHead);
        Task DeleteADMasterReportingHead(long MasterReportingHeadId);
        bool ADMasterReportingHeadExists(long id);
    }
}
