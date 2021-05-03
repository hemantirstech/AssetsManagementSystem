using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.GenCodeType
{
    [Serializable()]
    public class AddGenCodeTypeViewModel
    {
        [Key]
        public long GenCodeTypeId { get; set; }

        [Required(ErrorMessage = "Enter genCode type!")]
        [StringLength(30, MinimumLength = 3)]
        public string GenCodeTypeTitle { get; set; }

        [Required(ErrorMessage = "Enter print description!")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Print description should contain 2 or 5 character!")]
        public string GenCodeTypePrintDesc { get; set; }

        public string GenCodeTypeDesc { get; set; }
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
