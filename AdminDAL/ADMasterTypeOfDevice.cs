using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterTypeOfDevice", Schema = "dbo")]
    public class ADMasterTypeOfDevice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterTypeOfDevice()
        {
            this.ADMasterRegisteredDevices = new HashSet<ADMasterRegisteredDevice>();
        }

        [Key]
        public long MasterTypeOfDeviceId { get; set; }
        public string TypeOfDeviceTitle { get; set; }
        public string TypeOfDeviceName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterRegisteredDevice> ADMasterRegisteredDevices { get; set; }
    }
}
