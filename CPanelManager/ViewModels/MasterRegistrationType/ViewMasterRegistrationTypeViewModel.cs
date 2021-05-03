using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterRegistrationType
{
    [Serializable()]
    public class ViewMasterRegistrationTypeViewModel
    {
        [Key]
        public long MasterRegistrationTypeId { get; set; }

        [Required(ErrorMessage = "Enter Registration Type Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string MasterRegistrationTypeTitle { get; set; }
        public string MasterRegistrationCode { get; set; }
        public string MasterRegistrationExpertType { get; set; }
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
