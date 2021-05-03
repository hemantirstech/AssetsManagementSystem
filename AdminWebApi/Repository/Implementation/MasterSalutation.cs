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
    public class MasterSalutation : IMasterSalutationInterface<MasterSalutationResult>
    {

        readonly AdminContext _Context;

        public MasterSalutation(AdminContext context)
        {
            _Context = context;
        }


        public IEnumerable<MasterSalutationResult> GetAllADMasterSalutation()
        {
            try
            {
                //var _data = (from MS in _Context.ADMasterSalutations
                //             select new
                //             {
                //                 MS.MasterSalutationId,
                //                 MS.SalutationTitle,
                //                 MS.SalutationCode,
                //                 MS.IsActive,
                //                 MS.EnterById,
                //                 MS.EnterDate,
                //                 MS.ModifiedById,
                //                 MS.ModifiedDate,
                //             });

                var _data = _Context.ADMasterSalutations.ToList();

                List<MasterSalutationResult> objMasterSalutationResultList = new List<MasterSalutationResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterSalutationResult = new MasterSalutationResult();

                    _MasterSalutationResult.MasterSalutationId = _Item.MasterSalutationId;
                    _MasterSalutationResult.SalutationTitle = _Item.SalutationTitle;
                    _MasterSalutationResult.SalutationCode = _Item.SalutationCode;                    
                    _MasterSalutationResult.EnterById = _Item.EnterById;
                    _MasterSalutationResult.EnterDate = _Item.EnterDate;
                    _MasterSalutationResult.ModifiedById = _Item.ModifiedById;
                    _MasterSalutationResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterSalutationResult.IsActive = _Item.IsActive;

                    _MasterSalutationResult.ActiveColor = "green";
                    _MasterSalutationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterSalutationResult.IsActive == false)
                    {
                        _MasterSalutationResult.ActiveColor = "red";
                        _MasterSalutationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterSalutationResultList.Add(_MasterSalutationResult);
                }

                return objMasterSalutationResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterSalutationResult GetADMasterSalutationByID(long MasterSalutationId)
        {
            try
            {
                var _data = (from MS in _Context.ADMasterSalutations
                             where MS.MasterSalutationId == MasterSalutationId
                             select new
                             {
                                 MS.MasterSalutationId,
                                 MS.SalutationTitle,
                                 MS.SalutationCode,
                                 MS.IsActive,
                                 MS.EnterById,
                                 MS.EnterDate,
                                 MS.ModifiedById,
                                 MS.ModifiedDate,
                             });

                var _Item = _data.Where(a => a.MasterSalutationId == MasterSalutationId).FirstOrDefault();

                MasterSalutationResult _MasterSalutationResult = new MasterSalutationResult();
                if (_data != null)
                {
                    _MasterSalutationResult.MasterSalutationId = _Item.MasterSalutationId;
                    _MasterSalutationResult.SalutationTitle = _Item.SalutationTitle;
                    _MasterSalutationResult.SalutationCode = _Item.SalutationCode;          
                    _MasterSalutationResult.EnterById = _Item.EnterById;
                    _MasterSalutationResult.EnterDate = _Item.EnterDate;
                    _MasterSalutationResult.ModifiedById = _Item.ModifiedById;
                    _MasterSalutationResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterSalutationResult.IsActive = _Item.IsActive;

                    if (_MasterSalutationResult.IsActive == false)
                    {
                        _MasterSalutationResult.ActiveColor = "red";
                        _MasterSalutationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterSalutationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterSalutation(ADMasterSalutation objADMasterSalutation)
        {
            try
            {
                _Context.ADMasterSalutations.Add(objADMasterSalutation);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateADMasterSalutation(ADMasterSalutation objADMasterSalutation)
        {
            try
            {
                _Context.Entry(objADMasterSalutation).State = EntityState.Modified;
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteADMasterSalutation(long MasterSalutationId)
        {
            try
            {
                var objADMasterSalutation = _Context.ADMasterSalutations.Find(MasterSalutationId);
                _Context.ADMasterSalutations.Remove(objADMasterSalutation);
               await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ADMasterSalutationExists(long MasterSalutationId)
        {
            try
            {
                return _Context.ADMasterSalutations.Any(e => e.MasterSalutationId == MasterSalutationId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
