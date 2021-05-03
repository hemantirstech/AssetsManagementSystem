using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADGenCodeMaster", Schema = "dbo")]
    public class ADGenCodeMaster
    {
        [Key]
        public long GenCodeMasterId { get; set; }

        [Required]
        [MaxLength(100)]
        public string GenCodeMasterTitle { get; set; }
        
        [MaxLength(10)]
        public string PrintDesc { get; set; }

        //Foreign key for ADGenCodeType
        public long GenCodeTypeId { get; set; }
        [ForeignKey("GenCodeTypeId")]
        public virtual ADGenCodeType ADGenCodeType { get; set; }

        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
