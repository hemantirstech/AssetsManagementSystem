using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterDepartment : IMasterDepartmentInterface<MasterDepartmentResult>
    {

        readonly AdminContext _Context;

        public MasterDepartment(AdminContext context)
        {
            _Context = context;
        }


        public IEnumerable<MasterDepartmentResult> GetAllADMasterDepartment()
        {
            try
            {
                var _data = (from MD in _Context.ADMasterDepartments
                             select new
                             {
                                 MD.MasterDepartmentId,
                                 MD.DepartmentTitle,
                                 MD.DepartmentCode,
                                 MD.DepartmentDescription,
                                 MD.IsActive,
                                 MD.EnterById,
                                 MD.EnterDate,
                                 MD.ModifiedById,
                                 MD.ModifiedDate,
                             });

               // var _data = _Context.ADMasterDepartments.ToList();

                List<MasterDepartmentResult> objMasterDepartmentResultList = new List<MasterDepartmentResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterDepartmentResult = new MasterDepartmentResult();

                    _MasterDepartmentResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterDepartmentResult.DepartmentTitle = _Item.DepartmentTitle;
                    _MasterDepartmentResult.DepartmentCode = _Item.DepartmentCode;
                    _MasterDepartmentResult.DepartmentDescription = _Item.DepartmentDescription;                    
                    _MasterDepartmentResult.EnterById = _Item.EnterById;
                    _MasterDepartmentResult.EnterDate = _Item.EnterDate;
                    _MasterDepartmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterDepartmentResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterDepartmentResult.IsActive = _Item.IsActive;

                    _MasterDepartmentResult.ActiveColor = "green";
                    _MasterDepartmentResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterDepartmentResult.IsActive == false)
                    {
                        _MasterDepartmentResult.ActiveColor = "red";
                        _MasterDepartmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterDepartmentResultList.Add(_MasterDepartmentResult);
                }

                return objMasterDepartmentResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterDepartmentResult GetADMasterDepartmentByID(long MasterDepartmentId)
        {
            try
            {
                var _data = (from MD in _Context.ADMasterDepartments
                             where MD.MasterDepartmentId == MasterDepartmentId
                             select new
                             {
                                 MD.MasterDepartmentId,
                                 MD.DepartmentTitle,
                                 MD.DepartmentCode,
                                 MD.DepartmentDescription,
                                 MD.IsActive,
                                 MD.EnterById,
                                 MD.EnterDate,
                                 MD.ModifiedById,
                                 MD.ModifiedDate,
                             });

                var _Item = _data.Where(a=>a.MasterDepartmentId== MasterDepartmentId).FirstOrDefault();

                MasterDepartmentResult _MasterDepartmentResult = new MasterDepartmentResult();
                if (_data != null)
                {
                    _MasterDepartmentResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterDepartmentResult.DepartmentTitle = _Item.DepartmentTitle;
                    _MasterDepartmentResult.DepartmentCode = _Item.DepartmentCode;
                    _MasterDepartmentResult.DepartmentDescription = _Item.DepartmentDescription;
                    _MasterDepartmentResult.EnterById = _Item.EnterById;
                    _MasterDepartmentResult.EnterDate = _Item.EnterDate;
                    _MasterDepartmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterDepartmentResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterDepartmentResult.IsActive = _Item.IsActive;
                  
                    if (_MasterDepartmentResult.IsActive == false)
                    {
                        _MasterDepartmentResult.ActiveColor = "red";
                        _MasterDepartmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterDepartmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterDepartment(ADMasterDepartment objADMasterDepartment)
        {
            try
            {
                _Context.ADMasterDepartments.Add(objADMasterDepartment);
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterDepartment(ADMasterDepartment objADMasterDepartment)
        {
            try
            {
                _Context.Entry(objADMasterDepartment).State = EntityState.Modified;
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterDepartment(long MasterDepartmentId)
        {
            try
            {
                var objADMasterDepartment = _Context.ADMasterDepartments.Find(MasterDepartmentId);
                _Context.ADMasterDepartments.Remove(objADMasterDepartment);
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterDepartmentExists(long MasterDepartmentId)
        {
            try
            {
                return _Context.ADMasterDepartments.Any(e => e.MasterDepartmentId == MasterDepartmentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
