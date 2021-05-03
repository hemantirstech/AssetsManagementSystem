using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterIndustryGroup", Schema = "dbo")]
    public class ADMasterIndustryGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterIndustryGroup()
        {
            this.ADMasterIndustryTypes = new HashSet<ADMasterIndustryType>();
        }

        [Key]
        public long MasterIndustryGroupId { get; set; }
        public string IndustryGroupTitle { get; set; }
        public string IndustryGroupCode { get; set; }
        public string IndustryGroupDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterIndustryType> ADMasterIndustryTypes { get; set; }
        
    }
}
