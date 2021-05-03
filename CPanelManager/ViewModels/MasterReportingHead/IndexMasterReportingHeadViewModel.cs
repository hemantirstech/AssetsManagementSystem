using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterReportingHead
{
    public class IndexMasterReportingHeadViewModel : PaginationViewModel
    {
        public List<MasterReportingHeadViewModel> MasterReportingHeadList { get; set; }
    }
}
