using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterPaymentType
{
    public class IndexMasterPaymentTypeViewModel : PaginationViewModel
    {
        public List<MasterPaymentTypeViewModel> MasterPaymentTypeList { get; set; }
    }
}
