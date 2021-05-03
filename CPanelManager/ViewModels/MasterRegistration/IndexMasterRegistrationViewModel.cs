using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterRegistration
{
    public class IndexMasterRegistrationViewModel : PaginationViewModel
    {
        public List<MasterRegistrationViewModel> MasterRegistrationList { get; set; }
    }
}
