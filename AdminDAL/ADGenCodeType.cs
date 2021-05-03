using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADGenCodeType", Schema = "dbo")]
    public class ADGenCodeType
    {
        [Key]
        public long GenCodeTypeId { get; set; }

        [Required]
        public string GenCodeTypeTitle { get; set; }                
        public string GenCodeTypePrintDesc { get; set; }                
        public string GenCodeTypeDesc { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<ADGenCodeMaster> ADGenCodeMasters { get; set; }
    }
}
