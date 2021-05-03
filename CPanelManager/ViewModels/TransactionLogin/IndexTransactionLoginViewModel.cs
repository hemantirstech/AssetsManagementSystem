using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.TransactionLogin
{
    public class IndexTransactionLoginViewModel : PaginationViewModel
    {
        public List<TransactionLoginViewModel> TransactionLoginList { get; set; }
    }
}
