using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterVendorInterface<MasterVendorResult>
    {
        IEnumerable<MasterVendorResult> GetAllADMasterVendor();

        MasterVendorResult GetADMasterVendorByID(long MasterVendorId);

        Task InsertADMasterVendor(ADMasterVendor objADMasterVendor);
        Task UpdateADMasterVendor(ADMasterVendor objADMasterVendor);
        Task DeleteADMasterVendor(long MasterVendorId);
        bool ADMasterVendorExists(long MasterVendorId);
    }
}
