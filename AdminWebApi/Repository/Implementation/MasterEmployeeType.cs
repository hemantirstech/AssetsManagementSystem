using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterEmployeeType : IMasterEmployeeTypeInterface<MasterEmployeeTypeResult>
    {
        readonly AdminContext _Context;

        public MasterEmployeeType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterEmployeeTypeResult> GetAllADMasterEmployeeType()
        {
            try
            {
                var _data = _Context.ADMasterEmployeeTypes.ToList();

                List<MasterEmployeeTypeResult> objMasterEmployeeTypeList = new List<MasterEmployeeTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterEmployeeTypeResult _objMasterEmployeeTypeResult = new MasterEmployeeTypeResult();

                    _objMasterEmployeeTypeResult.MasterEmployeeTypeId = _Item.MasterEmployeeTypeId;
                    _objMasterEmployeeTypeResult.EmployeeTypeTitle = _Item.EmployeeTypeTitle;
                    _objMasterEmployeeTypeResult.EmpTypCode = _Item.EmpTypCode;
                    _objMasterEmployeeTypeResult.Remark = _Item.Remark;

                    _objMasterEmployeeTypeResult.IsActive = _Item.IsActive;
                    _objMasterEmployeeTypeResult.ActiveColor = "green";
                    _objMasterEmployeeTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterEmployeeTypeResult.IsActive == false)
                    {
                        _objMasterEmployeeTypeResult.ActiveColor = "red";
                        _objMasterEmployeeTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterEmployeeTypeList.Add(_objMasterEmployeeTypeResult);
                }

                return objMasterEmployeeTypeList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterEmployeeTypeResult GetADMasterEmployeeTypeByID(long MasterEmployeeTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterEmployeeTypes.Find(MasterEmployeeTypeId);

                MasterEmployeeTypeResult _objMasterEmployeeTypeResult = new MasterEmployeeTypeResult();

                if (_Item != null)
                {
                    _objMasterEmployeeTypeResult.MasterEmployeeTypeId = _Item.MasterEmployeeTypeId;
                    _objMasterEmployeeTypeResult.EmployeeTypeTitle = _Item.EmployeeTypeTitle;
                    _objMasterEmployeeTypeResult.EmpTypCode = _Item.EmpTypCode;
                    _objMasterEmployeeTypeResult.Remark = _Item.Remark;

                    _objMasterEmployeeTypeResult.IsActive = _Item.IsActive;
                    _objMasterEmployeeTypeResult.ActiveColor = "green";
                    _objMasterEmployeeTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterEmployeeTypeResult.IsActive == false)
                    {
                        _objMasterEmployeeTypeResult.ActiveColor = "red";
                        _objMasterEmployeeTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterEmployeeTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterEmployeeType(ADMasterEmployeeType objADMasterEmployeeType)
        {
            try
            {
                _Context.ADMasterEmployeeTypes.Add(objADMasterEmployeeType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterEmployeeType(ADMasterEmployeeType objADMasterEmployeeType)
        {
            try
            {
                _Context.Entry(objADMasterEmployeeType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterEmployeeType(long MasterEmployeeTypeId)
        {
            try
            {
                ADMasterEmployeeType objADMasterEmployeeType = _Context.ADMasterEmployeeTypes.Find(MasterEmployeeTypeId);
                _Context.ADMasterEmployeeTypes.Remove(objADMasterEmployeeType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterEmployeeTypeExists(long MasterEmployeeTypeId)
        {
            try
            {
                return _Context.ADMasterEmployeeTypes.Any(e => e.MasterEmployeeTypeId == MasterEmployeeTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
