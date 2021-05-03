using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterMessageType
{
    public class IndexMasterMessageTypeViewModel : PaginationViewModel
    {
        public List<MasterMessageTypeViewModel> MasterMessageTypeList { get; set; }
    }
}
