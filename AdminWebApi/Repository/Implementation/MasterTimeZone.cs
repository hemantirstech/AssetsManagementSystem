using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterTimeZone : IMasterTimeZoneInterface<MasterTimeZoneResult>
    {
        readonly AdminContext _Context;
        public MasterTimeZone(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterTimeZoneResult> GetAllADMasterTimeZone()
        {
            try
            {
                var _data = _Context.ADMasterTimeZones.ToList();

                List<MasterTimeZoneResult> objMasterTimeZoneList = new List<MasterTimeZoneResult>();
                foreach (var _Item in _data)
                {
                    MasterTimeZoneResult _objMasterTimeZoneResult = new MasterTimeZoneResult();

                    _objMasterTimeZoneResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _objMasterTimeZoneResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _objMasterTimeZoneResult.TimeZoneOffset = _Item.TimeZoneOffset;
                    _objMasterTimeZoneResult.HasDst = _Item.HasDst;
                    

                    _objMasterTimeZoneResult.IsActive = _Item.IsActive;
                    _objMasterTimeZoneResult.ActiveColor = "green";
                    _objMasterTimeZoneResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTimeZoneResult.IsActive == false)
                    {
                        _objMasterTimeZoneResult.ActiveColor = "red";
                        _objMasterTimeZoneResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterTimeZoneList.Add(_objMasterTimeZoneResult);
                }

                return objMasterTimeZoneList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterTimeZoneResult GetADMasterTimeZoneByID(long MasterTimeZoneId)
        {
            try
            {
                var _Item = _Context.ADMasterTimeZones.Find(MasterTimeZoneId);

                MasterTimeZoneResult _objMasterTimeZoneResult = new MasterTimeZoneResult();

                if (_Item != null)
                {
                    _objMasterTimeZoneResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _objMasterTimeZoneResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _objMasterTimeZoneResult.TimeZoneOffset = _Item.TimeZoneOffset;
                    _objMasterTimeZoneResult.HasDst = _Item.HasDst;
                  

                    _objMasterTimeZoneResult.IsActive = _Item.IsActive;
                    _objMasterTimeZoneResult.ActiveColor = "green";
                    _objMasterTimeZoneResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTimeZoneResult.IsActive == false)
                    {
                        _objMasterTimeZoneResult.ActiveColor = "red";
                        _objMasterTimeZoneResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterTimeZoneResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterTimeZone(ADMasterTimeZone objADMasterTimeZone)
        {
            try
            {
                _Context.ADMasterTimeZones.Add(objADMasterTimeZone);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterTimeZone(ADMasterTimeZone objADMasterTimeZone)
        {
            try
            {
                _Context.Entry(objADMasterTimeZone).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterTimeZone(long MasterTimeZoneId)
        {
            try
            {
                ADMasterTimeZone objADMasterTimeZone = _Context.ADMasterTimeZones.Find(MasterTimeZoneId);
                _Context.ADMasterTimeZones.Remove(objADMasterTimeZone);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterTimeZoneExists(long MasterTimeZoneId)
        {
            try
            {
                return _Context.ADMasterTimeZones.Any(e => e.MasterTimeZoneId == MasterTimeZoneId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
