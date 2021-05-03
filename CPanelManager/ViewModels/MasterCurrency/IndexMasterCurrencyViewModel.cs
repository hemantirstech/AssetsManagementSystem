using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCurrency
{
    public class IndexMasterCurrencyViewModel : PaginationViewModel
    {
        public List<MasterCurrencyViewModel> MasterCurrencyList { get; set; }
    }
}
