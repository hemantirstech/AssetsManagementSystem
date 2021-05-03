using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterCompany", Schema = "dbo")]
    public class ADMasterCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterCompany()
        {
            this.ADMasterBranches = new HashSet<ADMasterBranch>();
            this.ADMasterConfigurations = new HashSet<ADMasterConfiguration>();            
            this.ADMessageNotifications = new HashSet<ADMessageNotification>();
        }

        [Key]
        public long MasterCompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public string ContactPerson { get; set; }

        public Nullable<long> MasterDesignationId { get; set; }
        [ForeignKey("MasterDesignationId")]
        public virtual ADMasterDesignation ADMasterDesignation { get; set; }

        public Nullable<System.DateTime> DateofRegistration { get; set; }


        public Nullable<long> MasterCompanyTypeId { get; set; }
        [ForeignKey("MasterCompanyTypeId")]
        public virtual ADMasterCompanyType ADMasterCompanyType { get; set; }


        public string RegistrationNumber { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string SEZRegistrationNumber { get; set; }
        public string SAC_Code { get; set; }
        public string LUT_AppliactionReference { get; set; }
        public string CompanyLogo { get; set; }
        public string ReportLogo { get; set; }
        
        public Nullable<long> MasterCurrencyId { get; set; }
        [ForeignKey("MasterCurrencyId")]
        public virtual ADMasterCurrency ADMasterCurrency { get; set; }


        public Nullable<long> MasterTimeZoneId { get; set; }
        [ForeignKey("MasterTimeZoneId")]
        public virtual ADMasterTimeZone ADMasterTimeZone { get; set; }

        public Nullable<long> MasterAddressTypeId { get; set; }
        [ForeignKey("MasterAddressTypeId")]
        public virtual ADMasterAddressType ADMasterAddressType { get; set; }


        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public Nullable<long> MasterCountryId { get; set; }
        [ForeignKey("MasterCountryId")]
        public virtual ADMasterCountry ADMasterCountry { get; set; }

        public Nullable<long> MasterStateId { get; set; }
        [ForeignKey("MasterStateId")]
        public virtual ADMasterState ADMasterState { get; set; }

        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterBranch> ADMasterBranches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterConfiguration> ADMasterConfigurations { get; set; }
        
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMessageNotification> ADMessageNotifications { get; set; }
        
    }
}
