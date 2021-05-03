using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterFunction", Schema = "dbo")]
    public class ADMasterFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterFunction()
        {
            this.ADProfileTaskMappings = new HashSet<ADProfileTaskMapping>();
        }

        [Key]
        public long MasterFunctionId { get; set; }
        public string FunctionTitle { get; set; }
        public string FunctionLink { get; set; }
        public string FunctionIcon { get; set; }
        public string FunctionIconColour { get; set; }
        public Nullable<long> ParentMasterFunctionId { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADProfileTaskMapping> ADProfileTaskMappings { get; set; }
    }
}
