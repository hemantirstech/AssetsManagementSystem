using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterTypeOfDeviceInterface<MasterTypeOfDeviceResult>
    {
        IEnumerable<MasterTypeOfDeviceResult> GetAllADMasterTypeOfDevice();

        MasterTypeOfDeviceResult GetADMasterTypeOfDeviceByID(long MasterTypeOfDeviceId);

        Task InsertADMasterTypeOfDevice(ADMasterTypeOfDevice objADMasterTypeOfDevice);
        Task UpdateADMasterTypeOfDevice(ADMasterTypeOfDevice objADMasterTypeOfDevice);
        Task DeleteADMasterTypeOfDevice(long MasterTimeZoneId);
        bool ADMasterTypeOfDeviceExists(long id);
    }
}
