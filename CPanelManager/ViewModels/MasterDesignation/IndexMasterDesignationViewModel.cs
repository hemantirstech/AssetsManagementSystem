using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterDesignation
{
    public class IndexMasterDesignationViewModel : PaginationViewModel
    {
        public List<MasterDesignationViewModel> MasterDesignationList { get; set; }
    }
}
