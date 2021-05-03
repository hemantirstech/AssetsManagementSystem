using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterAddressTypeInterface<MasterAddressTypeResult> 
    {
        IEnumerable<MasterAddressTypeResult> GetAllADMasterAddressType();

        MasterAddressTypeResult GetADMasterAddressTypeByID(long MasterAddressTypeId);

        Task InsertADMasterAddressType(ADMasterAddressType objADMasterAddressType);
        Task UpdateADMasterAddressType(ADMasterAddressType objADMasterAddressType);
        Task DeleteADMasterAddressType(long MasterAddressTypeId);
        bool ADMasterAddressTypeExists(long id);
    }
}
