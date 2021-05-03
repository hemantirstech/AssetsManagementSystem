using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterAssetsAssignment", Schema = "dbo")]
    public partial class ASMasterAssetsAssignment
    {        
        [Key]
        public long MasterAssetsAssignmentId { get; set; }
        public Nullable<System.DateTime> AssetsAssignmentDate { get; set; }
        public long MasterProductChildId { get; set; }
        [ForeignKey("MasterProductChildId")]
        public virtual ASMasterProductChild ASMasterProductChilds { get; set; }

        public Nullable<long> MasterEmployeeId { get; set; }

        public Nullable<bool> IsAssetsDeAssign { get; set; }

        public Nullable<System.DateTime> AssetsDeAssignmentDate { get; set; }
        public string DeAssignReason { get; set; }

        public Nullable<long> MasterLocationId { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
