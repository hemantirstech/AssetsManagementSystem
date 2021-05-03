using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterProfile : IMasterProfileInterface<MasterProfileResult>
    {
        readonly AdminContext _Context;
        public MasterProfile(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterProfileResult> GetAllADMasterProfile()
        {
            try
            {
                //var _data = _Context.ADMasterProfiles.Join(_Context.ADMasterProfiles, ADMasterProfile=> ADMasterProfile.ParentMasterProfileId, ADMasterProfilesNew => ADMasterProfilesNew.MasterProfileId,(ADMasterProfiles, ADMasterProfilesNew) => new { ADMasterProfiles, ADMasterProfilesNew }).Select(a=> new { a.ADMasterProfiles, ParentFunctionTitle= a.ADMasterProfilesNew.FunctionTitle}).ToList();

                var _data = _Context.ADMasterProfiles.ToList();

                List<MasterProfileResult> objMasterProfileResultList = new List<MasterProfileResult>();

                foreach (var Item in _data.ToList())
                {
                    var _MasterProfileResult = new MasterProfileResult
                    {
                        MasterProfileId = Item.MasterProfileId,
                        ProfileTitle = Item.ProfileTitle,
                        ProfileDescription = Item.ProfileDescription,
                        ProfileCode = Item.ProfileCode,

                        IsDelete = Item.IsDelete,
                        DeleteColor = "green",
                        DeleteIcon = "glyphicon glyphicon-ok",

                        IsInsert = Item.IsInsert,
                        InsertColor = "green",
                        InsertIcon = "glyphicon glyphicon-ok",

                        IsUpdate = Item.IsUpdate,
                        UpdateColor = "green",
                        UpdateIcon = "glyphicon glyphicon-ok",

                        IsSelect = Item.IsSelect,
                        SelectColor = "green",
                        SelectIcon = "glyphicon glyphicon-ok",

                        IsActive = Item.IsActive,
                        ActiveColor = "green",
                        ActiveIcon = "glyphicon glyphicon-ok"
                    };

                    if (_MasterProfileResult.IsDelete == false)
                    {
                        _MasterProfileResult.DeleteColor = "red";
                        _MasterProfileResult.DeleteIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsInsert == false)
                    {
                        _MasterProfileResult.InsertColor = "red";
                        _MasterProfileResult.InsertIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsUpdate == false)
                    {
                        _MasterProfileResult.UpdateColor = "red";
                        _MasterProfileResult.UpdateIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsSelect == false)
                    {
                        _MasterProfileResult.SelectColor = "red";
                        _MasterProfileResult.SelectIcon = "glyphicon glyphicon-remove";
                    }
                    if (_MasterProfileResult.IsActive == false)
                    {
                        _MasterProfileResult.ActiveColor = "red";
                        _MasterProfileResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProfileResultList.Add(_MasterProfileResult);
                }

                return objMasterProfileResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public MasterProfileResult GetADMasterProfileByID(long MasterProfileId)
        {
            try
            {
                var _Item = _Context.ADMasterProfiles.Find(MasterProfileId);

                var _MasterProfileResult = new MasterProfileResult();

                if (_Item != null)
                {
                    _MasterProfileResult.MasterProfileId = _Item.MasterProfileId;
                    _MasterProfileResult.ProfileTitle = _Item.ProfileTitle;
                    _MasterProfileResult.ProfileDescription = _Item.ProfileDescription;
                    _MasterProfileResult.ProfileCode = _Item.ProfileCode;

                    _MasterProfileResult.IsDelete = _Item.IsDelete;
                    _MasterProfileResult.DeleteColor = "green";
                    _MasterProfileResult.DeleteIcon = "glyphicon glyphicon-ok";

                    _MasterProfileResult.IsInsert = _Item.IsInsert;
                    _MasterProfileResult.InsertColor = "green";
                    _MasterProfileResult.InsertIcon = "glyphicon glyphicon-ok";

                    _MasterProfileResult.IsUpdate = _Item.IsUpdate;
                    _MasterProfileResult.UpdateColor = "green";
                    _MasterProfileResult.UpdateIcon = "glyphicon glyphicon-ok";

                    _MasterProfileResult.IsSelect = _Item.IsSelect;
                    _MasterProfileResult.SelectColor = "green";
                    _MasterProfileResult.SelectIcon = "glyphicon glyphicon-ok";

                    _MasterProfileResult.IsActive = _Item.IsActive;
                    _MasterProfileResult.ActiveColor = "green";
                    _MasterProfileResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterProfileResult.IsDelete == false)
                    {
                        _MasterProfileResult.DeleteColor = "red";
                        _MasterProfileResult.DeleteIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsInsert == false)
                    {
                        _MasterProfileResult.InsertColor = "red";
                        _MasterProfileResult.InsertIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsUpdate == false)
                    {
                        _MasterProfileResult.UpdateColor = "red";
                        _MasterProfileResult.UpdateIcon = "glyphicon glyphicon-remove";
                    }

                    if (_MasterProfileResult.IsSelect == false)
                    {
                        _MasterProfileResult.SelectColor = "red";
                        _MasterProfileResult.SelectIcon = "glyphicon glyphicon-remove";
                    }
                    if (_MasterProfileResult.IsActive == false)
                    {
                        _MasterProfileResult.ActiveColor = "red";
                        _MasterProfileResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterProfileResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterProfile(ADMasterProfile objADMasterProfile)
        {
            try
            {
                _Context.ADMasterProfiles.Add(objADMasterProfile);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       

        public async Task UpdateADMasterProfile(ADMasterProfile objADMasterProfile)
        {
            try
            {
                _Context.Entry(objADMasterProfile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterProfile(long MasterProfileId)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    //Delete All related record belongs to MasterProfileID from ADProfileTaskMappings Table
                    _Context.ADProfileTaskMappings.RemoveRange(_Context.ADProfileTaskMappings.Where(a => a.MasterProfileId == MasterProfileId));
                    await _Context.SaveChangesAsync();

                    ADMasterProfile objADMasterProfile = _Context.ADMasterProfiles.Find(MasterProfileId);

                    _Context.ADMasterProfiles.Remove(objADMasterProfile);
                    await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public bool ADMasterProfileExists(long id)
        {
            try
            {
                return _Context.ADMasterProfiles.Any(e => e.MasterProfileId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
