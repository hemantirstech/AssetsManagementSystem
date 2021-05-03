using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterTypeOfDevice: IMasterTypeOfDeviceInterface<MasterTypeOfDeviceResult>
    {
        readonly AdminContext _Context;
        public MasterTypeOfDevice(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterTypeOfDeviceResult> GetAllADMasterTypeOfDevice()
        {
            try
            {
                var _data = _Context.ADMasterTypeOfDevices.ToList();

                List<MasterTypeOfDeviceResult> objMasterTypeOfDeviceList = new List<MasterTypeOfDeviceResult>();
                foreach (var _Item in _data)
                {
                    MasterTypeOfDeviceResult _objMasterTypeOfDeviceResult = new MasterTypeOfDeviceResult();

                    _objMasterTypeOfDeviceResult.MasterTypeOfDeviceId = _Item.MasterTypeOfDeviceId;
                    _objMasterTypeOfDeviceResult.TypeOfDeviceTitle = _Item.TypeOfDeviceTitle;
                    _objMasterTypeOfDeviceResult.TypeOfDeviceName = _Item.TypeOfDeviceName;


                    _objMasterTypeOfDeviceResult.IsActive = _Item.IsActive;
                    _objMasterTypeOfDeviceResult.ActiveColor = "green";
                    _objMasterTypeOfDeviceResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTypeOfDeviceResult.IsActive == false)
                    {
                        _objMasterTypeOfDeviceResult.ActiveColor = "red";
                        _objMasterTypeOfDeviceResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterTypeOfDeviceList.Add(_objMasterTypeOfDeviceResult);
                }

                return objMasterTypeOfDeviceList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterTypeOfDeviceResult GetADMasterTypeOfDeviceByID(long MasterTypeOfDeviceId)
        {
            try
            {
                var _Item = _Context.ADMasterTypeOfDevices.Find(MasterTypeOfDeviceId);

                MasterTypeOfDeviceResult _objMasterTypeOfDeviceResult = new MasterTypeOfDeviceResult();

                if (_Item != null)
                {
                    _objMasterTypeOfDeviceResult.MasterTypeOfDeviceId = _Item.MasterTypeOfDeviceId;
                    _objMasterTypeOfDeviceResult.TypeOfDeviceTitle = _Item.TypeOfDeviceTitle;
                    _objMasterTypeOfDeviceResult.TypeOfDeviceName = _Item.TypeOfDeviceName;
                   

                    _objMasterTypeOfDeviceResult.IsActive = _Item.IsActive;
                    _objMasterTypeOfDeviceResult.ActiveColor = "green";
                    _objMasterTypeOfDeviceResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTypeOfDeviceResult.IsActive == false)
                    {
                        _objMasterTypeOfDeviceResult.ActiveColor = "red";
                        _objMasterTypeOfDeviceResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterTypeOfDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterTypeOfDevice(ADMasterTypeOfDevice objADMasterTypeOfDevice)
        {
            try
            {
                _Context.ADMasterTypeOfDevices.Add(objADMasterTypeOfDevice);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterTypeOfDevice(ADMasterTypeOfDevice objADMasterTypeOfDevice)
        {
            try
            {
                _Context.Entry(objADMasterTypeOfDevice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterTypeOfDevice(long MasterTypeOfDeviceId)
        {
            try
            {
                ADMasterTypeOfDevice objADMasterTypeOfDevice = _Context.ADMasterTypeOfDevices.Find(MasterTypeOfDeviceId);
                _Context.ADMasterTypeOfDevices.Remove(objADMasterTypeOfDevice);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterTypeOfDeviceExists(long MasterTypeOfDeviceId)
        {
            try
            {
                return _Context.ADMasterTypeOfDevices.Any(e => e.MasterTypeOfDeviceId == MasterTypeOfDeviceId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
