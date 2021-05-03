using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProductChild
{
    public class IndexMasterProductChildViewModel : PaginationViewModel
    {
        public List<MasterProductChildViewModel> MasterProductChildList { get; set; }
    }
}
