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
    public class AssetsMaxTableMasterIdController : ControllerBase
    {
        private readonly IMaxTableMasterIdInterface<SPMaxTableMasterIdResult> _IMaxTableMasterIdInterface;
        public AssetsMaxTableMasterIdController(IMaxTableMasterIdInterface<SPMaxTableMasterIdResult> IMaxTableMasterIdInterface)
        {
            _IMaxTableMasterIdInterface = IMaxTableMasterIdInterface;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPMaxTableMasterIdResult>>> GetMaxTableMasterId(string TableName)
        {
            try
            {
                var _MaxTableResult = _IMaxTableMasterIdInterface.GetMaxTableMasterId(TableName).ToList();

                return _MaxTableResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
