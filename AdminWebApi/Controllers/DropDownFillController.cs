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
    public class DropDownFillController : ControllerBase
    {
        private readonly IDropDownFillInterface<SPDropDownFillResult> _IDropDownFillInterface;
        public DropDownFillController(IDropDownFillInterface<SPDropDownFillResult> IDropDownFillInterface)
        {
            _IDropDownFillInterface = IDropDownFillInterface;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPDropDownFillResult>>> DropDownFill(string TableName, long MasterId, string Type)
        {
            try
            {          
                var _DropDownFillResult = _IDropDownFillInterface.GetDropDownFill(TableName,MasterId,Type).ToList();

                return _DropDownFillResult.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
