using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterCountry : IMasterCountryInterface<MasterCountryResult>
    {
        readonly AdminContext _Context;

        public MasterCountry(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterCountryResult> GetAllADMasterCountry()
        {
            try
            {
                var _data = _Context.ADMasterCountries.ToList();

                List<MasterCountryResult> objMasterCountryList = new List<MasterCountryResult>();
                foreach (var _Item in _data)
                {
                    MasterCountryResult _objMasterCountryResult = new MasterCountryResult();

                    _objMasterCountryResult.MasterCountryId = _Item.MasterCountryId;
                    _objMasterCountryResult.CountryTitle = _Item.CountryTitle;
                    _objMasterCountryResult.CountryCode = _Item.CountryCode;
                    _objMasterCountryResult.CountryDialCode = _Item.CountryDialCode;
                    _objMasterCountryResult.CountryFlag = _Item.CountryFlag;

                    _objMasterCountryResult.IsActive = _Item.IsActive;
                    _objMasterCountryResult.ActiveColor = "green";
                    _objMasterCountryResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCountryResult.IsActive == false)
                    {
                        _objMasterCountryResult.ActiveColor = "red";
                        _objMasterCountryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterCountryList.Add(_objMasterCountryResult);
                }

                return objMasterCountryList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterCountryResult GetADMasterCountryByID(long MasterCountryId)
        {
            try
            {
                var _Item = _Context.ADMasterCountries.Find(MasterCountryId);

                MasterCountryResult _objMasterCountryResult = new MasterCountryResult();

                if (_Item != null)
                {
                    _objMasterCountryResult.MasterCountryId = _Item.MasterCountryId;
                    _objMasterCountryResult.CountryTitle = _Item.CountryTitle;
                    _objMasterCountryResult.CountryCode = _Item.CountryCode;
                    _objMasterCountryResult.CountryDialCode = _Item.CountryDialCode;
                    _objMasterCountryResult.CountryFlag = _Item.CountryFlag;

                    _objMasterCountryResult.IsActive = _Item.IsActive;
                    _objMasterCountryResult.ActiveColor = "green";
                    _objMasterCountryResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCountryResult.IsActive == false)
                    {
                        _objMasterCountryResult.ActiveColor = "red";
                        _objMasterCountryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterCountryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterCountry(ADMasterCountry objADMasterCountry)
        {
            try
            {
                _Context.ADMasterCountries.Add(objADMasterCountry);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterCountry(ADMasterCountry objADMasterCountry)
        {
            try
            {
                _Context.Entry(objADMasterCountry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterCountry(long MasterCountryId)
        {
            try
            {
                ADMasterCountry objADMasterCountry = _Context.ADMasterCountries.Find(MasterCountryId);
                _Context.ADMasterCountries.Remove(objADMasterCountry);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterCountryExists(long MasterCountryId)
        {
            try
            {
                return _Context.ADMasterCountries.Any(e => e.MasterCountryId == MasterCountryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
