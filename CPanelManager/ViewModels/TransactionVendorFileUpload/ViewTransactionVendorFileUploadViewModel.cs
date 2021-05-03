using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.TransactionVendorFileUpload
{
    [Serializable()]
    public class ViewTransactionVendorFileUploadViewModel
    {
        [Key]
        public long TransactionVendorFileUploadId { get; set; }
        public Nullable<long> MasterVendorId { get; set; }
        public string TransactionVendorFileName { get; set; }
         public string UploadFile { get; set; }
         public long Sequence { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }

    }
}
