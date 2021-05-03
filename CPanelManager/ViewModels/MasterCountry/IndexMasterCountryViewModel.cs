using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterCountry
{
    public class IndexMasterCountryViewModel : PaginationViewModel
    {
        public List<MasterCountryViewModel> MasterCountryList { get; set; }
    }
}
