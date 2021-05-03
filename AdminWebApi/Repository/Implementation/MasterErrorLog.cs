using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterErrorLog : IMasterErrorLogInterface<MasterErrorLogResult>
    {
        readonly AdminContext _Context;

        public MasterErrorLog(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterErrorLogResult> GetAllADMasterErrorLog()
        {
            try
            {
                var _data = _Context.ADMasterErrorLogs.ToList();

                List<MasterErrorLogResult> objMasterErrorLogList = new List<MasterErrorLogResult>();
                foreach (var _Item in _data)
                {
                    MasterErrorLogResult _objMasterErrorLogResult = new MasterErrorLogResult();

                    _objMasterErrorLogResult.MasterErrorLogId = _Item.MasterErrorLogId;
                    _objMasterErrorLogResult.MasterId = _Item.MasterId;
                    _objMasterErrorLogResult.SPName = _Item.SPName;
                    _objMasterErrorLogResult.TableName = _Item.TableName;
                    _objMasterErrorLogResult.Mode = _Item.Mode;
                    _objMasterErrorLogResult.ModeVersion = _Item.ModeVersion;
                    _objMasterErrorLogResult.ErrorMessage = _Item.ErrorMessage;
                    _objMasterErrorLogResult.LineNumber = _Item.LineNumber;
                    _objMasterErrorLogResult.StepComplete = _Item.StepComplete;
                    _objMasterErrorLogResult.EnterDate = _Item.EnterDate;

                    objMasterErrorLogList.Add(_objMasterErrorLogResult);
                }

                return objMasterErrorLogList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterErrorLogResult GetADMasterErrorLogByID(long MasterErrorLogId)
        {
            try
            {
                var _Item = _Context.ADMasterErrorLogs.Find(MasterErrorLogId);

                MasterErrorLogResult _objMasterErrorLogResult = new MasterErrorLogResult();

                if (_Item != null)
                {
                    _objMasterErrorLogResult.MasterErrorLogId = _Item.MasterErrorLogId;
                    _objMasterErrorLogResult.MasterId = _Item.MasterId;
                    _objMasterErrorLogResult.SPName = _Item.SPName;
                    _objMasterErrorLogResult.TableName = _Item.TableName;
                    _objMasterErrorLogResult.Mode = _Item.Mode;
                    _objMasterErrorLogResult.ModeVersion = _Item.ModeVersion;
                    _objMasterErrorLogResult.ErrorMessage = _Item.ErrorMessage;
                    _objMasterErrorLogResult.LineNumber = _Item.LineNumber;
                    _objMasterErrorLogResult.StepComplete = _Item.StepComplete;
                    _objMasterErrorLogResult.EnterDate = _Item.EnterDate;

                }

                return _objMasterErrorLogResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterErrorLog(ADMasterErrorLog objADMasterErrorLog)
        {
            try
            {
                _Context.ADMasterErrorLogs.Add(objADMasterErrorLog);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterErrorLog(ADMasterErrorLog objADMasterErrorLog)
        {
            try
            {
                _Context.Entry(objADMasterErrorLog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterErrorLog(long MasterErrorLogId)
        {
            try
            {
                ADMasterErrorLog objADMasterErrorLog = _Context.ADMasterErrorLogs.Find(MasterErrorLogId);
                _Context.ADMasterErrorLogs.Remove(objADMasterErrorLog);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterErrorLogExists(long MasterErrorLogId)
        {
            try
            {
                return _Context.ADMasterErrorLogs.Any(e => e.MasterErrorLogId == MasterErrorLogId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
