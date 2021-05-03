using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterMailTemplate : IMasterMailTemplateInterface<MasterMailTemplateResult>
    {
        readonly AdminContext _Context;

        public MasterMailTemplate(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterMailTemplateResult> GetAllADMasterMailTemplate()
        {
            try
            {
                var _data = _Context.ADMasterMailTemplates.ToList();

                List<MasterMailTemplateResult> objMasterMailTemplateResultList = new List<MasterMailTemplateResult>();
                foreach (var _Item in _data)
                {
                    MasterMailTemplateResult _objMasterMailTemplateResult = new MasterMailTemplateResult();

                    _objMasterMailTemplateResult.MasterMailTemplateId = _Item.MasterMailTemplateId;
                    _objMasterMailTemplateResult.MailTemplateTitle = _Item.MailTemplateTitle;
                    _objMasterMailTemplateResult.MailSubject = _Item.MailSubject;
                    _objMasterMailTemplateResult.MailBody = _Item.MailBody;

                    //_objMasterMailTemplateResult.IsActive = _Item.IsActive;
                    //_objMasterMailTemplateResult.ActiveColor = "green";
                    //_objMasterMailTemplateResult.ActiveIcon = "glyphicon glyphicon-ok";

                    //if (_objMasterMailTemplateResult.IsActive == false)
                    //{
                    //    _objMasterMailTemplateResult.ActiveColor = "red";
                    //    _objMasterMailTemplateResult.ActiveIcon = "glyphicon glyphicon-remove";
                    //}

                    objMasterMailTemplateResultList.Add(_objMasterMailTemplateResult);
                }

                return objMasterMailTemplateResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterMailTemplateResult GetADMasterMailTemplateByID(long MasterMailTemplateId)
        {
            try
            {
                var _Item = _Context.ADMasterMailTemplates.Find(MasterMailTemplateId);

                MasterMailTemplateResult _objMasterMailTemplateResult = new MasterMailTemplateResult();

                if (_Item != null)
                {
                    _objMasterMailTemplateResult.MasterMailTemplateId = _Item.MasterMailTemplateId;
                    _objMasterMailTemplateResult.MailTemplateTitle = _Item.MailTemplateTitle;
                    _objMasterMailTemplateResult.MailSubject = _Item.MailSubject;
                    _objMasterMailTemplateResult.MailBody = _Item.MailBody;

                    //_objMasterMailTemplateResult.IsActive = _Item.IsActive;
                    //_objMasterMailTemplateResult.ActiveColor = "green";
                    //_objMasterMailTemplateResult.ActiveIcon = "glyphicon glyphicon-ok";

                    //if (_objMasterMailTemplateResult.IsActive == false)
                    //{
                    //    _objMasterMailTemplateResult.ActiveColor = "red";
                    //    _objMasterMailTemplateResult.ActiveIcon = "glyphicon glyphicon-remove";
                    //}
                }

                return _objMasterMailTemplateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterMailTemplate(ADMasterMailTemplate objADMasterMailTemplate)
        {
            try
            {
                _Context.ADMasterMailTemplates.Add(objADMasterMailTemplate);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterMailTemplate(ADMasterMailTemplate objADMasterMailTemplate)
        {
            try
            {
                _Context.Entry(objADMasterMailTemplate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterMailTemplate(long MasterMailTemplateId)
        {
            try
            {
                ADMasterMailTemplate objADMasterMailTemplate = _Context.ADMasterMailTemplates.Find(MasterMailTemplateId);
                _Context.ADMasterMailTemplates.Remove(objADMasterMailTemplate);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterMailTemplateExists(long MasterMailTemplateId)
        {
            try
            {
                return _Context.ADMasterMailTemplates.Any(e => e.MasterMailTemplateId == MasterMailTemplateId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
