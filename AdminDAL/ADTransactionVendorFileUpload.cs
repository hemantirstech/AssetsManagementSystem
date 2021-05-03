using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADTransactionVendorFileUpload", Schema = "dbo")]
    public class ADTransactionVendorFileUpload
    {
        public ADTransactionVendorFileUpload()
        {

        }

        [Key]
        public long TransactionVendorFileUploadId { get; set; }     

        public Nullable<long> MasterVendorId { get; set; }
        [ForeignKey("MasterVendorId")]
        public virtual ADMasterVendor ADMasterVendor { get; set; }
        public string TransactionVendorFileName { get; set; }
         public string UploadFile { get; set; }
      
        public long Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }



    }
}
