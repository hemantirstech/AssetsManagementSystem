using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterBusinessVerticle", Schema = "dbo")]
    public class ADMasterBusinessVerticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterBusinessVerticle()
        {
        }
        [Key]
        public long MasterBusinessVerticleId { get; set; }
        public string BusinessVerticleTitle { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
    }
}
