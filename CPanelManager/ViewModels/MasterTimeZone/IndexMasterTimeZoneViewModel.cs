using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterTimeZone
{
    public class IndexMasterTimeZoneViewModel : PaginationViewModel
    {
        public List<MasterTimeZoneViewModel> MasterTimeZoneList { get; set; }
    }
}
