using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CPanelManager.ViewModels;

namespace CPanelManager.ViewModels.GenCodeType
{
    public class IndexGenCodeTypeViewModel : PaginationViewModel
    {
        public List<GenCodeTypeViewModel> GenCodeTypeList { get; set; }        
    }
}
