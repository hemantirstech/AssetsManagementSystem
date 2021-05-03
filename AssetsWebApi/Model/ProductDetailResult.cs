using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
   
    public partial class ProductDetailResult
    {        
        public long MasterCategoryId { get; set; }
        public Nullable<long> MasterCategoryType { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryColour { get; set; }
        public string CategoryIcon { get; set; }
        public long MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public string ProductChildTitle { get; set; }
        public string ProductChildSKU { get; set; }
        public string ManufacturerPartNumber { get; set; }


        public long TotalAssetsInStock { get; set; }
        public long AssetsAssign { get; set; }
        public long AssetsInRepair { get; set; }
        public long AssetsInDead { get; set; }
        public long AssetsInSold { get; set; }
        public long ServiceInExpire { get; set; }
        public Decimal TotalAssetsCost { get; set; }
        public Decimal TotalAssetsDepreciatedValue { get; set; }
    }
}
