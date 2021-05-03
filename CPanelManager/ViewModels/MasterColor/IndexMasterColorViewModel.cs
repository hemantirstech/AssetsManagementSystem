using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterColor
{
    public class IndexMasterColorViewModel : PaginationViewModel
    {
        public List<MasterColorViewModel> MasterColorList { get; set; }
    }
}
