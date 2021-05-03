using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMasterProductInterface<MasterProductResult>
    {
        IEnumerable<MasterProductResult> GetAllASMasterProduct();

        MasterProductResult GetASMasterProductByID(long MasterProductId);

        IEnumerable<MasterProductResult> GetASMasterProductByID(long MasterCategoryId, long MasterSubCategoryId, long MasterBrandId);

        Task InsertASMasterProduct(ASMasterProductMerge objASMasterProductMerge);
        Task UpdateASMasterProduct(ASMasterProduct objADMasterProduct);
        Task DeleteASMasterProduct(long MasterProductId);
        bool ASMasterProductExists(long MasterProductId);
    }
}
