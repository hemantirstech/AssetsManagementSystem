using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASTransactionProductHistory", Schema = "dbo")]
    public partial class ASTransactionProductHistory
    {
        [Key]
        public long TransactionProductHistoryId { get; set; }
        public long MasterProductChildId { get; set; }
        [ForeignKey("MasterProductChildId")]
       // public virtual ASMasterProductChild ASMasterProductChild { get; set; }

        public Nullable<long> MasterSubscriptionTypeId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> SubscriptionPrice { get; set; }
        public Nullable<long> MasterSubscriptionVendorId { get; set; }

        public Nullable<System.DateTime> SubscriptionDate { get; set; }
        public Nullable<System.DateTime> SubscriptionStartDate { get; set; }
        public Nullable<System.DateTime> SubscriptionExpiryDate { get; set; }
        public string UploadInvoice { get; set; }
        public string UploadDocument { get; set; }
        public string UploadWarretyCard { get; set; }

        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
