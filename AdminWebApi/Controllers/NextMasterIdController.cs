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
    public class NextMasterIdController : ControllerBase
    {
        private readonly INextMasterIdInterface<SPNextMasterIdResult> _INextMasterIdInterface;
        public NextMasterIdController(INextMasterIdInterface<SPNextMasterIdResult> INextMasterIdInterface)
        {
            _INextMasterIdInterface = INextMasterIdInterface;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPNextMasterIdResult>>> NextMasterId(string TableName)
        {
            try
            {          
                var _CheckAvailableResult = _INextMasterIdInterface.GetNextMasterId(TableName).ToList();

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
