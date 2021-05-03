using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminDAL;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using AdminDAL;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileTaskMappingWithFunctionController : ControllerBase
    {
        private readonly IProfileTaskMappingInterface<SPProfileTaskMappingResult> _IProfileTaskMappingInterface;
        public ProfileTaskMappingWithFunctionController(IProfileTaskMappingInterface<SPProfileTaskMappingResult> IProfileTaskMappingInterface)
        {
            _IProfileTaskMappingInterface = IProfileTaskMappingInterface;
        }


        //// GET: api/ProfileTaskMappings/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPProfileTaskMappingResult>>> GetAllADProfileTaskMappingWithFunction(long MasterProfileId)
        {
            try
            {
                var objSPProfileTaskMappingResult = _IProfileTaskMappingInterface.GetAllADProfileTaskMappingWithFunction(MasterProfileId);

                return objSPProfileTaskMappingResult.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
