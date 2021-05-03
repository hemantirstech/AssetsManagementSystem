using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterSubCategory
{
    public class MasterSubCategoryViewModel
    {

        [Key]
        public long MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public string SubCategoryCode { get; set; }
        public string SubCategoryDescription { get; set; }
        public string SubCategoryImage { get; set; }
        public long MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }

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
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
