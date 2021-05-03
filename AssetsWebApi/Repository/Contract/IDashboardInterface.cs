using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IDashboardInterface<ProductDetailResult>
    {
        IEnumerable<ProductDetailResult> GetAllASMasterCategory();
        IEnumerable<ProductDetailResult> GetAllMicrosoftSubCategory();
        IEnumerable<ProductDetailResult> GetAllASMasterSubCategory(long MasterSubCategoryId, long MasterBranchId);
        IEnumerable<ProductDetailResult> GetAllASMasterSubCategoryEmployee(long MasterEmployeeId);
    }
}
