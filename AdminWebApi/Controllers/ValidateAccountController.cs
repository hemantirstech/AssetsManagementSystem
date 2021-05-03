using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateAccountController : ControllerBase
    {
        private readonly IValidateAccountInterface<SPValidateAccountResult> _IValidateAccountInterface;
        public ValidateAccountController(IValidateAccountInterface<SPValidateAccountResult> IValidateAccountInterface)
        {
            _IValidateAccountInterface = IValidateAccountInterface;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPValidateAccountResult>>> ValidateAccount(string UserName,string Password,string VerificationCode,string MacId, string SessionId)
        {
            try
            {          
                var _ValidateAccountResult = _IValidateAccountInterface.GetValidateAccount(UserName, Password, VerificationCode, MacId, SessionId, "",0,"").ToList();

                return  _ValidateAccountResult;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
