using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterPaymentType : IMasterPaymentTypeInterface<MasterPaymentTypeResult>
    {
        readonly AdminContext _Context;

        public MasterPaymentType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterPaymentTypeResult> GetAllADMasterPaymentType()
        {
            try
            {
                var _data = _Context.ADMasterPaymentTypes.ToList();

                List<MasterPaymentTypeResult> objMasterPaymentTypeResultList = new List<MasterPaymentTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterPaymentTypeResult _objMasterPaymentTypeResult = new MasterPaymentTypeResult();

                    _objMasterPaymentTypeResult.MasterPaymentTypeId = _Item.MasterPaymentTypeId;
                    _objMasterPaymentTypeResult.MasterPaymentTitle = _Item.MasterPaymentTitle;

                    _objMasterPaymentTypeResult.IsActive = _Item.IsActive;
                    _objMasterPaymentTypeResult.ActiveColor = "green";
                    _objMasterPaymentTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterPaymentTypeResult.IsActive == false)
                    {
                        _objMasterPaymentTypeResult.ActiveColor = "red";
                        _objMasterPaymentTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterPaymentTypeResultList.Add(_objMasterPaymentTypeResult);
                }

                return objMasterPaymentTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterPaymentTypeResult GetADMasterPaymentTypeByID(long MasterPaymentTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterPaymentTypes.Find(MasterPaymentTypeId);

                MasterPaymentTypeResult _objMasterPaymentTypeResult = new MasterPaymentTypeResult();

                if (_Item != null)
                {
                    _objMasterPaymentTypeResult.MasterPaymentTypeId = _Item.MasterPaymentTypeId;
                    _objMasterPaymentTypeResult.MasterPaymentTitle = _Item.MasterPaymentTitle;

                    _objMasterPaymentTypeResult.IsActive = _Item.IsActive;
                    _objMasterPaymentTypeResult.ActiveColor = "green";
                    _objMasterPaymentTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterPaymentTypeResult.IsActive == false)
                    {
                        _objMasterPaymentTypeResult.ActiveColor = "red";
                        _objMasterPaymentTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterPaymentTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterPaymentType(ADMasterPaymentType objADMasterPaymentType)
        {
            try
            {
                _Context.ADMasterPaymentTypes.Add(objADMasterPaymentType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterPaymentType(ADMasterPaymentType objADMasterPaymentType)
        {
            try
            {
                _Context.Entry(objADMasterPaymentType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterPaymentType(long MasterPaymentTypeId)
        {
            try
            {
                ADMasterPaymentType objADMasterPaymentType = _Context.ADMasterPaymentTypes.Find(MasterPaymentTypeId);
                _Context.ADMasterPaymentTypes.Remove(objADMasterPaymentType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterPaymentTypeExists(long MasterPaymentTypeId)
        {
            try
            {
                return _Context.ADMasterPaymentTypes.Any(e => e.MasterPaymentTypeId == MasterPaymentTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
