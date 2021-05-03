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
    public class MasterCategory : IMasterCategoryInterface<MasterCategoryResult>
    {

        readonly AssetsContext _Context;

        public MasterCategory(AssetsContext context)
        {
            _Context = context;
        }


        public IEnumerable<MasterCategoryResult> GetAllASMasterCategory()
        {
            try
            {
                var _data = (from MC in _Context.ASMasterCategories

                             select new
                             {
                                 MC.MasterCategoryId,
                                 MC.CategoryCode,
                                 MC.CategoryTitle,
                                 MC.MasterCategoryType,
                                 MC.MasterCategoryDescription,
                                 MC.CategoryImage,
                                 MC.Sequence,
                                 MC.IsActive,                                
                                 MC.EnterById,
                                 MC.EnterDate,
                                 MC.ModifiedById,
                                 MC.ModifiedDate,
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

                List<MasterCategoryResult> objMasterCategoryResulttList = new List<MasterCategoryResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterCategoryResult = new MasterCategoryResult();

                    _MasterCategoryResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterCategoryResult.CategoryCode = _Item.CategoryCode;
                    _MasterCategoryResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterCategoryResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterCategoryResult.MasterCategoryDescription = _Item.MasterCategoryDescription;
                    _MasterCategoryResult.CategoryImage = _Item.CategoryImage;
                  
                    _MasterCategoryResult.Sequence = _Item.Sequence;
                    _MasterCategoryResult.IsActive = _Item.IsActive;
                    _MasterCategoryResult.EnterById = _Item.EnterById;
                    _MasterCategoryResult.EnterDate = _Item.EnterDate;
                    _MasterCategoryResult.ModifiedById = _Item.ModifiedById;
                    _MasterCategoryResult.ModifiedDate = _Item.ModifiedDate;

                    _MasterCategoryResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterCategoryResult.AssetsAssign = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _MasterCategoryResult.AssetsInRepair = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterCategoryResult.ServiceInExpire = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterCategoryResult.AssetsInSold = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == true).Count());
                    _MasterCategoryResult.AssetsInDead = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == true && a.IsSaleProduct == false).Count());
                    _MasterCategoryResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _MasterCategoryResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);

                    objMasterCategoryResulttList.Add(_MasterCategoryResult);
                }
                return objMasterCategoryResulttList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterCategoryResult GetASMasterCategoryByID(long MasterCategoryId)
        {
            try
            {
                var _data = (from MC in _Context.ASMasterCategories
                             where MC.MasterCategoryId == MasterCategoryId
                             select new
                             {
                                 MC.MasterCategoryId,
                                 MC.CategoryCode,
                                 MC.CategoryTitle,
                                 MC.MasterCategoryType,
                                 MC.MasterCategoryDescription,
                                 MC.CategoryImage,
                                 MC.Sequence,
                                 MC.IsActive,
                                 MC.EnterById,
                                 MC.EnterDate,
                                 MC.ModifiedById,
                                 MC.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterCategoryResult _MasterCategoryResult = new MasterCategoryResult();
                if (_Item != null)
                {
                    _MasterCategoryResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterCategoryResult.CategoryCode = _Item.CategoryCode;
                    _MasterCategoryResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterCategoryResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterCategoryResult.MasterCategoryDescription = _Item.MasterCategoryDescription;
                    _MasterCategoryResult.CategoryImage = _Item.CategoryImage;

                    _MasterCategoryResult.Sequence = _Item.Sequence;
                    _MasterCategoryResult.IsActive = _Item.IsActive;
                    _MasterCategoryResult.ActiveColor = "green";
                    _MasterCategoryResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterCategoryResult.EnterById = _Item.EnterById;
                    _MasterCategoryResult.EnterDate = _Item.EnterDate;
                    _MasterCategoryResult.ModifiedById = _Item.ModifiedById;
                    _MasterCategoryResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterCategoryResult.IsActive == false)
                    {
                        _MasterCategoryResult.ActiveColor = "red";
                        _MasterCategoryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                return _MasterCategoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task InsertASMasterCategory(ASMasterCategory objADMasterCategory)
        {
            try
            {
                _Context.ASMasterCategories.Add(objADMasterCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateASMasterCategory(ASMasterCategory objADMasterCategory)
        {
            try
            {
                _Context.Entry(objADMasterCategory).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterCategory(long MasterCategoryId)
        {
            try
            {
                var objASMasterCategory = _Context.ASMasterCategories.Find(MasterCategoryId);
                _Context.ASMasterCategories.Remove(objASMasterCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterCategoryExists(long MasterCategoryId)
        {
            try
            {
                return _Context.ASMasterCategories.Any(e => e.MasterCategoryId == MasterCategoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
