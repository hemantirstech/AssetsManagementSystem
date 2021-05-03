using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMasterAssetsAssignmentInterface<MasterAssetsAssignmentResult>
    {
        IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignment();

        MasterAssetsAssignmentResult GetASMasterAssetsAssignmentByID(long MasterEmployeeId);
        IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignmentNotAssign(long MasterCategoryId, long MasterSubCategoryId, long MasterBranchId);

        IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignment(long MasterEmployeeId);
        Task InsertASMasterAssetsAssignment(ASMasterAssetsAssignment objADMasterAssetsAssignment);
        Task UpdateASMasterAssetsAssignment(ASMasterAssetsAssignment objADMasterAssetsAssignment);
        Task DeleteASMasterAssetsAssignment(long MasterAssetsAssignmentId);
        bool ASMasterAssetsAssignmentExists(long MasterAssetsAssignmentId);
    }
}
