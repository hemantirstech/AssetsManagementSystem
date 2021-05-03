using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterRegistrationType : IMasterRegistrationTypeInterface<MasterRegistrationTypeResult>
    {
        readonly AdminContext _Context;

        public MasterRegistrationType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterRegistrationTypeResult> GetAllADMasterRegistrationType()
        {
            try
            {
                var _data = _Context.ADMasterRegistrationType.ToList();

                List<MasterRegistrationTypeResult> _objMasterRegistrationTypeResultList = new List<MasterRegistrationTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterRegistrationTypeResult _objMasterRegistrationTypeResult = new MasterRegistrationTypeResult();

                    _objMasterRegistrationTypeResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterRegistrationTypeResult.MasterRegistrationTypeTitle = _Item.MasterRegistrationTypeTitle;
                    _objMasterRegistrationTypeResult.MasterRegistrationCode = _Item.MasterRegistrationCode;
                    _objMasterRegistrationTypeResult.MasterRegistrationExpertType = _Item.MasterRegistrationExpertType;
  
                    _objMasterRegistrationTypeResult.IsActive = _Item.IsActive;
                    _objMasterRegistrationTypeResult.ActiveColor = "green";
                    _objMasterRegistrationTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegistrationTypeResult.IsActive == false)
                    {
                        _objMasterRegistrationTypeResult.ActiveColor = "red";
                        _objMasterRegistrationTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _objMasterRegistrationTypeResultList.Add(_objMasterRegistrationTypeResult);
                }

                return _objMasterRegistrationTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterRegistrationTypeResult GetADMasterRegistrationTypeByID(long MasterRegistrationTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterRegistrationType.Find(MasterRegistrationTypeId);

                MasterRegistrationTypeResult _objMasterRegistrationTypeResult = new MasterRegistrationTypeResult();

                if (_Item != null)
                {
                    _objMasterRegistrationTypeResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterRegistrationTypeResult.MasterRegistrationTypeTitle = _Item.MasterRegistrationTypeTitle;
                    _objMasterRegistrationTypeResult.MasterRegistrationCode = _Item.MasterRegistrationCode;
                    _objMasterRegistrationTypeResult.MasterRegistrationExpertType = _Item.MasterRegistrationExpertType;

                    _objMasterRegistrationTypeResult.IsActive = _Item.IsActive;
                    _objMasterRegistrationTypeResult.ActiveColor = "green";
                    _objMasterRegistrationTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegistrationTypeResult.IsActive == false)
                    {
                        _objMasterRegistrationTypeResult.ActiveColor = "red";
                        _objMasterRegistrationTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterRegistrationTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterRegistrationType(ADMasterRegistrationType objADMasterRegistrationType)
        {
            try
            {
                _Context.ADMasterRegistrationType.Add(objADMasterRegistrationType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterRegistrationType(ADMasterRegistrationType objADMasterRegistrationType)
        {
            try
            {
                _Context.Entry(objADMasterRegistrationType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterRegistrationType(long MasterRegistrationTypeId)
        {
            try
            {
                ADMasterRegistrationType objADMasterRegistrationType = _Context.ADMasterRegistrationType.Find(MasterRegistrationTypeId);
                _Context.ADMasterRegistrationType.Remove(objADMasterRegistrationType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterRegistrationTypeExists(long MasterRegistrationTypeId)
        {
            try
            {
                return _Context.ADMasterRegistrationType.Any(e => e.MasterRegistrationTypeId == MasterRegistrationTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
