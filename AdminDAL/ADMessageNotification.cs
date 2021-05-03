using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMessageNotification", Schema = "dbo")]
    public class ADMessageNotification
    {
        [Key]
        public long MasterMessageNotificationId { get; set; }
        public Nullable<System.DateTime> MessageDate { get; set; }


        public Nullable<long> MasterMessageTypeId { get; set; }
        [ForeignKey("MasterMessageTypeId")]
        public virtual ADMasterMessageType ADMasterMessageType { get; set; }


        public Nullable<long> MessageFrom { get; set; }
        public Nullable<long> MessageTo { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDescription { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsSend { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string ShareTo { get; set; }

        public Nullable<long> MasterCompanyId { get; set; }
        [ForeignKey("MasterCompanyId")]
        public virtual ADMasterCompany ADMasterCompany { get; set; }


        public Nullable<long> MasterBranchId { get; set; }
        [ForeignKey("MasterBranchId")]
        public virtual ADMasterBranch ADMasterBranch { get; set; }


        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
        
        
    }
}
