using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterRegistrationType
{
    public class IndexMasterRegistrationTypeViewModel : PaginationViewModel
    {
        public List<MasterRegistrationTypeViewModel> MasterRegistrationTypeList { get; set; }
    }
}
