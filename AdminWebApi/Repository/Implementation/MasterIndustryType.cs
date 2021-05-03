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
    public class MasterIndustryType : IMasterIndustryTypeInterface<MasterIndustryTypeResult>
    {
        readonly AdminContext _Context;

        public MasterIndustryType(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterIndustryTypeResult> GetAllADMasterIndustryType()
        {
            try
            {
                var _data = (from IT in _Context.ADMasterIndustryTypes

                             select new
                             {
                                 IT.MasterIndustryTypeId,
                                 IT.MasterIndustryGroupId,
                                 IT.IndustryTypeTitle,
                                 IT.IndustryTypeCode,
                                 IT.IndustryTypeDescription,
                                 IT.IsActive,
                                 IT.EnterById,
                                 IT.EnterDate,
                                 IT.ModifiedById,
                                 IT.ModifiedDate,
                             });

                List<MasterIndustryTypeResult> objMasterIndustryTypeResultList = new List<MasterIndustryTypeResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterIndustryTypeResult = new MasterIndustryTypeResult();

                    _MasterIndustryTypeResult.MasterIndustryTypeId = _Item.MasterIndustryTypeId;
                    _MasterIndustryTypeResult.MasterIndustryGroupId = _Item.MasterIndustryGroupId;
                    _MasterIndustryTypeResult.IndustryTypeTitle = _Item.IndustryTypeTitle;
                    _MasterIndustryTypeResult.IndustryTypeCode = _Item.IndustryTypeCode;
                    _MasterIndustryTypeResult.IndustryTypeDescription = _Item.IndustryTypeDescription;
                    _MasterIndustryTypeResult.EnterById = _Item.EnterById;
                    _MasterIndustryTypeResult.EnterDate = _Item.EnterDate;
                    _MasterIndustryTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustryTypeResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustryTypeResult.IsActive = _Item.IsActive;
                    _MasterIndustryTypeResult.ActiveColor = "green";
                    _MasterIndustryTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustryTypeResult.IsActive == false)
                    {
                        _MasterIndustryTypeResult.ActiveColor = "red";
                        _MasterIndustryTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterIndustryTypeResultList.Add(_MasterIndustryTypeResult);
                }

                return objMasterIndustryTypeResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public MasterIndustryTypeResult GetADMasterIndustryTypeByID(long MasterIndustryTypeId)
        {
            try
            {
                var _data = (from IT in _Context.ADMasterIndustryTypes
                             where IT.MasterIndustryTypeId == MasterIndustryTypeId
                             select new
                             {
                                 IT.MasterIndustryTypeId,
                                 IT.MasterIndustryGroupId,
                                 IT.IndustryTypeTitle,
                                 IT.IndustryTypeCode,
                                 IT.IndustryTypeDescription,
                                 IT.IsActive,
                                 IT.EnterById,
                                 IT.EnterDate,
                                 IT.ModifiedById,
                                 IT.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterIndustryTypeResult _MasterIndustryTypeResult = new MasterIndustryTypeResult();
                if (_data != null)
                {
                    _MasterIndustryTypeResult.MasterIndustryTypeId = _Item.MasterIndustryTypeId;
                    _MasterIndustryTypeResult.MasterIndustryGroupId = _Item.MasterIndustryGroupId;
                    _MasterIndustryTypeResult.IndustryTypeTitle = _Item.IndustryTypeTitle;
                    _MasterIndustryTypeResult.IndustryTypeCode = _Item.IndustryTypeCode;
                    _MasterIndustryTypeResult.IndustryTypeDescription = _Item.IndustryTypeDescription;
                    _MasterIndustryTypeResult.EnterById = _Item.EnterById;
                    _MasterIndustryTypeResult.EnterDate = _Item.EnterDate;
                    _MasterIndustryTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustryTypeResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustryTypeResult.IsActive = _Item.IsActive;
                    _MasterIndustryTypeResult.ActiveColor = "green";
                    _MasterIndustryTypeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustryTypeResult.IsActive == false)
                    {
                        _MasterIndustryTypeResult.ActiveColor = "red";
                        _MasterIndustryTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                return _MasterIndustryTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task InsertADMasterIndustryType(ADMasterIndustryType objADMasterIndustryType)
        {
            try
            {
                _Context.ADMasterIndustryTypes.Add(objADMasterIndustryType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterIndustryType(ADMasterIndustryType objADMasterIndustryType)
        {
            try
            {
                _Context.Entry(objADMasterIndustryType).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterIndustryType(long MasterIndustryTypeId)
        {
            try
            {
                var objADMasterIndustryType = _Context.ADMasterIndustryTypes.Find(MasterIndustryTypeId);
                _Context.ADMasterIndustryTypes.Remove(objADMasterIndustryType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterIndustryTypeExists(long MasterIndustryTypeId)
        {
            try
            {
                return _Context.ADMasterIndustryTypes.Any(e => e.MasterIndustryTypeId == MasterIndustryTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
