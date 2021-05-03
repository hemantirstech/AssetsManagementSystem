using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterLogin", Schema = "dbo")]
    public class ADMasterLogin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterLogin()
        {
            this.ADMasterRegisteredDevices = new HashSet<ADMasterRegisteredDevice>();
            this.ADTransactionLogins = new HashSet<ADTransactionLogin>();
        }

        [Key]
        public long MasterLoginId { get; set; }

        public Nullable<long> MasterRegistrationTypeId { get; set; }
        [ForeignKey("MasterRegistrationTypeId")]
        public virtual ADMasterRegistrationType ADMasterRegistrationType { get; set; }

        public Nullable<long> MasterRegistrationId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public Nullable<long> MasterProfileId { get; set; }
        [ForeignKey("MasterProfileId")]
        public virtual ADMasterProfile ADMasterProfile { get; set; }

        public string VerificationCode { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsFirstLogin { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
       
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterRegisteredDevice> ADMasterRegisteredDevices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADTransactionLogin> ADTransactionLogins { get; set; }
    }
}
