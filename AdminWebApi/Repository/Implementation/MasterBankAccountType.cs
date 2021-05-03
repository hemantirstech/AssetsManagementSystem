using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterBankAccountType : IMasterBankAccountTypeInterface<MasterBankAccountTypeResult>
    {
        readonly AdminContext _Context;
        public MasterBankAccountType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterBankAccountTypeResult> GetAllADMasterBankAccountType()
        {
            try
            {
                var _data = _Context.ADMasterBankAccountTypes.ToList();

                List<MasterBankAccountTypeResult> objMasterBankAccountTypeResultList = new List<MasterBankAccountTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterBankAccountTypeResult _objMasterBankAccountTypeResult = new MasterBankAccountTypeResult();

                    _objMasterBankAccountTypeResult.MasterBankAccountTypeId = _Item.MasterBankAccountTypeId;
                    _objMasterBankAccountTypeResult.MasterBankAccountTypeTitle = _Item.MasterBankAccountTypeTitle;
                    _objMasterBankAccountTypeResult.MasterBankAccountCode = _Item.MasterBankAccountCode;

                    _objMasterBankAccountTypeResult.IsActive = _Item.IsActive;
                    _objMasterBankAccountTypeResult.ActiveColor = "green";
                    _objMasterBankAccountTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterBankAccountTypeResult.IsActive == false)
                    {
                        _objMasterBankAccountTypeResult.ActiveColor = "red";
                        _objMasterBankAccountTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterBankAccountTypeResultList.Add(_objMasterBankAccountTypeResult);
                }

                return objMasterBankAccountTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterBankAccountTypeResult GetADMasterBankAccountTypeByID(long MasterBankAccountTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterBankAccountTypes.Find(MasterBankAccountTypeId);

                MasterBankAccountTypeResult _objMasterBankAccountTypeResult = new MasterBankAccountTypeResult();

                if (_Item != null)
                {
                    _objMasterBankAccountTypeResult.MasterBankAccountTypeId = _Item.MasterBankAccountTypeId;
                    _objMasterBankAccountTypeResult.MasterBankAccountTypeTitle = _Item.MasterBankAccountTypeTitle;
                    _objMasterBankAccountTypeResult.MasterBankAccountCode = _Item.MasterBankAccountCode;

                    _objMasterBankAccountTypeResult.IsActive = _Item.IsActive;
                    _objMasterBankAccountTypeResult.ActiveColor = "green";
                    _objMasterBankAccountTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterBankAccountTypeResult.IsActive == false)
                    {
                        _objMasterBankAccountTypeResult.ActiveColor = "red";
                        _objMasterBankAccountTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterBankAccountTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterBankAccountType(ADMasterBankAccountType objADMasterBankAccountType)
        {
            try
            {
                _Context.ADMasterBankAccountTypes.Add(objADMasterBankAccountType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterBankAccountType(ADMasterBankAccountType objADMasterBankAccountType)
        {
            try
            {
                _Context.Entry(objADMasterBankAccountType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterBankAccountType(long MasterBankAccountTypeId)
        {
            try
            {
                ADMasterBankAccountType objADMasterBankAccountType = _Context.ADMasterBankAccountTypes.Find(MasterBankAccountTypeId);
                _Context.ADMasterBankAccountTypes.Remove(objADMasterBankAccountType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterBankAccountTypeExists(long MasterBankAccountTypeId)
        {
            try
            {
                return _Context.ADMasterBankAccountTypes.Any(e => e.MasterBankAccountTypeId == MasterBankAccountTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
