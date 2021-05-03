using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;


namespace AdminWebApi.Repository.Implementation
{
    public class MasterAddressType : IMasterAddressTypeInterface<MasterAddressTypeResult>
    {
        readonly AdminContext _Context;

        public MasterAddressType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterAddressTypeResult> GetAllADMasterAddressType()
        {
            try
            {
                var _data = _Context.ADMasterAddressTypes.ToList();

                List<MasterAddressTypeResult> objMasterAddressTypeResultList = new List<MasterAddressTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterAddressTypeResult _objMasterAddressTypeResult = new MasterAddressTypeResult();

                    _objMasterAddressTypeResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _objMasterAddressTypeResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _objMasterAddressTypeResult.AddressTypeCode = _Item.AddressTypeCode;

                    _objMasterAddressTypeResult.IsActive = _Item.IsActive;
                    _objMasterAddressTypeResult.ActiveColor = "green";
                    _objMasterAddressTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterAddressTypeResult.IsActive == false)
                    {
                        _objMasterAddressTypeResult.ActiveColor = "red";
                        _objMasterAddressTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterAddressTypeResultList.Add(_objMasterAddressTypeResult);
                }

                return objMasterAddressTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterAddressTypeResult GetADMasterAddressTypeByID(long MasterAddressTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterAddressTypes.Find(MasterAddressTypeId);

                MasterAddressTypeResult _objMasterAddressTypeResult = new MasterAddressTypeResult();

                if (_Item != null)
                {
                    _objMasterAddressTypeResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _objMasterAddressTypeResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _objMasterAddressTypeResult.AddressTypeCode = _Item.AddressTypeCode;

                    _objMasterAddressTypeResult.IsActive = _Item.IsActive;
                    _objMasterAddressTypeResult.ActiveColor = "green";
                    _objMasterAddressTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterAddressTypeResult.IsActive == false)
                    {
                        _objMasterAddressTypeResult.ActiveColor = "red";
                        _objMasterAddressTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterAddressTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterAddressType(ADMasterAddressType objADMasterAddressType)
        {
            try
            {
                _Context.ADMasterAddressTypes.Add(objADMasterAddressType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterAddressType(ADMasterAddressType objADMasterAddressType)
        {
            try
            {
                _Context.Entry(objADMasterAddressType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterAddressType(long MasterAddressTypeId)
        {
            try
            {
                ADMasterAddressType objADMasterAddressType = _Context.ADMasterAddressTypes.Find(MasterAddressTypeId);
                _Context.ADMasterAddressTypes.Remove(objADMasterAddressType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterAddressTypeExists(long MasterAddressTypeId)
        {
            try
            {
                return _Context.ADMasterAddressTypes.Any(e => e.MasterAddressTypeId == MasterAddressTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
