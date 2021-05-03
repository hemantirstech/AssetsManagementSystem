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
    public class AssetsDropDownFillController : ControllerBase
    {
        private readonly IDropDownFillInterface<SPDropDownFillResult> _IDropDownFillInterface;
        public AssetsDropDownFillController(IDropDownFillInterface<SPDropDownFillResult> IDropDownFillInterface)
        {
            _IDropDownFillInterface = IDropDownFillInterface;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPDropDownFillResult>>> GetDropDownFill(string TableName, long MasterId, string Type)
        {
            try
            {
                var _DropDownFillResult = _IDropDownFillInterface.GetDropDownFill(TableName, MasterId, Type).ToList();

                return _DropDownFillResult.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
