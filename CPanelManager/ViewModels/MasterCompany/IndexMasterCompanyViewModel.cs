using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCompany
{
    public class IndexMasterCompanyViewModel : PaginationViewModel
    {
        public List<MasterCompanyViewModel> MasterCompanyList { get; set; }
    }
}
