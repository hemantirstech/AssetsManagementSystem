using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterState : IMasterStateInterface<MasterStateResult>
    {
        readonly AdminContext _Context;
        public MasterState(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterStateResult> GetAllADMasterState()
        {
            try
            {
                var _data = _Context.ADMasterStates.ToList();

                List<MasterStateResult> objMasterStateList = new List<MasterStateResult>();
                foreach (var _Item in _data)
                {
                    MasterStateResult _objMasterStateResult = new MasterStateResult();

                    _objMasterStateResult.MasterStateId = _Item.MasterStateId;
                    _objMasterStateResult.StateTitle = _Item.StateTitle;
                    _objMasterStateResult.StateCode = _Item.StateCode;


                    _objMasterStateResult.IsActive = _Item.IsActive;
                    _objMasterStateResult.ActiveColor = "green";
                    _objMasterStateResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterStateResult.IsActive == false)
                    {
                        _objMasterStateResult.ActiveColor = "red";
                        _objMasterStateResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterStateList.Add(_objMasterStateResult);
                }

                return objMasterStateList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterStateResult GetADMasterStateByID(long MasterStateId)
        {
            try
            {
                var _Item = _Context.ADMasterStates.Find(MasterStateId);

                MasterStateResult _objMasterStateResult = new MasterStateResult();

                if (_Item != null)
                {
                    _objMasterStateResult.MasterStateId = _Item.MasterStateId;
                    _objMasterStateResult.StateTitle = _Item.StateTitle;
                    _objMasterStateResult.StateCode = _Item.StateCode;

                    _objMasterStateResult.IsActive = _Item.IsActive;
                    _objMasterStateResult.ActiveColor = "green";
                    _objMasterStateResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterStateResult.IsActive == false)
                    {
                        _objMasterStateResult.ActiveColor = "red";
                        _objMasterStateResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterStateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterState(ADMasterState objADMasterState)
        {
            try
            {
                _Context.ADMasterStates.Add(objADMasterState);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterState(ADMasterState objADMasterState)
        {
            try
            {
                _Context.Entry(objADMasterState).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterState(long MasterStateId)
        {
            try
            {
                ADMasterState objADMasterState = _Context.ADMasterStates.Find(MasterStateId);
                _Context.ADMasterStates.Remove(objADMasterState);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterStateExists(long MasterStateId)
        {
            try
            {
                return _Context.ADMasterStates.Any(e => e.MasterStateId == MasterStateId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
