using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterRegistration", Schema = "dbo")]
    public class ADMasterRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterRegistration()
        {
            
        }

        [Key]
        public long MasterRegistrationId { get; set; }

        public Nullable<long> MasterRegistrationTypeId { get; set; }
        [ForeignKey("MasterRegistrationTypeId")]
        public virtual ADMasterRegistrationType ADMasterRegistrationType { get; set; }

        public Nullable<long> MasterEmployeeId { get; set; }
        [ForeignKey("MasterEmployeeId")]
        public virtual ADMasterEmployee ADMasterEmployee { get; set; }

        public Nullable<long> MasterBDPId { get; set; }
        public Nullable<long> MasterClientId { get; set; }
        public Nullable<long> MasterResearchProfileId { get; set; }
        
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        
        
        
    }
}
