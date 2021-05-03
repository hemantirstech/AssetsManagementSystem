using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterProduct", Schema = "dbo")]
    public partial class ASMasterProduct
    {
        [Key]
        public long MasterProductId { get; set; }


        public Nullable<long> MasterSubCategoryId { get; set; }
        [ForeignKey("MasterSubCategoryId")]
        public virtual ASMasterSubCategory ASMasterSubCategories { get; set; }


        //public Nullable<long> MasterProductTypeId { get; set; }
        //[ForeignKey("MasterProductTypeId")]
        //public virtual ASMasterProduct ASMasterProducts { get; set; }


        public Nullable<long> MasterBrandId { get; set; }
        [ForeignKey("MasterBrandId")]
        public virtual ASMasterBrand ASMasterBrands { get; set; }


        public string ProductSKU { get; set; }
        public string ProductHSNCode { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductMainImage { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string LegalDisclamer { get; set; }
        public string SafetyWarning { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> DepreciatePercentage { get; set; }
        public Nullable<int> ReorderLevel { get; set; }

        //CountryOfOrigin = MasterCountryId from AdminWebApi MIcroservice
        public Nullable<long> CountryOfOrigin { get; set; }

        //ProductTaxCode = MasterTaxId from AdminWebApi MIcroservice
        public Nullable<long> ProductTaxCode { get; set; }

        //ProductCurrency = MasterCurrencyId from AdminWebApi MIcroservice
        public Nullable<long> ProductCurrency { get; set; }

        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
