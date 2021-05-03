using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterVendor
{
    public class IndexMasterVendorViewModel : PaginationViewModel
    {
        public List<MasterVendorViewModel> MasterVendorList { get; set; }
    }
}
