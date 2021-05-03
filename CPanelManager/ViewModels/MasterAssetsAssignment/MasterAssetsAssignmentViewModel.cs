﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPanelManager.ViewModels.MasterAssetsAssignment
{
    [Serializable()]
    public class MasterAssetsAssignmentViewModel
    {
        [Key]
        public long MasterAssetsAssignmentId { get; set; }
        public Nullable<System.DateTime> AssetsAssignmentDate { get; set; }
        public long MasterProductChildId { get; set; }
        public string ProductChildTitle { get; set; }

        public Nullable<long> MasterEmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public Nullable<bool> IsAssetsDeAssign { get; set; }

        public Nullable<System.DateTime> AssetsDeAssignmentDate { get; set; }
        public string DeAssignReason { get; set; }

        public Nullable<long> MasterLocationId { get; set; }
        public string LocationName { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }



        public long MasterProductId { get; set; }
        public string ProductTitle { get; set; }

        public string ProductChildSKU { get; set; }
        public string ManufacturerPartNumber { get; set; }

        public Nullable<int> ConditionType { get; set; }
        public string ConditionTypeTitle { get; set; }
        

        [DataType(DataType.Date)]
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

        [Required(ErrorMessage = "Select branch")]
        [Range(1, long.MaxValue, ErrorMessage = "Select branch")]
        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }

        //====================================================================================================================================
        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }

        public string BrandTitle { get; set; }

        public string ProductSKU { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> DepreciatePercentage { get; set; }
        public Nullable<int> ReorderLevel { get; set; }
    }
}
