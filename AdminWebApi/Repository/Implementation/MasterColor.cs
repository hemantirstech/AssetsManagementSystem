using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;
using AdminWebApi.Model;
using AdminWebApi.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterColor : IMasterColorInterface<MasterColorResult>
    {
        readonly AdminContext _Context;

        public MasterColor(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterColorResult> GetAllADMasterColor()
        {
            try
            {
                var _data = (from MC in _Context.ADMasterColors
                             
                             select new
                             {
                                 MC.MasterColorId,
                                 MC.ColorTitle,
                                 MC.ColorCode,
                                 MC.IsActive,
                                 MC.EnterById,
                                 MC.EnterDate,
                                 MC.ModifiedById,
                                 MC.ModifiedDate, 
                             });

                List<MasterColorResult> objMasterColorResultList = new List<MasterColorResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterColorResult = new MasterColorResult();

                    _MasterColorResult.MasterColorId = _Item.MasterColorId;
                    _MasterColorResult.ColorTitle = _Item.ColorTitle;
                    _MasterColorResult.ColorCode = _Item.ColorCode;
                    _MasterColorResult.IsActive = _Item.IsActive;
                    _MasterColorResult.ActiveColor = "green";
                    _MasterColorResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterColorResult.EnterById = _Item.EnterById;
                    _MasterColorResult.EnterDate = _Item.EnterDate;
                    _MasterColorResult.ModifiedById = _Item.ModifiedById;
                    _MasterColorResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterColorResult.IsActive == false)
                    {
                        _MasterColorResult.ActiveColor = "red";
                        _MasterColorResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterColorResultList.Add(_MasterColorResult);
                }
                return objMasterColorResultList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterColorResult GetADMasterColorByID(long MasterColorId)
        {
            try
            {
                var _data = (from MC in _Context.ADMasterColors
                             where MC.MasterColorId == MasterColorId
                             select new
                             {
                                 MC.MasterColorId,
                                 MC.ColorTitle,
                                 MC.ColorCode,
                                 MC.IsActive,
                                 MC.EnterById,
                                 MC.EnterDate,
                                 MC.ModifiedById,
                                 MC.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterColorResult _MasterColorResult = new MasterColorResult();
                if (_data != null)
                {
                    _MasterColorResult.MasterColorId = _Item.MasterColorId;
                    _MasterColorResult.ColorTitle = _Item.ColorTitle;
                    _MasterColorResult.ColorCode = _Item.ColorCode;
                    _MasterColorResult.IsActive = _Item.IsActive;
                    _MasterColorResult.ActiveColor = "green";
                    _MasterColorResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterColorResult.EnterById = _Item.EnterById;
                    _MasterColorResult.EnterDate = _Item.EnterDate;
                    _MasterColorResult.ModifiedById = _Item.ModifiedById;
                    _MasterColorResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterColorResult.IsActive == false)
                    {
                        _MasterColorResult.ActiveColor = "red";
                        _MasterColorResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                return _MasterColorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterColor(ADMasterColor objADMasterColor)
        {
            try
            {
                _Context.ADMasterColors.Add(objADMasterColor);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterColor(ADMasterColor objADMasterColor)
        {
            try
            {
                _Context.Entry(objADMasterColor).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterColor(long MasterColorId)
        {
            try
            {
                var objADMasterColor = _Context.ADMasterColors.Find(MasterColorId);
                _Context.ADMasterColors.Remove(objADMasterColor);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterColorExists(long MasterColorId)
        {
            try
            {
                return _Context.ADMasterColors.Any(e => e.MasterColorId == MasterColorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
