using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterSubCategory
{
    public class IndexMasterSubCategoryViewModel : PaginationViewModel
    {
        public List<MasterSubCategoryViewModel> MasterSubCategoryList { get; set; }
    }
}
