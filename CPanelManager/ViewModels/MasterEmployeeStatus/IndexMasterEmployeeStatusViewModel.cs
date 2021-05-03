using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterEmployeeStatus
{
    public class IndexMasterEmployeeStatusViewModel : PaginationViewModel
    {
        public List<MasterEmployeeStatusViewModel> MasterEmployeeStatusList { get; set; }
    }
}
