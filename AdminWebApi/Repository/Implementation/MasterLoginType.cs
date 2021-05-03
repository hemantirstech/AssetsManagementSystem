using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterLoginType : IMasterLoginTypeInterface<MasterLoginTypeResult>
    {
        readonly AdminContext _Context;

        public MasterLoginType(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterLoginTypeResult> GetAllADMasterLoginType()
        {
            try
            {
                var _data = _Context.ADMasterLoginTypes.ToList();

                List<MasterLoginTypeResult> objMasterLoginTypeResultList = new List<MasterLoginTypeResult>();
                foreach (var _Item in _data)
                {
                    MasterLoginTypeResult _objMasterLoginTypeResult = new MasterLoginTypeResult();

                    _objMasterLoginTypeResult.MasterLoginTypeId = _Item.MasterLoginTypeId;
                    _objMasterLoginTypeResult.MasterLoginTypeTitle = _Item.MasterLoginTypeTitle;

                    _objMasterLoginTypeResult.IsActive = _Item.IsActive;
                    _objMasterLoginTypeResult.ActiveColor = "green";
                    _objMasterLoginTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterLoginTypeResult.IsActive == false)
                    {
                        _objMasterLoginTypeResult.ActiveColor = "red";
                        _objMasterLoginTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterLoginTypeResultList.Add(_objMasterLoginTypeResult);
                }

                return objMasterLoginTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterLoginTypeResult GetADMasterLoginTypeByID(long MasterLoginTypeId)
        {
            try
            {
                var _Item = _Context.ADMasterLoginTypes.Find(MasterLoginTypeId);

                MasterLoginTypeResult _objMasterLoginTypeResult = new MasterLoginTypeResult();

                if (_Item != null)
                {
                    _objMasterLoginTypeResult.MasterLoginTypeId = _Item.MasterLoginTypeId;
                    _objMasterLoginTypeResult.MasterLoginTypeTitle = _Item.MasterLoginTypeTitle;

                    _objMasterLoginTypeResult.IsActive = _Item.IsActive;
                    _objMasterLoginTypeResult.ActiveColor = "green";
                    _objMasterLoginTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterLoginTypeResult.IsActive == false)
                    {
                        _objMasterLoginTypeResult.ActiveColor = "red";
                        _objMasterLoginTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterLoginTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterLoginType(ADMasterLoginType objADMasterLoginType)
        {
            try
            {
                _Context.ADMasterLoginTypes.Add(objADMasterLoginType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterLoginType(ADMasterLoginType objADMasterLoginType)
        {
            try
            {
                _Context.Entry(objADMasterLoginType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterLoginType(long MasterLoginTypeId)
        {
            try
            {
                ADMasterLoginType objADMasterLoginType = _Context.ADMasterLoginTypes.Find(MasterLoginTypeId);
                _Context.ADMasterLoginTypes.Remove(objADMasterLoginType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterLoginTypeExists(long MasterLoginTypeId)
        {
            try
            {
                return _Context.ADMasterLoginTypes.Any(e => e.MasterLoginTypeId == MasterLoginTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
