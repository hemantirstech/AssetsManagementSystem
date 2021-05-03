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
    public class MasterAssetsAssignment : IMasterAssetsAssignmentInterface<MasterAssetsAssignmentResult>
    {
        readonly AssetsContext _Context;

        public MasterAssetsAssignment(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignment()
        {
            try
            {
                var _data = (from MP in _Context.ASMasterProducts
                             join MB in _Context.ASMasterBrands on MP.MasterBrandId equals MB.MasterBrandId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join SC in _Context.ASMasterSubCategories on MP.MasterSubCategoryId equals SC.MasterSubCategoryId into SCGroup
                             from SC in SCGroup.DefaultIfEmpty()
                             join CA in _Context.ASMasterCategories on SC.MasterCategoryId equals CA.MasterCategoryId into CAGroup
                             from CA in CAGroup.DefaultIfEmpty()
                             join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId                             
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.PurchaseDate,
                                 MPC.PurchasePrice,
                                 MPC.DepreciatePrice,
                                 MPC.MasterVendorId,
                                 MPC.IsDeadAssets,
                                 MPC.IsTimeToSaleProduct,
                                 MPC.IsSaleProduct,
                                 MPC.SaleDate,
                                 MPC.SalePrice,
                                 MPC.ProductQty,
                                 MPC.ProductQtyUnit,
                                 MPC.NumberOfItemIncludeInProduct,
                                 MPC.ItemPackageQuantity,
                                 MPC.IterationOfWarranty,
                                 MPC.WarrantyStartDate,
                                 MPC.WarrantyExpiryDate,
                                 MPC.MasterBranchId,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,


                                 MAA.MasterAssetsAssignmentId,
                                 MAA.AssetsAssignmentDate,
                                 MAA.ASMasterProductChilds,
                                 MAA.MasterEmployeeId,
                                 MAA.IsAssetsDeAssign,
                                 MAA.AssetsDeAssignmentDate,
                                 MAA.DeAssignReason,
                                 MAA.MasterLocationId,
                                 MAA.Sequence,
                                 MAA.IsActive,
                                 MAA.EnterById,
                                 MAA.EnterDate,
                                 MAA.ModifiedById,
                                 MAA.ModifiedDate,
                             });

                List<MasterAssetsAssignmentResult> objMasterAssetsAssignmentResultList = new List<MasterAssetsAssignmentResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterAssetsAssignmentResult = new MasterAssetsAssignmentResult();


                    _MasterAssetsAssignmentResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterAssetsAssignmentResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;
                    _MasterAssetsAssignmentResult.AssetsDeAssignmentDate = _Item.AssetsDeAssignmentDate;
                    _MasterAssetsAssignmentResult.DeAssignReason = _Item.DeAssignReason;
                    _MasterAssetsAssignmentResult.MasterLocationId = _Item.MasterLocationId;
                    _MasterAssetsAssignmentResult.Sequence = _Item.Sequence;
                    _MasterAssetsAssignmentResult.IsActive = _Item.IsActive;
                    _MasterAssetsAssignmentResult.ActiveColor = "green";
                    _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterAssetsAssignmentResult.EnterById = _Item.EnterById;
                    _MasterAssetsAssignmentResult.EnterDate = _Item.EnterDate;
                    _MasterAssetsAssignmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterAssetsAssignmentResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterAssetsAssignmentResult.IsActive == false)
                    {
                        _MasterAssetsAssignmentResult.ActiveColor = "red";
                        _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _MasterAssetsAssignmentResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterAssetsAssignmentResult.MasterProductId = _Item.MasterProductId;
                    //_MasterAssetsAssignmentResult.ASMasterProducts = _Item.ASMasterProducts;
                    _MasterAssetsAssignmentResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterAssetsAssignmentResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterAssetsAssignmentResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    //_MasterAssetsAssignmentResult.ASMasterProductSizes = _Item.ASMasterProductSizes;
                    _MasterAssetsAssignmentResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterAssetsAssignmentResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterAssetsAssignmentResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterAssetsAssignmentResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterAssetsAssignmentResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterAssetsAssignmentResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterAssetsAssignmentResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterAssetsAssignmentResult.SaleDate = _Item.SaleDate;
                    _MasterAssetsAssignmentResult.SalePrice = _Item.SalePrice;
                    _MasterAssetsAssignmentResult.ProductQty = _Item.ProductQty;
                    _MasterAssetsAssignmentResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterAssetsAssignmentResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterAssetsAssignmentResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterAssetsAssignmentResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterAssetsAssignmentResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterAssetsAssignmentResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterAssetsAssignmentResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.EmployeeName = "";
                    _MasterAssetsAssignmentResult.ProductTitle = _Item.ProductTitle;
                    _MasterAssetsAssignmentResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterAssetsAssignmentResult.BrandTitle = _Item.BrandTitle;
                    _MasterAssetsAssignmentResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterAssetsAssignmentResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterAssetsAssignmentResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterAssetsAssignmentResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterAssetsAssignmentResult.ProductSKU = _Item.ProductSKU;
                    _MasterAssetsAssignmentResult.ProductModel = _Item.ProductModel;
                    _MasterAssetsAssignmentResult.Manufacturer = _Item.Manufacturer;
                    _MasterAssetsAssignmentResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterAssetsAssignmentResult.ReorderLevel = _Item.ReorderLevel;

                    objMasterAssetsAssignmentResultList.Add(_MasterAssetsAssignmentResult);
                }
                return objMasterAssetsAssignmentResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignmentNotAssign(long MasterCategoryId, long MasterSubCategoryId, long MasterBranchId)
        {
            try
            {
                var _data = (from MP in _Context.ASMasterProducts                                 
                             join MB in _Context.ASMasterBrands on MP.MasterBrandId equals MB.MasterBrandId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join SC in _Context.ASMasterSubCategories on MP.MasterSubCategoryId equals SC.MasterSubCategoryId into SCGroup
                             from SC in SCGroup.DefaultIfEmpty()
                             join CA in _Context.ASMasterCategories on SC.MasterCategoryId equals CA.MasterCategoryId into CAGroup
                             from CA in CAGroup.DefaultIfEmpty()
                             join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId
                             where MPC.MasterEmployeeId==0 && MPC.MasterBranchId == MasterBranchId
                             //|| (MasterCategoryId >0 && CA.MasterCategoryId == MasterCategoryId)
                             //|| (MasterSubCategoryId > 0 && SC.MasterSubCategoryId == MasterCategoryId)
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.PurchaseDate,
                                 MPC.PurchasePrice,
                                 MPC.DepreciatePrice,
                                 MPC.MasterVendorId,
                                 MPC.IsDeadAssets,
                                 MPC.IsTimeToSaleProduct,
                                 MPC.IsSaleProduct,
                                 MPC.SaleDate,
                                 MPC.SalePrice,
                                 MPC.ProductQty,
                                 MPC.ProductQtyUnit,
                                 MPC.NumberOfItemIncludeInProduct,
                                 MPC.ItemPackageQuantity,
                                 MPC.IterationOfWarranty,
                                 MPC.WarrantyStartDate,
                                 MPC.WarrantyExpiryDate,
                                 MPC.MasterBranchId,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 
                                 MPC.MasterEmployeeId,
                                 MPC.Sequence,
                                 MPC.IsActive,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,
                             });

                List<MasterAssetsAssignmentResult> objMasterAssetsAssignmentResultList = new List<MasterAssetsAssignmentResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterAssetsAssignmentResult = new MasterAssetsAssignmentResult();

                    
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.Sequence = _Item.Sequence;
                    _MasterAssetsAssignmentResult.IsActive = _Item.IsActive;
                    _MasterAssetsAssignmentResult.ActiveColor = "green";
                    _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterAssetsAssignmentResult.EnterById = _Item.EnterById;
                    _MasterAssetsAssignmentResult.EnterDate = _Item.EnterDate;
                    _MasterAssetsAssignmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterAssetsAssignmentResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterAssetsAssignmentResult.IsActive == false)
                    {
                        _MasterAssetsAssignmentResult.ActiveColor = "red";
                        _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _MasterAssetsAssignmentResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterAssetsAssignmentResult.MasterProductId = _Item.MasterProductId;
                    //_MasterAssetsAssignmentResult.ASMasterProducts = _Item.ASMasterProducts;
                    _MasterAssetsAssignmentResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterAssetsAssignmentResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterAssetsAssignmentResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    //_MasterAssetsAssignmentResult.ASMasterProductSizes = _Item.ASMasterProductSizes;
                    _MasterAssetsAssignmentResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterAssetsAssignmentResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterAssetsAssignmentResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterAssetsAssignmentResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterAssetsAssignmentResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterAssetsAssignmentResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterAssetsAssignmentResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterAssetsAssignmentResult.SaleDate = _Item.SaleDate;
                    _MasterAssetsAssignmentResult.SalePrice = _Item.SalePrice;
                    _MasterAssetsAssignmentResult.ProductQty = _Item.ProductQty;
                    _MasterAssetsAssignmentResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterAssetsAssignmentResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterAssetsAssignmentResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterAssetsAssignmentResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterAssetsAssignmentResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterAssetsAssignmentResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterAssetsAssignmentResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;

                    _MasterAssetsAssignmentResult.ProductTitle = _Item.ProductTitle;
                    _MasterAssetsAssignmentResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterAssetsAssignmentResult.BrandTitle = _Item.BrandTitle;
                    _MasterAssetsAssignmentResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterAssetsAssignmentResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterAssetsAssignmentResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterAssetsAssignmentResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterAssetsAssignmentResult.ProductSKU = _Item.ProductSKU;
                    _MasterAssetsAssignmentResult.ProductModel = _Item.ProductModel;
                    _MasterAssetsAssignmentResult.Manufacturer = _Item.Manufacturer;
                    _MasterAssetsAssignmentResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterAssetsAssignmentResult.ReorderLevel = _Item.ReorderLevel;

                    objMasterAssetsAssignmentResultList.Add(_MasterAssetsAssignmentResult);
                }
                return objMasterAssetsAssignmentResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public IEnumerable<MasterAssetsAssignmentResult> GetAllASMasterAssetsAssignment(long MasterEmployeeId)
        {
            try
            {
                var _data = (from MP in _Context.ASMasterProducts
                             join MB in _Context.ASMasterBrands on MP.MasterBrandId equals MB.MasterBrandId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join SC in _Context.ASMasterSubCategories on MP.MasterSubCategoryId equals SC.MasterSubCategoryId into SCGroup
                             from SC in SCGroup.DefaultIfEmpty()
                             join CA in _Context.ASMasterCategories on SC.MasterCategoryId equals CA.MasterCategoryId into CAGroup
                             from CA in CAGroup.DefaultIfEmpty()
                             join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId
                             where MAA.MasterEmployeeId == MasterEmployeeId 
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.PurchaseDate,
                                 MPC.PurchasePrice,
                                 MPC.DepreciatePrice,
                                 MPC.MasterVendorId,
                                 MPC.IsDeadAssets,
                                 MPC.IsTimeToSaleProduct,
                                 MPC.IsSaleProduct,
                                 MPC.SaleDate,
                                 MPC.SalePrice,
                                 MPC.ProductQty,
                                 MPC.ProductQtyUnit,
                                 MPC.NumberOfItemIncludeInProduct,
                                 MPC.ItemPackageQuantity,
                                 MPC.IterationOfWarranty,
                                 MPC.WarrantyStartDate,
                                 MPC.WarrantyExpiryDate,
                                 MPC.MasterBranchId,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,


                                 MAA.MasterAssetsAssignmentId,
                                 MAA.AssetsAssignmentDate,
                                 MAA.ASMasterProductChilds,
                                 MAA.MasterEmployeeId,
                                 MAA.IsAssetsDeAssign,
                                 MAA.AssetsDeAssignmentDate,
                                 MAA.DeAssignReason,
                                 MAA.MasterLocationId,
                                 MAA.Sequence,
                                 MAA.IsActive,
                                 MAA.EnterById,
                                 MAA.EnterDate,
                                 MAA.ModifiedById,
                                 MAA.ModifiedDate,
                             });

                List<MasterAssetsAssignmentResult> objMasterAssetsAssignmentResultList = new List<MasterAssetsAssignmentResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterAssetsAssignmentResult = new MasterAssetsAssignmentResult();


                    _MasterAssetsAssignmentResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterAssetsAssignmentResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;
                    _MasterAssetsAssignmentResult.AssetsDeAssignmentDate = _Item.AssetsDeAssignmentDate;
                    _MasterAssetsAssignmentResult.DeAssignReason = _Item.DeAssignReason;
                    _MasterAssetsAssignmentResult.MasterLocationId = _Item.MasterLocationId;
                    _MasterAssetsAssignmentResult.Sequence = _Item.Sequence;
                    _MasterAssetsAssignmentResult.IsActive = _Item.IsActive;
                    _MasterAssetsAssignmentResult.ActiveColor = "green";
                    _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterAssetsAssignmentResult.EnterById = _Item.EnterById;
                    _MasterAssetsAssignmentResult.EnterDate = _Item.EnterDate;
                    _MasterAssetsAssignmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterAssetsAssignmentResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterAssetsAssignmentResult.IsActive == false)
                    {
                        _MasterAssetsAssignmentResult.ActiveColor = "red";
                        _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _MasterAssetsAssignmentResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterAssetsAssignmentResult.MasterProductId = _Item.MasterProductId;
                    //_MasterAssetsAssignmentResult.ASMasterProducts = _Item.ASMasterProducts;
                    _MasterAssetsAssignmentResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterAssetsAssignmentResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterAssetsAssignmentResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    //_MasterAssetsAssignmentResult.ASMasterProductSizes = _Item.ASMasterProductSizes;
                    _MasterAssetsAssignmentResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterAssetsAssignmentResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterAssetsAssignmentResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterAssetsAssignmentResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterAssetsAssignmentResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterAssetsAssignmentResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterAssetsAssignmentResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterAssetsAssignmentResult.SaleDate = _Item.SaleDate;
                    _MasterAssetsAssignmentResult.SalePrice = _Item.SalePrice;
                    _MasterAssetsAssignmentResult.ProductQty = _Item.ProductQty;
                    _MasterAssetsAssignmentResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterAssetsAssignmentResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterAssetsAssignmentResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterAssetsAssignmentResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterAssetsAssignmentResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterAssetsAssignmentResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterAssetsAssignmentResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.EmployeeName = "";
                    _MasterAssetsAssignmentResult.ProductTitle = _Item.ProductTitle;
                    _MasterAssetsAssignmentResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterAssetsAssignmentResult.BrandTitle = _Item.BrandTitle;
                    _MasterAssetsAssignmentResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterAssetsAssignmentResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterAssetsAssignmentResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterAssetsAssignmentResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterAssetsAssignmentResult.ProductSKU = _Item.ProductSKU;
                    _MasterAssetsAssignmentResult.ProductModel = _Item.ProductModel;
                    _MasterAssetsAssignmentResult.Manufacturer = _Item.Manufacturer;
                    _MasterAssetsAssignmentResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterAssetsAssignmentResult.ReorderLevel = _Item.ReorderLevel;

                    objMasterAssetsAssignmentResultList.Add(_MasterAssetsAssignmentResult);
                }
                return objMasterAssetsAssignmentResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterAssetsAssignmentResult GetASMasterAssetsAssignmentByID(long MasterAssetsAssignmentId)
        {
            try
            {
                var _data = (from MP in _Context.ASMasterProducts
                                 //join CO in _AdminContext.ADMasterCountries on MP.CountryOfOrigin equals CO.MasterCountryId into COGroup
                                 //from CO in COGroup.DefaultIfEmpty()
                                 //join MT in _AdminContext.ADMasterTaxes on MP.ProductTaxCode equals MT.MasterTaxId into MTGroup
                                 //from MT in MTGroup.DefaultIfEmpty()
                                 //join MC in _AdminContext.ADMasterCurrencies on MP.ProductCurrency equals MC.MasterCurrencyId into MCGroup
                                 //from MC in MCGroup.DefaultIfEmpty()
                             join MB in _Context.ASMasterBrands on MP.MasterBrandId equals MB.MasterBrandId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join SC in _Context.ASMasterSubCategories on MP.MasterSubCategoryId equals SC.MasterSubCategoryId into SCGroup
                             from SC in SCGroup.DefaultIfEmpty()
                             join CA in _Context.ASMasterCategories on SC.MasterCategoryId equals CA.MasterCategoryId into CAGroup
                             from CA in CAGroup.DefaultIfEmpty()
                             join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId
                             where MAA.MasterAssetsAssignmentId == MasterAssetsAssignmentId
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.PurchaseDate,
                                 MPC.PurchasePrice,
                                 MPC.DepreciatePrice,
                                 MPC.MasterVendorId,
                                 MPC.IsDeadAssets,
                                 MPC.IsTimeToSaleProduct,
                                 MPC.IsSaleProduct,
                                 MPC.SaleDate,
                                 MPC.SalePrice,
                                 MPC.ProductQty,
                                 MPC.ProductQtyUnit,
                                 MPC.NumberOfItemIncludeInProduct,
                                 MPC.ItemPackageQuantity,
                                 MPC.IterationOfWarranty,
                                 MPC.WarrantyStartDate,
                                 MPC.WarrantyExpiryDate,
                                 MPC.MasterBranchId,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 MAA.MasterAssetsAssignmentId,
                                 MAA.AssetsAssignmentDate,
                                 MAA.ASMasterProductChilds,
                                 MAA.MasterEmployeeId,
                                 MAA.IsAssetsDeAssign,
                                 MAA.AssetsDeAssignmentDate,
                                 MAA.DeAssignReason,
                                 MAA.MasterLocationId,
                                 MAA.Sequence,
                                 MAA.IsActive,
                                 MAA.EnterById,
                                 MAA.EnterDate,
                                 MAA.ModifiedById,
                                 MAA.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                MasterAssetsAssignmentResult _MasterAssetsAssignmentResult = new MasterAssetsAssignmentResult();
                if (_data != null)
                {
                    _MasterAssetsAssignmentResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterAssetsAssignmentResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterAssetsAssignmentResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;
                    _MasterAssetsAssignmentResult.AssetsDeAssignmentDate = _Item.AssetsDeAssignmentDate;
                    _MasterAssetsAssignmentResult.DeAssignReason = _Item.DeAssignReason;
                    _MasterAssetsAssignmentResult.MasterLocationId = _Item.MasterLocationId;
                    _MasterAssetsAssignmentResult.Sequence = _Item.Sequence;
                    _MasterAssetsAssignmentResult.IsActive = _Item.IsActive;
                    _MasterAssetsAssignmentResult.ActiveColor = "green";
                    _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterAssetsAssignmentResult.EnterById = _Item.EnterById;
                    _MasterAssetsAssignmentResult.EnterDate = _Item.EnterDate;
                    _MasterAssetsAssignmentResult.ModifiedById = _Item.ModifiedById;
                    _MasterAssetsAssignmentResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterAssetsAssignmentResult.IsActive == false)
                    {
                        _MasterAssetsAssignmentResult.ActiveColor = "red";
                        _MasterAssetsAssignmentResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    _MasterAssetsAssignmentResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterAssetsAssignmentResult.MasterProductId = _Item.MasterProductId;
                    //_MasterAssetsAssignmentResult.ASMasterProducts = _Item.ASMasterProducts;
                    _MasterAssetsAssignmentResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterAssetsAssignmentResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterAssetsAssignmentResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    //_MasterAssetsAssignmentResult.ASMasterProductSizes = _Item.ASMasterProductSizes;
                    _MasterAssetsAssignmentResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterAssetsAssignmentResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterAssetsAssignmentResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterAssetsAssignmentResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterAssetsAssignmentResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterAssetsAssignmentResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterAssetsAssignmentResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterAssetsAssignmentResult.SaleDate = _Item.SaleDate;
                    _MasterAssetsAssignmentResult.SalePrice = _Item.SalePrice;
                    _MasterAssetsAssignmentResult.ProductQty = _Item.ProductQty;
                    _MasterAssetsAssignmentResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterAssetsAssignmentResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterAssetsAssignmentResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterAssetsAssignmentResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterAssetsAssignmentResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterAssetsAssignmentResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterAssetsAssignmentResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterAssetsAssignmentResult.MasterEmployeeId = _Item.MasterEmployeeId;

                    _MasterAssetsAssignmentResult.ProductTitle = _Item.ProductTitle;
                    _MasterAssetsAssignmentResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterAssetsAssignmentResult.BrandTitle = _Item.BrandTitle;
                    _MasterAssetsAssignmentResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterAssetsAssignmentResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterAssetsAssignmentResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterAssetsAssignmentResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterAssetsAssignmentResult.ProductSKU = _Item.ProductSKU;
                    _MasterAssetsAssignmentResult.ProductModel = _Item.ProductModel;
                    _MasterAssetsAssignmentResult.Manufacturer = _Item.Manufacturer;
                    _MasterAssetsAssignmentResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterAssetsAssignmentResult.ReorderLevel = _Item.ReorderLevel;
                }
                return _MasterAssetsAssignmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterAssetsAssignment(ASMasterAssetsAssignment objADMasterAssetsAssignment)
        {
            try
            {
                _Context.ASMasterAssetsAssignments.Add(objADMasterAssetsAssignment);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateASMasterAssetsAssignment(ASMasterAssetsAssignment objADMasterAssetsAssignment)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    //Get MasterProductChild Detail
                    var _MasterProductChild = _Context.ASMasterProductChilds.Find(objADMasterAssetsAssignment.MasterProductChildId);
                    _MasterProductChild.MasterEmployeeId = objADMasterAssetsAssignment.MasterEmployeeId;

                    _Context.Entry(_MasterProductChild).State = EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    ASMasterAssetsAssignment objASMasterAssetsAssignment = new ASMasterAssetsAssignment();
                    objASMasterAssetsAssignment.MasterAssetsAssignmentId = 0;

                    //Assignment Date is passed through ManufacturingDate
                    objASMasterAssetsAssignment.AssetsAssignmentDate = DateTime.Now;
                    objASMasterAssetsAssignment.MasterProductChildId = _MasterProductChild.MasterProductChildId;
                    objASMasterAssetsAssignment.MasterEmployeeId = _MasterProductChild.MasterEmployeeId;
                    objASMasterAssetsAssignment.MasterLocationId = _MasterProductChild.MasterBranchId;
                    objASMasterAssetsAssignment.IsActive = true;
                    objASMasterAssetsAssignment.IsAssetsDeAssign = false;

                    if (_MasterProductChild.MasterEmployeeId==0)
                    {
                        objASMasterAssetsAssignment.IsActive = false;
                        objASMasterAssetsAssignment.IsAssetsDeAssign = true;
                    }
                    
                    objASMasterAssetsAssignment.EnterById = _MasterProductChild.EnterById;
                    objASMasterAssetsAssignment.EnterDate = _MasterProductChild.EnterDate;
                    objASMasterAssetsAssignment.ModifiedById = _MasterProductChild.ModifiedById;
                    objASMasterAssetsAssignment.ModifiedDate = _MasterProductChild.ModifiedDate;

                    //Check Whether record already exist or not
                    long MasterAssetsAssignmentId;

                    if (objADMasterAssetsAssignment.MasterAssetsAssignmentId==0)
                    {
                        MasterAssetsAssignmentId = _Context.ASMasterAssetsAssignments.Where(a => a.MasterProductChildId == objADMasterAssetsAssignment.MasterProductChildId).Select(a => a.MasterAssetsAssignmentId).FirstOrDefault();
                    }
                    else
                    {
                        MasterAssetsAssignmentId = objADMasterAssetsAssignment.MasterAssetsAssignmentId;
                    }

                    if (MasterAssetsAssignmentId == null || MasterAssetsAssignmentId == 0)
                    {
                        if (_MasterProductChild.MasterEmployeeId != null && _MasterProductChild.MasterEmployeeId > 0)
                        {
                            _Context.ASMasterAssetsAssignments.Add(objASMasterAssetsAssignment);
                            await _Context.SaveChangesAsync();
                        }
                    }
                    else if (MasterAssetsAssignmentId != null && MasterAssetsAssignmentId > 0)
                    {
                        objASMasterAssetsAssignment.MasterAssetsAssignmentId = MasterAssetsAssignmentId;

                        if (_MasterProductChild.MasterEmployeeId != null && _MasterProductChild.MasterEmployeeId > 0)
                        {
                            _Context.Entry(objASMasterAssetsAssignment).State = EntityState.Modified;
                            await _Context.SaveChangesAsync();
                        }
                        else
                        {
                            var ASMasterAssetsAssignmentNew = _Context.ASMasterAssetsAssignments.Find(MasterAssetsAssignmentId);
                            _Context.ASMasterAssetsAssignments.Remove(ASMasterAssetsAssignmentNew);
                            await _Context.SaveChangesAsync();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task DeleteASMasterAssetsAssignment(long MasterAssetsAssignmentId)
        {
            try
            {
                var objADMasterAssetsAssignment = _Context.ASMasterAssetsAssignments.Find(MasterAssetsAssignmentId);
                _Context.ASMasterAssetsAssignments.Remove(objADMasterAssetsAssignment);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterAssetsAssignmentExists(long MasterAssetsAssignmentId)
        {
            try
            {
                return _Context.ASMasterAssetsAssignments.Any(e => e.MasterAssetsAssignmentId == MasterAssetsAssignmentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
