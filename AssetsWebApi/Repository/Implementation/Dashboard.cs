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
    public class Dashboard : IDashboardInterface<ProductDetailResult>
    {
        readonly AssetsContext _Context;

        public Dashboard(AssetsContext context)
        {
            _Context = context;
        }
               
        public IEnumerable<ProductDetailResult> GetAllASMasterCategory()
        {
            try
            {
                var _data = (from MC in _Context.ASMasterCategories
                             where MC.IsActive==true
                             select new
                             {
                                 MC.MasterCategoryId,
                                 MC.CategoryTitle   ,
                                 MC.MasterCategoryType,
                                 MC.CategoryCode,
                                 MC.CategoryImage
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

                List<ProductDetailResult> objProductDetailResultList = new List<ProductDetailResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _ProductDetailResult = new ProductDetailResult();
                    
                    _ProductDetailResult.MasterCategoryId = _Item.MasterCategoryId;
                    _ProductDetailResult.CategoryTitle = _Item.CategoryTitle;
                    _ProductDetailResult.MasterCategoryType = _Item.MasterCategoryType;
                    _ProductDetailResult.CategoryColour = _Item.CategoryCode;
                    _ProductDetailResult.CategoryIcon = _Item.CategoryImage;

                    _ProductDetailResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _ProductDetailResult.AssetsAssign = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _ProductDetailResult.AssetsInRepair = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _ProductDetailResult.ServiceInExpire = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.WarrantyExpiryDate <= DateTime.Now  && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());

                    _ProductDetailResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _ProductDetailResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);
                    
                    objProductDetailResultList.Add(_ProductDetailResult);
                }
                return objProductDetailResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductDetailResult> GetAllMicrosoftSubCategory()
        {
            try
            {
                var _data = (from MC in _Context.ASMasterCategories
                             where MC.IsActive == true
                             select new
                             {
                                 MC.MasterCategoryId,
                                 MC.CategoryTitle,
                                 MC.MasterCategoryType,
                                 MC.CategoryCode,
                                 MC.CategoryImage
                             });

                var _Productdata = (from MC in _Context.ASMasterCategories
                                    join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                                    join MP in _Context.ASMasterProducts on MSC.MasterSubCategoryId equals MP.MasterSubCategoryId into MPGroup
                                    from MP in MPGroup.DefaultIfEmpty()
                                    join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId into MPCGroup
                                    from MPC in MPCGroup.DefaultIfEmpty()
                                    where MP.MasterBrandId==13
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

                List<ProductDetailResult> objProductDetailResultList = new List<ProductDetailResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _ProductDetailResult = new ProductDetailResult();

                    _ProductDetailResult.MasterCategoryId = _Item.MasterCategoryId;
                    _ProductDetailResult.CategoryTitle = _Item.CategoryTitle;
                    _ProductDetailResult.MasterCategoryType = _Item.MasterCategoryType;
                    _ProductDetailResult.CategoryColour = _Item.CategoryCode;
                    _ProductDetailResult.CategoryIcon = _Item.CategoryImage;

                    _ProductDetailResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _ProductDetailResult.AssetsAssign = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _ProductDetailResult.AssetsInRepair = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _ProductDetailResult.ServiceInExpire = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());

                    _ProductDetailResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _ProductDetailResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterCategoryId == _Item.MasterCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);

                    objProductDetailResultList.Add(_ProductDetailResult);
                }
                return objProductDetailResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductDetailResult> GetAllASMasterSubCategory(long MasterSubCategoryId, long MasterBranchId)
        {
            try
            {
                var _data = (from MC in _Context.ASMasterCategories
                             join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                             where MC.IsActive == true && MSC.IsActive==true
                             select new
                             {
                                 MC.MasterCategoryId,
                                 MC.CategoryTitle,
                                 MC.MasterCategoryType,
                                 MC.CategoryCode,
                                 MC.CategoryImage,
                                 MSC.MasterSubCategoryId,
                                 MSC.SubCategoryTitle
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

                List<ProductDetailResult> objProductDetailResultList = new List<ProductDetailResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _ProductDetailResult = new ProductDetailResult();

                    _ProductDetailResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _ProductDetailResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _ProductDetailResult.MasterCategoryId = _Item.MasterCategoryId;
                    _ProductDetailResult.CategoryTitle = _Item.CategoryTitle;
                    _ProductDetailResult.MasterCategoryType = _Item.MasterCategoryType;
                    _ProductDetailResult.CategoryColour = _Item.CategoryCode;
                    _ProductDetailResult.CategoryIcon = _Item.CategoryImage;

                    if (MasterBranchId > 0)
                    {
                        _ProductDetailResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                        _ProductDetailResult.AssetsAssign = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                        _ProductDetailResult.AssetsInRepair = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                        _ProductDetailResult.ServiceInExpire = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());

                        _ProductDetailResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                        _ProductDetailResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterBranchId == MasterBranchId && a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);
                    }
                    else
                    {
                        _ProductDetailResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                        _ProductDetailResult.AssetsAssign = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                        _ProductDetailResult.AssetsInRepair = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                        _ProductDetailResult.ServiceInExpire = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());

                        _ProductDetailResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                        _ProductDetailResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterSubCategoryId == _Item.MasterSubCategoryId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);
                    }

                    objProductDetailResultList.Add(_ProductDetailResult);
                }
                return objProductDetailResultList.Where(a=>a.TotalAssetsInStock>0 || a.AssetsAssign >0 || a.AssetsInRepair >0 || a.ServiceInExpire>0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ProductDetailResult> GetAllASMasterSubCategoryEmployee(long MasterEmployeeId)
        {
            try
            {                
                var _data = (from MC in _Context.ASMasterCategories
                                    join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                                    join MP in _Context.ASMasterProducts on MSC.MasterSubCategoryId equals MP.MasterSubCategoryId into MPGroup
                                    from MP in MPGroup.DefaultIfEmpty()
                                    join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId into MPCGroup
                                    from MPC in MPCGroup.DefaultIfEmpty()
                                    where MPC.MasterEmployeeId == MasterEmployeeId
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
                                        MPC.MasterEmployeeId
                                    });

                List<ProductDetailResult> objProductDetailResultList = new List<ProductDetailResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _ProductDetailResult = new ProductDetailResult();

                    _ProductDetailResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _ProductDetailResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _ProductDetailResult.MasterCategoryId = _Item.MasterCategoryId;
                    _ProductDetailResult.CategoryTitle = _Item.CategoryTitle;
                    _ProductDetailResult.ProductChildTitle = _Item.ProductChildTitle;
                    _ProductDetailResult.ProductChildSKU = _Item.ProductChildSKU;
                    _ProductDetailResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;

                    _ProductDetailResult.TotalAssetsInStock = (_data.Where(a => a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _ProductDetailResult.AssetsAssign = (_data.Where(a =>  a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _ProductDetailResult.AssetsInRepair = (_data.Where(a => a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());

                    _ProductDetailResult.TotalAssetsCost = _Item.PurchasePrice ?? 0;
                    _ProductDetailResult.TotalAssetsDepreciatedValue = _Item.DepreciatePrice??0;

                    objProductDetailResultList.Add(_ProductDetailResult);
                }
                return objProductDetailResultList.Where(a => a.TotalAssetsInStock > 0 || a.AssetsAssign > 0 || a.AssetsInRepair > 0 || a.ServiceInExpire > 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
