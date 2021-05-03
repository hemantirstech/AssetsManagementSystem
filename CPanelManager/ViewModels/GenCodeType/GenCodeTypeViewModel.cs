using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CPanelManager.ViewModels.GenCodeType
{
    
    public class GenCodeTypeViewModel
    {
        
        public long GenCodeTypeId { get; set; }

        
        public string GenCodeTypeTitle { get; set; }

        
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
