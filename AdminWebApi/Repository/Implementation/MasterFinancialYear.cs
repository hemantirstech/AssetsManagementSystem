using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterFinancialYear : IMasterFinancialYearInterface<MasterFinancialYearResult>
    {
        readonly AdminContext _Context;

        public MasterFinancialYear(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterFinancialYearResult> GetAllADMasterFinancialYear()
        {
            try
            {
                var _data = _Context.ADMasterFinancialYears.ToList();

                List<MasterFinancialYearResult> objMasterFinancialYearList = new List<MasterFinancialYearResult>();
                foreach (var _Item in _data)
                {
                    MasterFinancialYearResult _objMasterFinancialYearResult = new MasterFinancialYearResult();

                    _objMasterFinancialYearResult.MasterFinancialYearId = _Item.MasterFinancialYearId;
                    _objMasterFinancialYearResult.FinancialYearDescription = _Item.FinancialYearDescription;
                    _objMasterFinancialYearResult.YearStartDate = _Item.YearStartDate;
                    _objMasterFinancialYearResult.YearEndDate = _Item.YearEndDate;
                    _objMasterFinancialYearResult.CashBook = _Item.CashBook;
                    _objMasterFinancialYearResult.YearLocked = _Item.YearLocked;
                    _objMasterFinancialYearResult.CurrentYear = _Item.CurrentYear;

                    _objMasterFinancialYearResult.IsActive = _Item.IsActive;
                    _objMasterFinancialYearResult.ActiveColor = "green";
                    _objMasterFinancialYearResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterFinancialYearResult.IsActive == false)
                    {
                        _objMasterFinancialYearResult.ActiveColor = "red";
                        _objMasterFinancialYearResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterFinancialYearList.Add(_objMasterFinancialYearResult);
                }

                return objMasterFinancialYearList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterFinancialYearResult GetADMasterFinancialYearByID(long MasterFinancialYearId)
        {
            try
            {
                var _Item = _Context.ADMasterFinancialYears.Find(MasterFinancialYearId);

                MasterFinancialYearResult _objMasterFinancialYearResult = new MasterFinancialYearResult();

                if (_Item != null)
                {
                    _objMasterFinancialYearResult.MasterFinancialYearId = _Item.MasterFinancialYearId;
                    _objMasterFinancialYearResult.FinancialYearDescription = _Item.FinancialYearDescription;
                    _objMasterFinancialYearResult.YearStartDate = _Item.YearStartDate;
                    _objMasterFinancialYearResult.YearEndDate = _Item.YearEndDate;
                    _objMasterFinancialYearResult.CashBook = _Item.CashBook;
                    _objMasterFinancialYearResult.YearLocked = _Item.YearLocked;
                    _objMasterFinancialYearResult.CurrentYear = _Item.CurrentYear;

                    _objMasterFinancialYearResult.IsActive = _Item.IsActive;
                    _objMasterFinancialYearResult.ActiveColor = "green";
                    _objMasterFinancialYearResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterFinancialYearResult.IsActive == false)
                    {
                        _objMasterFinancialYearResult.ActiveColor = "red";
                        _objMasterFinancialYearResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterFinancialYearResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterFinancialYear(ADMasterFinancialYear objADMasterFinancialYear)
        {
            try
            {
                _Context.ADMasterFinancialYears.Add(objADMasterFinancialYear);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterFinancialYear(ADMasterFinancialYear objADMasterFinancialYear)
        {
            try
            {
                _Context.Entry(objADMasterFinancialYear).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterFinancialYear(long MasterFinancialYearId)
        {
            try
            {
                ADMasterFinancialYear objADMasterFinancialYear = _Context.ADMasterFinancialYears.Find(MasterFinancialYearId);
                _Context.ADMasterFinancialYears.Remove(objADMasterFinancialYear);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterFinancialYearExists(long MasterFinancialYearId)
        {
            try
            {
                return _Context.ADMasterFinancialYears.Any(e => e.MasterFinancialYearId == MasterFinancialYearId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
