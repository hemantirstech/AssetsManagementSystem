using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterConfiguration
{
    public class IndexMasterConfigurationViewModel : PaginationViewModel
    {
        public List<MasterConfigurationViewModel> MasterConfigurationList { get; set; }
    }
}
