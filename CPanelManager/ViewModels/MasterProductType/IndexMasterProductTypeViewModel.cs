using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProductType
{
    public class IndexMasterProductTypeViewModel : PaginationViewModel
    {
        public List<MasterProductTypeViewModel> MasterProductTypeList { get; set; }
    }
}
