using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterCity : IMasterCityInterface<MasterCityResult>
    {
        readonly AdminContext _Context;
        public MasterCity(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterCityResult> GetAllADMasterCity()
        {
            try
            {
                var _data = _Context.ADMasterCities.ToList();

                List<MasterCityResult> objMasterCityResultList = new List<MasterCityResult>();
                foreach (var _Item in _data)
                {
                    MasterCityResult _objMasterCityResult = new MasterCityResult();

                    _objMasterCityResult.MasterCityId = _Item.MasterCityId;
                    _objMasterCityResult.MasterStateId = _Item.MasterStateId;
                    _objMasterCityResult.CityTitle = _Item.CityTitle;

                    _objMasterCityResult.IsActive = _Item.IsActive;
                    _objMasterCityResult.ActiveColor = "green";
                    _objMasterCityResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCityResult.IsActive == false)
                    {
                        _objMasterCityResult.ActiveColor = "red";
                        _objMasterCityResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterCityResultList.Add(_objMasterCityResult);
                }

                return objMasterCityResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterCityResult GetADMasterCityByID(long MasterCityId)
        {
            try
            {
                var _Item = _Context.ADMasterCities.Find(MasterCityId);

                MasterCityResult _objMasterCityResult = new MasterCityResult();

                if (_Item != null)
                {
                    _objMasterCityResult.MasterCityId = _Item.MasterCityId;
                    _objMasterCityResult.MasterStateId = _Item.MasterStateId;
                    _objMasterCityResult.CityTitle = _Item.CityTitle;

                    _objMasterCityResult.IsActive = _Item.IsActive;
                    _objMasterCityResult.ActiveColor = "green";
                    _objMasterCityResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCityResult.IsActive == false)
                    {
                        _objMasterCityResult.ActiveColor = "red";
                        _objMasterCityResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterCityResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterCity(ADMasterCity objADMasterCity)
        {
            try
            {
                _Context.ADMasterCities.Add(objADMasterCity);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterCity(ADMasterCity objADMasterCity)
        {
            try
            {
                _Context.Entry(objADMasterCity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterCity(long MasterCityId)
        {
            try
            {
                ADMasterCity objADMasterCity = _Context.ADMasterCities.Find(MasterCityId);
                _Context.ADMasterCities.Remove(objADMasterCity);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterCityExists(long MasterCityId)
        {
            try
            {
                return _Context.ADMasterCities.Any(e => e.MasterCityId == MasterCityId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
