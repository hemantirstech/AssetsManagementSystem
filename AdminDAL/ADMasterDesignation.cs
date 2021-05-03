using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterDesignation", Schema = "dbo")]
    public class ADMasterDesignation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterDesignation()
        {
            this.ADMasterCompanies = new HashSet<ADMasterCompany>();
            this.ADMasterEmployees = new HashSet<ADMasterEmployee>();
            this.ADMasterBranches = new HashSet<ADMasterBranch>();
        }

        [Key]
        public long MasterDesignationId { get; set; }
        public string DesignationTitle { get; set; }
        public string DesignationCode { get; set; }
        public string DesignationDescription { get; set; }
        public Nullable<long> MasterDepartmentId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterBranch> ADMasterBranches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterCompany> ADMasterCompanies { get; set; }
        public virtual ADMasterDepartment ADMasterDepartment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterEmployee> ADMasterEmployees { get; set; }
    }
}
