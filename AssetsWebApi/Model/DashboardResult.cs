using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
   
    public partial class DashboardResult
    {
        public List<ProductDetailResult> AssetsCategoryList { get; set; }
        public List<ProductDetailResult> AssetsSubCategoryList { get; set; }

        public long TotalAssetsInStock { get; set; }
        public long AssetsAssign { get; set; }
        public long AssetsInRepair { get; set; }

        public long TotalLaptopInStock { get; set; }
        public long LaptopAssign { get; set; }
        public long LaptopInRepair { get; set; }

        public long MicrosoftInStock { get; set; }
        public long MicrosoftAssign { get; set; }
        public long MicrosoftInExpire { get; set; }

    }
}
