using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterRegistration
{
    [Serializable()]
    public class ViewMasterRegistrationViewModel
    {
        [Key]
        public long MasterRegistrationId { get; set; }

        public Nullable<long> MasterRegistrationTypeId { get; set; }
       
        public Nullable<long> MasterEmployeeId { get; set; }
       
        public Nullable<long> MasterBDPId { get; set; }
        public Nullable<long> MasterClientId { get; set; }
        public Nullable<long> MasterResearchProfileId { get; set; }

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
