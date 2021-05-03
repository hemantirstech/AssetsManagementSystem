using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterIndustryGroup
{
    public class IndexMasterIndustryGroupViewModel : PaginationViewModel
    {
        public List<MasterIndustryGroupViewModel> MasterIndustryGroupList { get; set; }
    }
}
