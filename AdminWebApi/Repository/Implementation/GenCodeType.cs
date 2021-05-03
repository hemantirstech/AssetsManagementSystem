using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class GenCodeType : IGenCodeTypeInterface<GenCodeTypeResult>
    {
        readonly AdminContext _Context;

        public GenCodeType(AdminContext context)
        {
            _Context = context;
        }
             
        public IEnumerable<GenCodeTypeResult> GetAllADGenCodeType()
        {
            try
            {
                var _data = _Context.ADGenCodeTypes.ToList();

                List<GenCodeTypeResult> objGenCodeTypeResultList = new List<GenCodeTypeResult>();
                foreach (var _Item in _data)
                {
                    GenCodeTypeResult _objGenCodeTypeResult = new GenCodeTypeResult();

                    _objGenCodeTypeResult.GenCodeTypeId = _Item.GenCodeTypeId;
                    _objGenCodeTypeResult.GenCodeTypeTitle = _Item.GenCodeTypeTitle;
                    _objGenCodeTypeResult.GenCodeTypePrintDesc = _Item.GenCodeTypePrintDesc;
                    _objGenCodeTypeResult.GenCodeTypeDesc = _Item.GenCodeTypeDesc;
                    _objGenCodeTypeResult.Sequence = _Item.Sequence;

                    _objGenCodeTypeResult.IsActive = _Item.IsActive;
                    _objGenCodeTypeResult.ActiveColor = "green";
                    _objGenCodeTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objGenCodeTypeResult.IsActive == false)
                    {
                        _objGenCodeTypeResult.ActiveColor = "red";
                        _objGenCodeTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objGenCodeTypeResultList.Add(_objGenCodeTypeResult);
                }

                return objGenCodeTypeResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GenCodeTypeResult GetADGenCodeTypeByID(long GenCodeTypeId)
        {
            try
            {
                var _Item = _Context.ADGenCodeTypes.Find(GenCodeTypeId);

                GenCodeTypeResult _objGenCodeTypeResult = new GenCodeTypeResult();

                if (_Item != null)
                {
                    _objGenCodeTypeResult.GenCodeTypeId = _Item.GenCodeTypeId;
                    _objGenCodeTypeResult.GenCodeTypeTitle = _Item.GenCodeTypeTitle;
                    _objGenCodeTypeResult.GenCodeTypePrintDesc = _Item.GenCodeTypePrintDesc;
                    _objGenCodeTypeResult.GenCodeTypeDesc = _Item.GenCodeTypeDesc;
                    _objGenCodeTypeResult.Sequence = _Item.Sequence;

                    _objGenCodeTypeResult.IsActive = _Item.IsActive;
                    _objGenCodeTypeResult.ActiveColor = "green";
                    _objGenCodeTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objGenCodeTypeResult.IsActive == false)
                    {
                        _objGenCodeTypeResult.ActiveColor = "red";
                        _objGenCodeTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objGenCodeTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADGenCodeType(ADGenCodeType objADGenCodeType)
        {
            try
            {
                _Context.ADGenCodeTypes.Add(objADGenCodeType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADGenCodeType(ADGenCodeType objADGenCodeType)
        {
            try
            {
                _Context.Entry(objADGenCodeType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADGenCodeType(long GenCodeTypeId)
        {
            try
            {
                ADGenCodeType objADGenCodeType = _Context.ADGenCodeTypes.Find(GenCodeTypeId);
                _Context.ADGenCodeTypes.Remove(objADGenCodeType);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADGenCodeTypeExists(long GenCodeTypeId)
        {
            try
            {
                return _Context.ADGenCodeTypes.Any(e => e.GenCodeTypeId == GenCodeTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
