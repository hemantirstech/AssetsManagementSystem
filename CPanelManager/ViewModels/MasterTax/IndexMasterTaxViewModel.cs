using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterTax
{
    public class IndexMasterTaxViewModel : PaginationViewModel
    {
        public List<MasterTaxViewModel> MasterTaxList { get; set; }
    }
}
