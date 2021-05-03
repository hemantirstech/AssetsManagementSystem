using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterStatus", Schema = "dbo")]
    public class ADMasterStatus
    {
        [Key]
        public long MasterStatusId { get; set; }
        public string StatusTitle { get; set; }
        public string StatusCode { get; set; }

        public Nullable<long> MasterColorId { get; set; }
        [ForeignKey("MasterColorId")]
        public virtual ADMasterColor ADMasterColor { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
    }
}
