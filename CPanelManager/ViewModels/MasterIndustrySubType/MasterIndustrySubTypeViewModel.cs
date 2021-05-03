using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterIndustrySubType
{
    [Serializable()]
    public class MasterIndustrySubTypeViewModel
    {
        [Key]
        public long MasterIndustrySubTypeId { get; set; }

        public Nullable<long> MasterIndustryTypeId { get; set; }
       
        public string IndustrySubTypeTitle { get; set; }
        public string IndustrySubTypeCode { get; set; }
        public string IndustrySubTypeDescription { get; set; }
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
