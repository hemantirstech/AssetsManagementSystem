using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.TransactionProductHistory
{
    public class IndexTransactionProductHistoryViewModel : PaginationViewModel
    {
        public List<TransactionProductHistoryViewModel> TransactionProductHistoryList { get; set; }
    }
}
