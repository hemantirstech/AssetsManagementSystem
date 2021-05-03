using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterErrorLog
{
    public class IndexMasterErrorLogViewModel : PaginationViewModel
    {
        public List<MasterErrorLogViewModel> MasterErrorLogList { get; set; }
    }
}
