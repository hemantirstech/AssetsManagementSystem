using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterTax: IMasterTaxInterface<MasterTaxResult>
    {

        readonly AdminContext _Context;
        public MasterTax(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterTaxResult> GetAllADMasterTax()
        {
            try
            {
                var _data = _Context.ADMasterTaxes.ToList();

                List<MasterTaxResult> objMasterTaxList = new List<MasterTaxResult>();
                foreach (var _Item in _data)
                {
                    MasterTaxResult _objMasterTaxResult = new MasterTaxResult();

                    _objMasterTaxResult.MasterTaxId = _Item.MasterTaxId;
                    _objMasterTaxResult.TaxTitle = _Item.TaxTitle;
                    _objMasterTaxResult.IsTaxPercentageAmount = _Item.IsTaxPercentageAmount;
                    _objMasterTaxResult.TaxValue = _Item.TaxValue;
                    _objMasterTaxResult.TaxStartDate = _Item.TaxStartDate;
                    _objMasterTaxResult.TaxEndDate = _Item.TaxEndDate;


                    _objMasterTaxResult.IsActive = _Item.IsActive;
                    _objMasterTaxResult.ActiveColor = "green";
                    _objMasterTaxResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTaxResult.IsActive == false)
                    {
                        _objMasterTaxResult.ActiveColor = "red";
                        _objMasterTaxResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterTaxList.Add(_objMasterTaxResult);
                }

                return objMasterTaxList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterTaxResult GetADMasterTaxByID(long MasterTaxId)
        {
            try
            {
                var _Item = _Context.ADMasterTaxes.Find(MasterTaxId);

                MasterTaxResult _objMasterTaxResult = new MasterTaxResult();

                if (_Item != null)
                {
                    _objMasterTaxResult.MasterTaxId = _Item.MasterTaxId;
                    _objMasterTaxResult.TaxTitle = _Item.TaxTitle;
                    _objMasterTaxResult.IsTaxPercentageAmount = _Item.IsTaxPercentageAmount;
                    _objMasterTaxResult.TaxValue = _Item.TaxValue;
                    _objMasterTaxResult.TaxStartDate = _Item.TaxStartDate;
                    _objMasterTaxResult.TaxEndDate = _Item.TaxEndDate;

                    _objMasterTaxResult.IsActive = _Item.IsActive;
                    _objMasterTaxResult.ActiveColor = "green";
                    _objMasterTaxResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterTaxResult.IsActive == false)
                    {
                        _objMasterTaxResult.ActiveColor = "red";
                        _objMasterTaxResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterTaxResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterTax(ADMasterTax objADMasterTax)
        {
            try
            {
                _Context.ADMasterTaxes.Add(objADMasterTax);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterTax(ADMasterTax objADMasterTax)
        {
            try
            {
                _Context.Entry(objADMasterTax).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterTax(long MasterTaxId)
        {
            try
            {
                ADMasterTax objADMasterTax = _Context.ADMasterTaxes.Find(MasterTaxId);
                _Context.ADMasterTaxes.Remove(objADMasterTax);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterTaxExists(long MasterTaxId)
        {
            try
            {
                return _Context.ADMasterTaxes.Any(e => e.MasterTaxId == MasterTaxId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
