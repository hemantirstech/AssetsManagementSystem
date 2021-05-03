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
    public class MasterSubCategory : IMasterSubCategoryInterface<MasterSubCategoryResult>
    {
        readonly AssetsContext _Context;

        public MasterSubCategory(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterSubCategoryResult> GetAllASMasterSubCategory()
        {
            try
            {
                var _data = (from MSC in _Context.ASMasterSubCategories
                             join MC in _Context.ASMasterCategories on MSC.MasterCategoryId equals MC.MasterCategoryId
                             select new
                             {
                                 MSC.MasterSubCategoryId,
                                 MSC.SubCategoryTitle,
                                 MSC.SubCategoryCode,
                                 MSC.SubCategoryDescription,
                                 MSC.SubCategoryImage,
                                 MSC.MasterCategoryId,
                                 MC.CategoryTitle,
                                 MSC.Sequence,
                                 MSC.IsActive,
                                 MSC.EnterById,
                                 MSC.EnterDate,
                                 MSC.ModifiedById,
                                 MSC.ModifiedDate
                             });

                var _Productdata = (from MC in _Context.ASMasterCategories
                                    join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                                    join MP in _Context.ASMasterProducts on MSC.MasterSubCategoryId equals MP.MasterSubCategoryId into MPGroup
                                    from MP in MPGroup.DefaultIfEmpty()
                                    join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId into MPCGroup
                                    from MPC in MPCGroup.DefaultIfEmpty()
                                    select new
                                    {
                                        MC.MasterCategoryId,
                                        MC.CategoryTitle,
                                        MSC.MasterSubCategoryId,
                                        MSC.SubCategoryTitle,
                                        MPC.ProductChildTitle,
                                        MPC.ProductChildSKU,
                                        MPC.ManufacturerPartNumber,
                                        MPC.PurchasePrice,
                                        MPC.DepreciatePrice,
                                        MPC.IsActive,
                                        MPC.IsDeadAssets,
                                        MPC.IsSaleProduct,
                                        MPC.MasterBranchId,
                                        MPC.MasterEmployeeId,
                                        MPC.WarrantyExpiryDate
                                    });

                List<MasterSubCategoryResult> objMasterSubCategoryResultList = new List<MasterSubCategoryResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterSubCategoryResult = new MasterSubCategoryResult();

                    _MasterSubCategoryResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterSubCategoryResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterSubCategoryResult.SubCategoryCode = _Item.SubCategoryCode;
                    _MasterSubCategoryResult.SubCategoryDescription = _Item.SubCategoryDescription;
                    _MasterSubCategoryResult.SubCategoryImage = _Item.SubCategoryImage;
                    _MasterSubCategoryResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterSubCategoryResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterSubCategoryResult.Sequence = _Item.Sequence;
                    _MasterSubCategoryResult.IsActive = _Item.IsActive;
                    _MasterSubCategoryResult.EnterById = _Item.EnterById;
                    _MasterSubCategoryResult.EnterDate = _Item.EnterDate;
                    _MasterSubCategoryResult.ModifiedById = _Item.ModifiedById;
                    _MasterSubCategoryResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterSubCategoryResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterSubCategoryResult.AssetsAssign = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _MasterSubCategoryResult.AssetsInRepair = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterSubCategoryResult.ServiceInExpire = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterSubCategoryResult.AssetsInSold = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == true).Count());
                    _MasterSubCategoryResult.AssetsInDead = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == false && a.IsDeadAssets == true && a.IsSaleProduct == false).Count());
                    _MasterSubCategoryResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _MasterSubCategoryResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);

                    objMasterSubCategoryResultList.Add(_MasterSubCategoryResult);
                }
                return objMasterSubCategoryResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterSubCategoryResult GetASMasterSubCategoryByID(long MasterSubCategoryId)
        {
            try
            {
                var _data = (from MSC in _Context.ASMasterSubCategories
                             join MC in _Context.ASMasterCategories on MSC.MasterCategoryId equals MC.MasterCategoryId
                             where MSC.MasterSubCategoryId==MasterSubCategoryId
                             select new
                             {
                                 MSC.MasterSubCategoryId,
                                 MSC.SubCategoryTitle,
                                 MSC.SubCategoryCode,
                                 MSC.SubCategoryDescription,
                                 MSC.SubCategoryImage,
                                 MSC.MasterCategoryId,
                                 MC.CategoryTitle,
                                 MSC.Sequence,
                                 MSC.IsActive,
                                 MSC.EnterById,
                                 MSC.EnterDate,
                                 MSC.ModifiedById,
                                 MSC.ModifiedDate
                             });


                var _Item = _data.FirstOrDefault();

                MasterSubCategoryResult _MasterSubCategoryResult = new MasterSubCategoryResult();
                if (_Item != null)
                {
                    _MasterSubCategoryResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterSubCategoryResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterSubCategoryResult.SubCategoryCode = _Item.SubCategoryCode;
                    _MasterSubCategoryResult.SubCategoryDescription = _Item.SubCategoryDescription;
                    _MasterSubCategoryResult.SubCategoryImage = _Item.SubCategoryImage;
                    _MasterSubCategoryResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterSubCategoryResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterSubCategoryResult.Sequence = _Item.Sequence;
                    _MasterSubCategoryResult.IsActive = _Item.IsActive;
                    _MasterSubCategoryResult.ActiveColor = "green";
                    _MasterSubCategoryResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterSubCategoryResult.EnterById = _Item.EnterById;
                    _MasterSubCategoryResult.EnterDate = _Item.EnterDate;
                    _MasterSubCategoryResult.ModifiedById = _Item.ModifiedById;
                    _MasterSubCategoryResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterSubCategoryResult.IsActive == false)
                    {
                        _MasterSubCategoryResult.ActiveColor = "red";
                        _MasterSubCategoryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }
                return _MasterSubCategoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public async Task InsertASMasterSubCategory(ASMasterSubCategory objADMasterSubCategory)
        {
            try
            {
                _Context.ASMasterSubCategories.Add(objADMasterSubCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateASMasterSubCategory(ASMasterSubCategory objADMasterSubCategory)
        {
            try
            {
                _Context.Entry(objADMasterSubCategory).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterSubCategory(long MasterSubCategoryId)
        {
            try
            {
                var objADMasterSubCategory = _Context.ASMasterSubCategories.Find(MasterSubCategoryId);
                _Context.ASMasterSubCategories.Remove(objADMasterSubCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterSubCategoryExists(long MasterSubCategoryId)
        {
            try
            {
                return _Context.ASMasterSubCategories.Any(e => e.MasterSubCategoryId == MasterSubCategoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
