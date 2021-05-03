using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CPanelManager.ViewModels.LicenceAggrement
{
    [Serializable()]
    public class ViewLicenceAggrementViewModel
    {
        [Key]
        public long MasterCompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public string ContactPerson { get; set; }
        public Nullable<long> MasterDesignationId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateofRegistration { get; set; }

        public Nullable<long> MasterCompanyTypeId { get; set; }
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
        public Nullable<long> MasterTimeZoneId { get; set; }
        public Nullable<long> MasterAddressTypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<long> MasterCountryId { get; set; }
        public Nullable<long> MasterStateId { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
