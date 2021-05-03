using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCompanyType
{
    public class IndexMasterCompanyTypeViewModel : PaginationViewModel
    {
        public List<MasterCompanyTypeViewModel> MasterCompanyTypeList { get; set; }
    }
}
