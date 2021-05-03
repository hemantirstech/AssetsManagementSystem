using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;
using AdminWebApi.Model;
using AdminWebApi.Repository.Contract;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterEmployeeStatus : IMasterEmployeeStatusInterface<MasterEmployeeStatusResult>
    {
        readonly AdminContext _Context;

        public MasterEmployeeStatus(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterEmployeeStatusResult> GetAllADMasterEmployeeStatus()
        {
            try
            {
                var _data = _Context.ADMasterEmployeeStatus.ToList();

                List<MasterEmployeeStatusResult> objMasterEmployeeStatusList = new List<MasterEmployeeStatusResult>();
                foreach (var _Item in _data)
                {
                    MasterEmployeeStatusResult _objMasterEmployeeStatusResult = new MasterEmployeeStatusResult();

                    _objMasterEmployeeStatusResult.MasterEmployeeStatusId = _Item.MasterEmployeeStatusId;
                    _objMasterEmployeeStatusResult.EmployeeStatusTitle = _Item.EmployeeStatusTitle;

                    _objMasterEmployeeStatusResult.IsActive = _Item.IsActive;
                    _objMasterEmployeeStatusResult.ActiveColor = "green";
                    _objMasterEmployeeStatusResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterEmployeeStatusResult.IsActive == false)
                    {
                        _objMasterEmployeeStatusResult.ActiveColor = "red";
                        _objMasterEmployeeStatusResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterEmployeeStatusList.Add(_objMasterEmployeeStatusResult);
                }

                return objMasterEmployeeStatusList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterEmployeeStatusResult GetADMasterEmployeeStatusByID(long MasterEmployeeStatusId)
        {
            try
            {
                var _Item = _Context.ADMasterEmployeeStatus.Find(MasterEmployeeStatusId);

                MasterEmployeeStatusResult _objMasterEmployeeStatusResult = new MasterEmployeeStatusResult();

                if (_Item != null)
                {
                    _objMasterEmployeeStatusResult.MasterEmployeeStatusId = _Item.MasterEmployeeStatusId;
                    _objMasterEmployeeStatusResult.EmployeeStatusTitle = _Item.EmployeeStatusTitle;

                    _objMasterEmployeeStatusResult.IsActive = _Item.IsActive;
                    _objMasterEmployeeStatusResult.ActiveColor = "green";
                    _objMasterEmployeeStatusResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterEmployeeStatusResult.IsActive == false)
                    {
                        _objMasterEmployeeStatusResult.ActiveColor = "red";
                        _objMasterEmployeeStatusResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterEmployeeStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterEmployeeStatus(ADMasterEmployeeStatus objADMasterEmployeeStatus)
        {
            try
            {
                _Context.ADMasterEmployeeStatus.Add(objADMasterEmployeeStatus);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterEmployeeStatus(ADMasterEmployeeStatus objADMasterEmployeeStatus)
        {
            try
            {
                _Context.Entry(objADMasterEmployeeStatus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterEmployeeStatus(long MasterEmployeeStatusId)
        {
            try
            {
                ADMasterEmployeeStatus objADMasterEmployeeStatus = _Context.ADMasterEmployeeStatus.Find(MasterEmployeeStatusId);
                _Context.ADMasterEmployeeStatus.Remove(objADMasterEmployeeStatus);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterEmployeeStatusExists(long MasterEmployeeStatusId)
        {
            try
            {
                return _Context.ADMasterEmployeeStatus.Any(e => e.MasterEmployeeStatusId == MasterEmployeeStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
