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
    public class MasterEmployee : IMasterEmployeeInterface<MasterEmployeeResult>
    {
        readonly AdminContext _Context;

        public MasterEmployee(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterEmployeeResult> GetAllADMasterEmployee()
        {
            try
            {
                var _data = (from ME in _Context.ADMasterEmployees
                             join SA in _Context.ADMasterSalutations on ME.MasterSalutationId equals SA.MasterSalutationId into SAGroup
                             from SA in SAGroup.DefaultIfEmpty()
                             join MG in _Context.ADMasterGenders on ME.Gender equals MG.MasterGenderId into MGGroup
                             from MG in MGGroup.DefaultIfEmpty()
                             join MD in _Context.ADMasterDesignations on ME.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join DA in _Context.ADMasterDepartments on ME.MasterDepartmentId equals DA.MasterDepartmentId into DAGroup
                             from DA in DAGroup.DefaultIfEmpty()
                             join ME2 in _Context.ADMasterEmployees on ME.ReportingHeadId equals ME2.MasterEmployeeId into ME2Group
                             from ME2 in ME2Group.DefaultIfEmpty()
                             join MD2 in _Context.ADMasterDesignations on ME2.MasterDesignationId equals MD2.MasterDesignationId into MD2Group
                             from MD2 in MD2Group.DefaultIfEmpty()
                             join ET in _Context.ADMasterEmployeeTypes on ME.MasterEmployeeTypeId equals ET.MasterEmployeeTypeId into ETGroup
                             from ET in ETGroup.DefaultIfEmpty()
                             join TZ in _Context.ADMasterTimeZones on ME.MasterTimeZoneId equals TZ.MasterTimeZoneId into TZGroup
                             from TZ in TZGroup.DefaultIfEmpty()                             
                             join ES in _Context.ADMasterEmployeeStatus on ME.MasterEmployeeStatusId equals ES.MasterEmployeeStatusId into ESGroup
                             from ES in ESGroup.DefaultIfEmpty()                             
                             join CC in _Context.ADMasterCountries on ME.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on ME.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             join PT in _Context.ADMasterPaymentTypes on ME.MasterPaymentTypeId equals PT.MasterPaymentTypeId into PTGroup
                             from PT in PTGroup.DefaultIfEmpty()
                             join BA in _Context.ADMasterBankAccountTypes on ME.MasterBankAccountTypeId equals BA.MasterBankAccountTypeId into BAGroup
                             from BA in BAGroup.DefaultIfEmpty()
                             join MB in _Context.ADMasterBranches on ME.MasterBranchId equals MB.MasterBranchId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId into MCGroup
                             from MC in MCGroup.DefaultIfEmpty()
                             select new
                             {
                                 ME.MasterEmployeeId,
                                 ME.EmployeeCode,
                                 ME.MasterSalutationId,
                                 SA.SalutationTitle,
                                 ME.EmployeeName,
                                 ME.DateOfBirth,
                                 ME.DateOfJoining,
                                 ME.Gender,
                                 MG.GenderTitle,
                                 ME.PANNo,
                                 ME.AadhaarNo,
                                 ME.MasterDesignationId,
                                 MD.DesignationTitle,
                                 ME.MasterDepartmentId,
                                 DA.DepartmentTitle,
                                 ME.ReportingHeadId,
                                 ReportingHeadTitle = ME2.EmployeeName,
                                 ReportingHeadDesignationTitle = MD2.DesignationTitle,
                                 ME.MasterEmployeeTypeId,
                                 ET.EmployeeTypeTitle,
                                 ME.MasterTimeZoneId,
                                 TZ.TimeZoneTitle,
                                 ME.MasterEmployeeStatusId,
                                 ES.EmployeeStatusTitle,
                                 ME.DateOfLeavingOrganisation,
                                 ME.Address1,
                                 ME.Address2,
                                 ME.MasterCountryId,
                                 CC.CountryTitle,                                 
                                 ME.MasterStateId,
                                 MS.StateTitle,
                                 ME.City,
                                 ME.PinCode,
                                 ME.PhoneNumber,
                                 ME.MobileNumber,
                                 ME.Email,
                                 ME.MasterPaymentTypeId,
                                 PT.MasterPaymentTitle,
                                 ME.PaypalID,
                                 ME.PaypalLink,
                                 ME.IsPaypalAccountVerified,
                                 ME.MasterBankAccountTypeId,
                                 BA.MasterBankAccountTypeTitle,
                                 ME.BankName,
                                 ME.BankAccountNumber,
                                 ME.IFCSCode,
                                 ME.ShiftCode_RoutingNo_IBAN,
                                 ME.BankBranch,
                                 ME.BankCity,
                                 ME.BankAddress,
                                 ME.UploadBankDetail,
                                 ME.IsBankAccountVerified,
                                 ME.MasterBranchId,
                                 MB.BranchTitle,
                                 MB.MasterCompanyId,
                                 MC.CompanyTitle,
                                 MC.CompanyLogo,
                                 ME.IsActive
                                 
                             });

                List<MasterEmployeeResult> objMasterEmployeeResultList = new List<MasterEmployeeResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _MasterEmployeeResult = new MasterEmployeeResult();

                    _MasterEmployeeResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterEmployeeResult.EmployeeCode = _Item.EmployeeCode;
                    _MasterEmployeeResult.MasterSalutationId = _Item.MasterSalutationId;
                    _MasterEmployeeResult.SalutationTitle = _Item.SalutationTitle;
                    _MasterEmployeeResult.EmployeeName = _Item.EmployeeName;
                    _MasterEmployeeResult.DateOfJoining = _Item.DateOfJoining;
                    _MasterEmployeeResult.DateOfBirth = _Item.DateOfBirth;
                    _MasterEmployeeResult.Gender = _Item.Gender;
                    _MasterEmployeeResult.GenderTitle = _Item.GenderTitle;
                    _MasterEmployeeResult.PANNo = _Item.PANNo;
                    _MasterEmployeeResult.AadhaarNo = _Item.AadhaarNo;
                    _MasterEmployeeResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterEmployeeResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterEmployeeResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterEmployeeResult.DepartmentTitle = _Item.DepartmentTitle;
                    _MasterEmployeeResult.ReportingHeadId = _Item.ReportingHeadId;
                    _MasterEmployeeResult.ReportingHeadTitle = _Item.ReportingHeadTitle;
                    _MasterEmployeeResult.ReportingHeadDesignationTitle = _Item.ReportingHeadDesignationTitle;
                    _MasterEmployeeResult.MasterEmployeeTypeId = _Item.MasterEmployeeTypeId;
                    _MasterEmployeeResult.EmployeeTypeTitle = _Item.EmployeeTypeTitle;
                    _MasterEmployeeResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterEmployeeResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterEmployeeResult.MasterEmployeeStatusId = _Item.MasterEmployeeStatusId;
                    _MasterEmployeeResult.EmployeeStatusTitle = _Item.EmployeeStatusTitle;
                    _MasterEmployeeResult.DateOfLeavingOrganisation = _Item.DateOfLeavingOrganisation;
                    _MasterEmployeeResult.Address1 = _Item.Address1;
                    _MasterEmployeeResult.Address2 = _Item.Address2;
                    _MasterEmployeeResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterEmployeeResult.CountryTitle = _Item.CountryTitle;
                    _MasterEmployeeResult.MasterStateId = _Item.MasterStateId;
                    _MasterEmployeeResult.StateTitle = _Item.StateTitle;
                    _MasterEmployeeResult.City = _Item.City;
                    _MasterEmployeeResult.PinCode = _Item.PinCode;
                    _MasterEmployeeResult.MobileNumber = _Item.MobileNumber;
                    _MasterEmployeeResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterEmployeeResult.Email = _Item.Email;
                    _MasterEmployeeResult.MasterPaymentTypeId = _Item.MasterPaymentTypeId;
                    _MasterEmployeeResult.PaymentTypeTitle = _Item.MasterPaymentTitle;
                    _MasterEmployeeResult.PaypalID = _Item.PaypalID;
                    _MasterEmployeeResult.PaypalLink = _Item.PaypalLink;
                    _MasterEmployeeResult.IsPaypalAccountVerified = _Item.IsPaypalAccountVerified;
                    _MasterEmployeeResult.MasterBankAccountTypeId = _Item.MasterBankAccountTypeId;
                    _MasterEmployeeResult.BankName = _Item.BankName;
                    _MasterEmployeeResult.BankAccountNumber = _Item.BankAccountNumber;
                    _MasterEmployeeResult.IFCSCode = _Item.IFCSCode;
                    _MasterEmployeeResult.ShiftCode_RoutingNo_IBAN = _Item.ShiftCode_RoutingNo_IBAN;
                    _MasterEmployeeResult.BankBranch = _Item.BankBranch;
                    _MasterEmployeeResult.BankCity = _Item.BankCity;
                    _MasterEmployeeResult.BankAddress = _Item.BankAddress;
                    _MasterEmployeeResult.UploadBankDetail = _Item.UploadBankDetail;
                    _MasterEmployeeResult.IsBankAccountVerified = _Item.IsBankAccountVerified;
                    _MasterEmployeeResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterEmployeeResult.BranchTitle = _Item.BranchTitle;
                    _MasterEmployeeResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterEmployeeResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterEmployeeResult.CompanyLogo = _Item.CompanyLogo;

                    _MasterEmployeeResult.IsActive = _Item.IsActive;
                    _MasterEmployeeResult.ActiveColor = "green";
                    _MasterEmployeeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterEmployeeResult.IsActive == false)
                    {
                        _MasterEmployeeResult.ActiveColor = "red";
                        _MasterEmployeeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterEmployeeResultList.Add(_MasterEmployeeResult);
                }

                return objMasterEmployeeResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MasterEmployeeResult GetADMasterEmployeeByID(long MasterEmployeeId)
        {
            try
            {
                var _data = (from ME in _Context.ADMasterEmployees
                             join SA in _Context.ADMasterSalutations on ME.MasterSalutationId equals SA.MasterSalutationId into SAGroup
                             from SA in SAGroup.DefaultIfEmpty()
                             join MG in _Context.ADMasterGenders on ME.Gender equals MG.MasterGenderId into MGGroup
                             from MG in MGGroup.DefaultIfEmpty()
                             join MD in _Context.ADMasterDesignations on ME.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join DA in _Context.ADMasterDepartments on ME.MasterDepartmentId equals DA.MasterDepartmentId into DAGroup
                             from DA in DAGroup.DefaultIfEmpty()
                             join ME2 in _Context.ADMasterEmployees on ME.ReportingHeadId equals ME2.MasterEmployeeId into ME2Group
                             from ME2 in ME2Group.DefaultIfEmpty()
                             join MD2 in _Context.ADMasterDesignations on ME2.MasterDesignationId equals MD2.MasterDesignationId into MD2Group
                             from MD2 in MD2Group.DefaultIfEmpty()
                             join ET in _Context.ADMasterEmployeeTypes on ME.MasterEmployeeTypeId equals ET.MasterEmployeeTypeId into ETGroup
                             from ET in ETGroup.DefaultIfEmpty()
                             join TZ in _Context.ADMasterTimeZones on ME.MasterTimeZoneId equals TZ.MasterTimeZoneId into TZGroup
                             from TZ in TZGroup.DefaultIfEmpty()
                             join ES in _Context.ADMasterEmployeeStatus on ME.MasterEmployeeStatusId equals ES.MasterEmployeeStatusId into ESGroup
                             from ES in ESGroup.DefaultIfEmpty()
                             join CC in _Context.ADMasterCountries on ME.MasterCountryId equals CC.MasterCountryId into CCGroup
                             from CC in CCGroup.DefaultIfEmpty()
                             join MS in _Context.ADMasterStates on ME.MasterStateId equals MS.MasterStateId into MSGroup
                             from MS in MSGroup.DefaultIfEmpty()
                             join PT in _Context.ADMasterPaymentTypes on ME.MasterPaymentTypeId equals PT.MasterPaymentTypeId into PTGroup
                             from PT in PTGroup.DefaultIfEmpty()
                             join BA in _Context.ADMasterBankAccountTypes on ME.MasterBankAccountTypeId equals BA.MasterBankAccountTypeId into BAGroup
                             from BA in BAGroup.DefaultIfEmpty()
                             join MB in _Context.ADMasterBranches on ME.MasterBranchId equals MB.MasterBranchId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId into MCGroup
                             from MC in MCGroup.DefaultIfEmpty()
                             where ME.MasterEmployeeId == MasterEmployeeId
                             select new
                             {
                                 ME.MasterEmployeeId,
                                 ME.EmployeeCode,
                                 ME.MasterSalutationId,
                                 SA.SalutationTitle,
                                 ME.EmployeeName,
                                 ME.DateOfBirth,
                                 ME.DateOfJoining,
                                 ME.Gender,
                                 MG.GenderTitle,
                                 ME.PANNo,
                                 ME.AadhaarNo,
                                 ME.MasterDesignationId,
                                 MD.DesignationTitle,
                                 ME.MasterDepartmentId,
                                 DA.DepartmentTitle,
                                 ME.ReportingHeadId,
                                 ReportingHeadTitle = ME2.EmployeeName,
                                 ReportingHeadDesignationTitle = MD2.DesignationTitle,
                                 ME.MasterEmployeeTypeId,
                                 ET.EmployeeTypeTitle,
                                 ME.MasterTimeZoneId,
                                 TZ.TimeZoneTitle,
                                 ME.MasterEmployeeStatusId,
                                 ES.EmployeeStatusTitle,
                                 ME.DateOfLeavingOrganisation,
                                 ME.Address1,
                                 ME.Address2,
                                 ME.MasterCountryId,
                                 CC.CountryTitle,
                                 ME.MasterStateId,
                                 MS.StateTitle,
                                 ME.City,
                                 ME.PinCode,
                                 ME.PhoneNumber,
                                 ME.MobileNumber,
                                 ME.Email,
                                 ME.MasterPaymentTypeId,
                                 PT.MasterPaymentTitle,
                                 ME.PaypalID,
                                 ME.PaypalLink,
                                 ME.IsPaypalAccountVerified,
                                 ME.MasterBankAccountTypeId,
                                 BA.MasterBankAccountTypeTitle,
                                 ME.BankName,
                                 ME.BankAccountNumber,
                                 ME.IFCSCode,
                                 ME.ShiftCode_RoutingNo_IBAN,
                                 ME.BankBranch,
                                 ME.BankCity,
                                 ME.BankAddress,
                                 ME.UploadBankDetail,
                                 ME.IsBankAccountVerified,
                                 ME.MasterBranchId,
                                 MB.BranchTitle,
                                 MB.MasterCompanyId,
                                 MC.CompanyTitle,
                                 MC.CompanyLogo,
                                 ME.IsActive
                             });


                var _Item = _data.Where(a=>a.MasterEmployeeId == MasterEmployeeId).FirstOrDefault(); ;

                MasterEmployeeResult _MasterEmployeeResult = new MasterEmployeeResult();
                if (_data != null)
                {
                    _MasterEmployeeResult.MasterEmployeeId = _Item.MasterEmployeeId;
                    _MasterEmployeeResult.EmployeeCode = _Item.EmployeeCode;
                    _MasterEmployeeResult.MasterSalutationId = _Item.MasterSalutationId;
                    _MasterEmployeeResult.SalutationTitle = _Item.SalutationTitle;
                    _MasterEmployeeResult.EmployeeName = _Item.EmployeeName;
                    _MasterEmployeeResult.DateOfJoining = _Item.DateOfJoining;
                    _MasterEmployeeResult.DateOfBirth = _Item.DateOfBirth;
                    _MasterEmployeeResult.Gender = _Item.Gender;
                    _MasterEmployeeResult.GenderTitle = _Item.GenderTitle;
                    _MasterEmployeeResult.PANNo = _Item.PANNo;
                    _MasterEmployeeResult.AadhaarNo = _Item.AadhaarNo;
                    _MasterEmployeeResult.MasterDesignationId = _Item.MasterDesignationId;
                    _MasterEmployeeResult.DesignationTitle = _Item.DesignationTitle;
                    _MasterEmployeeResult.MasterDepartmentId = _Item.MasterDepartmentId;
                    _MasterEmployeeResult.DepartmentTitle = _Item.DepartmentTitle;
                    _MasterEmployeeResult.ReportingHeadId = _Item.ReportingHeadId;
                    _MasterEmployeeResult.ReportingHeadTitle = _Item.ReportingHeadTitle;
                    _MasterEmployeeResult.ReportingHeadDesignationTitle = _Item.ReportingHeadDesignationTitle;
                    _MasterEmployeeResult.MasterEmployeeTypeId = _Item.MasterEmployeeTypeId;
                    _MasterEmployeeResult.EmployeeTypeTitle = _Item.EmployeeTypeTitle;
                    _MasterEmployeeResult.MasterTimeZoneId = _Item.MasterTimeZoneId;
                    _MasterEmployeeResult.TimeZoneTitle = _Item.TimeZoneTitle;
                    _MasterEmployeeResult.MasterEmployeeStatusId = _Item.MasterEmployeeStatusId;
                    _MasterEmployeeResult.EmployeeStatusTitle = _Item.EmployeeStatusTitle;
                    _MasterEmployeeResult.DateOfLeavingOrganisation = _Item.DateOfLeavingOrganisation;
                    _MasterEmployeeResult.Address1 = _Item.Address1;
                    _MasterEmployeeResult.Address2 = _Item.Address2;
                    _MasterEmployeeResult.MasterCountryId = _Item.MasterCountryId;
                    _MasterEmployeeResult.CountryTitle = _Item.CountryTitle;
                    _MasterEmployeeResult.MasterStateId = _Item.MasterStateId;
                    _MasterEmployeeResult.StateTitle = _Item.StateTitle;
                    _MasterEmployeeResult.City = _Item.City;
                    _MasterEmployeeResult.PinCode = _Item.PinCode;
                    _MasterEmployeeResult.MobileNumber = _Item.MobileNumber;
                    _MasterEmployeeResult.PhoneNumber = _Item.PhoneNumber;
                    _MasterEmployeeResult.Email = _Item.Email;
                    _MasterEmployeeResult.MasterPaymentTypeId = _Item.MasterPaymentTypeId;
                    _MasterEmployeeResult.PaymentTypeTitle = _Item.MasterPaymentTitle;
                    _MasterEmployeeResult.PaypalID = _Item.PaypalID;
                    _MasterEmployeeResult.PaypalLink = _Item.PaypalLink;
                    _MasterEmployeeResult.IsPaypalAccountVerified = _Item.IsPaypalAccountVerified;
                    _MasterEmployeeResult.MasterBankAccountTypeId = _Item.MasterBankAccountTypeId;
                    _MasterEmployeeResult.BankName = _Item.BankName;
                    _MasterEmployeeResult.BankAccountNumber = _Item.BankAccountNumber;
                    _MasterEmployeeResult.IFCSCode = _Item.IFCSCode;
                    _MasterEmployeeResult.ShiftCode_RoutingNo_IBAN = _Item.ShiftCode_RoutingNo_IBAN;
                    _MasterEmployeeResult.BankBranch = _Item.BankBranch;
                    _MasterEmployeeResult.BankCity = _Item.BankCity;
                    _MasterEmployeeResult.BankAddress = _Item.BankAddress;
                    _MasterEmployeeResult.UploadBankDetail = _Item.UploadBankDetail;
                    _MasterEmployeeResult.IsBankAccountVerified = _Item.IsBankAccountVerified;
                    _MasterEmployeeResult.MasterBranchId = _Item.MasterBranchId;
                    _MasterEmployeeResult.BranchTitle = _Item.BranchTitle;
                    _MasterEmployeeResult.MasterCompanyId = _Item.MasterCompanyId;
                    _MasterEmployeeResult.CompanyTitle = _Item.CompanyTitle;
                    _MasterEmployeeResult.CompanyLogo = _Item.CompanyLogo;

                    _MasterEmployeeResult.IsActive = _Item.IsActive;
                    _MasterEmployeeResult.ActiveColor = "green";
                    _MasterEmployeeResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterEmployeeResult.IsActive == false)
                    {
                        _MasterEmployeeResult.ActiveColor = "red";
                        _MasterEmployeeResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }
                
                return _MasterEmployeeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterEmployee(ADMasterEmployee objADMasterEmployee)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.ADMasterEmployees.Add(objADMasterEmployee);
                    await _Context.SaveChangesAsync();

                    ADMasterLogin objADMasterLogin = new ADMasterLogin();
                    objADMasterLogin.MasterLoginId = 0;
                    objADMasterLogin.MasterRegistrationTypeId = 3;
                    objADMasterLogin.MasterRegistrationId = objADMasterEmployee.MasterEmployeeId;
                    objADMasterLogin.MasterProfileId = 3;
                    objADMasterLogin.UserName = objADMasterEmployee.Email;
                    objADMasterLogin.Password = "BQxfjG3oUEsTpNTUjOnKlA==";
                    objADMasterLogin.VerificationCode = "";
                    objADMasterLogin.IsFirstLogin = true;
                    objADMasterLogin.IsVerified = false;
                    objADMasterLogin.IsActive = false;
                    
                    objADMasterLogin.EnterById = objADMasterEmployee.EnterById;
                    objADMasterLogin.EnterDate = objADMasterEmployee.EnterDate;
                    objADMasterLogin.ModifiedById = objADMasterEmployee.ModifiedById;
                    objADMasterLogin.ModifiedDate = objADMasterEmployee.ModifiedDate;

                    _Context.ADMasterLogins.Add(objADMasterLogin);
                    await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task UpdateADMasterEmployee(ADMasterEmployee objADMasterEmployee)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.Entry(objADMasterEmployee).State = EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    var ADMasterLogin = _Context.ADMasterLogins.Where(a => a.MasterRegistrationId == objADMasterEmployee.MasterEmployeeId).FirstOrDefault();
                    ADMasterLogin.UserName = objADMasterEmployee.Email;

                    _Context.Entry(ADMasterLogin).State = EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

        }

        public async Task DeleteADMasterEmployee(long MasterEmployeeId)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    var ADMasterLogin = _Context.ADMasterLogins.Where(a => a.MasterRegistrationId == MasterEmployeeId).FirstOrDefault();
                    _Context.ADMasterLogins.Remove(ADMasterLogin);
                    await _Context.SaveChangesAsync();

                    var objADMasterEmployee = _Context.ADMasterEmployees.Find(MasterEmployeeId);
                    _Context.ADMasterEmployees.Remove(objADMasterEmployee);
                    await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new Exception(ex.Message);
                }
            }                
        }

        public bool ADMasterEmployeeExists(long MasterEmployeeId)
        {
            try
            {
                return _Context.ADMasterEmployees.Any(e => e.MasterEmployeeId == MasterEmployeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
