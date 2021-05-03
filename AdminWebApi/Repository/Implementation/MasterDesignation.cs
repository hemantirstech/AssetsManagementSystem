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
    public class MasterDesignation : IMasterDesignationInterface<MasterDesignationResult>
    {
        readonly AdminContext _Context;

        public MasterDesignation(AdminContext context)
        {
            _Context = context;
        }



        public IEnumerable<MasterDesignationResult> GetAllADMasterDesignation()
        {
            try
            {
                //var _data = (from MD in _Context.ADMasterDesignations
                //             select new
                //             {
                //                 MD.MasterDesignationId,
                //                 MD.DesignationTitle,
                //                 MD.DesignationCode,
                //                 MD.DesignationDescription,
                //                 MD.MasterDepartmentId,
                //                 MD.IsActive,
                //                 MD.EnterById,
                //                 MD.EnterDate,
                //                 MD.ModifiedById,
                //                 MD.ModifiedDate,
                //             });

                var _data = _Context.ADMasterDesignations.ToList();

                List<MasterDesignationResult> objMasterDesignationResultList = new List<MasterDesignationResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterDesignationResult = new MasterDesignationResult();

                    _MasterDesignationResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterDesignationResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterDesignationResult.DesignationCode = _Item.DesignationCode;
                    _MasterDesignationResult.DesignationDescription = _Item.DesignationDescription;
                    _MasterDesignationResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterDesignationResult.EnterById = _Item.EnterById;
                    _MasterDesignationResult.EnterDate = _Item.EnterDate;
                    _MasterDesignationResult.ModifiedById = _Item.ModifiedById;
                    _MasterDesignationResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterDesignationResult.IsActive = _Item.IsActive;

                    _MasterDesignationResult.ActiveColor = "green";
                    _MasterDesignationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterDesignationResult.IsActive == false)
                    {
                        _MasterDesignationResult.ActiveColor = "red";
                        _MasterDesignationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterDesignationResultList.Add(_MasterDesignationResult);
                }

                return objMasterDesignationResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterDesignationResult GetADMasterDesignationByID(long MasterDesignationId)
        {
            try
            {
                var _data = (from MD in _Context.ADMasterDesignations
                             select new
                             {
                                 MD.MasterDesignationId,
                                 MD.DesignationTitle,
                                 MD.DesignationCode,
                                 MD.DesignationDescription,
                                 MD.MasterDepartmentId,
                                 MD.IsActive,
                                 MD.EnterById,
                                 MD.EnterDate,
                                 MD.ModifiedById,
                                 MD.ModifiedDate,
                             });

                var _Item = _data.Where(a => a.MasterDesignationId == MasterDesignationId).FirstOrDefault();

                MasterDesignationResult _MasterDesignationResult = new MasterDesignationResult();
                if (_data != null)
                {
                    _MasterDesignationResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterDesignationResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterDesignationResult.DesignationCode = _Item.DesignationCode;
                    _MasterDesignationResult.DesignationDescription = _Item.DesignationDescription;
                    _MasterDesignationResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterDesignationResult.EnterById = _Item.EnterById;
                    _MasterDesignationResult.EnterDate = _Item.EnterDate;
                    _MasterDesignationResult.ModifiedById = _Item.ModifiedById;
                    _MasterDesignationResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterDesignationResult.IsActive = _Item.IsActive;

                    if (_MasterDesignationResult.IsActive == false)
                    {
                        _MasterDesignationResult.ActiveColor = "red";
                        _MasterDesignationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterDesignationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterDesignation(ADMasterDesignation objADMasterDesignation)
        {
            try
            {
                _Context.ADMasterDesignations.Add(objADMasterDesignation);
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterDesignation(ADMasterDesignation objADMasterDesignation)
        {
            try
            {
                _Context.Entry(objADMasterDesignation).State = EntityState.Modified;
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterDesignation(long MasterDesignationId)
        {
            try
            {
                var objADMasterDesignation = _Context.ADMasterDesignations.Find(MasterDesignationId);
                _Context.ADMasterDesignations.Remove(objADMasterDesignation);
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterDesignationExists(long MasterDesignationId)
        {
            try
            {
                return _Context.ADMasterDesignations.Any(e => e.MasterDesignationId == MasterDesignationId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
