using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterBrand
{
    public class IndexMasterBrandViewModel : PaginationViewModel
    {
        public List<MasterBrandViewModel> MasterBrandList { get; set; }
    }
}
