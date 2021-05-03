using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterRegisteredDevice", Schema = "dbo")]
    public class ADMasterRegisteredDevice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterRegisteredDevice()
        {
            this.ADTransactionLogins = new HashSet<ADTransactionLogin>();
        }

        [Key]
        public long MasterRegisteredDeviceId { get; set; }

        public Nullable<long> MasterLoginId { get; set; }
        [ForeignKey("MasterLoginId")]
        public virtual ADMasterLogin ADMasterLogin { get; set; }

        public string MacId { get; set; }

        public Nullable<long> MasterTypeOfDeviceId { get; set; }
        [ForeignKey("MasterTypeOfDeviceId")]
        public virtual ADMasterTypeOfDevice ADMasterTypeOfDevice { get; set; }


        public string DeviceVerificationCode { get; set; }
        public Nullable<bool> IsDeviceVerified { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }    
        
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADTransactionLogin> ADTransactionLogins { get; set; }
    }
}
