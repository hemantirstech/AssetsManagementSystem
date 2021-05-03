using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterRegion
{
    public class IndexMasterRegionViewModel : PaginationViewModel
    {
        public List<MasterRegionViewModel> MasterRegionList { get; set; }
    }
}
