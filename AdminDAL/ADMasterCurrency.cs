using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterCurrency", Schema = "dbo")]
    public class ADMasterCurrency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterCurrency()
        {
            this.ADMasterCompanies = new HashSet<ADMasterCompany>();
        }

        [Key]
        public long MasterCurrencyId { get; set; }
        public string CurrencyTitle { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<long> MasterCountryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ADMasterCountry ADMasterCountry { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterCompany> ADMasterCompanies { get; set; }

    }
}
