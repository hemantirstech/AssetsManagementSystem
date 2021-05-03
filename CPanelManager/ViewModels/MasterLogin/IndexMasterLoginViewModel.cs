using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterLogin
{
    public class IndexMasterLoginViewModel : PaginationViewModel
    {
        public List<MasterLoginViewModel> MasterLoginList { get; set; }
    }
}
