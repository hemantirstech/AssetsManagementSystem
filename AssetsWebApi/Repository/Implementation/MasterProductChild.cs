using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;
using AssetsWebApi.Model;
using AssetsWebApi.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using AdminDAL;

namespace AssetsWebApi.Repository.Implementation
{
    public class MasterProductChild : IMasterProductChildInterface<MasterProductChildResult>
    {
        readonly AssetsContext _Context;
        readonly AdminDAL.AdminContext _AdminContext;

        public MasterProductChild(AssetsContext context, AdminContext AdminContext) 
        {
            _Context = context;
            _AdminContext = AdminContext;
        }

        public IEnumerable<MasterProductChildResult> GetAllASMasterProductChild()
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
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                             from MAA in MAAGroup.DefaultIfEmpty()
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 //MPC.ASMasterProducts,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.ConditionType,
                                 MPC.ConditionNote,
                                 MPC.ManufacturingDate,
                                 MPC.ExpiryDate,
                                 MPC.ProductColour,
                                 MPC.ASMasterProductSizes,
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
                                 MPC.ServiceURL,
                                 MPC.ServiceUserName,
                                 MPC.ServicePassword,
                                 MPC.MasterBranchId,
                                 MPC.MasterEmployeeId,
                                 MPC.MasterCompanyOwnerId,
                                 MPC.IsActive,
                                 MPC.Sequence,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryType,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,
                                 MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                 MAA.AssetsAssignmentDate,                                 
                                 MAA.IsAssetsDeAssign
                             });

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                List<MasterProductChildResult> objMasterProductChildResultList = new List<MasterProductChildResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductChildResult = new MasterProductChildResult();

                    _MasterProductChildResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterProductChildResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductChildResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterProductChildResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterProductChildResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    _MasterProductChildResult.ConditionType = _Item.ConditionType;
                    _MasterProductChildResult.ConditionNote = _Item.ConditionNote;
                    _MasterProductChildResult.ManufacturingDate = _Item.ManufacturingDate;
                    _MasterProductChildResult.ExpiryDate = _Item.ExpiryDate;
                    _MasterProductChildResult.ProductColour = _Item.ProductColour;
                    _MasterProductChildResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterProductChildResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterProductChildResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterProductChildResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterProductChildResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterProductChildResult.SaleDate = _Item.SaleDate;
                    _MasterProductChildResult.SalePrice = _Item.SalePrice;
                    _MasterProductChildResult.ProductQty = _Item.ProductQty;
                    _MasterProductChildResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterProductChildResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterProductChildResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterProductChildResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterProductChildResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterProductChildResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterProductChildResult.ServiceURL = _Item.ServiceURL;
                    _MasterProductChildResult.ServiceUserName = _Item.ServiceUserName;
                    _MasterProductChildResult.ServicePassword = _Item.ServicePassword;
                    _MasterProductChildResult.MasterBranchId = _Item.MasterBranchId;                    
                    _MasterProductChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _Item.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductChildResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterProductChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _Item.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _Item.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;

                    _MasterProductChildResult.MasterCompanyOwnerId = _Item.MasterCompanyOwnerId;
                    _MasterProductChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _Item.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;



                    _MasterProductChildResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductChildResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductChildResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductChildResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductChildResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductChildResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductChildResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductChildResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterProductChildResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductChildResult.ProductModel = _Item.ProductModel;
                    _MasterProductChildResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductChildResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductChildResult.ReorderLevel = _Item.ReorderLevel;

                    _MasterProductChildResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterProductChildResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;                   
                    _MasterProductChildResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;

                    _MasterProductChildResult.IsActive = _Item.IsActive;
                    _MasterProductChildResult.Sequence = _Item.Sequence;
                    _MasterProductChildResult.ActiveColor = "green";
                    _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductChildResult.EnterById = _Item.EnterById;
                    _MasterProductChildResult.EnterDate = _Item.EnterDate;
                    _MasterProductChildResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductChildResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductChildResult.IsActive == false)
                    {
                        _MasterProductChildResult.ActiveColor = "red";
                        _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProductChildResultList.Add(_MasterProductChildResult);
                }
                return objMasterProductChildResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MasterProductChildResult> GetExpiryASMasterProductChild()
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
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                             from MAA in MAAGroup.DefaultIfEmpty()
                             where MPC.WarrantyExpiryDate <= DateTime.Now.AddDays(30) && MPC.WarrantyExpiryDate >= DateTime.Now
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 //MPC.ASMasterProducts,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.ConditionType,
                                 MPC.ConditionNote,
                                 MPC.ManufacturingDate,
                                 MPC.ExpiryDate,
                                 MPC.ProductColour,
                                 MPC.ASMasterProductSizes,
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
                                 MPC.ServiceURL,
                                 MPC.ServiceUserName,
                                 MPC.ServicePassword,
                                 MPC.MasterBranchId,
                                 MPC.MasterEmployeeId,
                                 MPC.MasterCompanyOwnerId,
                                 MPC.IsActive,
                                 MPC.Sequence,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryType,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                 MAA.AssetsAssignmentDate,
                                 MAA.IsAssetsDeAssign
                             });

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                List<MasterProductChildResult> objMasterProductChildResultList = new List<MasterProductChildResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductChildResult = new MasterProductChildResult();

                    _MasterProductChildResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterProductChildResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductChildResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterProductChildResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterProductChildResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    _MasterProductChildResult.ConditionType = _Item.ConditionType;
                    _MasterProductChildResult.ConditionNote = _Item.ConditionNote;
                    _MasterProductChildResult.ManufacturingDate = _Item.ManufacturingDate;
                    _MasterProductChildResult.ExpiryDate = _Item.ExpiryDate;
                    _MasterProductChildResult.ProductColour = _Item.ProductColour;
                    _MasterProductChildResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterProductChildResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterProductChildResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterProductChildResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterProductChildResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterProductChildResult.SaleDate = _Item.SaleDate;
                    _MasterProductChildResult.SalePrice = _Item.SalePrice;
                    _MasterProductChildResult.ProductQty = _Item.ProductQty;
                    _MasterProductChildResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterProductChildResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterProductChildResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterProductChildResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterProductChildResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterProductChildResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterProductChildResult.ServiceURL = _Item.ServiceURL;
                    _MasterProductChildResult.ServiceUserName = _Item.ServiceUserName;
                    _MasterProductChildResult.ServicePassword = _Item.ServicePassword;
                    _MasterProductChildResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterProductChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _Item.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductChildResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterProductChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _Item.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _Item.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;

                    _MasterProductChildResult.MasterCompanyOwnerId = _Item.MasterCompanyOwnerId;
                    _MasterProductChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _Item.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;


                    _MasterProductChildResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductChildResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductChildResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductChildResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductChildResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductChildResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductChildResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductChildResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterProductChildResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductChildResult.ProductModel = _Item.ProductModel;
                    _MasterProductChildResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductChildResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductChildResult.ReorderLevel = _Item.ReorderLevel;

                    _MasterProductChildResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterProductChildResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;
                    _MasterProductChildResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;

                    _MasterProductChildResult.IsActive = _Item.IsActive;
                    _MasterProductChildResult.Sequence = _Item.Sequence;
                    _MasterProductChildResult.ActiveColor = "green";
                    _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductChildResult.EnterById = _Item.EnterById;
                    _MasterProductChildResult.EnterDate = _Item.EnterDate;
                    _MasterProductChildResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductChildResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductChildResult.IsActive == false)
                    {
                        _MasterProductChildResult.ActiveColor = "red";
                        _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProductChildResultList.Add(_MasterProductChildResult);
                }
                return objMasterProductChildResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MasterProductChildResult> GetExpiredASMasterProductChild()
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
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                             from MAA in MAAGroup.DefaultIfEmpty()
                             where MPC.WarrantyExpiryDate <= DateTime.Now
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 //MPC.ASMasterProducts,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.ConditionType,
                                 MPC.ConditionNote,
                                 MPC.ManufacturingDate,
                                 MPC.ExpiryDate,
                                 MPC.ProductColour,
                                 MPC.ASMasterProductSizes,
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
                                 MPC.ServiceURL,
                                 MPC.ServiceUserName,
                                 MPC.ServicePassword,
                                 MPC.MasterBranchId,
                                 MPC.MasterEmployeeId,
                                 MPC.MasterCompanyOwnerId,
                                 MPC.IsActive,
                                 MPC.Sequence,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryType,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                 MAA.AssetsAssignmentDate,
                                 MAA.IsAssetsDeAssign
                             });

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                List<MasterProductChildResult> objMasterProductChildResultList = new List<MasterProductChildResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductChildResult = new MasterProductChildResult();

                    _MasterProductChildResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterProductChildResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductChildResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterProductChildResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterProductChildResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    _MasterProductChildResult.ConditionType = _Item.ConditionType;
                    _MasterProductChildResult.ConditionNote = _Item.ConditionNote;
                    _MasterProductChildResult.ManufacturingDate = _Item.ManufacturingDate;
                    _MasterProductChildResult.ExpiryDate = _Item.ExpiryDate;
                    _MasterProductChildResult.ProductColour = _Item.ProductColour;
                    _MasterProductChildResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterProductChildResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterProductChildResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterProductChildResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterProductChildResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterProductChildResult.SaleDate = _Item.SaleDate;
                    _MasterProductChildResult.SalePrice = _Item.SalePrice;
                    _MasterProductChildResult.ProductQty = _Item.ProductQty;
                    _MasterProductChildResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterProductChildResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterProductChildResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterProductChildResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterProductChildResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterProductChildResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterProductChildResult.ServiceURL = _Item.ServiceURL;
                    _MasterProductChildResult.ServiceUserName = _Item.ServiceUserName;
                    _MasterProductChildResult.ServicePassword = _Item.ServicePassword;
                    _MasterProductChildResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterProductChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _Item.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductChildResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterProductChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _Item.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _Item.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;

                    _MasterProductChildResult.MasterCompanyOwnerId = _Item.MasterCompanyOwnerId;
                    _MasterProductChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _Item.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;


                    _MasterProductChildResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductChildResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductChildResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductChildResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductChildResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductChildResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductChildResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductChildResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterProductChildResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductChildResult.ProductModel = _Item.ProductModel;
                    _MasterProductChildResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductChildResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductChildResult.ReorderLevel = _Item.ReorderLevel;

                    _MasterProductChildResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterProductChildResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;
                    _MasterProductChildResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;

                    _MasterProductChildResult.IsActive = _Item.IsActive;
                    _MasterProductChildResult.Sequence = _Item.Sequence;
                    _MasterProductChildResult.ActiveColor = "green";
                    _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductChildResult.EnterById = _Item.EnterById;
                    _MasterProductChildResult.EnterDate = _Item.EnterDate;
                    _MasterProductChildResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductChildResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductChildResult.IsActive == false)
                    {
                        _MasterProductChildResult.ActiveColor = "red";
                        _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterProductChildResultList.Add(_MasterProductChildResult);
                }
                return objMasterProductChildResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterProductChildResult GetASMasterProductChildByID(long MasterProductChildId)
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
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                             from MAA in MAAGroup.DefaultIfEmpty()
                             where MPC.MasterProductChildId == MasterProductChildId
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 //MPC.ASMasterProducts,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.ConditionType,
                                 MPC.ConditionNote,
                                 MPC.ManufacturingDate,
                                 MPC.ExpiryDate,
                                 MPC.ProductColour,
                                 MPC.ASMasterProductSizes,
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
                                 MPC.ServiceURL,
                                 MPC.ServiceUserName,
                                 MPC.ServicePassword,
                                 MPC.MasterBranchId,
                                 MPC.MasterEmployeeId,
                                 MPC.MasterCompanyOwnerId,
                                 MPC.IsActive,
                                 MPC.Sequence,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryType,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                 MAA.AssetsAssignmentDate,                                 
                                 MAA.IsAssetsDeAssign
                             });

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                var _Item = _data.FirstOrDefault();

                MasterProductChildResult _MasterProductChildResult = new MasterProductChildResult();
                if (_Item != null)
                {
                    _MasterProductChildResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterProductChildResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductChildResult.ProductChildSKU = _Item.ProductChildSKU;
                    _MasterProductChildResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterProductChildResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    _MasterProductChildResult.ConditionType = _Item.ConditionType;
                    _MasterProductChildResult.ConditionNote = _Item.ConditionNote;
                    _MasterProductChildResult.ManufacturingDate = _Item.ManufacturingDate;
                    _MasterProductChildResult.ExpiryDate = _Item.ExpiryDate;
                    _MasterProductChildResult.ProductColour = _Item.ProductColour;
                    _MasterProductChildResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterProductChildResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterProductChildResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterProductChildResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterProductChildResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterProductChildResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterProductChildResult.SaleDate = _Item.SaleDate;
                    _MasterProductChildResult.SalePrice = _Item.SalePrice;
                    _MasterProductChildResult.ProductQty = _Item.ProductQty;
                    _MasterProductChildResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterProductChildResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterProductChildResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterProductChildResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterProductChildResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterProductChildResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterProductChildResult.ServiceURL = _Item.ServiceURL;
                    _MasterProductChildResult.ServiceUserName = _Item.ServiceUserName;
                    _MasterProductChildResult.ServicePassword = _Item.ServicePassword;
                    _MasterProductChildResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterProductChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _Item.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductChildResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterProductChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _Item.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _Item.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;

                    _MasterProductChildResult.MasterCompanyOwnerId = _Item.MasterCompanyOwnerId;
                    _MasterProductChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _Item.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;


                    _MasterProductChildResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductChildResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductChildResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductChildResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductChildResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductChildResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductChildResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductChildResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterProductChildResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductChildResult.ProductModel = _Item.ProductModel;
                    _MasterProductChildResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductChildResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductChildResult.ReorderLevel = _Item.ReorderLevel;

                    _MasterProductChildResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterProductChildResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;                    
                    _MasterProductChildResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;

                    _MasterProductChildResult.IsActive = _Item.IsActive;
                    _MasterProductChildResult.Sequence = _Item.Sequence;
                    _MasterProductChildResult.ActiveColor = "green";
                    _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductChildResult.EnterById = _Item.EnterById;
                    _MasterProductChildResult.EnterDate = _Item.EnterDate;
                    _MasterProductChildResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductChildResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductChildResult.IsActive == false)
                    {
                        _MasterProductChildResult.ActiveColor = "red";
                        _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    //TransactionProductHistory List by MasterProductChildId

                    var _dataHistory = _Context.ASTransactionProductHistorys.Where(a => a.MasterProductChildId == _Item.MasterProductChildId).ToList();
                    List<TransactionProductHistoryResult> objTransactionProductHistoryResultList = new List<TransactionProductHistoryResult>();

                    foreach (var _ItemHistory in _dataHistory.ToList())
                    {
                        var _TransactionProductHistoryResult = new TransactionProductHistoryResult();

                        _TransactionProductHistoryResult.TransactionProductHistoryId = _ItemHistory.TransactionProductHistoryId;
                        _TransactionProductHistoryResult.MasterProductChildId = _ItemHistory.MasterProductChildId;
                        //  _TransactionProductHistoryResult.ASMasterProductChild = _ItemHistory.ASMasterProductChild;
                        _TransactionProductHistoryResult.MasterSubscriptionTypeId = _ItemHistory.MasterSubscriptionTypeId;
                        _TransactionProductHistoryResult.SubscriptionPrice = _ItemHistory.SubscriptionPrice;
                        _TransactionProductHistoryResult.MasterSubscriptionVendorId = _ItemHistory.MasterSubscriptionVendorId;
                        _TransactionProductHistoryResult.SubscriptionDate = _ItemHistory.SubscriptionDate;
                        _TransactionProductHistoryResult.SubscriptionStartDate = _ItemHistory.SubscriptionStartDate;
                        _TransactionProductHistoryResult.SubscriptionExpiryDate = _ItemHistory.SubscriptionExpiryDate;
                        _TransactionProductHistoryResult.UploadInvoice = _ItemHistory.UploadInvoice;
                        _TransactionProductHistoryResult.UploadDocument = _ItemHistory.UploadDocument;
                        _TransactionProductHistoryResult.UploadWarretyCard = _ItemHistory.UploadWarretyCard;

                        _TransactionProductHistoryResult.Sequence = _ItemHistory.Sequence;
                        _TransactionProductHistoryResult.IsActive = _ItemHistory.IsActive;

                        objTransactionProductHistoryResultList.Add(_TransactionProductHistoryResult);
                    }

                    _MasterProductChildResult.TransactionProductHistoryList = objTransactionProductHistoryResultList;
                }
                return _MasterProductChildResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterProductChildResult GetSingleOrDefaultASMasterProductChild(long MasterProductId)
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
                             join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                             from MAA in MAAGroup.DefaultIfEmpty()
                             where MPC.MasterProductId == MasterProductId
                             select new
                             {
                                 MPC.MasterProductChildId,
                                 MPC.MasterProductId,
                                 //MPC.ASMasterProducts,
                                 MPC.ProductChildSKU,
                                 MPC.ProductChildTitle,
                                 MPC.ManufacturerPartNumber,
                                 MPC.ConditionType,
                                 MPC.ConditionNote,
                                 MPC.ManufacturingDate,
                                 MPC.ExpiryDate,
                                 MPC.ProductColour,
                                 MPC.ASMasterProductSizes,
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
                                 MPC.ServiceURL,
                                 MPC.ServiceUserName,
                                 MPC.ServicePassword,
                                 MPC.MasterBranchId,
                                 MPC.MasterEmployeeId,
                                 MPC.MasterCompanyOwnerId,
                                 MPC.IsActive,
                                 MPC.Sequence,
                                 MPC.EnterById,
                                 MPC.EnterDate,
                                 MPC.ModifiedById,
                                 MPC.ModifiedDate,

                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 CA.MasterCategoryId,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryType,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.ProductModel,
                                 MP.Manufacturer,

                                 MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                 MAA.AssetsAssignmentDate,
                                 MAA.IsAssetsDeAssign
                             });

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                var _Item = _data.OrderByDescending(a=> a.MasterProductChildId).FirstOrDefault();

                MasterProductChildResult _MasterProductChildResult = new MasterProductChildResult();
                if (_Item != null)
                {
                    _MasterProductChildResult.MasterProductChildId = _Item.MasterProductChildId;
                    _MasterProductChildResult.MasterProductId = _Item.MasterProductId;
                    //_MasterProductChildResult.ASMasterProducts = _Item.ASMasterProducts;
                    _MasterProductChildResult.ProductChildSKU = _Context.ASMasterProductChilds.Where(a => a.MasterProductId == MasterProductId).Max(a => a.ProductChildSKU); //.FirstOrDefault();  ;
                    _MasterProductChildResult.ProductChildTitle = _Item.ProductChildTitle;
                    _MasterProductChildResult.ManufacturerPartNumber = _Item.ManufacturerPartNumber;
                    _MasterProductChildResult.ConditionType = _Item.ConditionType;
                    _MasterProductChildResult.ConditionNote = _Item.ConditionNote;
                    _MasterProductChildResult.ManufacturingDate = _Item.ManufacturingDate;
                    _MasterProductChildResult.ExpiryDate = _Item.ExpiryDate;
                    _MasterProductChildResult.ProductColour = _Item.ProductColour;
                    //_MasterProductChildResult.ASMasterProductSizes = _Item.ASMasterProductSizes;
                    _MasterProductChildResult.PurchaseDate = _Item.PurchaseDate;
                    _MasterProductChildResult.PurchasePrice = _Item.PurchasePrice;
                    _MasterProductChildResult.DepreciatePrice = _Item.DepreciatePrice;
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.IsDeadAssets = _Item.IsDeadAssets;
                    _MasterProductChildResult.IsTimeToSaleProduct = _Item.IsTimeToSaleProduct;
                    _MasterProductChildResult.IsSaleProduct = _Item.IsSaleProduct;
                    _MasterProductChildResult.SaleDate = _Item.SaleDate;
                    _MasterProductChildResult.SalePrice = _Item.SalePrice;
                    _MasterProductChildResult.ProductQty = _Item.ProductQty;
                    _MasterProductChildResult.ProductQtyUnit = _Item.ProductQtyUnit;
                    _MasterProductChildResult.NumberOfItemIncludeInProduct = _Item.NumberOfItemIncludeInProduct;
                    _MasterProductChildResult.ItemPackageQuantity = _Item.ItemPackageQuantity;
                    _MasterProductChildResult.IterationOfWarranty = _Item.IterationOfWarranty;
                    _MasterProductChildResult.WarrantyStartDate = _Item.WarrantyStartDate;
                    _MasterProductChildResult.WarrantyExpiryDate = _Item.WarrantyExpiryDate;
                    _MasterProductChildResult.ServiceURL = _Item.ServiceURL;
                    _MasterProductChildResult.ServiceUserName = _Item.ServiceUserName;
                    _MasterProductChildResult.ServicePassword = _Item.ServicePassword;
                    _MasterProductChildResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterProductChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _Item.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductChildResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterProductChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _Item.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                    _MasterProductChildResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterProductChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _Item.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;

                    _MasterProductChildResult.MasterCompanyOwnerId = _Item.MasterCompanyOwnerId;
                    _MasterProductChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _Item.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;


                    _MasterProductChildResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductChildResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductChildResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductChildResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductChildResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductChildResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductChildResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductChildResult.MasterCategoryType = _Item.MasterCategoryType;
                    _MasterProductChildResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductChildResult.ProductModel = _Item.ProductModel;
                    _MasterProductChildResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductChildResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductChildResult.ReorderLevel = _Item.ReorderLevel;

                    _MasterProductChildResult.MasterAssetsAssignmentId = _Item.MasterAssetsAssignmentId;
                    _MasterProductChildResult.AssetsAssignmentDate = _Item.AssetsAssignmentDate;                   
                    _MasterProductChildResult.IsAssetsDeAssign = _Item.IsAssetsDeAssign;

                    _MasterProductChildResult.IsActive = _Item.IsActive;
                    _MasterProductChildResult.Sequence = _Item.Sequence;
                    _MasterProductChildResult.ActiveColor = "green";
                    _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _MasterProductChildResult.EnterById = _Item.EnterById;
                    _MasterProductChildResult.EnterDate = _Item.EnterDate;
                    _MasterProductChildResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductChildResult.ModifiedDate = _Item.ModifiedDate;

                    if (_MasterProductChildResult.IsActive == false)
                    {
                        _MasterProductChildResult.ActiveColor = "red";
                        _MasterProductChildResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                return _MasterProductChildResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterProductChild(ASMasterProductChild objASMasterProductChild)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    objASMasterProductChild.ProductQty = 1;
                    objASMasterProductChild.IsActive = true;
                    objASMasterProductChild.IsDeadAssets = false;
                    objASMasterProductChild.IsSaleProduct = false;

                    _Context.ASMasterProductChilds.Add(objASMasterProductChild);
                    await _Context.SaveChangesAsync();

                    //Insert in TransactionProductHistory 
                    ASTransactionProductHistory objASTransactionProductHistory = new ASTransactionProductHistory();
                    if (objASMasterProductChild.MasterEmployeeId != null && objASMasterProductChild.MasterEmployeeId > 0)
                    {
                        objASTransactionProductHistory.MasterProductChildId = objASMasterProductChild.MasterProductChildId;
                        objASTransactionProductHistory.MasterSubscriptionTypeId = 1;
                        objASTransactionProductHistory.MasterSubscriptionVendorId = objASMasterProductChild.MasterVendorId;
                        objASTransactionProductHistory.SubscriptionPrice = objASMasterProductChild.PurchasePrice;
                        objASTransactionProductHistory.SubscriptionDate = objASMasterProductChild.PurchaseDate;
                        objASTransactionProductHistory.SubscriptionStartDate = objASMasterProductChild.WarrantyStartDate;
                        objASTransactionProductHistory.SubscriptionExpiryDate = objASMasterProductChild.WarrantyExpiryDate;

                        _Context.ASTransactionProductHistorys.Add(objASTransactionProductHistory);
                        await _Context.SaveChangesAsync();
                    }

                    ASMasterAssetsAssignment objASMasterAssetsAssignment = new ASMasterAssetsAssignment();
                    if (objASMasterProductChild.MasterEmployeeId != null && objASMasterProductChild.MasterEmployeeId > 0)
                    {
                        //ASMasterAssetsAssignment
                        objASMasterAssetsAssignment.MasterAssetsAssignmentId = 0;

                        //Assignment Date is passed through ManufacturingDate
                        objASMasterAssetsAssignment.AssetsAssignmentDate = objASMasterProductChild.ManufacturingDate;
                        objASMasterAssetsAssignment.MasterProductChildId = objASMasterProductChild.MasterProductChildId;
                        objASMasterAssetsAssignment.MasterEmployeeId = objASMasterProductChild.MasterEmployeeId;
                        objASMasterAssetsAssignment.MasterLocationId = objASMasterProductChild.MasterBranchId;
                        objASMasterAssetsAssignment.IsActive = true;
                        objASMasterAssetsAssignment.IsAssetsDeAssign = false;
                        objASMasterAssetsAssignment.EnterById = objASMasterProductChild.EnterById;
                        objASMasterAssetsAssignment.EnterDate = objASMasterProductChild.EnterDate;
                        objASMasterAssetsAssignment.ModifiedById = objASMasterProductChild.ModifiedById;
                        objASMasterAssetsAssignment.ModifiedDate = objASMasterProductChild.ModifiedDate;
                        _Context.ASMasterAssetsAssignments.Add(objASMasterAssetsAssignment);
                        await _Context.SaveChangesAsync();
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
        public async Task UpdateASMasterProductChild(ASMasterProductChild objASMasterProductChild)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    objASMasterProductChild.ProductQty = 1;
                    objASMasterProductChild.IsActive = true;
                    objASMasterProductChild.IsDeadAssets = false;
                    objASMasterProductChild.IsSaleProduct = false;

                    _Context.Entry(objASMasterProductChild).State = EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    ASMasterAssetsAssignment objASMasterAssetsAssignment = new ASMasterAssetsAssignment();
                    objASMasterAssetsAssignment.MasterAssetsAssignmentId = 0;

                    //Assignment Date is passed through ManufacturingDate
                    objASMasterAssetsAssignment.AssetsAssignmentDate = objASMasterProductChild.ManufacturingDate;
                    objASMasterAssetsAssignment.MasterProductChildId = objASMasterProductChild.MasterProductChildId;
                    objASMasterAssetsAssignment.MasterEmployeeId = objASMasterProductChild.MasterEmployeeId;
                    objASMasterAssetsAssignment.MasterLocationId = objASMasterProductChild.MasterBranchId;
                    objASMasterAssetsAssignment.IsActive = true;
                    objASMasterAssetsAssignment.IsAssetsDeAssign = false;
                    objASMasterAssetsAssignment.EnterById = objASMasterProductChild.EnterById;
                    objASMasterAssetsAssignment.EnterDate = objASMasterProductChild.EnterDate;
                    objASMasterAssetsAssignment.ModifiedById = objASMasterProductChild.ModifiedById;
                    objASMasterAssetsAssignment.ModifiedDate = objASMasterProductChild.ModifiedDate;
                    

                    //Check Whether record already exist or not
                    var MasterAssetsAssignmentId = _Context.ASMasterAssetsAssignments.Where(a => a.MasterProductChildId == objASMasterProductChild.MasterProductChildId).Select(a => a.MasterAssetsAssignmentId).FirstOrDefault();
                    

                    if (MasterAssetsAssignmentId==null || MasterAssetsAssignmentId==0)
                    {
                        if (objASMasterProductChild.MasterEmployeeId != null && objASMasterProductChild.MasterEmployeeId > 0)
                        {
                            _Context.ASMasterAssetsAssignments.Add(objASMasterAssetsAssignment);
                            await _Context.SaveChangesAsync();
                        }
                    }
                    else if (MasterAssetsAssignmentId != null && MasterAssetsAssignmentId > 0)
                    {
                        objASMasterAssetsAssignment.MasterAssetsAssignmentId = MasterAssetsAssignmentId;

                        if (objASMasterProductChild.MasterEmployeeId != null && objASMasterProductChild.MasterEmployeeId > 0)
                        {                            
                            _Context.Entry(objASMasterAssetsAssignment).State = EntityState.Modified;
                            await _Context.SaveChangesAsync();
                        }
                        else
                        {
                            var ASMasterAssetsAssignmentNew = _Context.ASMasterAssetsAssignments.Find(MasterAssetsAssignmentId);
                            objASMasterAssetsAssignment.AssetsDeAssignmentDate = DateTime.Now;
                            objASMasterAssetsAssignment.DeAssignReason = "DeAssign";
                            objASMasterAssetsAssignment.IsActive = false;
                            objASMasterAssetsAssignment.IsAssetsDeAssign = true;

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


        public async Task UpdateASMasterProductChildStatus(ASMasterProductChildStatus objASMasterProductChildStatus)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    long MasterProductId = objASMasterProductChildStatus.MasterProductId;
                    long[] IsRepairs = objASMasterProductChildStatus.IsRepairs;
                    long[] IsDeadAssets = objASMasterProductChildStatus.IsDeadAssets;
                    long[] IsSaleProducts = objASMasterProductChildStatus.IsSaleProducts;

                    //Update If Sale Assets
                    if(IsSaleProducts != null && IsSaleProducts.Length > 0)
                    {
                        foreach (long IsSaleProduct in IsSaleProducts)
                        {
                            var objASMasterProductChild = _Context.ASMasterProductChilds.Find(IsSaleProduct);

                            objASMasterProductChild.IsActive = false;
                            objASMasterProductChild.IsDeadAssets = false;
                            objASMasterProductChild.IsSaleProduct = true;

                            objASMasterProductChild.MasterEmployeeId = 0;
                            objASMasterProductChild.ModifiedById = 1;
                            objASMasterProductChild.ModifiedDate = DateTime.Now;
                            _Context.Entry(objASMasterProductChild).State = EntityState.Modified;
                            await _Context.SaveChangesAsync();

                            var objASMasterAssetsAssignment = _Context.ASMasterAssetsAssignments.Where(a=>a.MasterProductChildId== IsSaleProduct).FirstOrDefault();
                            
                            if(objASMasterAssetsAssignment!=null)
                            {
                                objASMasterAssetsAssignment.MasterEmployeeId = 0;
                                objASMasterAssetsAssignment.IsActive = false;
                                objASMasterAssetsAssignment.IsAssetsDeAssign = true;
                                objASMasterAssetsAssignment.AssetsDeAssignmentDate = DateTime.Now;
                                objASMasterAssetsAssignment.DeAssignReason = "Sold";
                                _Context.Entry(objASMasterAssetsAssignment).State = EntityState.Modified;
                                await _Context.SaveChangesAsync();
                            }
                            
                        }
                    }

                    //Update If Dead
                    if (IsDeadAssets != null && IsDeadAssets.Length > 0)
                    {
                        foreach (long IsDeadAsset in IsDeadAssets)
                        {
                            var objASMasterProductChild = _Context.ASMasterProductChilds.Find(IsDeadAsset);

                            objASMasterProductChild.IsActive = false;
                            objASMasterProductChild.IsDeadAssets = true;
                            objASMasterProductChild.IsSaleProduct = false;

                            objASMasterProductChild.MasterEmployeeId = 0;
                            objASMasterProductChild.ModifiedById = 1;
                            objASMasterProductChild.ModifiedDate = DateTime.Now;
                            _Context.Entry(objASMasterProductChild).State = EntityState.Modified;
                            await _Context.SaveChangesAsync();

                            var objASMasterAssetsAssignment = _Context.ASMasterAssetsAssignments.Where(a => a.MasterProductChildId == IsDeadAsset).FirstOrDefault();

                            if (objASMasterAssetsAssignment != null)
                            {                                
                                objASMasterAssetsAssignment.MasterEmployeeId = 0;
                                objASMasterAssetsAssignment.IsActive = false;
                                objASMasterAssetsAssignment.IsAssetsDeAssign = true;
                                objASMasterAssetsAssignment.AssetsDeAssignmentDate = DateTime.Now;
                                objASMasterAssetsAssignment.DeAssignReason = "Dead Asset";
                                _Context.Entry(objASMasterAssetsAssignment).State = EntityState.Modified;
                                await _Context.SaveChangesAsync();
                            }                                
                        }
                    }

                    //Update If Repair
                    if (IsRepairs != null && IsRepairs.Length > 0)
                    {
                        foreach (long IsRepair in IsRepairs)
                        {
                            var objASMasterProductChild = _Context.ASMasterProductChilds.Find(IsRepair);

                            objASMasterProductChild.IsActive = false;
                            objASMasterProductChild.IsDeadAssets = false;
                            objASMasterProductChild.IsSaleProduct = false;

                            objASMasterProductChild.MasterEmployeeId = 0;
                            objASMasterProductChild.ModifiedById = 1;
                            objASMasterProductChild.ModifiedDate = DateTime.Now;
                            _Context.Entry(objASMasterProductChild).State = EntityState.Modified;
                            await _Context.SaveChangesAsync();

                            var objASMasterAssetsAssignment = _Context.ASMasterAssetsAssignments.Where(a => a.MasterProductChildId == IsRepair).FirstOrDefault();

                            if (objASMasterAssetsAssignment != null)
                            {
                                objASMasterAssetsAssignment.MasterEmployeeId = 0;
                                objASMasterAssetsAssignment.IsActive = false;
                                objASMasterAssetsAssignment.IsAssetsDeAssign = true;
                                objASMasterAssetsAssignment.AssetsDeAssignmentDate = DateTime.Now;
                                objASMasterAssetsAssignment.DeAssignReason = "Repair";
                                _Context.Entry(objASMasterAssetsAssignment).State = EntityState.Modified;
                                await _Context.SaveChangesAsync();
                            }
                            
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

        public async Task DeleteASMasterProductChild(long MasterProductChildId)
        {
            try
            {
                var objASMasterProductChild = _Context.ASMasterProductChilds.Find(MasterProductChildId);
                _Context.ASMasterProductChilds.Remove(objASMasterProductChild);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterProductChildExists(long MasterProductChildId)
        {
            try
            {
                return _Context.ASMasterProductChilds.Any(e => e.MasterProductChildId == MasterProductChildId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
