using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterBranchInterface<MasterBranchResult>
    {
        IEnumerable<MasterBranchResult> GetAllADMasterBranch();

        MasterBranchResult GetADMasterBranchByID(long MasterBranchId);

        Task InsertADMasterBranch(ADMasterBranch objADMasterBranch);
        Task UpdateADMasterBranch(ADMasterBranch objADMasterBranch);
        Task DeleteADMasterBranch(long MasterBranchId);
        bool ADMasterBranchExists(long id);
    }
}
