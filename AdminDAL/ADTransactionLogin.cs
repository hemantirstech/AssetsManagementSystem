using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADTransactionLogin", Schema = "dbo")]
    public class ADTransactionLogin
    {
        [Key]
        public long TransactionLoginId { get; set; }

        public Nullable<long> MasterLoginId { get; set; }
        [ForeignKey("MasterLoginId")]
        public virtual ADMasterLogin ADMasterLogin { get; set; }


        public Nullable<System.DateTime> LoginDate { get; set; }
        public Nullable<System.DateTime> LogoutDate { get; set; }
        public string LocationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SessionIP { get; set; }


        public Nullable<long> MasterRegisteredDeviceId { get; set; }
        [ForeignKey("MasterRegisteredDeviceId")]
        public virtual ADMasterRegisteredDevice ADMasterRegisteredDevice { get; set; }


        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        
    }
}
