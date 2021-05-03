using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
   public interface IMasterProductTypeInterface<MasterProductTypeResult>
    {
        IEnumerable<MasterProductTypeResult> GetAllASMasterProductType();

        MasterProductTypeResult GetASMasterProductTypeByID(long MasterProductTypeId);

        Task InsertASMasterProductType(ASMasterProductType objADMasterProductType);
        Task UpdateASMasterProductType(ASMasterProductType objADMasterProductType);
        Task DeleteASMasterProductType(long MasterProductTypeId);
        bool ASMasterProductTypeExists(long MasterProductTypeId);
    }
}
