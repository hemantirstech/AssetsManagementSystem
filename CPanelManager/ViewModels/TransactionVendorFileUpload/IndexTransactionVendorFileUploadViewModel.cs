using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.TransactionVendorFileUpload
{
    public class IndexTransactionVendorFileUploadViewModel : PaginationViewModel
    {
        public List<TransactionVendorFileUploadViewModel> TransactionVendorFileUploadList { get; set; }
    }
}
