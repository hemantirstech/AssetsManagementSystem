using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterBankAccountType
{
    public class IndexMasterBankAccountTypeViewModel : PaginationViewModel
    {
        public List<MasterBankAccountTypeViewModel> MasterBankAccountTypeList { get; set; }
    }
}
