using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProductSize
{
    public class IndexMasterProductSizeViewModel : PaginationViewModel
    {
        public List<MasterProductSizeViewModel> MasterProductSizeList { get; set; }
    }
}
