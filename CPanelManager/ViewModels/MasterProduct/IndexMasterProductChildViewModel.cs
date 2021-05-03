using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterProduct
{
    public class IndexMasterProductChildViewModel
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
        public string ProductHSNCode { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }

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

        public long TotalAssetsInStock { get; set; }
        public long AssetsAssign { get; set; }
        public long AssetsInRepair { get; set; }
        public long AssetsInDead { get; set; }
        public long AssetsInSold { get; set; }
        public long ServiceInExpire { get; set; }
        public Decimal TotalAssetsCost { get; set; }
        public Decimal TotalAssetsDepreciatedValue { get; set; }
        //Child Product
        public List<MasterProductAssignChildResult> ProductAssignChildList { get; set; }
    }
}
