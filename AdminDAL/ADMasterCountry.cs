using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterCountry", Schema = "dbo")]
    public class ADMasterCountry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterCountry()
        {
            this.ADMasterBranches = new HashSet<ADMasterBranch>();
            this.ADMasterCompanies = new HashSet<ADMasterCompany>();
            this.ADMasterCurrencies = new HashSet<ADMasterCurrency>();
            this.ADMasterEmployees = new HashSet<ADMasterEmployee>();
            this.ADMasterStates = new HashSet<ADMasterState>();
        }

        [Key]
        public long MasterCountryId { get; set; }
        public string CountryTitle { get; set; }
        public string CountryCode { get; set; }
        public string CountryDialCode { get; set; }
        public string CountryFlag { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterBranch> ADMasterBranches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterCompany> ADMasterCompanies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterCurrency> ADMasterCurrencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterEmployee> ADMasterEmployees { get; set; }
        public virtual ICollection<ADMasterState> ADMasterStates { get; set; }
    }
}
