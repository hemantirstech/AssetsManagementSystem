using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using AssetsDAL;
namespace AssetsWebApi.Model
{
    public class TransactionProductHistoryResult
    {
        [Key]
        public long TransactionProductHistoryId { get; set; }
        public long MasterProductChildId { get; set; }
        
      //  public virtual ASMasterProductChild ASMasterProductChild { get; set; }

        public Nullable<long> MasterSubscriptionTypeId { get; set; }

      
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

        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
    }
}
