using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterStatus
{
    public class IndexMasterStatusViewModel : PaginationViewModel
    {
        public List<MasterStatusViewModel> MasterStatusList { get; set; }
    }
}
