using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;


namespace AdminWebApi.Repository.Implementation
{
    public class MasterStatus : IMasterStatusInterface<MasterStatusResult>
    {
        readonly AdminContext _Context;
        public MasterStatus(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterStatusResult> GetAllADMasterStatus()
        {
            try
            {
                var _data = _Context.ADMasterStatuss.ToList();

                List<MasterStatusResult> objMasterStatusList = new List<MasterStatusResult>();
                foreach (var _Item in _data)
                {
                    MasterStatusResult _objMasterStatusResult = new MasterStatusResult();

                    _objMasterStatusResult.MasterStatusId = _Item.MasterStatusId;
                    _objMasterStatusResult.StatusTitle = _Item.StatusTitle;
                    _objMasterStatusResult.StatusCode = _Item.StatusCode;
                    _objMasterStatusResult.MasterColorId = _Item.MasterColorId;


                    _objMasterStatusResult.IsActive = _Item.IsActive;
                    _objMasterStatusResult.ActiveColor = "green";
                    _objMasterStatusResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterStatusResult.IsActive == false)
                    {
                        _objMasterStatusResult.ActiveColor = "red";
                        _objMasterStatusResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterStatusList.Add(_objMasterStatusResult);
                }

                return objMasterStatusList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterStatusResult GetADMasterStatusByID(long MasterStatusId)
        {
            try
            {
                var _Item = _Context.ADMasterStatuss.Find(MasterStatusId);

                MasterStatusResult _objMasterStatusResult = new MasterStatusResult();

                if (_Item != null)
                {
                    _objMasterStatusResult.MasterStatusId = _Item.MasterStatusId;
                    _objMasterStatusResult.StatusTitle = _Item.StatusTitle;
                    _objMasterStatusResult.StatusCode = _Item.StatusCode;
                    _objMasterStatusResult.MasterColorId = _Item.MasterColorId;

                    _objMasterStatusResult.IsActive = _Item.IsActive;
                    _objMasterStatusResult.ActiveColor = "green";
                    _objMasterStatusResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterStatusResult.IsActive == false)
                    {
                        _objMasterStatusResult.ActiveColor = "red";
                        _objMasterStatusResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterStatus(ADMasterStatus objADMasterStatus)
        {
            try
            {
                _Context.ADMasterStatuss.Add(objADMasterStatus);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterStatus(ADMasterStatus objADMasterStatus)
        {
            try
            {
                _Context.Entry(objADMasterStatus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterStatus(long MasterStatusId)
        {
            try
            {
                ADMasterStatus objADMasterStatus = _Context.ADMasterStatuss.Find(MasterStatusId);
                _Context.ADMasterStatuss.Remove(objADMasterStatus);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterStatusExists(long MasterStatusId)
        {
            try
            {
                return _Context.ADMasterStatuss.Any(e => e.MasterStatusId == MasterStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
