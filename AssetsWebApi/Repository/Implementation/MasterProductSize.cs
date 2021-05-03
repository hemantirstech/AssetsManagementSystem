using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;
using AssetsWebApi.Model;
using AssetsWebApi.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AssetsWebApi.Repository.Implementation
{
    public class MasterProductSize : IMasterProductSizeInterface<MasterProductSizeResult>
    {
        readonly AssetsContext _Context;

        public MasterProductSize(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterProductSizeResult> GetAllASMasterProductSize()
        {
            try
            {
                var _data = (from MPS in _Context.ASMasterProductSizes

                             select new
                             {
                                 MPS.MasterProductSizeId,
                                 MPS.ProductSizeTitle,
                                 MPS.ASMasterProducts,                                
                                 MPS.Sequence,
                                 MPS.IsActive,
                                 MPS.EnterById,
                                 MPS.EnterDate,
                                 MPS.ModifiedById,
                                 MPS.ModifiedDate,

                              

                             });

                List<MasterProductSizeResult> objMasterProductSizeResultList = new List<MasterProductSizeResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductSizeResult = new MasterProductSizeResult();

                    _MasterProductSizeResult.MasterProductSizeId = _Item.MasterProductSizeId;
                    _MasterProductSizeResult.ProductSizeTitle = _Item.ProductSizeTitle;                   
                    _MasterProductSizeResult.Sequence = _Item.Sequence;
                    _MasterProductSizeResult.IsActive = _Item.IsActive;
                    _MasterProductSizeResult.ActiveColor = "green";
                    _MasterProductSizeResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductSizeResult.EnterById = _Item.EnterById;
                    _MasterProductSizeResult.EnterDate = _Item.EnterDate;
                    _MasterProductSizeResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductSizeResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductSizeResult.IsActive == false)
                    {
                        _MasterProductSizeResult.ActiveColor = "red";
                        _MasterProductSizeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProductSizeResultList.Add(_MasterProductSizeResult);
                }
                return objMasterProductSizeResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterProductSizeResult GetASMasterProductSizeByID(long MasterProductSizeId)
        {
            try
            {
                var _data = (from MPS in _Context.ASMasterProductSizes
                             where MPS.MasterProductSizeId == MasterProductSizeId
                             select new
                             {
                                 MPS.MasterProductSizeId,
                                 MPS.ProductSizeTitle,
                                 MPS.ASMasterProducts,
                                 MPS.Sequence,
                                 MPS.IsActive,
                                 MPS.EnterById,
                                 MPS.EnterDate,
                                 MPS.ModifiedById,
                                 MPS.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterProductSizeResult _MasterProductSizeResult = new MasterProductSizeResult();
                if (_Item != null)
                {
                    _MasterProductSizeResult.MasterProductSizeId = _Item.MasterProductSizeId;
                    _MasterProductSizeResult.ProductSizeTitle = _Item.ProductSizeTitle;
                    _MasterProductSizeResult.Sequence = _Item.Sequence;
                    _MasterProductSizeResult.IsActive = _Item.IsActive;
                    _MasterProductSizeResult.ActiveColor = "green";
                    _MasterProductSizeResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductSizeResult.EnterById = _Item.EnterById;
                    _MasterProductSizeResult.EnterDate = _Item.EnterDate;
                    _MasterProductSizeResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductSizeResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductSizeResult.IsActive == false)
                    {
                        _MasterProductSizeResult.ActiveColor = "red";
                        _MasterProductSizeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }
                return _MasterProductSizeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterProductSize(ASMasterProductSize objADMasterProductSize)
        {
            try
            {
                _Context.ASMasterProductSizes.Add(objADMasterProductSize);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateASMasterProductSize(ASMasterProductSize objADMasterProductSize)
        {
            try
            {
                _Context.Entry(objADMasterProductSize).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterProductSize(long MasterProductSizeId)
        {
            try
            {
                var objADMasterProductSize = _Context.ASMasterProductSizes.Find(MasterProductSizeId);
                _Context.ASMasterProductSizes.Remove(objADMasterProductSize);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterProductSizeExists(long MasterProductSizeId)
        {
            try
            {
                return _Context.ASMasterProductSizes.Any(e => e.MasterProductSizeId == MasterProductSizeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
