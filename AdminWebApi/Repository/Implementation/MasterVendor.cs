using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterVendor : IMasterVendorInterface<MasterVendorResult>
    {
        readonly AdminContext _Context;

        public MasterVendor(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterVendorResult> GetAllADMasterVendor()
        {
            try
            {
                var _data = (from MC in _Context.ADMasterVendors
                             join MD in _Context.ADMasterDesignations on MC.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join CT in _Context.ADMasterCompanyTypes on MC.MasterCompanyTypeId equals CT.MasterCompanyTypeId into CTGroup
                             from CT in CTGroup.DefaultIfEmpty()
                             join TZ in _Context.ADMasterTimeZones on MC.MasterTimeZoneId equals TZ.MasterTimeZoneId into TZGroup
                             from TZ in TZGroup.DefaultIfEmpty()
                             join CR in _Context.ADMasterCurrencies on MC.MasterCurrencyId equals CR.MasterCurrencyId into CRGroup
                             from CR in CRGroup.DefaultIfEmpty()
                             join AT in _Context.ADMasterAddressTypes on MC.MasterAddressTypeId equals AT.MasterAddressTypeId into ATGroup
                             from AT in ATGroup.DefaultIfEmpty()
                             join CC in _Context.ADMasterCountries on MC.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on MC.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             select new
                             {
                                 MC.MasterVendorId,
                                 MC.VendorTitle,
                                 MC.ContactPerson,
                                 MC.MasterDesignationId,
                                 MC.DateofRegistration,
                                 MC.MasterCompanyTypeId,
                                 MC.RegistrationNumber,
                                 MC.PANNumber,
                                 MC.GSTNumber,
                                 MC.TANNumber,
                                 MC.SEZRegistrationNumber,
                                 MC.SAC_Code,
                                 MC.LUT_AppliactionReference,
                                 MC.CompanyLogo,
                                 MC.ReportLogo,
                                 MC.MasterCurrencyId,
                                 MC.MasterTimeZoneId,
                                 MC.MasterAddressTypeId,
                                 MC.Address1,
                                 MC.Address2,
                                 MC.MasterCountryId,
                                 MC.MasterStateId,
                                 MC.City,
                                 MC.PinCode,
                                 MC.PhoneNumber,
                                 MC.MobileNumber,
                                 MC.Fax,
                                 MC.Email,
                                 MC.URL,
                                 MC.IsActive,
                                 MD.DesignationTitle,
                                 CT.CompanyTypeTitle,
                                 TZ.TimeZoneTitle,
                                 CR.CurrencyTitle,
                                 AT.AddressTypeTitle,
                                 CC.CountryTitle,
                                 MS.StateTitle
                             });

                List<MasterVendorResult> objMasterVendorResultList = new List<MasterVendorResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterVendorResult = new MasterVendorResult();

                    _MasterVendorResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterVendorResult.VendorTitle = _Item.VendorTitle;
                    _MasterVendorResult.ContactPerson = _Item.ContactPerson;
                    _MasterVendorResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterVendorResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterVendorResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterVendorResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _MasterVendorResult.CompanyTypeTitle = _Item.CompanyTypeTitle;
                    _MasterVendorResult.RegistrationNumber = _Item.RegistrationNumber;
                    _MasterVendorResult.PANNumber = _Item.PANNumber;
                    _MasterVendorResult.GSTNumber = _Item.GSTNumber;
                    _MasterVendorResult.TANNumber = _Item.TANNumber;
                    _MasterVendorResult.SEZRegistrationNumber = _Item.SEZRegistrationNumber;
                    _MasterVendorResult.SAC_Code = _Item.SAC_Code;
                    _MasterVendorResult.LUT_AppliactionReference = _Item.LUT_AppliactionReference;
                    _MasterVendorResult.CompanyLogo = _Item.CompanyLogo;
                    _MasterVendorResult.ReportLogo = _Item.ReportLogo;
                    _MasterVendorResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _MasterVendorResult.CurrencyTitle = _Item.CurrencyTitle;
                    _MasterVendorResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterVendorResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterVendorResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterVendorResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterVendorResult.Address1 = _Item.Address1;
                    _MasterVendorResult.Address2 = _Item.Address2;
                    _MasterVendorResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterVendorResult.CountryTitle = _Item.CountryTitle;
                    _MasterVendorResult.MasterStateId = _Item.MasterStateId;
                    _MasterVendorResult.StateTitle = _Item.StateTitle;
                    _MasterVendorResult.City = _Item.City;
                    _MasterVendorResult.PinCode = _Item.PinCode;
                    _MasterVendorResult.MobileNumber = _Item.MobileNumber;
                    _MasterVendorResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterVendorResult.Fax = _Item.Fax;
                    _MasterVendorResult.Email = _Item.Email;
                    _MasterVendorResult.URL = _Item.URL;                    
                    _MasterVendorResult.IsActive = _Item.IsActive;
                    _MasterVendorResult.ActiveColor = "green";
                    _MasterVendorResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterVendorResult.IsActive == false)
                    {
                        _MasterVendorResult.ActiveColor = "red";
                        _MasterVendorResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterVendorResultList.Add(_MasterVendorResult);
                }

                return objMasterVendorResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterVendorResult GetADMasterVendorByID(long MasterVendorId)
        {
            try
            {
                var _data = (from MC in _Context.ADMasterVendors
                             join MD in _Context.ADMasterDesignations on MC.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join CT in _Context.ADMasterCompanyTypes on MC.MasterCompanyTypeId equals CT.MasterCompanyTypeId into CTGroup
                             from CT in CTGroup.DefaultIfEmpty()
                             join TZ in _Context.ADMasterTimeZones on MC.MasterTimeZoneId equals TZ.MasterTimeZoneId into TZGroup
                             from TZ in TZGroup.DefaultIfEmpty()
                             join CR in _Context.ADMasterCurrencies on MC.MasterCurrencyId equals CR.MasterCurrencyId into CRGroup
                             from CR in CRGroup.DefaultIfEmpty()
                             join AT in _Context.ADMasterAddressTypes on MC.MasterAddressTypeId equals AT.MasterAddressTypeId into ATGroup
                             from AT in ATGroup.DefaultIfEmpty()
                             join CC in _Context.ADMasterCountries on MC.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on MC.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             where MC.MasterVendorId == MasterVendorId
                             select new
                             {
                                 MC.MasterVendorId,
                                 MC.VendorTitle,
                                 MC.ContactPerson,
                                 MC.MasterDesignationId,
                                 MC.DateofRegistration,
                                 MC.MasterCompanyTypeId,
                                 MC.RegistrationNumber,
                                 MC.PANNumber,
                                 MC.GSTNumber,
                                 MC.TANNumber,
                                 MC.SEZRegistrationNumber,
                                 MC.SAC_Code,
                                 MC.LUT_AppliactionReference,
                                 MC.CompanyLogo,
                                 MC.ReportLogo,
                                 MC.MasterCurrencyId,
                                 MC.MasterTimeZoneId,
                                 MC.MasterAddressTypeId,
                                 MC.Address1,
                                 MC.Address2,
                                 MC.MasterCountryId,
                                 MC.MasterStateId,
                                 MC.City,
                                 MC.PinCode,
                                 MC.PhoneNumber,
                                 MC.MobileNumber,
                                 MC.Fax,
                                 MC.Email,
                                 MC.URL,
                                 MC.IsActive,
                                 MD.DesignationTitle,
                                 CT.CompanyTypeTitle,
                                 TZ.TimeZoneTitle,
                                 CR.CurrencyTitle,
                                 AT.AddressTypeTitle,
                                 CC.CountryTitle,
                                 MS.StateTitle
                             });


                var _Item = _data.Where(a => a.MasterVendorId == MasterVendorId).FirstOrDefault(); 

                MasterVendorResult _MasterVendorResult = new MasterVendorResult();
                if (_data != null)
                {
                    _MasterVendorResult.MasterVendorId = _Item.MasterVendorId;
                    _MasterVendorResult.VendorTitle = _Item.VendorTitle;
                    _MasterVendorResult.ContactPerson = _Item.ContactPerson;
                    _MasterVendorResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterVendorResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterVendorResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterVendorResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _MasterVendorResult.CompanyTypeTitle = _Item.CompanyTypeTitle;
                    _MasterVendorResult.RegistrationNumber = _Item.RegistrationNumber;
                    _MasterVendorResult.PANNumber = _Item.PANNumber;
                    _MasterVendorResult.GSTNumber = _Item.GSTNumber;
                    _MasterVendorResult.TANNumber = _Item.TANNumber;
                    _MasterVendorResult.SEZRegistrationNumber = _Item.SEZRegistrationNumber;
                    _MasterVendorResult.SAC_Code = _Item.SAC_Code;
                    _MasterVendorResult.LUT_AppliactionReference = _Item.LUT_AppliactionReference;
                    _MasterVendorResult.CompanyLogo = _Item.CompanyLogo;
                    _MasterVendorResult.ReportLogo = _Item.ReportLogo;
                    _MasterVendorResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _MasterVendorResult.CurrencyTitle = _Item.CurrencyTitle;
                    _MasterVendorResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterVendorResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterVendorResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterVendorResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterVendorResult.Address1 = _Item.Address1;
                    _MasterVendorResult.Address2 = _Item.Address2;
                    _MasterVendorResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterVendorResult.CountryTitle = _Item.CountryTitle;
                    _MasterVendorResult.MasterStateId = _Item.MasterStateId;
                    _MasterVendorResult.StateTitle = _Item.StateTitle;
                    _MasterVendorResult.City = _Item.City;
                    _MasterVendorResult.PinCode = _Item.PinCode;
                    _MasterVendorResult.MobileNumber = _Item.MobileNumber;
                    _MasterVendorResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterVendorResult.Fax = _Item.Fax;
                    _MasterVendorResult.Email = _Item.Email;
                    _MasterVendorResult.URL = _Item.URL;
                    _MasterVendorResult.IsActive = _Item.IsActive;
                    _MasterVendorResult.ActiveColor = "green";
                    _MasterVendorResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterVendorResult.IsActive == false)
                    {
                        _MasterVendorResult.ActiveColor = "red";
                        _MasterVendorResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }

                return _MasterVendorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task InsertADMasterVendor(ADMasterVendor objADMasterVendor)
        {
            try
            {
                _Context.ADMasterVendors.Add(objADMasterVendor);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterVendor(ADMasterVendor objADMasterVendor)
        {
            try
            {
                _Context.Entry(objADMasterVendor).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterVendor(long MasterVendorId)
        {
            try
            {
                var objADMasterVendor = _Context.ADMasterVendors.Find(MasterVendorId);
                _Context.ADMasterVendors.Remove(objADMasterVendor);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterVendorExists(long MasterVendorId)
        {
            try
            {
                return _Context.ADMasterVendors.Any(e => e.MasterVendorId == MasterVendorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
