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
    public class MasterProduct : IMasterProductInterface<MasterProductResult>
    {
        readonly AssetsContext _Context;
        readonly AdminDAL.AdminContext _AdminContext;

        public MasterProduct(AssetsContext context, AdminContext AdminContext)
        {
            _Context = context;
            _AdminContext = AdminContext;
        }   

        public IEnumerable<MasterProductResult> GetAllASMasterProduct()
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
                             select new
                             {
                                 MP.MasterProductId,
                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.ProductHSNCode,
                                 MP.ProductBarCode,
                                 MP.ProductMainImage,
                                 MP.Description,
                                 MP.Specification,
                                 MP.LegalDisclamer,
                                 MP.SafetyWarning,
                                 MP.ProductModel,
                                 MP.Manufacturer,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.CountryOfOrigin,
                                 //CO.CountryTitle,
                                 MP.ProductTaxCode,
                                 //MT.TaxTitle,
                                 //MT.IsTaxPercentageAmount,
                                 //MT.TaxValue,
                                 MP.ProductCurrency,
                                 //MC.CurrencyTitle,
                                 MP.Sequence,
                                 MP.IsActive,
                                 MP.EnterById,
                                 MP.EnterDate,
                                 MP.ModifiedById,
                                 MP.ModifiedDate,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryId
                                 //TotalAssets = 2,
                                 //AssetsAllocated = 1
                             }).ToList();

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
                                        MPC.MasterProductId,
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

                var _AdminCountries = _AdminContext.ADMasterCountries.ToList();
                var _AdminTaxes = _AdminContext.ADMasterTaxes.ToList();
                var _AdminCurrencies = _AdminContext.ADMasterCurrencies.ToList();

                List<MasterProductResult> objMasterProductResultList = new List<MasterProductResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterProductResult = new MasterProductResult();

                    _MasterProductResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductResult.ProductHSNCode = _Item.ProductHSNCode;
                    _MasterProductResult.ProductBarCode = _Item.ProductBarCode;
                    _MasterProductResult.ProductMainImage = _Item.ProductMainImage;
                    _MasterProductResult.Description = _Item.Description;
                    _MasterProductResult.Specification = _Item.Specification;
                    _MasterProductResult.LegalDisclamer = _Item.LegalDisclamer;
                    _MasterProductResult.SafetyWarning = _Item.SafetyWarning;
                    _MasterProductResult.ProductModel = _Item.ProductModel;
                    _MasterProductResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductResult.ReorderLevel = _Item.ReorderLevel;
                    _MasterProductResult.CountryOfOrigin = _Item.CountryOfOrigin;
                    _MasterProductResult.CountryOfOriginTitle = _AdminCountries.Where(a=>a.MasterCountryId == _Item.CountryOfOrigin).Select(a=>a.CountryTitle).FirstOrDefault();
                    _MasterProductResult.ProductTaxCode = _Item.ProductTaxCode;
                    _MasterProductResult.ProductTaxCodeTitle = _AdminTaxes.Where(a => a.MasterTaxId == _Item.ProductTaxCode).Select(a => a.TaxTitle).FirstOrDefault();
                    _MasterProductResult.ProductCurrency = _Item.ProductCurrency;
                    _MasterProductResult.ProductCurrencyTitle = _AdminCurrencies.Where(a => a.MasterCurrencyId == _Item.ProductCurrency).Select(a => a.CurrencyTitle).FirstOrDefault(); ;
                    _MasterProductResult.Sequence = _Item.Sequence;
                    
                    _MasterProductResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsAssign = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _MasterProductResult.AssetsInRepair = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.ServiceInExpire = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsInSold = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == true).Count());
                    _MasterProductResult.AssetsInDead = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == true && a.IsSaleProduct == false).Count());
                    _MasterProductResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _MasterProductResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a=>a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);


                    _MasterProductResult.IsActive = _Item.IsActive;
                    _MasterProductResult.EnterById = _Item.EnterById;
                    _MasterProductResult.EnterDate = _Item.EnterDate;
                    _MasterProductResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductResult.ModifiedDate = _Item.ModifiedDate;

                    objMasterProductResultList.Add(_MasterProductResult);
                }
                return objMasterProductResultList.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterProductResult GetASMasterProductByID(long MasterProductId)
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
                             where MP.MasterProductId == MasterProductId
                             select new
                             {
                                 MP.MasterProductId,
                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.ProductHSNCode,
                                 MP.ProductBarCode,
                                 MP.ProductMainImage,
                                 MP.Description,
                                 MP.Specification,
                                 MP.LegalDisclamer,
                                 MP.SafetyWarning,
                                 MP.ProductModel,
                                 MP.Manufacturer,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.CountryOfOrigin,
                                 //CO.CountryTitle,
                                 MP.ProductTaxCode,
                                 //MT.TaxTitle,
                                 //MT.IsTaxPercentageAmount,
                                 //MT.TaxValue,
                                 MP.ProductCurrency,
                                 //MC.CurrencyTitle,
                                 MP.Sequence,
                                 MP.IsActive,
                                 MP.EnterById,
                                 MP.EnterDate,
                                 MP.ModifiedById,
                                 MP.ModifiedDate,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryId
                             });

                var _AdminCountries = _AdminContext.ADMasterCountries.ToList();
                var _AdminTaxes = _AdminContext.ADMasterTaxes.ToList();
                var _AdminCurrencies = _AdminContext.ADMasterCurrencies.ToList();
               

                var _Productdata = (from MC in _Context.ASMasterCategories
                                    join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                                    join MP in _Context.ASMasterProducts on MSC.MasterSubCategoryId equals MP.MasterSubCategoryId into MPGroup
                                    from MP in MPGroup.DefaultIfEmpty()
                                    join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId into MPCGroup
                                    from MPC in MPCGroup.DefaultIfEmpty()
                                    where MP.MasterProductId == MasterProductId
                                    select new
                                    {
                                        MC.MasterCategoryId,
                                        MC.CategoryTitle,
                                        MSC.MasterSubCategoryId,
                                        MSC.SubCategoryTitle,
                                        MPC.MasterProductId,
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

                var _Item = _data.FirstOrDefault();

                MasterProductResult _MasterProductResult = new MasterProductResult();
                if (_data != null)
                {
                    _MasterProductResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductResult.ProductHSNCode = _Item.ProductHSNCode;
                    _MasterProductResult.ProductBarCode = _Item.ProductBarCode;
                    _MasterProductResult.ProductMainImage = _Item.ProductMainImage;
                    _MasterProductResult.Description = _Item.Description;
                    _MasterProductResult.Specification = _Item.Specification;
                    _MasterProductResult.LegalDisclamer = _Item.LegalDisclamer;
                    _MasterProductResult.SafetyWarning = _Item.SafetyWarning;
                    _MasterProductResult.ProductModel = _Item.ProductModel;
                    _MasterProductResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductResult.ReorderLevel = _Item.ReorderLevel;
                    _MasterProductResult.CountryOfOrigin = _Item.CountryOfOrigin;
                    _MasterProductResult.CountryOfOriginTitle = _AdminCountries.Where(a => a.MasterCountryId == _Item.CountryOfOrigin).Select(a => a.CountryTitle).FirstOrDefault();
                    _MasterProductResult.ProductTaxCode = _Item.ProductTaxCode;
                    _MasterProductResult.ProductTaxCodeTitle = _AdminTaxes.Where(a => a.MasterTaxId == _Item.ProductTaxCode).Select(a => a.TaxTitle).FirstOrDefault();
                    _MasterProductResult.ProductCurrency = _Item.ProductCurrency;
                    _MasterProductResult.ProductCurrencyTitle = _AdminCurrencies.Where(a => a.MasterCurrencyId == _Item.ProductCurrency).Select(a => a.CurrencyTitle).FirstOrDefault(); ;

                    _MasterProductResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsAssign = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _MasterProductResult.AssetsInRepair = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.ServiceInExpire = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsInSold = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == true).Count());
                    _MasterProductResult.AssetsInDead = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == true && a.IsSaleProduct == false).Count());
                    _MasterProductResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _MasterProductResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);

                    _MasterProductResult.Sequence = _Item.Sequence;
                    _MasterProductResult.IsActive = _Item.IsActive;
                    _MasterProductResult.EnterById = _Item.EnterById;
                    _MasterProductResult.EnterDate = _Item.EnterDate;
                    _MasterProductResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterProductResult.MaxProductSKU = _Context.ASMasterProductChilds.Where(a => a.MasterProductId == _Item.MasterProductId).Max(a=>a.ProductChildSKU);

                }

                var _dataChild = (from MPC in _Context.ASMasterProductChilds
                                  join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                                  from MAA in MAAGroup.DefaultIfEmpty()
                                  where MPC.MasterProductId == _Item.MasterProductId
                                  select new { MPC.MasterProductChildId,MPC.MasterProductId,MPC.ProductChildSKU,MPC.ManufacturerPartNumber,MPC.PurchaseDate,MPC.PurchasePrice,MPC.DepreciatePrice,MPC.WarrantyExpiryDate,MPC.WarrantyStartDate,MPC.IterationOfWarranty,MPC.MasterBranchId,MPC.MasterEmployeeId,MPC.MasterVendorId,MPC.IsDeadAssets,MPC.IsSaleProduct,MPC.MasterCompanyOwnerId,MPC.IsActive,
                                      MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId!=null? MAA.MasterAssetsAssignmentId:0), MAA.AssetsAssignmentDate,MAA.IsAssetsDeAssign});

                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();
                var _AdminCompany = _AdminContext.ADMasterCompanies.ToList();

                List<MasterProductAssignChildResult> objMasterProductAssignChildResultList = new List<MasterProductAssignChildResult>();
                foreach (var _ItemChild in _dataChild.ToList())
                {
                    MasterProductAssignChildResult _MasterProductAssignChildResult = new MasterProductAssignChildResult();

                    _MasterProductAssignChildResult.MasterProductChildId = _ItemChild.MasterProductChildId;
                    _MasterProductAssignChildResult.MasterProductId = _ItemChild.MasterProductId;
                    _MasterProductAssignChildResult.ProductChildSKU = _ItemChild.ProductChildSKU;
                    _MasterProductAssignChildResult.ManufacturerPartNumber = _ItemChild.ManufacturerPartNumber;
                    _MasterProductAssignChildResult.PurchaseDate = _ItemChild.PurchaseDate;
                    _MasterProductAssignChildResult.PurchasePrice = _ItemChild.PurchasePrice;
                    _MasterProductAssignChildResult.DepreciatePrice = _ItemChild.DepreciatePrice;
                    _MasterProductAssignChildResult.WarrantyExpiryDate = _ItemChild.WarrantyExpiryDate;
                    _MasterProductAssignChildResult.WarrantyStartDate = _ItemChild.WarrantyStartDate;
                    _MasterProductAssignChildResult.IterationOfWarranty = _ItemChild.IterationOfWarranty;
                    _MasterProductAssignChildResult.IsActive = _ItemChild.IsActive;
                    _MasterProductAssignChildResult.IsDeadAssets = _ItemChild.IsDeadAssets;
                    _MasterProductAssignChildResult.IsSaleProduct = _ItemChild.IsSaleProduct;
                    _MasterProductAssignChildResult.MasterBranchId = _ItemChild.MasterBranchId;
                    _MasterProductAssignChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _ItemChild.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                    _MasterProductAssignChildResult.MasterEmployeeId = _ItemChild.MasterEmployeeId;
                    _MasterProductAssignChildResult.EmployeeName = _AdminEmployees.Where(a=>a.MasterEmployeeId == _ItemChild.MasterEmployeeId).Select(a=>a.EmployeeName).FirstOrDefault();
                    _MasterProductAssignChildResult.MasterAssetsAssignmentId = _ItemChild.MasterAssetsAssignmentId;
                    _MasterProductAssignChildResult.AssetsAssignmentDate = _ItemChild.AssetsAssignmentDate;
                    _MasterProductAssignChildResult.MasterVendorId = _ItemChild.MasterVendorId;
                    _MasterProductAssignChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _ItemChild.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;                   
                    _MasterProductAssignChildResult.IsAssetsDeAssign = _ItemChild.IsAssetsDeAssign;

                    _MasterProductAssignChildResult.MasterCompanyOwnerId = _ItemChild.MasterCompanyOwnerId;
                    _MasterProductAssignChildResult.CompanyOwnerTitle = _AdminCompany.Where(a => a.MasterCompanyId == _ItemChild.MasterCompanyOwnerId).Select(a => a.CompanyTitle).FirstOrDefault(); ;

                    objMasterProductAssignChildResultList.Add(_MasterProductAssignChildResult);
                }
                _MasterProductResult.ProductAssignChildList = objMasterProductAssignChildResultList;

                return _MasterProductResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MasterProductResult> GetASMasterProductByID(long MasterCategoryId, long MasterSubCategoryId, long MasterBrandId)
        {
            try
            {
                //Get Parent Product data as per MasterCategoryId Or MasterSubCategoryId Or  MasterBranchId
                var _data = (from MP in _Context.ASMasterProducts
                             join MB in _Context.ASMasterBrands on MP.MasterBrandId equals MB.MasterBrandId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join SC in _Context.ASMasterSubCategories on MP.MasterSubCategoryId equals SC.MasterSubCategoryId into SCGroup
                             from SC in SCGroup.DefaultIfEmpty()
                             join CA in _Context.ASMasterCategories on SC.MasterCategoryId equals CA.MasterCategoryId into CAGroup
                             from CA in CAGroup.DefaultIfEmpty()
                             where (MasterCategoryId>0 && MasterSubCategoryId==0 && MasterBrandId==0 && SC.MasterCategoryId == MasterCategoryId) 
                             || (MasterCategoryId == 0 && MasterSubCategoryId > 0 && MasterBrandId == 0 && MP.MasterSubCategoryId == MasterSubCategoryId)
                             || (MasterCategoryId == 0 && MasterSubCategoryId == 0 && MasterBrandId > 0 && MP.MasterBrandId == MasterBrandId)
                             select new
                             {
                                 MP.MasterProductId,
                                 MP.ProductTitle,
                                 MP.ProductSKU,
                                 MP.ProductHSNCode,
                                 MP.ProductBarCode,
                                 MP.ProductMainImage,
                                 MP.Description,
                                 MP.Specification,
                                 MP.LegalDisclamer,
                                 MP.SafetyWarning,
                                 MP.ProductModel,
                                 MP.Manufacturer,
                                 MP.DepreciatePercentage,
                                 MP.ReorderLevel,
                                 MP.CountryOfOrigin,
                                 //CO.CountryTitle,
                                 MP.ProductTaxCode,
                                 //MT.TaxTitle,
                                 //MT.IsTaxPercentageAmount,
                                 //MT.TaxValue,
                                 MP.ProductCurrency,
                                 //MC.CurrencyTitle,
                                 MP.Sequence,
                                 MP.IsActive,
                                 MP.EnterById,
                                 MP.EnterDate,
                                 MP.ModifiedById,
                                 MP.ModifiedDate,
                                 MP.MasterBrandId,
                                 MB.BrandTitle,
                                 MP.MasterSubCategoryId,
                                 SC.SubCategoryTitle,
                                 CA.CategoryTitle,
                                 CA.MasterCategoryId
                             });

                var _AdminCountries = _AdminContext.ADMasterCountries.ToList();
                var _AdminTaxes = _AdminContext.ADMasterTaxes.ToList();
                var _AdminCurrencies = _AdminContext.ADMasterCurrencies.ToList();
                var _AdminBranches = _AdminContext.ADMasterBranches.ToList();
                var _AdminEmployees = _AdminContext.ADMasterEmployees.ToList();
                var _AdminVendors = _AdminContext.ADMasterVendors.ToList();

                //Get Child Product data as per MasterCategoryId Or MasterSubCategoryId Or  MasterBranchId
                var _Productdata = (from MC in _Context.ASMasterCategories
                                    join MSC in _Context.ASMasterSubCategories on MC.MasterCategoryId equals MSC.MasterCategoryId
                                    join MP in _Context.ASMasterProducts on MSC.MasterSubCategoryId equals MP.MasterSubCategoryId into MPGroup
                                    from MP in MPGroup.DefaultIfEmpty()
                                    join MPC in _Context.ASMasterProductChilds on MP.MasterProductId equals MPC.MasterProductId into MPCGroup
                                    from MPC in MPCGroup.DefaultIfEmpty()
                                    where (MasterCategoryId > 0 && MasterSubCategoryId == 0 && MasterBrandId == 0 && MSC.MasterCategoryId == MasterCategoryId)
                                    || (MasterCategoryId == 0 && MasterSubCategoryId > 0 && MasterBrandId == 0 && MP.MasterSubCategoryId == MasterSubCategoryId)
                                    || (MasterCategoryId == 0 && MasterSubCategoryId == 0 && MasterBrandId > 0 && MP.MasterBrandId == MasterBrandId)
                                    select new
                                    {
                                        MC.MasterCategoryId,
                                        MC.CategoryTitle,
                                        MSC.MasterSubCategoryId,
                                        MSC.SubCategoryTitle,
                                        MPC.MasterProductId,
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

                //insert search data in model List<MasterProductResult> object with parent and child
                List<MasterProductResult> objMasterProductResultList = new List<MasterProductResult>();

                foreach (var _Item in _data.ToList())
                {                   
                    MasterProductResult _MasterProductResult = new MasterProductResult();
                    _MasterProductResult.MasterProductId = _Item.MasterProductId;
                    _MasterProductResult.ProductTitle = _Item.ProductTitle;
                    _MasterProductResult.MasterBrandId = _Item.MasterBrandId;
                    _MasterProductResult.BrandTitle = _Item.BrandTitle;
                    _MasterProductResult.MasterSubCategoryId = _Item.MasterSubCategoryId;
                    _MasterProductResult.SubCategoryTitle = _Item.SubCategoryTitle;
                    _MasterProductResult.MasterCategoryId = _Item.MasterCategoryId;
                    _MasterProductResult.CategoryTitle = _Item.CategoryTitle;
                    _MasterProductResult.ProductSKU = _Item.ProductSKU;
                    _MasterProductResult.ProductHSNCode = _Item.ProductHSNCode;
                    _MasterProductResult.ProductBarCode = _Item.ProductBarCode;
                    _MasterProductResult.ProductMainImage = _Item.ProductMainImage;
                    _MasterProductResult.Description = _Item.Description;
                    _MasterProductResult.Specification = _Item.Specification;
                    _MasterProductResult.LegalDisclamer = _Item.LegalDisclamer;
                    _MasterProductResult.SafetyWarning = _Item.SafetyWarning;
                    _MasterProductResult.ProductModel = _Item.ProductModel;
                    _MasterProductResult.Manufacturer = _Item.Manufacturer;
                    _MasterProductResult.DepreciatePercentage = _Item.DepreciatePercentage;
                    _MasterProductResult.ReorderLevel = _Item.ReorderLevel;
                    _MasterProductResult.CountryOfOrigin = _Item.CountryOfOrigin;
                    _MasterProductResult.CountryOfOriginTitle = _AdminCountries.Where(a => a.MasterCountryId == _Item.CountryOfOrigin).Select(a => a.CountryTitle).FirstOrDefault();
                    _MasterProductResult.ProductTaxCode = _Item.ProductTaxCode;
                    _MasterProductResult.ProductTaxCodeTitle = _AdminTaxes.Where(a => a.MasterTaxId == _Item.ProductTaxCode).Select(a => a.TaxTitle).FirstOrDefault();
                    _MasterProductResult.ProductCurrency = _Item.ProductCurrency;
                    _MasterProductResult.ProductCurrencyTitle = _AdminCurrencies.Where(a => a.MasterCurrencyId == _Item.ProductCurrency).Select(a => a.CurrencyTitle).FirstOrDefault(); ;

                    _MasterProductResult.TotalAssetsInStock = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsAssign = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false && (a.MasterEmployeeId != 0)).Count());
                    _MasterProductResult.AssetsInRepair = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.ServiceInExpire = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.WarrantyExpiryDate <= DateTime.Now && a.IsActive == true && a.IsDeadAssets == false && a.IsSaleProduct == false).Count());
                    _MasterProductResult.AssetsInSold = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == false && a.IsSaleProduct == true).Count());
                    _MasterProductResult.AssetsInDead = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsActive == false && a.IsDeadAssets == true && a.IsSaleProduct == false).Count());
                    _MasterProductResult.TotalAssetsCost = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.PurchasePrice) ?? 0);
                    _MasterProductResult.TotalAssetsDepreciatedValue = (_Productdata.Where(a => a.MasterProductId == _Item.MasterProductId && a.IsDeadAssets == false && a.IsSaleProduct == false).Sum(a => a.DepreciatePrice) ?? 0);

                    _MasterProductResult.Sequence = _Item.Sequence;
                    _MasterProductResult.IsActive = _Item.IsActive;
                    _MasterProductResult.EnterById = _Item.EnterById;
                    _MasterProductResult.EnterDate = _Item.EnterDate;
                    _MasterProductResult.ModifiedById = _Item.ModifiedById;
                    _MasterProductResult.ModifiedDate = _Item.ModifiedDate;
                    _MasterProductResult.MaxProductSKU = _Context.ASMasterProductChilds.Where(a => a.MasterProductId == _Item.MasterProductId).Max(a => a.ProductChildSKU);

                    objMasterProductResultList.Add(_MasterProductResult);

                    var _dataChild = (from MPC in _Context.ASMasterProductChilds
                                      join MAA in _Context.ASMasterAssetsAssignments on MPC.MasterProductChildId equals MAA.MasterProductChildId into MAAGroup
                                      from MAA in MAAGroup.DefaultIfEmpty()
                                      where MPC.MasterProductId == _Item.MasterProductId
                                      select new
                                      {
                                          MPC.MasterProductChildId,
                                          MPC.MasterProductId,
                                          MPC.ProductChildSKU,
                                          MPC.ManufacturerPartNumber,
                                          MPC.PurchaseDate,
                                          MPC.PurchasePrice,
                                          MPC.DepreciatePrice,
                                          MPC.WarrantyExpiryDate,
                                          MPC.WarrantyStartDate,
                                          MPC.IterationOfWarranty,
                                          MPC.MasterBranchId,
                                          MPC.MasterEmployeeId,
                                          MPC.MasterVendorId,
                                          MPC.IsDeadAssets,
                                          MPC.IsSaleProduct,
                                          MPC.IsActive,
                                          MasterAssetsAssignmentId = (MAA.MasterAssetsAssignmentId != null ? MAA.MasterAssetsAssignmentId : 0),
                                          MAA.AssetsAssignmentDate,
                                          MAA.IsAssetsDeAssign
                                      });


                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    List<MasterProductAssignChildResult> objMasterProductAssignChildResultList = new List<MasterProductAssignChildResult>();
                    foreach (var _ItemChild in _dataChild.ToList())
                    {
                        MasterProductAssignChildResult _MasterProductAssignChildResult = new MasterProductAssignChildResult();

                        _MasterProductAssignChildResult.MasterProductChildId = _ItemChild.MasterProductChildId;
                        _MasterProductAssignChildResult.MasterProductId = _ItemChild.MasterProductId;
                        _MasterProductAssignChildResult.ProductChildSKU = _ItemChild.ProductChildSKU;
                        _MasterProductAssignChildResult.ManufacturerPartNumber = _ItemChild.ManufacturerPartNumber;
                        _MasterProductAssignChildResult.PurchaseDate = _ItemChild.PurchaseDate;
                        _MasterProductAssignChildResult.PurchasePrice = _ItemChild.PurchasePrice;
                        _MasterProductAssignChildResult.DepreciatePrice = _ItemChild.DepreciatePrice;
                        _MasterProductAssignChildResult.WarrantyExpiryDate = _ItemChild.WarrantyExpiryDate;
                        _MasterProductAssignChildResult.WarrantyStartDate = _ItemChild.WarrantyStartDate;
                        _MasterProductAssignChildResult.IterationOfWarranty = _ItemChild.IterationOfWarranty;
                        _MasterProductAssignChildResult.IsActive = _ItemChild.IsActive;
                        _MasterProductAssignChildResult.IsDeadAssets = _ItemChild.IsDeadAssets;
                        _MasterProductAssignChildResult.IsSaleProduct = _ItemChild.IsSaleProduct;
                        _MasterProductAssignChildResult.MasterBranchId = _ItemChild.MasterBranchId;
                        _MasterProductAssignChildResult.BranchTitle = _AdminBranches.Where(a => a.MasterBranchId == _ItemChild.MasterBranchId).Select(a => a.BranchTitle).FirstOrDefault();
                        _MasterProductAssignChildResult.MasterEmployeeId = _ItemChild.MasterEmployeeId;
                        _MasterProductAssignChildResult.EmployeeName = _AdminEmployees.Where(a => a.MasterEmployeeId == _ItemChild.MasterEmployeeId).Select(a => a.EmployeeName).FirstOrDefault();
                        _MasterProductAssignChildResult.MasterAssetsAssignmentId = _ItemChild.MasterAssetsAssignmentId;
                        _MasterProductAssignChildResult.AssetsAssignmentDate = _ItemChild.AssetsAssignmentDate;
                        _MasterProductAssignChildResult.MasterVendorId = _ItemChild.MasterVendorId;
                        _MasterProductAssignChildResult.VendorTitle = _AdminVendors.Where(a => a.MasterVendorId == _ItemChild.MasterVendorId).Select(a => a.VendorTitle).FirstOrDefault(); ;
                        _MasterProductAssignChildResult.IsAssetsDeAssign = _ItemChild.IsAssetsDeAssign;

                        objMasterProductAssignChildResultList.Add(_MasterProductAssignChildResult);
                    }
                    _MasterProductResult.ProductAssignChildList = objMasterProductAssignChildResultList;

                }

                return objMasterProductResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASMasterProduct(ASMasterProductMerge objASMasterProductMerge)
        {
            using(var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    ASMasterProduct objASMasterProduct = objASMasterProductMerge.ASMasterProduct;
                    ASMasterProductChild objASMasterProductChild = objASMasterProductMerge.ASMasterProductChild;
                    ASMasterAssetsAssignment objASMasterAssetsAssignment = objASMasterProductMerge.ASMasterAssetsAssignment;                    

                    _Context.ASMasterProducts.Add(objASMasterProduct);
                    await _Context.SaveChangesAsync();

                    objASMasterProductChild.MasterProductId = objASMasterProduct.MasterProductId;
                    objASMasterProductChild.ProductChildTitle = objASMasterProduct.ProductTitle;
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


                    if (objASMasterProductChild.MasterEmployeeId!=null && objASMasterProductChild.MasterEmployeeId >0)
                    {
                        //ASMasterAssetsAssignment
                        objASMasterAssetsAssignment.MasterAssetsAssignmentId = 0;
                        objASMasterAssetsAssignment.IsActive = true;
                        objASMasterAssetsAssignment.AssetsAssignmentDate = objASMasterAssetsAssignment.AssetsAssignmentDate;
                        objASMasterAssetsAssignment.MasterProductChildId = objASMasterProductChild.MasterProductChildId;
                        objASMasterAssetsAssignment.MasterEmployeeId = objASMasterProductChild.MasterEmployeeId;

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
        public async Task UpdateASMasterProduct(ASMasterProduct objASMasterProduct)
        {
            try
            {
                _Context.Entry(objASMasterProduct).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASMasterProduct(long MasterProductId)
        {
            try
            {
                var objASMasterProduct = _Context.ASMasterProducts.Find(MasterProductId);
                _Context.ASMasterProducts.Remove(objASMasterProduct);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASMasterProductExists(long MasterProductId)
        {
            try
            {
                return _Context.ASMasterProducts.Any(e => e.MasterProductId == MasterProductId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
