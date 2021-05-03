using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterProfile", Schema = "dbo")]
    public class ADMasterProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterProfile()
        {
            this.ADMasterLogins = new HashSet<ADMasterLogin>();
            this.ADProfileTaskMappings = new HashSet<ADProfileTaskMapping>();
        }

        [Key]
        public long MasterProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileDescription { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsInsert { get; set; }
        public Nullable<bool> IsUpdate { get; set; }
        public Nullable<bool> IsSelect { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterLogin> ADMasterLogins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADProfileTaskMapping> ADProfileTaskMappings { get; set; }
    }
}
