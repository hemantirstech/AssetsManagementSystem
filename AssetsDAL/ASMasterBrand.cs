using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterBrand", Schema = "dbo")]
    public partial class ASMasterBrand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASMasterBrand()
        {
            this.ASMasterProducts = new HashSet<ASMasterProduct>();
        }

        [Key]
        public long MasterBrandId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandLogo { get; set; }
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
