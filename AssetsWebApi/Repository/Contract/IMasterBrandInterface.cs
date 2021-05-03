using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
  
    public interface IMasterBrandInterface<MasterBrandResult>
    {
        IEnumerable<MasterBrandResult> GetAllASMasterBrand();

        MasterBrandResult GetASMasterBrandByID(long MasterBrandId);

        Task InsertASMasterBrand(ASMasterBrand objASMasterBrand);
        Task UpdateASMasterBrand(ASMasterBrand objASMasterBrand);
        Task DeleteASMasterBrand(long MasterBrandId);
        bool ASMasterBrandExists(long MasterBrandId);
    }
}
