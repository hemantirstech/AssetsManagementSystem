using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterCompanyType
{
    [Serializable()]
    public class MasterCompanyTypeViewModel
    {

        [Key]
        public long MasterCompanyTypeId { get; set; }

        [Required(ErrorMessage = "Enter Company Type Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string CompanyTypeTitle { get; set; }
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
