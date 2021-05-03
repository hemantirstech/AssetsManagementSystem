using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterIndustrySubType", Schema = "dbo")]
    public class ADMasterIndustrySubType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterIndustrySubType()
        {

        }

        [Key]
        public long MasterIndustrySubTypeId { get; set; }

        public Nullable<long> MasterIndustryTypeId { get; set; }
        [ForeignKey("MasterIndustryTypeId")]
        public virtual ADMasterIndustryType ADMasterIndustryType { get; set; }

        public string IndustrySubTypeTitle { get; set; }
        public string IndustrySubTypeCode { get; set; }
        public string IndustrySubTypeDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
    }
}
