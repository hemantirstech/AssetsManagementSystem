using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterCity", Schema = "dbo")]
    public class ADMasterCity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterCity()
        {
        }

        [Key]
        public long MasterCityId { get; set; }

        //Foreign Key
        public Nullable<long> MasterStateId { get; set; }
        [ForeignKey("MasterStateId")]
        public virtual ADMasterState ADMasterState { get; set; }


        public string CityTitle { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
               
    }
}
