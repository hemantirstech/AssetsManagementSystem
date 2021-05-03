using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterFinancialYear
{
    public class IndexMasterFinancialYearViewModel : PaginationViewModel
    {
        public List<MasterFinancialYearViewModel> MasterFinancialYearList { get; set; }
    }
}
