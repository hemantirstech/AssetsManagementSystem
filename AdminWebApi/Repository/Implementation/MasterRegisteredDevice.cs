using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterRegisteredDevice : IMasterRegisteredDeviceInterface<MasterRegisteredDeviceResult>
    {
        readonly AdminContext _Context;

        public MasterRegisteredDevice(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterRegisteredDeviceResult> GetAllADMasterRegisteredDevice()
        {
            try
            {
                var _data = _Context.ADMasterRegisteredDevices.ToList();

                List<MasterRegisteredDeviceResult> objMasterRegisteredDeviceResultList = new List<MasterRegisteredDeviceResult>();
                foreach (var _Item in _data)
                {
                    MasterRegisteredDeviceResult _objMasterRegisteredDeviceResult = new MasterRegisteredDeviceResult();

                    _objMasterRegisteredDeviceResult.MasterRegisteredDeviceId = _Item.MasterRegisteredDeviceId;
                    _objMasterRegisteredDeviceResult.MasterLoginId = _Item.MasterLoginId;
                    _objMasterRegisteredDeviceResult.MacId = _Item.MacId;
                    _objMasterRegisteredDeviceResult.MasterTypeOfDeviceId = _Item.MasterTypeOfDeviceId;
                    _objMasterRegisteredDeviceResult.DeviceVerificationCode = _Item.DeviceVerificationCode;
                    _objMasterRegisteredDeviceResult.IsDeviceVerified = _Item.IsDeviceVerified;

                    _objMasterRegisteredDeviceResult.IsActive = _Item.IsActive;
                    _objMasterRegisteredDeviceResult.ActiveColor = "green";
                    _objMasterRegisteredDeviceResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegisteredDeviceResult.IsActive == false)
                    {
                        _objMasterRegisteredDeviceResult.ActiveColor = "red";
                        _objMasterRegisteredDeviceResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterRegisteredDeviceResultList.Add(_objMasterRegisteredDeviceResult);
                }

                return objMasterRegisteredDeviceResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterRegisteredDeviceResult GetADMasterRegisteredDeviceByID(long MasterRegisteredDeviceId)
        {
            try
            {
                var _Item = _Context.ADMasterRegisteredDevices.Find(MasterRegisteredDeviceId);

                MasterRegisteredDeviceResult _objMasterRegisteredDeviceResult = new MasterRegisteredDeviceResult();

                if (_Item != null)
                {
                    _objMasterRegisteredDeviceResult.MasterRegisteredDeviceId = _Item.MasterRegisteredDeviceId;
                    _objMasterRegisteredDeviceResult.MasterLoginId = _Item.MasterLoginId;
                    _objMasterRegisteredDeviceResult.MacId = _Item.MacId;
                    _objMasterRegisteredDeviceResult.MasterTypeOfDeviceId = _Item.MasterTypeOfDeviceId;
                    _objMasterRegisteredDeviceResult.DeviceVerificationCode = _Item.DeviceVerificationCode;
                    _objMasterRegisteredDeviceResult.IsDeviceVerified = _Item.IsDeviceVerified;

                    _objMasterRegisteredDeviceResult.IsActive = _Item.IsActive;
                    _objMasterRegisteredDeviceResult.ActiveColor = "green";
                    _objMasterRegisteredDeviceResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegisteredDeviceResult.IsActive == false)
                    {
                        _objMasterRegisteredDeviceResult.ActiveColor = "red";
                        _objMasterRegisteredDeviceResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterRegisteredDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterRegisteredDevice(ADMasterRegisteredDevice objADMasterRegisteredDevice)
        {
            try
            {
                _Context.ADMasterRegisteredDevices.Add(objADMasterRegisteredDevice);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterRegisteredDevice(ADMasterRegisteredDevice objADMasterRegisteredDevice)
        {
            try
            {
                _Context.Entry(objADMasterRegisteredDevice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterRegisteredDevice(long MasterRegisteredDeviceId)
        {
            try
            {
                ADMasterRegisteredDevice objADMasterRegisteredDevice = _Context.ADMasterRegisteredDevices.Find(MasterRegisteredDeviceId);
                _Context.ADMasterRegisteredDevices.Remove(objADMasterRegisteredDevice);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterRegisteredDeviceExists(long MasterRegisteredDeviceId)
        {
            try
            {
                return _Context.ADMasterRegisteredDevices.Any(e => e.MasterRegisteredDeviceId == MasterRegisteredDeviceId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
