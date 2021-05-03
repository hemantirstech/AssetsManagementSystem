using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterProductType", Schema = "dbo")]
    public partial class ASMasterProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASMasterProductType()
        {
            this.ASMasterProducts = new HashSet<ASMasterProduct>();
        }

        [Key]
        public long MasterProductTypeId { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeTitle { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ProductTypeImage { get; set; }

        public long MasterSubCategoryId { get; set; }
        [ForeignKey("MasterSubCategoryId")]
        public virtual ASMasterSubCategory ASMasterSubCategory { get; set; }
        
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASMasterProduct> ASMasterProducts { get; set; }
    }
}
