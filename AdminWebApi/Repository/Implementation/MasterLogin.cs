using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterLogin : IMasterLoginInterface<MasterLoginResult>
    {
        readonly AdminContext _Context;

        public MasterLogin(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterLoginResult> GetAllADMasterLogin()
        {
            try
            {
                var _data = (from ML in _Context.ADMasterLogins
                             join MR in _Context.ADMasterRegistrationType on ML.MasterRegistrationTypeId equals MR.MasterRegistrationTypeId
                             join ME in _Context.ADMasterEmployees on ML.MasterRegistrationId equals ME.MasterEmployeeId
                             join SA in _Context.ADMasterSalutations on ME.MasterSalutationId equals SA.MasterSalutationId into SAGroup
                             from SA in SAGroup.DefaultIfEmpty()
                             join MD in _Context.ADMasterDesignations on ME.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join DA in _Context.ADMasterDepartments on ME.MasterDepartmentId equals DA.MasterDepartmentId into DAGroup
                             from DA in DAGroup.DefaultIfEmpty()
                             join MB in _Context.ADMasterBranches on ME.MasterBranchId equals MB.MasterBranchId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId into MCGroup
                             from MC in MCGroup.DefaultIfEmpty()
                             join MP in _Context.ADMasterProfiles on ML.MasterProfileId equals MP.MasterProfileId
                             select new { 
                                ML.MasterLoginId, ML.MasterRegistrationTypeId, ML.MasterRegistrationId, ML.UserName, ML.Password,ML.MasterProfileId,
                                ML.VerificationCode,ML.IsVerified, ML.IsFirstLogin,ML.IsActive,
                                MR.MasterRegistrationTypeTitle,
                                ME.EmployeeName,
                                MP.ProfileTitle,
                                SA.SalutationTitle,
                                MD.DesignationTitle,
                                DA.DepartmentTitle,
                                MB.BranchTitle,
                                MC.CompanyTitle
                             });

                List<MasterLoginResult> objMasterLoginResultList = new List<MasterLoginResult>();
                foreach (var _Item in _data)
                {
                    MasterLoginResult _objMasterLoginResult = new MasterLoginResult();

                    _objMasterLoginResult.MasterLoginId = _Item.MasterLoginId;
                    _objMasterLoginResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterLoginResult.RegistrationTypeTitle = _Item.MasterRegistrationTypeTitle;
                    _objMasterLoginResult.MasterRegistrationId = _Item.MasterRegistrationId;
                    _objMasterLoginResult.RegistrationTitle = _Item.EmployeeName;
                    _objMasterLoginResult.SalutationTitle = _Item.SalutationTitle;
                    _objMasterLoginResult.DesignationTitle = _Item.DesignationTitle;
                    _objMasterLoginResult.DepartmentTitle = _Item.DepartmentTitle;
                    _objMasterLoginResult.BranchTitle = _Item.BranchTitle;
                    _objMasterLoginResult.CompanyTitle = _Item.CompanyTitle;

                    _objMasterLoginResult.UserName = _Item.UserName;
                    _objMasterLoginResult.Password = _Item.Password;
                    _objMasterLoginResult.MasterProfileId = _Item.MasterProfileId;
                    _objMasterLoginResult.ProfileTitle = _Item.ProfileTitle;
                    _objMasterLoginResult.VerificationCode = _Item.VerificationCode;
                    _objMasterLoginResult.IsVerified = _Item.IsVerified;
                    _objMasterLoginResult.IsFirstLogin = _Item.IsFirstLogin;
                    _objMasterLoginResult.IsActive = _Item.IsActive;                    

                    objMasterLoginResultList.Add(_objMasterLoginResult);
                }

                return objMasterLoginResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterLoginResult GetADMasterLoginByID(long MasterLoginId)
        {
            try
            {
                var _data = (from ML in _Context.ADMasterLogins
                             join MR in _Context.ADMasterRegistrationType on ML.MasterRegistrationTypeId equals MR.MasterRegistrationTypeId
                             join ME in _Context.ADMasterEmployees on ML.MasterRegistrationId equals ME.MasterEmployeeId
                             join SA in _Context.ADMasterSalutations on ME.MasterSalutationId equals SA.MasterSalutationId into SAGroup
                             from SA in SAGroup.DefaultIfEmpty()
                             join MD in _Context.ADMasterDesignations on ME.MasterDesignationId equals MD.MasterDesignationId into MDGroup
                             from MD in MDGroup.DefaultIfEmpty()
                             join DA in _Context.ADMasterDepartments on ME.MasterDepartmentId equals DA.MasterDepartmentId into DAGroup
                             from DA in DAGroup.DefaultIfEmpty()
                             join MB in _Context.ADMasterBranches on ME.MasterBranchId equals MB.MasterBranchId into MBGroup
                             from MB in MBGroup.DefaultIfEmpty()
                             join MC in _Context.ADMasterCompanies on MB.MasterCompanyId equals MC.MasterCompanyId into MCGroup
                             from MC in MCGroup.DefaultIfEmpty()
                             join MP in _Context.ADMasterProfiles on ML.MasterProfileId equals MP.MasterProfileId
                             where ML.MasterLoginId == MasterLoginId
                             select new
                             {
                                 ML.MasterLoginId,
                                 ML.MasterRegistrationTypeId,
                                 ML.MasterRegistrationId,
                                 ML.UserName,
                                 ML.Password,
                                 ML.MasterProfileId,
                                 ML.VerificationCode,
                                 ML.IsVerified,
                                 ML.IsFirstLogin,
                                 ML.IsActive,
                                 MR.MasterRegistrationTypeTitle,
                                 ME.EmployeeName,
                                 MP.ProfileTitle,
                                 SA.SalutationTitle,
                                 MD.DesignationTitle,
                                 DA.DepartmentTitle,
                                 MB.BranchTitle,
                                 MC.CompanyTitle
                             });

                var _Item = _data.FirstOrDefault();

                MasterLoginResult _objMasterLoginResult = new MasterLoginResult();

                if (_Item != null)
                {
                    _objMasterLoginResult.MasterLoginId = _Item.MasterLoginId;
                    _objMasterLoginResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                    _objMasterLoginResult.RegistrationTypeTitle = _Item.MasterRegistrationTypeTitle;
                    _objMasterLoginResult.MasterRegistrationId = _Item.MasterRegistrationId;
                    _objMasterLoginResult.RegistrationTitle = _Item.EmployeeName;
                    _objMasterLoginResult.SalutationTitle = _Item.SalutationTitle;
                    _objMasterLoginResult.DesignationTitle = _Item.DesignationTitle;
                    _objMasterLoginResult.DepartmentTitle = _Item.DepartmentTitle;
                    _objMasterLoginResult.BranchTitle = _Item.BranchTitle;
                    _objMasterLoginResult.CompanyTitle = _Item.CompanyTitle;

                    _objMasterLoginResult.UserName = _Item.UserName;
                    _objMasterLoginResult.Password = _Item.Password;
                    _objMasterLoginResult.MasterProfileId = _Item.MasterProfileId;
                    _objMasterLoginResult.ProfileTitle = _Item.ProfileTitle;
                    _objMasterLoginResult.VerificationCode = _Item.VerificationCode;
                    _objMasterLoginResult.IsVerified = _Item.IsVerified;
                    _objMasterLoginResult.IsFirstLogin = _Item.IsFirstLogin;
                    _objMasterLoginResult.IsActive = _Item.IsActive;
                }

                return _objMasterLoginResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterLogin(ADMasterLogin objADMasterLogin)
        {            
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    //_Context.ADMasterEmployees.Add(objADMasterEmployee);
                    //await _Context.SaveChangesAsync();                    

                    //_Context.ADMasterLogins.Add(objADMasterLogin);
                    //await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task UpdateADMasterLogin(ADMasterLogin objADMasterLogin)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.Entry(objADMasterLogin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    var ADMasterEmployee = _Context.ADMasterEmployees.Where(a => a.MasterEmployeeId == objADMasterLogin.MasterRegistrationId).FirstOrDefault();
                    ADMasterEmployee.Email = objADMasterLogin.UserName;

                    _Context.Entry(ADMasterEmployee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public async Task DeleteADMasterLogin(long MasterLoginId)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    var _MasterEmployeeId = _Context.ADMasterLogins.Where(a => a.MasterLoginId == MasterLoginId).Select(a => a.MasterRegistrationId).FirstOrDefault();
                    
                    var objADMasterEmployee = _Context.ADMasterEmployees.Where(a=>a.MasterEmployeeId== _MasterEmployeeId).FirstOrDefault();
                    _Context.ADMasterEmployees.Remove(objADMasterEmployee);
                    await _Context.SaveChangesAsync();

                    var ADMasterLogin = _Context.ADMasterLogins.Find(MasterLoginId);
                    _Context.ADMasterLogins.Remove(ADMasterLogin);
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

        public bool ADMasterLoginExists(long MasterLoginId)
        {
            try
            {
                return _Context.ADMasterLogins.Any(e => e.MasterLoginId == MasterLoginId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
