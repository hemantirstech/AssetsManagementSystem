using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class GenCodeMaster : IGenCodeMasterInterface<GenCodeMasterResult>
    {
        readonly AdminContext _Context;
        public GenCodeMaster(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<GenCodeMasterResult> GetAllADGenCodeMaster()
        {
            try
            {
                var _data = _Context.ADGenCodeMasters.ToList();


                List<GenCodeMasterResult> objGenCodeMasterResultList = new List<GenCodeMasterResult>();
                foreach (var _Item in _data)
                {
                    GenCodeMasterResult _objGenCodeMasterResult = new GenCodeMasterResult();

                    _objGenCodeMasterResult.GenCodeMasterId = _Item.GenCodeMasterId;
                    _objGenCodeMasterResult.GenCodeMasterTitle = _Item.GenCodeMasterTitle;
                    _objGenCodeMasterResult.PrintDesc = _Item.PrintDesc;
                    _objGenCodeMasterResult.GenCodeTypeId = _Item.GenCodeTypeId;
                   // _objGenCodeMasterResult.GenCodeTypeTitle = _Item.GenCodeTypeTitle;
                    _objGenCodeMasterResult.Sequence = _Item.Sequence;

                    _objGenCodeMasterResult.IsActive = _Item.IsActive;
                    _objGenCodeMasterResult.ActiveColor = "green";
                    _objGenCodeMasterResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objGenCodeMasterResult.IsActive == false)
                    {
                        _objGenCodeMasterResult.ActiveColor = "red";
                        _objGenCodeMasterResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objGenCodeMasterResultList.Add(_objGenCodeMasterResult);
                }

                return objGenCodeMasterResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GenCodeMasterResult GetADGenCodeMasterByID(long GenCodeMasterId)
        {
            try
            {
                var _Item = _Context.ADGenCodeMasters.Find(GenCodeMasterId);

                GenCodeMasterResult _objGenCodeMasterResult = new GenCodeMasterResult();

                if (_Item != null)
                {
                    _objGenCodeMasterResult.GenCodeMasterId = _Item.GenCodeMasterId;
                    _objGenCodeMasterResult.GenCodeMasterTitle = _Item.GenCodeMasterTitle;
                    _objGenCodeMasterResult.PrintDesc = _Item.PrintDesc;
                    _objGenCodeMasterResult.GenCodeTypeId = _Item.GenCodeTypeId;
                 //   _objGenCodeMasterResult.GenCodeTypeTitle = _Item.GenCodeTypeTitle;
                    _objGenCodeMasterResult.Sequence = _Item.Sequence;

                    _objGenCodeMasterResult.IsActive = _Item.IsActive;
                    _objGenCodeMasterResult.ActiveColor = "green";
                    _objGenCodeMasterResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objGenCodeMasterResult.IsActive == false)
                    {
                        _objGenCodeMasterResult.ActiveColor = "red";
                        _objGenCodeMasterResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objGenCodeMasterResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADGenCodeMaster(ADGenCodeMaster objADGenCodeMaster)
            {
                try
                {
                    _Context.ADGenCodeMasters.Add(objADGenCodeMaster);

                    await _Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
        }

            public async Task UpdateADGenCodeMaster(ADGenCodeMaster objADGenCodeMaster)
            {
                try
                {
                    _Context.Entry(objADGenCodeMaster).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    await _Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task DeleteADGenCodeMaster(long GenCodeMasterId)
            {
                try
                {
                    ADGenCodeMaster objADGenCodeMaster = _Context.ADGenCodeMasters.Find(GenCodeMasterId);
                    _Context.ADGenCodeMasters.Remove(objADGenCodeMaster);

                    await _Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public bool ADGenCodeMasterExists(long GenCodeMasterId)
            {
                try
                {
                    return _Context.ADGenCodeMasters.Any(e => e.GenCodeMasterId == GenCodeMasterId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
