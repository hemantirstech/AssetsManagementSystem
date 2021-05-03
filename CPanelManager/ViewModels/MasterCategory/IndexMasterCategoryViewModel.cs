using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCategory
{
    public class IndexMasterCategoryViewModel : PaginationViewModel
    {
        public List<MasterCategoryViewModel> MasterCategoryList { get; set; }
    }
}
