using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterCategory", Schema = "dbo")]
    public partial class ASMasterCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASMasterCategory()
        {
            this.ASMasterSubCategories = new HashSet<ASMasterSubCategory>();
        }

        [Key]
        public long MasterCategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterCategoryType { get; set; }
        public string MasterCategoryDescription { get; set; }
        public string CategoryImage { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASMasterSubCategory> ASMasterSubCategories { get; set; }
    }
}
