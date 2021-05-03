using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterConfiguration : IMasterConfigurationInterface<MasterConfigurationResult>
    {
        readonly AdminContext _Context;
        public MasterConfiguration(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterConfigurationResult> GetAllADMasterConfiguration()
        {
            try
            {
                var _data = _Context.ADMasterConfigurations.ToList();

                List<MasterConfigurationResult> objMasterConfigurationList = new List<MasterConfigurationResult>();
                foreach (var _Item in _data)
                {
                    MasterConfigurationResult _objMasterConfigurationResult = new MasterConfigurationResult();

                    _objMasterConfigurationResult.MasterConfigurationId = _Item.MasterConfigurationId;
                    _objMasterConfigurationResult.MasterCompanyId = _Item.MasterCompanyId;
                    _objMasterConfigurationResult.EnableNoActivity5Day = _Item.EnableNoActivity5Day;
                    _objMasterConfigurationResult.EnableLoginMACIdAuthentication = _Item.EnableLoginMACIdAuthentication;
                    _objMasterConfigurationResult.EnablePasswordResetByAdmin = _Item.EnablePasswordResetByAdmin;
                    _objMasterConfigurationResult.EnableEmailVerification = _Item.EnableEmailVerification;
                    _objMasterConfigurationResult.EnableMobileVerification = _Item.EnableMobileVerification;
                    _objMasterConfigurationResult.SMTPServer = _Item.SMTPServer;
                    _objMasterConfigurationResult.SMTPPort = _Item.SMTPPort;
                    _objMasterConfigurationResult.SMTPUserName = _Item.SMTPUserName;
                    _objMasterConfigurationResult.SMTPPassword = _Item.SMTPPassword;
                    _objMasterConfigurationResult.SMSUrl = _Item.SMSUrl;
                    _objMasterConfigurationResult.SMSKey = _Item.SMSKey;
                    _objMasterConfigurationResult.SMSSenderId = _Item.SMSSenderId;
                    _objMasterConfigurationResult.SMSPassword = _Item.SMSPassword;

                    _objMasterConfigurationResult.IsActive = _Item.IsActive;
                    _objMasterConfigurationResult.ActiveColor = "green";
                    _objMasterConfigurationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterConfigurationResult.IsActive == false)
                    {
                        _objMasterConfigurationResult.ActiveColor = "red";
                        _objMasterConfigurationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterConfigurationList.Add(_objMasterConfigurationResult);
                }

                return objMasterConfigurationList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterConfigurationResult GetADMasterConfigurationByID(long MasterConfigurationId)
        {
            try
            {
                var _Item = _Context.ADMasterConfigurations.Find(MasterConfigurationId);

                MasterConfigurationResult _objMasterConfigurationResult = new MasterConfigurationResult();

                if (_Item != null)
                {
                    _objMasterConfigurationResult.MasterConfigurationId = _Item.MasterConfigurationId;
                    _objMasterConfigurationResult.MasterCompanyId = _Item.MasterCompanyId;
                    _objMasterConfigurationResult.EnableNoActivity5Day = _Item.EnableNoActivity5Day;
                    _objMasterConfigurationResult.EnableLoginMACIdAuthentication = _Item.EnableLoginMACIdAuthentication;
                    _objMasterConfigurationResult.EnablePasswordResetByAdmin = _Item.EnablePasswordResetByAdmin;
                    _objMasterConfigurationResult.EnableEmailVerification = _Item.EnableEmailVerification;
                    _objMasterConfigurationResult.EnableMobileVerification = _Item.EnableMobileVerification;
                    _objMasterConfigurationResult.SMTPServer = _Item.SMTPServer;
                    _objMasterConfigurationResult.SMTPPort = _Item.SMTPPort;
                    _objMasterConfigurationResult.SMTPUserName = _Item.SMTPUserName;
                    _objMasterConfigurationResult.SMTPPassword = _Item.SMTPPassword;
                    _objMasterConfigurationResult.SMSUrl = _Item.SMSUrl;
                    _objMasterConfigurationResult.SMSKey = _Item.SMSKey;
                    _objMasterConfigurationResult.SMSSenderId = _Item.SMSSenderId;
                    _objMasterConfigurationResult.SMSPassword = _Item.SMSPassword;

                    _objMasterConfigurationResult.IsActive = _Item.IsActive;
                    _objMasterConfigurationResult.ActiveColor = "green";
                    _objMasterConfigurationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterConfigurationResult.IsActive == false)
                    {
                        _objMasterConfigurationResult.ActiveColor = "red";
                        _objMasterConfigurationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterConfigurationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterConfiguration(ADMasterConfiguration objADMasterConfiguration)
        {
            try
            {
                _Context.ADMasterConfigurations.Add(objADMasterConfiguration);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterConfiguration(ADMasterConfiguration objADMasterConfiguration)
        {
            try
            {
                _Context.Entry(objADMasterConfiguration).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterConfiguration(long MasterConfigurationId)
        {
            try
            {
                ADMasterConfiguration objADMasterConfiguration = _Context.ADMasterConfigurations.Find(MasterConfigurationId);
                _Context.ADMasterConfigurations.Remove(objADMasterConfiguration);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterConfigurationExists(long MasterConfigurationId)
        {
            try
            {
                return _Context.ADMasterConfigurations.Any(e => e.MasterConfigurationId == MasterConfigurationId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
