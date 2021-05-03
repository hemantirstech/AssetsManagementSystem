using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterRegisteredDeviceInterface<MasterRegisteredDeviceResult>
    {
        IEnumerable<MasterRegisteredDeviceResult> GetAllADMasterRegisteredDevice();
        MasterRegisteredDeviceResult GetADMasterRegisteredDeviceByID(long MasterRegisteredDeviceId);
        Task InsertADMasterRegisteredDevice(ADMasterRegisteredDevice objADMasterRegisteredDevice);
        Task UpdateADMasterRegisteredDevice(ADMasterRegisteredDevice objADMasterRegisteredDevice);
        Task DeleteADMasterRegisteredDevice(long MasterRegisteredDeviceId);
        bool ADMasterRegisteredDeviceExists(long id);
    }
}
