using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPanelManager.ViewModels.MasterProduct
{
    public class AddMasterProductViewModel
    {

        [Key]
        public long MasterProductId { get; set; }

        public long DummyMasterProductId { get; set; }

        [Required(ErrorMessage = "Select category")]
        [Range(1, long.MaxValue, ErrorMessage = "Select category")]
        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }

        [Required(ErrorMessage = "Select sub category")]
        [Range(1, long.MaxValue, ErrorMessage = "Select sub category")]
        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }

        [Required(ErrorMessage = "Select brand")]
        [Range(1, long.MaxValue, ErrorMessage = "Select brand")]
        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }

        [Required(ErrorMessage = "Enter Product SKU Title!")]
        [StringLength(100, MinimumLength = 3)]
        public string ProductSKU { get; set; }
        public string ProductHSNCode { get; set; }
        public string ProductBarCode { get; set; }

        [Required(ErrorMessage = "Enter Product Title!")]
        [StringLength(100, MinimumLength = 3)]
        public string ProductTitle { get; set; }
        public string ProductMainImage { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string LegalDisclamer { get; set; }
        public string SafetyWarning { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }

        public Nullable<decimal> DepreciatePercentage { get; set; }
        public Nullable<int> ReorderLevel { get; set; }

        //CountryOfOrigin = MasterCountryId from AdminWebApi MIcroservice
        public Nullable<long> CountryOfOrigin { get; set; }
        public string CountryOfOriginTitle { get; set; }

        //ProductTaxCode = MasterTaxId from AdminWebApi MIcroservice
        public Nullable<long> ProductTaxCode { get; set; }
        public string ProductTaxCodeTitle { get; set; }

        //ProductCurrency = MasterCurrencyId from AdminWebApi MIcroservice
        public Nullable<long> ProductCurrency { get; set; }
        public string ProductCurrencyTitle { get; set; }


        //=====================================================================================================================================

        public long MasterProductChildId { get; set; }


        public string ProductChildSKU { get; set; }

        public string ProductChildTitle { get; set; }

        [Required(ErrorMessage = "Select serial no")]
        public string ManufacturerPartNumber { get; set; }

        public Nullable<int> ConditionType { get; set; }
        public string ConditionTypeTitle { get; set; }
        public string ConditionNote { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ManufacturingDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        //ProductColour = MasterColour from AdminWebApi MIcroservice
        public Nullable<long> ProductColour { get; set; }
        public string ProductColourTitle { get; set; }

        public Nullable<long> MasterProductSizeId { get; set; }
        public string ProductSizeTitle { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> DepreciatePrice { get; set; }

        

        public Nullable<long> MasterVendorId { get; set; }
        public string VendorTitle { get; set; }

        public Nullable<long> MasterCompanyOwnerId { get; set; }
        public string CompanyOwnerTitle { get; set; }

        public Nullable<bool> IsDeadAssets { get; set; }
        public Nullable<bool> IsTimeToSaleProduct { get; set; }

        public Nullable<bool> IsSaleProduct { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SaleDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> SalePrice { get; set; }


        public Nullable<long> ProductQty { get; set; }
        public Nullable<long> ProductQtyUnit { get; set; }
        public Nullable<int> NumberOfItemIncludeInProduct { get; set; }
        public Nullable<int> ItemPackageQuantity { get; set; }

        public Nullable<int> IterationOfWarranty { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> WarrantyExpiryDate { get; set; }

        public string ServiceURL { get; set; }
        public string ServiceUserName { get; set; }
        public string ServicePassword { get; set; }

        [Required(ErrorMessage = "Select branch")]
        [Range(1, long.MaxValue, ErrorMessage = "Select branch")]
        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }


        public Nullable<long> MasterEmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> AssetsAssignmentDate { get; set; }

        //============================================================================================================================
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }


        //Child Product
        public List<MasterProductAssignChildResult> ProductAssignChildList { get; set; }
    }
}
