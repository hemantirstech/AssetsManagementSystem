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

    public class MasterBrand : IMasterBrandInterface<MasterBrandResult>
    {
        readonly AssetsContext _Context;

        public MasterBrand(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterBrandResult> GetAllASMasterBrand()
        {
            try
            {
                var _data = (from MB in _Context.ASMasterBrands

                             select new
                             {
                                 MB.MasterBrandId,
                                 MB.BrandTitle,
                                 MB.BrandLogo,
                                 MB.Sequence,
                                 MB.IsActive,
                                 MB.EnterById,
                                 MB.EnterDate,
                                 MB.ModifiedById,
                                 MB.ModifiedDate,



                             });

                List<MasterBrandResult> objMasterBrandResultList = new List<MasterBrandResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterBrandResult = new MasterBrandResult();

                    _MasterBrandResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterBrandResult.BrandTitle = _Item.BrandTitle;
                    _MasterBrandResult.BrandLogo = _Item.BrandLogo;
                    _MasterBrandResult.Sequence = _Item.Sequence;
                    _MasterBrandResult.IsActive = _Item.IsActive;
                    _MasterBrandResult.ActiveColor = "green";
                    _MasterBrandResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterBrandResult.EnterById = _Item.EnterById;
                    _MasterBrandResult.EnterDate = _Item.EnterDate;
                    _MasterBrandResult.ModifiedById = _Item.ModifiedById;
                    _MasterBrandResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterBrandResult.IsActive == false)
                    {
                        _MasterBrandResult.ActiveColor = "red";
                        _MasterBrandResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterBrandResultList.Add(_MasterBrandResult);
                }
                return objMasterBrandResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterBrandResult GetASMasterBrandByID(long MasterBrandId)
        {
            try
            {
                var _data = (from MB in _Context.ASMasterBrands
                             where MB.MasterBrandId == MasterBrandId
                             select new
                             {
                                 MB.MasterBrandId,
                                 MB.BrandTitle,
                                 MB.BrandLogo,
                                 MB.Sequence,
                                 MB.IsActive,
                                 MB.EnterById,
                                 MB.EnterDate,
                                 MB.ModifiedById,
                                 MB.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterBrandResult _MasterBrandResult = new MasterBrandResult();
                if (_Item != null)
                {
                    _MasterBrandResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterBrandResult.BrandTitle = _Item.BrandTitle;
                    _MasterBrandResult.BrandLogo = _Item.BrandLogo;
                    _MasterBrandResult.Sequence = _Item.Sequence;
                    _MasterBrandResult.IsActive = _Item.IsActive;
                    _MasterBrandResult.ActiveColor = "green";
                    _MasterBrandResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterBrandResult.EnterById = _Item.EnterById;
                    _MasterBrandResult.EnterDate = _Item.EnterDate;
                    _MasterBrandResult.ModifiedById = _Item.ModifiedById;
                    _MasterBrandResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterBrandResult.IsActive == false)
                    {
                        _MasterBrandResult.ActiveColor = "red";
                        _MasterBrandResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }
                return _MasterBrandResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterBrand(ASMasterBrand objADMasterBrand)
        {
            try
            {
                _Context.ASMasterBrands.Add(objADMasterBrand);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateASMasterBrand(ASMasterBrand objADMasterBrand)
        {
            try
            {
                _Context.Entry(objADMasterBrand).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterBrand(long MasterBrandId)
        {
            try
            {
                var objADMasterBrand = _Context.ASMasterBrands.Find(MasterBrandId);
                _Context.ASMasterBrands.Remove(objADMasterBrand);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterBrandExists(long MasterBrandId)
        {
            try
            {
                return _Context.ASMasterBrands.Any(e => e.MasterBrandId == MasterBrandId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
