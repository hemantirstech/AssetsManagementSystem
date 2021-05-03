using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterAddressType
{
    public class IndexMasterAddressTypeViewModel : PaginationViewModel
    {
        public List<MasterAddressTypeViewModel> MasterAddressTypeList { get; set; }
    }
}
