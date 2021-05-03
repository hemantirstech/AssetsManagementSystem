using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterIndustryType", Schema = "dbo")]
    public class ADMasterIndustryType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterIndustryType()
        {
            this.ADMasterIndustrySubTypes = new HashSet<ADMasterIndustrySubType>();
        }

        [Key]
        public long MasterIndustryTypeId { get; set; }

        public Nullable<long> MasterIndustryGroupId { get; set; }
        [ForeignKey("MasterIndustryGroupId")]
        public virtual ADMasterIndustryGroup ADMasterIndustryGroup { get; set; }


        public string IndustryTypeTitle { get; set; }
        public string IndustryTypeCode { get; set; }
        public string IndustryTypeDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterIndustrySubType> ADMasterIndustrySubTypes { get; set; }
       
    }
}
