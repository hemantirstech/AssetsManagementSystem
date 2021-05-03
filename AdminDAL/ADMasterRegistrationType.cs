using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterRegistrationType", Schema = "dbo")]
    public class ADMasterRegistrationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterRegistrationType()
        {
            this.ADMasterLogins = new HashSet<ADMasterLogin>();
            this.ADMasterRegistrations = new HashSet<ADMasterRegistration>();
        }

        [Key]
        public long MasterRegistrationTypeId { get; set; }
        public string MasterRegistrationTypeTitle { get; set; }
        public string MasterRegistrationCode { get; set; }
        public string MasterRegistrationExpertType { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterRegistration> ADMasterRegistrations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterLogin> ADMasterLogins { get; set; }
    }
}
