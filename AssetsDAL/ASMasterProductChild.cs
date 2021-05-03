using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterProductChild", Schema = "dbo")]
    public partial class ASMasterProductChild
    {
        [Key]
        public long MasterProductChildId { get; set; }
        public long MasterProductId { get; set; }
        [ForeignKey("MasterProductId")]
        public virtual ASMasterProduct ASMasterProducts { get; set; }


        public string ProductChildSKU { get; set; }
        public string ProductChildTitle { get; set; }

        
        public string ManufacturerPartNumber { get; set; }
        public Nullable<int> ConditionType { get; set; }
        public string ConditionNote { get; set; }
        public Nullable<System.DateTime> ManufacturingDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        //ProductColour = MasterColour from AdminWebApi MIcroservice
        public Nullable<long> ProductColour { get; set; }
                
        public Nullable<long> MasterProductSizeId { get; set; }
        [ForeignKey("MasterProductSizeId")]
        public virtual ASMasterProductSize ASMasterProductSizes { get; set; }

        public Nullable<System.DateTime> PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> DepreciatePrice { get; set; }
        public Nullable<long> MasterVendorId { get; set; }


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
        public Nullable<long> MasterEmployeeId { get; set; }
        public Nullable<long> MasterCompanyOwnerId { get; set; }
        public Nullable<long> MasterSubscriptionTypeId { get; set; }

        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
