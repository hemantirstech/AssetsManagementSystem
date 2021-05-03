using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterDesignation
{
    [Serializable()]
    public class ViewMasterDesignationViewModel
    {
        [Key]
        public long MasterDesignationId { get; set; }

        [Required(ErrorMessage = "Enter Designation Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string DesignationTitle { get; set; }
        public string DesignationCode { get; set; }
        public string DesignationDescription { get; set; }
        public Nullable<long> MasterDepartmentId { get; set; }
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
