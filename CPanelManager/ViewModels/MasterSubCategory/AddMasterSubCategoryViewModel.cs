using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterSubCategory
{
    public class AddMasterSubCategoryViewModel
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

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
