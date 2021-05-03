using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMasterCategoryInterface<MasterCategoryResult>
    {
        IEnumerable<MasterCategoryResult> GetAllASMasterCategory();

        MasterCategoryResult GetASMasterCategoryByID(long MasterCategoryId);

        Task InsertASMasterCategory(ASMasterCategory objADMasterCategory);
        Task UpdateASMasterCategory(ASMasterCategory objADMasterCategory);
        Task DeleteASMasterCategory(long MasterCategoryId);
        bool ASMasterCategoryExists(long MasterCategoryId);
    }
}
