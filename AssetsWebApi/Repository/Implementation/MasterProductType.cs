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
    public class MasterProductType : IMasterProductTypeInterface<MasterProductTypeResult>
    {
        readonly AssetsContext _Context;

        public MasterProductType(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterProductTypeResult> GetAllASMasterProductType()
        {
            try
            {
                var _data = (from MPT in _Context.ASMasterProductTypes

                             select new
                             {
                                 MPT.MasterProductTypeId,
                                 MPT.ProductTypeCode,
                                 MPT.ProductTypeTitle,
                                 MPT.ProductTypeDescription,
                                 MPT.ProductTypeImage,
                                 MPT.ASMasterSubCategory,

                                 MPT.Sequence,
                                 MPT.IsActive,
                                 MPT.EnterById,
                                 MPT.EnterDate,
                                 MPT.ModifiedById,
                                 MPT.ModifiedDate,



                             });

                List<MasterProductTypeResult> objMasterProductTypeResultList = new List<MasterProductTypeResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductTypeResult = new MasterProductTypeResult();

                    _MasterProductTypeResult.MasterProductTypeId = _Item.MasterProductTypeId;
                    _MasterProductTypeResult.ProductTypeTitle = _Item.ProductTypeTitle;
                    _MasterProductTypeResult.ProductTypeCode = _Item.ProductTypeCode;
                    _MasterProductTypeResult.ProductTypeDescription = _Item.ProductTypeDescription;
                    _MasterProductTypeResult.ProductTypeImage = _Item.ProductTypeImage;
                   // _MasterProductTypeResult.ASMasterSubCategory = _Item.ASMasterSubCategory;

                    _MasterProductTypeResult.Sequence = _Item.Sequence;
                    _MasterProductTypeResult.IsActive = _Item.IsActive;
                    _MasterProductTypeResult.ActiveColor = "green";
                    _MasterProductTypeResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductTypeResult.EnterById = _Item.EnterById;
                    _MasterProductTypeResult.EnterDate = _Item.EnterDate;
                    _MasterProductTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductTypeResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductTypeResult.IsActive == false)
                    {
                        _MasterProductTypeResult.ActiveColor = "red";
                        _MasterProductTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProductTypeResultList.Add(_MasterProductTypeResult);
                }
                return objMasterProductTypeResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterProductTypeResult GetASMasterProductTypeByID(long MasterProductTypeId)
        {
            try
            {
                var _data = (from MPT in _Context.ASMasterProductTypes
                             where MPT.MasterProductTypeId == MasterProductTypeId
                             select new
                             {
                                 MPT.MasterProductTypeId,
                                 MPT.ProductTypeCode,
                                 MPT.ProductTypeTitle,
                                 MPT.ProductTypeDescription,
                                 MPT.ProductTypeImage,
                                 MPT.ASMasterSubCategory,

                                 MPT.Sequence,
                                 MPT.IsActive,
                                 MPT.EnterById,
                                 MPT.EnterDate,
                                 MPT.ModifiedById,
                                 MPT.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterProductTypeResult _MasterProductTypeResult = new MasterProductTypeResult();
                if (_Item != null)
                {
                    _MasterProductTypeResult.MasterProductTypeId = _Item.MasterProductTypeId;
                    _MasterProductTypeResult.ProductTypeTitle = _Item.ProductTypeTitle;
                    _MasterProductTypeResult.ProductTypeCode = _Item.ProductTypeCode;
                    _MasterProductTypeResult.ProductTypeDescription = _Item.ProductTypeDescription;
                    _MasterProductTypeResult.ProductTypeImage = _Item.ProductTypeImage;
                 // _MasterProductTypeResult.ASMasterSubCategory = _Item.ASMasterSubCategory;
                    _MasterProductTypeResult.Sequence = _Item.Sequence;
                    _MasterProductTypeResult.IsActive = _Item.IsActive;
                    _MasterProductTypeResult.ActiveColor = "green";
                    _MasterProductTypeResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductTypeResult.EnterById = _Item.EnterById;
                    _MasterProductTypeResult.EnterDate = _Item.EnterDate;
                    _MasterProductTypeResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductTypeResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductTypeResult.IsActive == false)
                    {
                        _MasterProductTypeResult.ActiveColor = "red";
                        _MasterProductTypeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }
                return _MasterProductTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterProductType(ASMasterProductType objADMasterProductType)
        {
            try
            {
                _Context.ASMasterProductTypes.Add(objADMasterProductType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateASMasterProductType(ASMasterProductType objADMasterProductType)
        {
            try
            {
                _Context.Entry(objADMasterProductType).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterProductType(long MasterProductTypeId)
        {
            try
            {
                var objADMasterProductType = _Context.ASMasterProductTypes.Find(MasterProductTypeId);
                _Context.ASMasterProductTypes.Remove(objADMasterProductType);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterProductTypeExists(long MasterProductTypeId)
        {
            try
            {
                return _Context.ASMasterProductTypes.Any(e => e.MasterProductTypeId == MasterProductTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
