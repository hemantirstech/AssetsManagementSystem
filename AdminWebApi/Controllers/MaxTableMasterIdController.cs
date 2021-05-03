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
    public class MaxTableMasterIdController : ControllerBase
    {
        private readonly IMaxTableMasterIdInterface<SPMaxTableMasterIdResult> _IMaxTableMasterIdInterface;
        public MaxTableMasterIdController(IMaxTableMasterIdInterface<SPMaxTableMasterIdResult> IMaxTableMasterIdInterface)
        {
            _IMaxTableMasterIdInterface = IMaxTableMasterIdInterface;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPMaxTableMasterIdResult>>> MaxTableMasterId(string TableName)
        {
            try
            {          
                var _MaxTableResult = _IMaxTableMasterIdInterface.GetMaxTableMasterId(TableName).ToList();

                return _MaxTableResult;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
