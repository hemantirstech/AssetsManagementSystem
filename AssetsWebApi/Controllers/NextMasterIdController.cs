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
    public class AssetsNextMasterIdController : ControllerBase
    {
        private readonly INextMasterIdInterface<SPNextMasterIdResult> _INextMasterIdInterface;
        public AssetsNextMasterIdController(INextMasterIdInterface<SPNextMasterIdResult> INextMasterIdInterface)
        {
            _INextMasterIdInterface = INextMasterIdInterface;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPNextMasterIdResult>>> GetNextMasterId(string TableName)
        {
            try
            {
                var _CheckAvailableResult = _INextMasterIdInterface.GetNextMasterId(TableName).ToList();

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
