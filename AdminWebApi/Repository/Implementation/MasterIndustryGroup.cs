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
    public class MasterIndustryGroup : IMasterIndustryGroupInterface<MasterIndustryGroupResult>
    {
        readonly AdminContext _Context;

        public MasterIndustryGroup(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterIndustryGroupResult> GetAllADMasterIndustryGroup()
        {
            try
            {
                var _data = (from MI in _Context.ADMasterIndustryGroups
                             
                             select new
                             {
                                 MI.MasterIndustryGroupId,
                                 MI.IndustryGroupTitle,
                                 MI.IndustryGroupCode,
                                 MI.IndustryGroupDescription,
                                 MI.IsActive,
                                 MI.EnterById,
                                 MI.EnterDate,
                                 MI.ModifiedById,
                                 MI.ModifiedDate,
                             });

                List<MasterIndustryGroupResult> objMasterIndustryGroupResult = new List<MasterIndustryGroupResult>();
                
                foreach (var _Item in _data.ToList())
                {
                    var _MasterIndustryGroupResult = new MasterIndustryGroupResult();

                    _MasterIndustryGroupResult.MasterIndustryGroupId = _Item.MasterIndustryGroupId;
                    _MasterIndustryGroupResult.IndustryGroupTitle = _Item.IndustryGroupTitle;
                    _MasterIndustryGroupResult.IndustryGroupCode = _Item.IndustryGroupCode;
                    _MasterIndustryGroupResult.IndustryGroupDescription = _Item.IndustryGroupDescription;
                    _MasterIndustryGroupResult.EnterById = _Item.EnterById;
                    _MasterIndustryGroupResult.EnterDate = _Item.EnterDate;
                    _MasterIndustryGroupResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustryGroupResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustryGroupResult.IsActive = _Item.IsActive;
                    _MasterIndustryGroupResult.ActiveColor = "green";
                    _MasterIndustryGroupResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustryGroupResult.IsActive == false)
                    {
                        _MasterIndustryGroupResult.ActiveColor = "red";
                        _MasterIndustryGroupResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterIndustryGroupResult.Add(_MasterIndustryGroupResult);
                }

                return objMasterIndustryGroupResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterIndustryGroupResult GetADMasterIndustryGroupByID(long MasterIndustryGroupId)
        {
            try
            {
                var _data = (from MI in _Context.ADMasterIndustryGroups

                             where MI.MasterIndustryGroupId == MasterIndustryGroupId
                             select new
                             {
                                 MI.MasterIndustryGroupId,
                                 MI.IndustryGroupTitle,
                                 MI.IndustryGroupCode,
                                 MI.IndustryGroupDescription,
                                 MI.IsActive,
                                 MI.EnterById,
                                 MI.EnterDate,
                                 MI.ModifiedById,
                                 MI.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterIndustryGroupResult _MasterIndustryGroupResult = new MasterIndustryGroupResult();
                if (_data != null)
                {
                    _MasterIndustryGroupResult.MasterIndustryGroupId = _Item.MasterIndustryGroupId;
                    _MasterIndustryGroupResult.IndustryGroupTitle = _Item.IndustryGroupTitle;
                    _MasterIndustryGroupResult.IndustryGroupCode = _Item.IndustryGroupCode;
                    _MasterIndustryGroupResult.IndustryGroupDescription = _Item.IndustryGroupDescription;
                    _MasterIndustryGroupResult.EnterById = _Item.EnterById;
                    _MasterIndustryGroupResult.EnterDate = _Item.EnterDate;
                    _MasterIndustryGroupResult.ModifiedById = _Item.ModifiedById;
                    _MasterIndustryGroupResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterIndustryGroupResult.IsActive = _Item.IsActive;
                    _MasterIndustryGroupResult.ActiveColor = "green";
                    _MasterIndustryGroupResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterIndustryGroupResult.IsActive == false)
                    {
                        _MasterIndustryGroupResult.ActiveColor = "red";
                        _MasterIndustryGroupResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterIndustryGroupResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterIndustryGroup(ADMasterIndustryGroup objADMasterIndustryGroup)
        {
            try
            {
                _Context.ADMasterIndustryGroups.Add(objADMasterIndustryGroup);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterIndustryGroup(ADMasterIndustryGroup objADMasterIndustryGroup)
        {
            try
            {
                _Context.Entry(objADMasterIndustryGroup).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterIndustryGroup(long MasterIndustryGroupId)
        {
            try
            {
                var objADMasterIndustryGroup = _Context.ADMasterIndustryGroups.Find(MasterIndustryGroupId);
                _Context.ADMasterIndustryGroups.Remove(objADMasterIndustryGroup);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterIndustryGroupExists(long MasterIndustryGroupId)
        {
            try
            {
                return _Context.ADMasterIndustryGroups.Any(e => e.MasterIndustryGroupId == MasterIndustryGroupId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
