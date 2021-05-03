using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProfile
{
    public class IndexMasterProfileViewModel : PaginationViewModel
    {
        public List<MasterProfileViewModel> MasterProfileList { get; set; }
    }
}
