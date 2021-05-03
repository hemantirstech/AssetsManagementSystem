﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace CPanelManager.ViewModels.TransactionProductHistory
{

    [Serializable()]
    public class AddTransactionProductHistoryViewModel
    {
        [Key]
        public long TransactionProductHistoryId { get; set; }
        public long MasterProductChildId { get; set; }

       // public virtual ASMasterProductChild ASMasterProductChild { get; set; }

        public Nullable<long> MasterSubscriptionTypeId { get; set; }

        public string ProductTitle { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> SubscriptionPrice { get; set; }
        public Nullable<long> MasterSubscriptionVendorId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SubscriptionDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SubscriptionStartDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SubscriptionExpiryDate { get; set; }
        public string UploadInvoice { get; set; }
        public string UploadDocument { get; set; }
        public string UploadWarretyCard { get; set; }


        public Nullable<long> MasterVendorId { get; set; }
        public string VendorTitle { get; set; }

        public string IndexType { get; set; }

        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
