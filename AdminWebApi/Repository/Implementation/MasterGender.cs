using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterGender : IMasterGenderInterface<MasterGenderResult>
    {
        readonly AdminContext _Context;

        public MasterGender(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterGenderResult> GetAllADMasterGender()
        {
            try
            {
                var _data = _Context.ADMasterGenders.ToList();


                List<MasterGenderResult> objMasterGenderResulttList = new List<MasterGenderResult>();
                foreach (var _Item in _data)
                {
                    MasterGenderResult _objMasterGenderResult = new MasterGenderResult();

                    _objMasterGenderResult.MasterGenderId = _Item.MasterGenderId;
                    _objMasterGenderResult.GenderTitle = _Item.GenderTitle;
                    _objMasterGenderResult.Gendercode = _Item.Gendercode;
                    _objMasterGenderResult.GenderIcon = _Item.GenderIcon;

                    _objMasterGenderResult.IsActive = _Item.IsActive;
                    _objMasterGenderResult.ActiveColor = "green";
                    _objMasterGenderResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterGenderResult.IsActive == false)
                    {
                        _objMasterGenderResult.ActiveColor = "red";
                        _objMasterGenderResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterGenderResulttList.Add(_objMasterGenderResult);
                }

                return objMasterGenderResulttList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterGenderResult GetADMasterGenderByID(long MasterGenderId)
        {
            try
            {
                var _Item = _Context.ADMasterGenders.Find(MasterGenderId);

                MasterGenderResult _objMasterGenderResult = new MasterGenderResult();

                if (_Item != null)
                {
                    _objMasterGenderResult.MasterGenderId = _Item.MasterGenderId;
                    _objMasterGenderResult.GenderTitle = _Item.GenderTitle;
                    _objMasterGenderResult.Gendercode = _Item.Gendercode;
                    _objMasterGenderResult.GenderIcon = _Item.GenderIcon;

                    _objMasterGenderResult.IsActive = _Item.IsActive;
                    _objMasterGenderResult.ActiveColor = "green";
                    _objMasterGenderResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterGenderResult.IsActive == false)
                    {
                        _objMasterGenderResult.ActiveColor = "red";
                        _objMasterGenderResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterGenderResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterGender(ADMasterGender objADMasterGender)
        {
            try
            {
                _Context.ADMasterGenders.Add(objADMasterGender);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterGender(ADMasterGender objADMasterGender)
        {
            try
            {
                _Context.Entry(objADMasterGender).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterGender(long MasterGenderId)
        {
            try
            {
                ADMasterGender objADMasterGender = _Context.ADMasterGenders.Find(MasterGenderId);
                _Context.ADMasterGenders.Remove(objADMasterGender);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterGenderExists(long MasterGenderId)
        {
            try
            {
                return _Context.ADMasterGenders.Any(e => e.MasterGenderId == MasterGenderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
