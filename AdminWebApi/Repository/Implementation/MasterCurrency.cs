using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterCurrency : IMasterCurrencyInterface<MasterCurrencyResult>
    {
        readonly AdminContext _Context;

        public MasterCurrency(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterCurrencyResult> GetAllADMasterCurrency()
        {
            try
            {
                var _data = _Context.ADMasterCurrencies.ToList();

                List<MasterCurrencyResult> objMasterCurrencyList = new List<MasterCurrencyResult>();
                foreach (var _Item in _data)
                {
                    MasterCurrencyResult _objMasterCurrencyResult = new MasterCurrencyResult();

                    _objMasterCurrencyResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _objMasterCurrencyResult.CurrencyTitle = _Item.CurrencyTitle;
                    _objMasterCurrencyResult.CurrencySymbol = _Item.CurrencySymbol;
                    _objMasterCurrencyResult.MasterCountryId = _Item.MasterCountryId;

                    _objMasterCurrencyResult.IsActive = _Item.IsActive;
                    _objMasterCurrencyResult.ActiveColor = "green";
                    _objMasterCurrencyResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCurrencyResult.IsActive == false)
                    {
                        _objMasterCurrencyResult.ActiveColor = "red";
                        _objMasterCurrencyResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterCurrencyList.Add(_objMasterCurrencyResult);
                }

                return objMasterCurrencyList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterCurrencyResult GetADMasterCurrencyByID(long MasterCurrencyId)
        {
            try
            {
                var _Item = _Context.ADMasterCurrencies.Find(MasterCurrencyId);

                MasterCurrencyResult _objMasterCurrencyResult = new MasterCurrencyResult();

                if (_Item != null)
                {
                    _objMasterCurrencyResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _objMasterCurrencyResult.CurrencyTitle = _Item.CurrencyTitle;
                    _objMasterCurrencyResult.CurrencySymbol = _Item.CurrencySymbol;
                    _objMasterCurrencyResult.MasterCountryId = _Item.MasterCountryId;

                    _objMasterCurrencyResult.IsActive = _Item.IsActive;
                    _objMasterCurrencyResult.ActiveColor = "green";
                    _objMasterCurrencyResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterCurrencyResult.IsActive == false)
                    {
                        _objMasterCurrencyResult.ActiveColor = "red";
                        _objMasterCurrencyResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterCurrencyResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterCurrency(ADMasterCurrency objADMasterCurrency)
        {
            try
            {
                _Context.ADMasterCurrencies.Add(objADMasterCurrency);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterCurrency(ADMasterCurrency objADMasterCurrency)
        {
            try
            {
                _Context.Entry(objADMasterCurrency).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterCurrency(long MasterCurrencyId)
        {
            try
            {
                ADMasterCurrency objADMasterCurrency = _Context.ADMasterCurrencies.Find(MasterCurrencyId);
                _Context.ADMasterCurrencies.Remove(objADMasterCurrency);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterCurrencyExists(long MasterCurrencyId)
        {
            try
            {
                return _Context.ADMasterCurrencies.Any(e => e.MasterCurrencyId == MasterCurrencyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
