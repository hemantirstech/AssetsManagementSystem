using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterCompanyType : IMasterCompanyTypeInterface<MasterCompanyTypeResult>
    {
        readonly AdminContext _Context;
        public MasterCompanyType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterCompanyTypeResult> GetAllADMasterCompanyType()
        {
            try
            {
                var _data = _Context.ADMasterCompanyTypes.ToList();

                List<MasterCompanyTypeResult> objMasterCompanyTypeList = new List<MasterCompanyTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterCompanyTypeResult _objMasterCompanyTypeResult = new MasterCompanyTypeResult();

                    _objMasterCompanyTypeResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _objMasterCompanyTypeResult.CompanyTypeTitle = _Item.CompanyTypeTitle;

                    _objMasterCompanyTypeResult.IsActive = _Item.IsActive;
                    _objMasterCompanyTypeResult.ActiveColor = "green";
                    _objMasterCompanyTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCompanyTypeResult.IsActive == false)
                    {
                        _objMasterCompanyTypeResult.ActiveColor = "red";
                        _objMasterCompanyTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterCompanyTypeList.Add(_objMasterCompanyTypeResult);
                }

                return objMasterCompanyTypeList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterCompanyTypeResult GetADMasterCompanyTypeByID(long MasterCompanyTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterCompanyTypes.Find(MasterCompanyTypeId);

                MasterCompanyTypeResult _objMasterCompanyTypeResult = new MasterCompanyTypeResult();

                if (_Item != null)
                {
                    _objMasterCompanyTypeResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _objMasterCompanyTypeResult.CompanyTypeTitle = _Item.CompanyTypeTitle;

                    _objMasterCompanyTypeResult.IsActive = _Item.IsActive;
                    _objMasterCompanyTypeResult.ActiveColor = "green";
                    _objMasterCompanyTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCompanyTypeResult.IsActive == false)
                    {
                        _objMasterCompanyTypeResult.ActiveColor = "red";
                        _objMasterCompanyTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterCompanyTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterCompanyType(ADMasterCompanyType objADMasterCompanyType)
        {
            try
            {
                _Context.ADMasterCompanyTypes.Add(objADMasterCompanyType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterCompanyType(ADMasterCompanyType objADMasterCompanyType)
        {
            try
            {
                _Context.Entry(objADMasterCompanyType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterCompanyType(long MasterCompanyTypeId)
        {
            try
            {
                ADMasterCompanyType objADMasterCompanyType = _Context.ADMasterCompanyTypes.Find(MasterCompanyTypeId);
                _Context.ADMasterCompanyTypes.Remove(objADMasterCompanyType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterCompanyTypeExists(long MasterCompanyTypeId)
        {
            try
            {
                return _Context.ADMasterCompanyTypes.Any(e => e.MasterCompanyTypeId == MasterCompanyTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
