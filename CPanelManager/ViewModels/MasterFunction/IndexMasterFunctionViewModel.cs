using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterFunction
{
    public class IndexMasterFunctionViewModel : PaginationViewModel
    {
        public List<MasterFunctionViewModel> MasterFunctionList { get; set; }
    }
}
