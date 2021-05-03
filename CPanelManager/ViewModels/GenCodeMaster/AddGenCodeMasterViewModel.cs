using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.GenCodeMaster
{
 
    [Serializable()]
    public class AddGenCodeMasterViewModel
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
        
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
