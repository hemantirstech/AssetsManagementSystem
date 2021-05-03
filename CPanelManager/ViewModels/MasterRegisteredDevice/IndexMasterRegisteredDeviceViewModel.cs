using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterRegisteredDevice
{
    public class IndexMasterRegisteredDeviceViewModel : PaginationViewModel
    {
        public List<MasterRegisteredDeviceViewModel> MasterRegisteredDeviceList { get; set; }
    }
}
