using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCategory
{
    [Serializable()]
    public class ViewMasterCategoryViewModel
    {
        [Key]
        public long MasterCategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterCategoryType { get; set; }
        public string MasterCategoryDescription { get; set; }
        public string CategoryImage { get; set; }
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
