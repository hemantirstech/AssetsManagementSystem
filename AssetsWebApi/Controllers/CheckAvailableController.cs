using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AssetsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsCheckAvailableController : ControllerBase
    {
        private readonly ICheckAvailableInterface<SPCheckAvailableResult> _ICheckAvailableInterface;
        public AssetsCheckAvailableController(ICheckAvailableInterface<SPCheckAvailableResult> ICheckAvailableInterface)
        {
            _ICheckAvailableInterface = ICheckAvailableInterface;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPCheckAvailableResult>>> GetCheckAvailable(string TableName, string NameAvailable, long NameId)
        {
            try
            {
                var _CheckAvailableResult = _ICheckAvailableInterface.GetCheckAvailable(TableName, NameAvailable, NameId).ToList();

                return _CheckAvailableResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
