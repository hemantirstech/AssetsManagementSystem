using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterBranch
{
    public class IndexMasterBranchViewModel : PaginationViewModel
    {
        public List<MasterBranchViewModel> MasterBranchList { get; set; }
    }
}
