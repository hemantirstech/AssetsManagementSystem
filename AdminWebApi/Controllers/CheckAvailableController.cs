using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminWebApi.Model;
using AdminWebApi.Repository.Contract;
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
    public class CheckAvailableController : ControllerBase
    {
        private readonly ICheckAvailableInterface<SPCheckAvailableResult> _ICheckAvailableInterface;
        public CheckAvailableController(ICheckAvailableInterface<SPCheckAvailableResult> ICheckAvailableInterface)
        {
            _ICheckAvailableInterface = ICheckAvailableInterface;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPCheckAvailableResult>>> CheckAvailable(string TableName, string NameAvailable, long NameId)
        {
            try
            {          
                var _CheckAvailableResult = _ICheckAvailableInterface.GetCheckAvailable(TableName, NameAvailable, NameId).ToList();

                return _CheckAvailableResult;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
