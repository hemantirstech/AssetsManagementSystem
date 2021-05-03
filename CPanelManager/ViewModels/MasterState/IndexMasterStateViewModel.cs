using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterState
{
    public class IndexMasterStateViewModel : PaginationViewModel
    {
        public List<MasterStateViewModel> MasterStateList { get; set; }
    }
}
