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
    public class MasterIndustrySubType : IMasterIndustrySubTypeInterface<MasterIndustrySubTypeResult>
    {
        readonly AdminContext _Context;

        public MasterIndustrySubType(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterIndustrySubTypeResult> GetAllADMasterIndustrySubType()
        {
            try
            {
                var _data = (from MI in _Context.ADMasterIndustrySubTypes
                             
                             select new
                             {
                                 MI.MasterIndustrySubTypeId,
                                 MI.MasterIndustryTypeId,
                                 MI.IndustrySubTypeTitle,
                                 MI.IndustrySubTypeCode,
                                 MI.IndustrySubTypeDescription,
                                 MI.IsActive,
                                 MI.EnterById,
                                 MI.EnterDate,
                                 MI.ModifiedById,
                                 MI.ModifiedDate
                             });

                List<MasterIndustrySubTypeResult> objMasterIndustrySubTypeResultList = new List<MasterIndustrySubTypeResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterIndustrySubTypeResult = new MasterIndustrySubTypeResult();

                    _MasterIndustrySubTypeResult.MasterIndustrySubTypeId = _Item.MasterIndustrySubTypeId;
                    _MasterIndustrySubTypeResult.MasterIndustryTypeId = _Item.MasterIndustryTypeId;
                    _MasterIndustrySubTypeResult.IndustrySubTypeTitle = _Item.IndustrySubTypeTitle;
                    _MasterIndustrySubTypeResult.IndustrySubTypeCode = _Item.IndustrySubTypeCode;
                    _MasterIndustrySubTypeResult.IndustrySubTypeDescription = _Item.IndustrySubTypeDescription;
                    _MasterIndustrySubTypeResult.EnterById = _Item.EnterById;
                    _MasterIndustrySubTypeResult.EnterDate = _Item.EnterDate;
                    _MasterIndustrySubTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustrySubTypeResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustrySubTypeResult.IsActive = _Item.IsActive;
                    _MasterIndustrySubTypeResult.ActiveColor = "green";
                    _MasterIndustrySubTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustrySubTypeResult.IsActive == false)
                    {
                        _MasterIndustrySubTypeResult.ActiveColor = "red";
                        _MasterIndustrySubTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterIndustrySubTypeResultList.Add(_MasterIndustrySubTypeResult);
                }
                return objMasterIndustrySubTypeResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterIndustrySubTypeResult GetADMasterIndustrySubTypeByID(long MasterIndustrySubTypeId)
        {
            try
            {
                var _data = (from MI in _Context.ADMasterIndustrySubTypes
                             
                             where MI.MasterIndustrySubTypeId == MasterIndustrySubTypeId
                             select new
                             {
                                 MI.MasterIndustrySubTypeId,
                                 MI.MasterIndustryTypeId,
                                 MI.IndustrySubTypeTitle,
                                 MI.IndustrySubTypeCode,
                                 MI.IndustrySubTypeDescription,
                                 MI.IsActive,
                                 MI.EnterById,
                                 MI.EnterDate,
                                 MI.ModifiedById,
                                 MI.ModifiedDate
                             });


                var _Item = _data.FirstOrDefault();

                MasterIndustrySubTypeResult _MasterIndustrySubTypeResult = new MasterIndustrySubTypeResult();
                if (_data != null)
                {
                    _MasterIndustrySubTypeResult.MasterIndustrySubTypeId = _Item.MasterIndustrySubTypeId;
                    _MasterIndustrySubTypeResult.MasterIndustryTypeId = _Item.MasterIndustryTypeId;
                    _MasterIndustrySubTypeResult.IndustrySubTypeTitle = _Item.IndustrySubTypeTitle;
                    _MasterIndustrySubTypeResult.IndustrySubTypeCode = _Item.IndustrySubTypeCode;
                    _MasterIndustrySubTypeResult.IndustrySubTypeDescription = _Item.IndustrySubTypeDescription;
                    _MasterIndustrySubTypeResult.EnterById = _Item.EnterById;
                    _MasterIndustrySubTypeResult.EnterDate = _Item.EnterDate;
                    _MasterIndustrySubTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustrySubTypeResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustrySubTypeResult.IsActive = _Item.IsActive;
                    _MasterIndustrySubTypeResult.ActiveColor = "green";
                    _MasterIndustrySubTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustrySubTypeResult.IsActive == false)
                    {
                        _MasterIndustrySubTypeResult.ActiveColor = "red";
                        _MasterIndustrySubTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterIndustrySubTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterIndustrySubType(ADMasterIndustrySubType objADMasterIndustrySubType)
        {
            try
            {
                _Context.ADMasterIndustrySubTypes.Add(objADMasterIndustrySubType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterIndustrySubType(ADMasterIndustrySubType objADMasterIndustrySubType)
        {
            try
            {
                _Context.Entry(objADMasterIndustrySubType).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterIndustrySubType(long MasterIndustrySubTypeId)
        {
            try
            {
                var objADMasterIndustrySubType = _Context.ADMasterIndustrySubTypes.Find(MasterIndustrySubTypeId);
                _Context.ADMasterIndustrySubTypes.Remove(objADMasterIndustrySubType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterIndustrySubTypeExists(long MasterIndustrySubTypeId)
        {
            try
            {
                return _Context.ADMasterIndustrySubTypes.Any(e => e.MasterIndustrySubTypeId == MasterIndustrySubTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
