using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterEmployee
{
    public class IndexMasterEmployeeViewModel : PaginationViewModel
    {
        public List<MasterEmployeeViewModel> MasterEmployeeList { get; set; }
    }
}
