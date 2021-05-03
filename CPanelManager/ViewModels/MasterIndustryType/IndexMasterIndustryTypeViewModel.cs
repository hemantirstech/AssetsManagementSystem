using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterIndustryType
{
    public class IndexMasterIndustryTypeViewModel : PaginationViewModel
    {
        public List<MasterIndustryTypeViewModel> MasterIndustryTypeList { get; set; }
    }
}
