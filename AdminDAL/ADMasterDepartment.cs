using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterDepartment", Schema = "dbo")]
    public class ADMasterDepartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterDepartment()
        {
            this.ADMasterDesignations = new HashSet<ADMasterDesignation>();
            this.ADMasterEmployees = new HashSet<ADMasterEmployee>();
        }

        [Key]
        public long MasterDepartmentId { get; set; }
        public string DepartmentTitle { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterDesignation> ADMasterDesignations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterEmployee> ADMasterEmployees { get; set; }
    }
}
