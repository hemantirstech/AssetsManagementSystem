using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterEmployeeType
{
    public class IndexMasterEmployeeTypeViewModel : PaginationViewModel
    {
        public List<MasterEmployeeTypeViewModel> MasterEmployeeTypeList { get; set; }
    }
}
