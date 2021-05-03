using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class ValidateAccount: IValidateAccountInterface<SPValidateAccountResult>
    {
        readonly AdminContext _Context;
        public ValidateAccount(AdminContext context)
        {
            _Context = context;
        }


        public IEnumerable<SPValidateAccountResult> GetValidateAccount(string UserName, string Password, string VerificationCode, string MacId, string SessionId, string Mode, int ModeVersion, string SearchCondition)
        {
            var _data = _Context.SP_ADValidateAccount(UserName, Password, VerificationCode, MacId, SessionId, Mode, ModeVersion, SearchCondition).ToList();

            List<SPValidateAccountResult> _SPValidateAccountResultList = new List<SPValidateAccountResult>();
            foreach (var _Item in _data)
            {
                SPValidateAccountResult _SPValidateAccountResult = new SPValidateAccountResult();

                _SPValidateAccountResult.MasterLoginId = _Item.MasterLoginId;
                _SPValidateAccountResult.MasterRegistrationTypeTitle = _Item.MasterRegistrationTypeTitle;
                //_SPValidateAccountResult.MasterRegistrationTypeId = _Item.MasterRegistrationTypeId;
                _SPValidateAccountResult.MasterRegistrationId = _Item.MasterRegistrationId;
                _SPValidateAccountResult.UserName = _Item.UserName;
                _SPValidateAccountResult.IsVerified = _Item.IsVerified;
                _SPValidateAccountResult.IsFirstLogin = _Item.IsFirstLogin;
                _SPValidateAccountResult.ValidationCount = _Item.ValidationCount;
                _SPValidateAccountResult.MasterEmployeeId = _Item.MasterEmployeeId;
                _SPValidateAccountResult.MasterSalutationId = _Item.MasterSalutationId;
                _SPValidateAccountResult.EmployeeName = _Item.EmployeeName;
                _SPValidateAccountResult.Email = _Item.Email;
                _SPValidateAccountResult.MobileNumber = _Item.MobileNumber;

                _SPValidateAccountResult.MasterCompanyId = _Item.MasterCompanyId;
                _SPValidateAccountResult.CompanyTitle = _Item.CompanyTitle;
                _SPValidateAccountResult.MasterBranchId = _Item.MasterBranchId;
                _SPValidateAccountResult.BranchTitle = _Item.BranchTitle;
                _SPValidateAccountResult.MasterProfileId = _Item.MasterProfileId;
                _SPValidateAccountResult.ProfileTitle = _Item.ProfileTitle;
                
                _SPValidateAccountResult.MasterFunctionId = _Item.MasterFunctionId;
                _SPValidateAccountResult.FunctionTitle = _Item.FunctionTitle;
                _SPValidateAccountResult.FunctionLink = _Item.FunctionLink;
                _SPValidateAccountResult.FunctionIcon = _Item.FunctionIcon;
                _SPValidateAccountResult.FunctionIconColour = _Item.FunctionIconColour;
                _SPValidateAccountResult.ParentMasterFunctionId = _Item.ParentMasterFunctionId;
                _SPValidateAccountResult.LastLoginDate = _Item.LastLoginDate;

                _SPValidateAccountResult.Sequence = _Item.Sequence;
                _SPValidateAccountResult.IsDelete = _Item.IsDelete;
                _SPValidateAccountResult.IsInsert = _Item.IsInsert;
                _SPValidateAccountResult.IsUpdate = _Item.IsUpdate;
                _SPValidateAccountResult.IsSelect = _Item.IsSelect;

                _SPValidateAccountResultList.Add(_SPValidateAccountResult);
            }

            return _SPValidateAccountResultList;
        }


    }
}
