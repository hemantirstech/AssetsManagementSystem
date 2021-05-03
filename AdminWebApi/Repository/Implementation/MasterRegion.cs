using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterRegion : IMasterRegionInterface<MasterRegionResult>
    {
        readonly AdminContext _Context;

        public MasterRegion(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterRegionResult> GetAllADMasterRegion()
        {
            try
            {
                var _data = _Context.ADMasterRegions.ToList();

                List<MasterRegionResult> objMasterRegionResultList = new List<MasterRegionResult>();
                foreach (var _Item in _data)
                {
                    MasterRegionResult _objMasterRegionResult = new MasterRegionResult();

                    _objMasterRegionResult.MasterRegionId = _Item.MasterRegionId;
                    _objMasterRegionResult.MasterRegionTitle = _Item.MasterRegionTitle;

                    _objMasterRegionResult.IsActive = _Item.IsActive;
                    _objMasterRegionResult.ActiveColor = "green";
                    _objMasterRegionResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegionResult.IsActive == false)
                    {
                        _objMasterRegionResult.ActiveColor = "red";
                        _objMasterRegionResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterRegionResultList.Add(_objMasterRegionResult);
                }

                return objMasterRegionResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterRegionResult GetADMasterRegionByID(long MasterRegionId)
        {
            try
            {
                var _Item = _Context.ADMasterRegions.Find(MasterRegionId);

                MasterRegionResult _objMasterRegionResult = new MasterRegionResult();

                if (_Item != null)
                {
                    _objMasterRegionResult.MasterRegionId = _Item.MasterRegionId;
                    _objMasterRegionResult.MasterRegionTitle = _Item.MasterRegionTitle;

                    _objMasterRegionResult.IsActive = _Item.IsActive;
                    _objMasterRegionResult.ActiveColor = "green";
                    _objMasterRegionResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterRegionResult.IsActive == false)
                    {
                        _objMasterRegionResult.ActiveColor = "red";
                        _objMasterRegionResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterRegionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterRegion(ADMasterRegion objADMasterRegion)
        {
            try
            {
                _Context.ADMasterRegions.Add(objADMasterRegion);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterRegion(ADMasterRegion objADMasterRegion)
        {
            try
            {
                _Context.Entry(objADMasterRegion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterRegion(long MasterRegionId)
        {
            try
            {
                ADMasterRegion objADMasterRegion = _Context.ADMasterRegions.Find(MasterRegionId);
                _Context.ADMasterRegions.Remove(objADMasterRegion);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterRegionExists(long MasterRegionId)
        {
            try
            {
                return _Context.ADMasterRegions.Any(e => e.MasterRegionId == MasterRegionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
