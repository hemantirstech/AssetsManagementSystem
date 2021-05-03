using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterBranch", Schema = "dbo")]
    public class ADMasterBranch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterBranch()
        {
            this.ADMasterEmployees = new HashSet<ADMasterEmployee>();
            this.ADMessageNotifications = new HashSet<ADMessageNotification>();
        }

        [Key]
        public long MasterBranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchTitle { get; set; }
        public string ContactPerson { get; set; }

        public Nullable<long> MasterDesignationId { get; set; }
        [ForeignKey("MasterDesignationId")]
        public virtual ADMasterDesignation ADMasterDesignation { get; set; }


        public Nullable<System.DateTime> DateofRegistration { get; set; }

        
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

        public Nullable<long> MasterCompanyId { get; set; }
        [ForeignKey("MasterCompanyId")]
        public virtual ADMasterCompany ADMasterCompany { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMasterEmployee> ADMasterEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMessageNotification> ADMessageNotifications { get; set; }
    }
}
