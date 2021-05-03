using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
   public interface IMasterProductSizeInterface<MasterProductSizeResult>
    {
        IEnumerable<MasterProductSizeResult> GetAllASMasterProductSize();

        MasterProductSizeResult GetASMasterProductSizeByID(long MasterProductSizeId);

        Task InsertASMasterProductSize(ASMasterProductSize objADMasterProductSize);
        Task UpdateASMasterProductSize(ASMasterProductSize objADMasterProductSize);
        Task DeleteASMasterProductSize(long MasterProductSizeId);
        bool ASMasterProductSizeExists(long MasterProductSizeId);
    }
}
