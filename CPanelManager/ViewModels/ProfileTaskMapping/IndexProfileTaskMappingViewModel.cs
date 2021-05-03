using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.ProfileTaskMapping
{
    public class IndexProfileTaskMappingViewModel:PaginationViewModel
    {
        public List<ViewProfileTaskMappingViewModel> ViewProfileTaskMappingList { get; set; }

        public List<MasterProfileViewModel> MasterProfileViewModelList { get; set; }
    }
}
