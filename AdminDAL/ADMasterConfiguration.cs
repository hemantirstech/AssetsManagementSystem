using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterConfiguration", Schema = "dbo")]
    public class ADMasterConfiguration
    {
        [Key]
        public long MasterConfigurationId { get; set; }


        public Nullable<long> MasterCompanyId { get; set; }
        [ForeignKey("MasterCompanyId")]
        public virtual ADMasterCompany ADMasterCompany { get; set; }


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
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
    }
}
