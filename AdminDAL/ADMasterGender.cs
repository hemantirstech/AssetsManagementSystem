using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterGender", Schema = "dbo")]
    public class ADMasterGender
    {
        [Key]
        public long MasterGenderId { get; set; }
        public string GenderTitle { get; set; }
        public string Gendercode { get; set; }
        public string GenderIcon { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
