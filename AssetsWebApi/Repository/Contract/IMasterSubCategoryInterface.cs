using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMasterSubCategoryInterface<MasterSubCategoryResult>
    {
        IEnumerable<MasterSubCategoryResult> GetAllASMasterSubCategory();

        MasterSubCategoryResult GetASMasterSubCategoryByID(long MasterSubCategoryId);
        
        Task InsertASMasterSubCategory(ASMasterSubCategory objADMasterSubCategory);
        Task UpdateASMasterSubCategory(ASMasterSubCategory objADMasterSubCategory);
        Task DeleteASMasterSubCategory(long MasterSubCategoryId);
        bool ASMasterSubCategoryExists(long MasterSubCategoryId);

    }
}
