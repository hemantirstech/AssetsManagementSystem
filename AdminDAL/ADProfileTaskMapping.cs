using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADProfileTaskMapping", Schema = "dbo")]
    public class ADProfileTaskMapping
    {
        [Key]
        public long MasterProfileTaskMappingId { get; set; }


        public Nullable<long> MasterProfileId { get; set; }
        [ForeignKey("MasterProfileId")]
        public virtual ADMasterProfile ADMasterProfile { get; set; }


        public Nullable<long> MasterFunctionId { get; set; }
        [ForeignKey("MasterFunctionId")]
        public virtual ADMasterFunction ADMasterFunction { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsInsert { get; set; }
        public Nullable<bool> IsUpdate { get; set; }
        public Nullable<bool> IsSelect { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
        
    }
}
