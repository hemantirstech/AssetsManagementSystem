using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterRegistration : IMasterRegistrationInterface<MasterRegistrationResult>
    {
        readonly AdminContext _Context;

        public MasterRegistration(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterRegistrationResult> GetAllADMasterRegistration()
        {
            try
            {
                var _data = _Context.ADMasterRegistration.ToList();

                List<MasterRegistrationResult> objMasterRegistrationResultList = new List<MasterRegistrationResult>();
                foreach (var _Item in _data)
                {
                    MasterRegistrationResult _objMasterRegistrationResult = new MasterRegistrationResult();

                    _objMasterRegistrationResult.MasterRegistrationId = _Item.MasterRegistrationId;
                    _objMasterRegistrationResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterRegistrationResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _objMasterRegistrationResult.MasterBDPId = _Item.MasterBDPId;
                    _objMasterRegistrationResult.MasterClientId = _Item.MasterClientId;
                    _objMasterRegistrationResult.MasterResearchProfileId = _Item.MasterResearchProfileId;

                    _objMasterRegistrationResult.IsActive = _Item.IsActive;
                    _objMasterRegistrationResult.ActiveColor = "green";
                    _objMasterRegistrationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegistrationResult.IsActive == false)
                    {
                        _objMasterRegistrationResult.ActiveColor = "red";
                        _objMasterRegistrationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterRegistrationResultList.Add(_objMasterRegistrationResult);
                }

                return objMasterRegistrationResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterRegistrationResult GetADMasterRegistrationByID(long MasterRegistrationId)
        {
            try
            {
                var _Item = _Context.ADMasterRegistration.Find(MasterRegistrationId);

                MasterRegistrationResult _objMasterRegistrationResult = new MasterRegistrationResult();

                if (_Item != null)
                {
                    _objMasterRegistrationResult.MasterRegistrationId = _Item.MasterRegistrationId;
                    _objMasterRegistrationResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterRegistrationResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _objMasterRegistrationResult.MasterBDPId = _Item.MasterBDPId;
                    _objMasterRegistrationResult.MasterClientId = _Item.MasterClientId;
                    _objMasterRegistrationResult.MasterResearchProfileId = _Item.MasterResearchProfileId;

                    _objMasterRegistrationResult.IsActive = _Item.IsActive;
                    _objMasterRegistrationResult.ActiveColor = "green";
                    _objMasterRegistrationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegistrationResult.IsActive == false)
                    {
                        _objMasterRegistrationResult.ActiveColor = "red";
                        _objMasterRegistrationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterRegistrationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterRegistration(ADMasterRegistration objADMasterRegistration)
        {
            try
            {
                _Context.ADMasterRegistration.Add(objADMasterRegistration);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterRegistration(ADMasterRegistration objADMasterRegistration)
        {
            try
            {
                _Context.Entry(objADMasterRegistration).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterRegistration(long MasterRegistrationId)
        {
            try
            {
                ADMasterRegistration objADMasterRegistration = _Context.ADMasterRegistration.Find(MasterRegistrationId);
                _Context.ADMasterRegistration.Remove(objADMasterRegistration);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterRegistrationExists(long MasterRegistrationId)
        {
            try
            {
                return _Context.ADMasterRegistration.Any(e => e.MasterRegistrationId == MasterRegistrationId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
