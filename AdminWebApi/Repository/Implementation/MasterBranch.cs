using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterBranch : IMasterBranchInterface<MasterBranchResult>
    {
        readonly AdminContext _Context;
        private readonly ILogger _logger;

        public MasterBranch(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterBranchResult> GetAllADMasterBranch()
        {
            try
            {
                var _data = (from MB in _Context.ADMasterBranches
                             join MD in _Context.ADMasterDesignations on MB.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join AT in _Context.ADMasterAddressTypes on MB.MasterAddressTypeId equals AT.MasterAddressTypeId into ATGroup
                             from AT in ATGroup.DefaultIfEmpty()
                             join CC in _Context.ADMasterCountries on MB.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on MB.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId
                             select new
                             {
                                 MB.MasterBranchId,
                                 MB.BranchCode,
                                 MB.BranchTitle,
                                 MB.ContactPerson,
                                 MB.MasterDesignationId,
                                 MB.DateofRegistration,
                                 MB.MasterAddressTypeId,
                                 MB.Address1,
                                 MB.Address2,
                                 MB.MasterCountryId,
                                 MB.MasterStateId,
                                 MB.City,
                                 MB.PinCode,
                                 MB.PhoneNumber,
                                 MB.MobileNumber,
                                 MB.Fax,
                                 MB.Email,
                                 MB.URL,
                                 MB.MasterCompanyId,
                                 MB.IsActive,
                                 MD.DesignationTitle,
                                 AT.AddressTypeTitle,
                                 CC.CountryTitle,
                                 MS.StateTitle,
                                 MC.CompanyTitle
                             });

                List<MasterBranchResult> objMasterBranchResultList = new List<MasterBranchResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterBranchResult = new MasterBranchResult();

                    _MasterBranchResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterBranchResult.BranchCode = _Item.BranchCode;
                    _MasterBranchResult.BranchTitle = _Item.BranchTitle;
                    _MasterBranchResult.ContactPerson = _Item.ContactPerson;
                    _MasterBranchResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterBranchResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterBranchResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterBranchResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterBranchResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterBranchResult.Address1 = _Item.Address1;
                    _MasterBranchResult.Address2 = _Item.Address2;
                    _MasterBranchResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterBranchResult.CountryTitle = _Item.CountryTitle;
                    _MasterBranchResult.MasterStateId = _Item.MasterStateId;
                    _MasterBranchResult.StateTitle = _Item.StateTitle;
                    _MasterBranchResult.City = _Item.City;
                    _MasterBranchResult.PinCode = _Item.PinCode;
                    _MasterBranchResult.MobileNumber = _Item.MobileNumber;
                    _MasterBranchResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterBranchResult.Fax = _Item.Fax;
                    _MasterBranchResult.Email = _Item.Email;
                    _MasterBranchResult.URL = _Item.URL;
                    _MasterBranchResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterBranchResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterBranchResult.IsActive = _Item.IsActive;
                    _MasterBranchResult.ActiveColor = "green";
                    _MasterBranchResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterBranchResult.IsActive == false)
                    {
                        _MasterBranchResult.ActiveColor = "red";
                        _MasterBranchResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterBranchResultList.Add(_MasterBranchResult);
                }

                return objMasterBranchResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterBranchResult GetADMasterBranchByID(long MasterBranchId)
        {
            try
            {
                var _data = (from MB in _Context.ADMasterBranches
                             join MD in _Context.ADMasterDesignations on MB.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join AT in _Context.ADMasterAddressTypes on MB.MasterAddressTypeId equals AT.MasterAddressTypeId into ATGroup
                             from AT in ATGroup.DefaultIfEmpty()
                             join CC in _Context.ADMasterCountries on MB.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on MB.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId
                             where MB.MasterBranchId == MasterBranchId
                             select new
                             {
                                 MB.MasterBranchId,
                                 MB.BranchCode,
                                 MB.BranchTitle,
                                 MB.ContactPerson,
                                 MB.MasterDesignationId,
                                 MB.DateofRegistration,
                                 MB.MasterAddressTypeId,
                                 MB.Address1,
                                 MB.Address2,
                                 MB.MasterCountryId,
                                 MB.MasterStateId,
                                 MB.City,
                                 MB.PinCode,
                                 MB.PhoneNumber,
                                 MB.MobileNumber,
                                 MB.Fax,
                                 MB.Email,
                                 MB.URL,
                                 MB.MasterCompanyId,
                                 MB.IsActive,
                                 MD.DesignationTitle,
                                 AT.AddressTypeTitle,
                                 CC.CountryTitle,
                                 MS.StateTitle,
                                 MC.CompanyTitle
                             });


                var _Item = _data.Where(a => a.MasterBranchId == MasterBranchId).FirstOrDefault(); 

                MasterBranchResult _MasterBranchResult = new MasterBranchResult();
                if (_data != null)
                {
                    _MasterBranchResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterBranchResult.BranchCode = _Item.BranchCode;
                    _MasterBranchResult.BranchTitle = _Item.BranchTitle;
                    _MasterBranchResult.ContactPerson = _Item.ContactPerson;
                    _MasterBranchResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterBranchResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterBranchResult.DateofRegistration = _Item.DateofRegistration;
                    _MasterBranchResult.MasterAddressTypeId = _Item.MasterAddressTypeId;
                    _MasterBranchResult.AddressTypeTitle = _Item.AddressTypeTitle;
                    _MasterBranchResult.Address1 = _Item.Address1;
                    _MasterBranchResult.Address2 = _Item.Address2;
                    _MasterBranchResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterBranchResult.CountryTitle = _Item.CountryTitle;
                    _MasterBranchResult.MasterStateId = _Item.MasterStateId;
                    _MasterBranchResult.StateTitle = _Item.StateTitle;
                    _MasterBranchResult.City = _Item.City;
                    _MasterBranchResult.PinCode = _Item.PinCode;
                    _MasterBranchResult.MobileNumber = _Item.MobileNumber;
                    _MasterBranchResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterBranchResult.Fax = _Item.Fax;
                    _MasterBranchResult.Email = _Item.Email;
                    _MasterBranchResult.URL = _Item.URL;
                    _MasterBranchResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterBranchResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterBranchResult.IsActive = _Item.IsActive;
                    _MasterBranchResult.ActiveColor = "green";
                    _MasterBranchResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterBranchResult.IsActive == false)
                    {
                        _MasterBranchResult.ActiveColor = "red";
                        _MasterBranchResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                
                return _MasterBranchResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterBranch(ADMasterBranch objADMasterBranch)
        {
            try
            {
                _Context.ADMasterBranches.Add(objADMasterBranch);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterBranch(ADMasterBranch objADMasterBranch)
        {
            try
            {
                _Context.Entry(objADMasterBranch).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterBranch(long MasterBranchId)
        {
            try
            {
                var objADMasterBranch = _Context.ADMasterBranches.Find(MasterBranchId);
                _Context.ADMasterBranches.Remove(objADMasterBranch);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterBranchExists(long MasterBranchId)
        {
            try
            {
                return _Context.ADMasterBranches.Any(e => e.MasterBranchId == MasterBranchId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
