using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCity
{
    public class IndexMasterCityViewModel : PaginationViewModel
    {
        public List<MasterCityViewModel> MasterCityList { get; set; }
    }
}
