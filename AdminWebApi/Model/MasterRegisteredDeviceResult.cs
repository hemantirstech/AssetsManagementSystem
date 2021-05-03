using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class MasterRegisteredDeviceResult
    {
        [Key]
        public long MasterRegisteredDeviceId { get; set; }
        public Nullable<long> MasterLoginId { get; set; }
        public string MacId { get; set; }
        public Nullable<long> MasterTypeOfDeviceId { get; set; }
        public string DeviceVerificationCode { get; set; }
        public Nullable<bool> IsDeviceVerified { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
