using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
    
    public partial class MasterProductResult
    {
        [Key]
        public long MasterProductId { get; set; }

        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }
        public string ProductSKU { get; set; }

        public string MaxProductSKU { get; set; }

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

        public Nullable<long> AssetsAllocated { get; set; }
        public Nullable<long> TotalAssets { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public long TotalAssetsInStock { get; set; }
        public long AssetsAssign { get; set; }
        public long AssetsInRepair { get; set; }
        public long AssetsInDead { get; set; }
        public long AssetsInSold { get; set; }
        public long ServiceInExpire { get; set; }
        public Decimal TotalAssetsCost { get; set; }
        public Decimal TotalAssetsDepreciatedValue { get; set; }

        public List<MasterProductAssignChildResult> ProductAssignChildList { get; set; }

    }
}
