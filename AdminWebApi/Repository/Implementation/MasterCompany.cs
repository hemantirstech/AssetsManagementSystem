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
    public class MasterCompany : IMasterCompanyInterface<MasterCompanyResult>
    {
        readonly AdminContext _Context;

        public MasterCompany(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterCompanyResult> GetAllADMasterCompany()
        {
            try
            {
                var _data = (from MC in _Context.ADMasterCompanies
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
                                 MC.MasterCompanyId,
                                 MC.CompanyTitle,
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

                List<MasterCompanyResult> objMasterCompanyResultList = new List<MasterCompanyResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterCompanyResult = new MasterCompanyResult();

                    _MasterCompanyResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterCompanyResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterCompanyResult.ContactPerson = _Item.ContactPerson;
                    _MasterCompanyResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterCompanyResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterCompanyResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterCompanyResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _MasterCompanyResult.CompanyTypeTitle = _Item.CompanyTypeTitle;
                    _MasterCompanyResult.RegistrationNumber = _Item.RegistrationNumber;
                    _MasterCompanyResult.PANNumber = _Item.PANNumber;
                    _MasterCompanyResult.GSTNumber = _Item.GSTNumber;
                    _MasterCompanyResult.TANNumber = _Item.TANNumber;
                    _MasterCompanyResult.SEZRegistrationNumber = _Item.SEZRegistrationNumber;
                    _MasterCompanyResult.SAC_Code = _Item.SAC_Code;
                    _MasterCompanyResult.LUT_AppliactionReference = _Item.LUT_AppliactionReference;
                    _MasterCompanyResult.CompanyLogo = _Item.CompanyLogo;
                    _MasterCompanyResult.ReportLogo = _Item.ReportLogo;
                    _MasterCompanyResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _MasterCompanyResult.CurrencyTitle = _Item.CurrencyTitle;
                    _MasterCompanyResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterCompanyResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterCompanyResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterCompanyResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterCompanyResult.Address1 = _Item.Address1;
                    _MasterCompanyResult.Address2 = _Item.Address2;
                    _MasterCompanyResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterCompanyResult.CountryTitle = _Item.CountryTitle;
                    _MasterCompanyResult.MasterStateId = _Item.MasterStateId;
                    _MasterCompanyResult.StateTitle = _Item.StateTitle;
                    _MasterCompanyResult.City = _Item.City;
                    _MasterCompanyResult.PinCode = _Item.PinCode;
                    _MasterCompanyResult.MobileNumber = _Item.MobileNumber;
                    _MasterCompanyResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterCompanyResult.Fax = _Item.Fax;
                    _MasterCompanyResult.Email = _Item.Email;
                    _MasterCompanyResult.URL = _Item.URL;                    
                    _MasterCompanyResult.IsActive = _Item.IsActive;
                    _MasterCompanyResult.ActiveColor = "green";
                    _MasterCompanyResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterCompanyResult.IsActive == false)
                    {
                        _MasterCompanyResult.ActiveColor = "red";
                        _MasterCompanyResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterCompanyResultList.Add(_MasterCompanyResult);
                }

                return objMasterCompanyResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterCompanyResult GetADMasterCompanyByID(long MasterCompanyId)
        {
            try
            {
                var _data = (from MC in _Context.ADMasterCompanies
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
                             where MC.MasterCompanyId == MasterCompanyId
                             select new
                             {
                                 MC.MasterCompanyId,
                                 MC.CompanyTitle,
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


                var _Item = _data.Where(a => a.MasterCompanyId == MasterCompanyId).FirstOrDefault(); 

                MasterCompanyResult _MasterCompanyResult = new MasterCompanyResult();
                if (_data != null)
                {
                    _MasterCompanyResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterCompanyResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterCompanyResult.ContactPerson = _Item.ContactPerson;
                    _MasterCompanyResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterCompanyResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterCompanyResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterCompanyResult.MasterCompanyTypeId = _Item.MasterCompanyTypeId;
                    _MasterCompanyResult.CompanyTypeTitle = _Item.CompanyTypeTitle;
                    _MasterCompanyResult.RegistrationNumber = _Item.RegistrationNumber;
                    _MasterCompanyResult.PANNumber = _Item.PANNumber;
                    _MasterCompanyResult.GSTNumber = _Item.GSTNumber;
                    _MasterCompanyResult.TANNumber = _Item.TANNumber;
                    _MasterCompanyResult.SEZRegistrationNumber = _Item.SEZRegistrationNumber;
                    _MasterCompanyResult.SAC_Code = _Item.SAC_Code;
                    _MasterCompanyResult.LUT_AppliactionReference = _Item.LUT_AppliactionReference;
                    _MasterCompanyResult.CompanyLogo = _Item.CompanyLogo;
                    _MasterCompanyResult.ReportLogo = _Item.ReportLogo;
                    _MasterCompanyResult.MasterCurrencyId = _Item.MasterCurrencyId;
                    _MasterCompanyResult.CurrencyTitle = _Item.CurrencyTitle;
                    _MasterCompanyResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterCompanyResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterCompanyResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterCompanyResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterCompanyResult.Address1 = _Item.Address1;
                    _MasterCompanyResult.Address2 = _Item.Address2;
                    _MasterCompanyResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterCompanyResult.CountryTitle = _Item.CountryTitle;
                    _MasterCompanyResult.MasterStateId = _Item.MasterStateId;
                    _MasterCompanyResult.StateTitle = _Item.StateTitle;
                    _MasterCompanyResult.City = _Item.City;
                    _MasterCompanyResult.PinCode = _Item.PinCode;
                    _MasterCompanyResult.MobileNumber = _Item.MobileNumber;
                    _MasterCompanyResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterCompanyResult.Fax = _Item.Fax;
                    _MasterCompanyResult.Email = _Item.Email;
                    _MasterCompanyResult.URL = _Item.URL;                    
                    _MasterCompanyResult.IsActive = _Item.IsActive;
                    _MasterCompanyResult.ActiveColor = "green";
                    _MasterCompanyResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterCompanyResult.IsActive == false)
                    {
                        _MasterCompanyResult.ActiveColor = "red";
                        _MasterCompanyResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                
                return _MasterCompanyResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task InsertADMasterCompany(ADMasterCompany objADMasterCompany)
        {
            try
            {
                _Context.ADMasterCompanies.Add(objADMasterCompany);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterCompany(ADMasterCompany objADMasterCompany)
        {
            try
            {
                _Context.Entry(objADMasterCompany).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterCompany(long MasterCompanyId)
        {
            try
            {
                var objADMasterCompany = _Context.ADMasterCompanies.Find(MasterCompanyId);
                _Context.ADMasterCompanies.Remove(objADMasterCompany);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterCompanyExists(long MasterCompanyId)
        {
            try
            {
                return _Context.ADMasterCompanies.Any(e => e.MasterCompanyId == MasterCompanyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
