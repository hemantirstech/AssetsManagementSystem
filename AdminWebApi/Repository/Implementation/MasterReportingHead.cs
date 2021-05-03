using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterReportingHead : IMasterReportingHeadInterface<MasterReportingHeadResult>
    {
        readonly AdminContext _Context;

        public MasterReportingHead(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterReportingHeadResult> GetAllADMasterReportingHead()
        {
            try
            {
                var _data = _Context.ADMasterReportingHeads.ToList();

                List<MasterReportingHeadResult> _objMasterReportingHeadResultList = new List<MasterReportingHeadResult>();
                foreach (var _Item in _data)
                {
                    MasterReportingHeadResult _objMasterReportingHeadResult = new MasterReportingHeadResult();

                    _objMasterReportingHeadResult.MasterReportingHeadId = _Item.MasterReportingHeadId;
                    _objMasterReportingHeadResult.ReportingHeadTitle = _Item.ReportingHeadTitle;
                    _objMasterReportingHeadResult.ReportingDescription = _Item.ReportingDescription;

                    _objMasterReportingHeadResult.IsActive = _Item.IsActive;
                    _objMasterReportingHeadResult.ActiveColor = "green";
                    _objMasterReportingHeadResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterReportingHeadResult.IsActive == false)
                    {
                        _objMasterReportingHeadResult.ActiveColor = "red";
                        _objMasterReportingHeadResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _objMasterReportingHeadResultList.Add(_objMasterReportingHeadResult);
                }

                return _objMasterReportingHeadResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterReportingHeadResult GetADMasterReportingHeadByID(long MasterReportingHeadId)
        {
            try
            {
                var _Item = _Context.ADMasterReportingHeads.Find(MasterReportingHeadId);

                MasterReportingHeadResult _objMasterReportingHeadResult = new MasterReportingHeadResult();

                if (_Item != null)
                {
                    _objMasterReportingHeadResult.MasterReportingHeadId = _Item.MasterReportingHeadId;
                    _objMasterReportingHeadResult.ReportingHeadTitle = _Item.ReportingHeadTitle;
                    _objMasterReportingHeadResult.ReportingDescription = _Item.ReportingDescription;

                    _objMasterReportingHeadResult.IsActive = _Item.IsActive;
                    _objMasterReportingHeadResult.ActiveColor = "green";
                    _objMasterReportingHeadResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterReportingHeadResult.IsActive == false)
                    {
                        _objMasterReportingHeadResult.ActiveColor = "red";
                        _objMasterReportingHeadResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterReportingHeadResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterReportingHead(ADMasterReportingHead objADMasterReportingHead)
        {
            try
            {
                _Context.ADMasterReportingHeads.Add(objADMasterReportingHead);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterReportingHead(ADMasterReportingHead objADMasterReportingHead)
        {
            try
            {
                _Context.Entry(objADMasterReportingHead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterReportingHead(long MasterReportingHeadId)
        {
            try
            {
                ADMasterReportingHead objADMasterReportingHead = _Context.ADMasterReportingHeads.Find(MasterReportingHeadId);
                _Context.ADMasterReportingHeads.Remove(objADMasterReportingHead);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterReportingHeadExists(long MasterReportingHeadId)
        {
            try
            {
                return _Context.ADMasterReportingHeads.Any(e => e.MasterReportingHeadId == MasterReportingHeadId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
