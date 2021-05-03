using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface IMasterProductChildInterface<MasterProductChildResult>
    {
        IEnumerable<MasterProductChildResult> GetAllASMasterProductChild();

        IEnumerable<MasterProductChildResult> GetExpiryASMasterProductChild();
        IEnumerable<MasterProductChildResult> GetExpiredASMasterProductChild();

        MasterProductChildResult GetASMasterProductChildByID(long MasterProductChildId);
        MasterProductChildResult GetSingleOrDefaultASMasterProductChild(long MasterProductId);

        Task InsertASMasterProductChild(ASMasterProductChild objADMasterProductChild);
        Task UpdateASMasterProductChild(ASMasterProductChild objADMasterProductChild);
        Task UpdateASMasterProductChildStatus(ASMasterProductChildStatus objASMasterProductChildStatus);
        Task DeleteASMasterProductChild(long MasterProductChildId);
        bool ASMasterProductChildExists(long MasterProductChildId);
    }
}
