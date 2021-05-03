using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{

    public partial class MasterProductChildResult
    {
        [Key]
        public long MasterProductChildId { get; set; }
        public long MasterProductId { get; set; }
        public string ProductTitle { get; set; }

        public string ProductChildSKU { get; set; }
        public string ProductChildTitle { get; set; }

        
        public string ManufacturerPartNumber { get; set; }
        public Nullable<int> ConditionType { get; set; }
        public string ConditionTypeTitle { get; set; }
        public string ConditionNote { get; set; }
        public Nullable<System.DateTime> ManufacturingDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        //ProductColour = MasterColour from AdminWebApi MIcroservice
        public Nullable<long> ProductColour { get; set; }
        public string ProductColourTitle { get; set; }

        public Nullable<long> MasterProductSizeId { get; set; }
        public string ProductSizeTitle { get; set; }

        public Nullable<System.DateTime> PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> DepreciatePrice { get; set; }
        public Nullable<long> MasterVendorId { get; set; }
        public string VendorTitle { get; set; }


        public Nullable<bool> IsDeadAssets { get; set; }
        public Nullable<bool> IsTimeToSaleProduct { get; set; }

        public Nullable<bool> IsSaleProduct { get; set; }

        public Nullable<System.DateTime> SaleDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> SalePrice { get; set; }


        public Nullable<long> ProductQty { get; set; }
        public Nullable<long> ProductQtyUnit { get; set; }
        public Nullable<int> NumberOfItemIncludeInProduct { get; set; }
        public Nullable<int> ItemPackageQuantity { get; set; }

        public Nullable<int> IterationOfWarranty { get; set; }
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }
        public Nullable<System.DateTime> WarrantyExpiryDate { get; set; }

        public string ServiceURL { get; set; }
        public string ServiceUserName { get; set; }
        public string ServicePassword { get; set; }

        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }
        public Nullable<long> MasterEmployeeId { get; set; }
        public string EmployeeName { get; set; }


        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterCategoryType { get; set; }
        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }
        public string ProductSKU { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<decimal> DepreciatePercentage { get; set; }
        public Nullable<int> ReorderLevel { get; set; }


        public long MasterAssetsAssignmentId { get; set; }
        public Nullable<System.DateTime> AssetsAssignmentDate { get; set; }

        public Nullable<bool> IsAssetsDeAssign { get; set; }

        public Nullable<System.DateTime> AssetsDeAssignmentDate { get; set; }
        public string DeAssignReason { get; set; }

        public Nullable<long> MasterLocationId { get; set; }
        public string LocationName { get; set; }

        public Nullable<long> MasterCompanyOwnerId { get; set; }
        public string CompanyOwnerTitle { get; set; }

        public List<TransactionProductHistoryResult> TransactionProductHistoryList { get; set; }
    }
}
