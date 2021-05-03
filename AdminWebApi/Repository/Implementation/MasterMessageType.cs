using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterMessageType : IMasterMessageTypeInterface<MasterMessageTypeResult>
    {
        readonly AdminContext _Context;

        public MasterMessageType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterMessageTypeResult> GetAllADMasterMessageType()
        {
            try
            {
                var _data = _Context.ADMasterMessageTypes.ToList();

                List<MasterMessageTypeResult> objMasterMessageTypeResultList = new List<MasterMessageTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterMessageTypeResult _objMasterMessageTypeResult = new MasterMessageTypeResult();

                    _objMasterMessageTypeResult.MasterMessageTypeId = _Item.MasterMessageTypeId;
                    _objMasterMessageTypeResult.MessageTitle = _Item.MessageTitle;

                    _objMasterMessageTypeResult.IsActive = _Item.IsActive;
                    _objMasterMessageTypeResult.ActiveColor = "green";
                    _objMasterMessageTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterMessageTypeResult.IsActive == false)
                    {
                        _objMasterMessageTypeResult.ActiveColor = "red";
                        _objMasterMessageTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterMessageTypeResultList.Add(_objMasterMessageTypeResult);
                }

                return objMasterMessageTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterMessageTypeResult GetADMasterMessageTypeByID(long MasterMessageTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterMessageTypes.Find(MasterMessageTypeId);

                MasterMessageTypeResult _objMasterMessageTypeResult = new MasterMessageTypeResult();

                if (_Item != null)
                {
                    _objMasterMessageTypeResult.MasterMessageTypeId = _Item.MasterMessageTypeId;
                    _objMasterMessageTypeResult.MessageTitle = _Item.MessageTitle;

                    _objMasterMessageTypeResult.IsActive = _Item.IsActive;
                    _objMasterMessageTypeResult.ActiveColor = "green";
                    _objMasterMessageTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterMessageTypeResult.IsActive == false)
                    {
                        _objMasterMessageTypeResult.ActiveColor = "red";
                        _objMasterMessageTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterMessageTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterMessageType(ADMasterMessageType objADMasterMessageType)
        {
            try
            {
                _Context.ADMasterMessageTypes.Add(objADMasterMessageType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterMessageType(ADMasterMessageType objADMasterMessageType)
        {
            try
            {
                _Context.Entry(objADMasterMessageType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterMessageType(long MasterMessageTypeId)
        {
            try
            {
                ADMasterMessageType objADMasterMessageType = _Context.ADMasterMessageTypes.Find(MasterMessageTypeId);
                _Context.ADMasterMessageTypes.Remove(objADMasterMessageType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterMessageTypeExists(long MasterMessageTypeId)
        {
            try
            {
                return _Context.ADMasterMessageTypes.Any(e => e.MasterMessageTypeId == MasterMessageTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
