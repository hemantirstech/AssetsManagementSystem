using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class TransactionLoginResult
    {
        [Key]
        public long TransactionLoginId { get; set; }
        public Nullable<long> MasterLoginId { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
        public Nullable<System.DateTime> LogoutDate { get; set; }
        public string LocationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SessionIP { get; set; }
        public Nullable<long> MasterRegisteredDeviceId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
