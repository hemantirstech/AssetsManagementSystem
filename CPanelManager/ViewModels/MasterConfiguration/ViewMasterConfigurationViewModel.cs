using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterConfiguration
{
    [Serializable()]
    public class ViewMasterConfigurationViewModel
    {
        [Key]
        public long MasterConfigurationId { get; set; }

        [Required(ErrorMessage = "Select Company")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Company")]
        public Nullable<long> MasterCompanyId { get; set; }

        public Nullable<bool> EnableNoActivity5Day { get; set; }
        public Nullable<bool> EnableLoginMACIdAuthentication { get; set; }
        public Nullable<bool> EnablePasswordResetByAdmin { get; set; }
        public Nullable<bool> EnableEmailVerification { get; set; }
        public Nullable<bool> EnableMobileVerification { get; set; }
        public string SMTPServer { get; set; }
        public Nullable<long> SMTPPort { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMSUrl { get; set; }
        public string SMSKey { get; set; }
        public string SMSSenderId { get; set; }
        public string SMSPassword { get; set; }
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
