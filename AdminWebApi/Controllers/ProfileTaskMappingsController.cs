using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminDAL;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileTaskMappingsController : ControllerBase
    {
        private readonly IProfileTaskMappingInterface<SPProfileTaskMappingResult> _IProfileTaskMappingInterface;

        public ProfileTaskMappingsController(IProfileTaskMappingInterface<SPProfileTaskMappingResult> IProfileTaskMappingInterface)
        {
            _IProfileTaskMappingInterface = IProfileTaskMappingInterface;
        }

        // GET: api/ProfileTaskMappings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SPProfileTaskMappingResult>>> GetADProfileTaskMappings()
        {
            try
            {
                return _IProfileTaskMappingInterface.GetAllADProfileTaskMappingWithFunction(1).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        // GET: api/ProfileTaskMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SPProfileTaskMappingResult>> GetADProfileTaskMapping(long id)
        {
            try
            {
                var objSPProfileTaskMappingResult = _IProfileTaskMappingInterface.GetADProfileTaskMappingById(id);

                if (objSPProfileTaskMappingResult == null)
                {
                    return NotFound();
                }

                return objSPProfileTaskMappingResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/ProfileTaskMappings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<SPProfileTaskMappingResult>> PutADProfileTaskMapping(long id, ADProfileTaskMapping objADProfileTaskMapping)
        {
            if (id != objADProfileTaskMapping.MasterProfileTaskMappingId)
            {
                return BadRequest();
            }
            
            try
            {
                await _IProfileTaskMappingInterface.UpdateADProfileTaskMapping(objADProfileTaskMapping);
                return _IProfileTaskMappingInterface.GetADProfileTaskMappingById(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IProfileTaskMappingInterface.ADProfileTaskMappingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NoContent();
        }

        // POST: api/ProfileTaskMappings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IEnumerable<SPProfileTaskMappingResult>>> PostADProfileTaskMapping(List<ADProfileTaskMapping> objADProfileTaskMappingList)
        {
           try
            {
                bool _CompleteOperation = await _IProfileTaskMappingInterface.InsertUpdateADProfileTaskMapping(objADProfileTaskMappingList);

                long _MasterProfileID = objADProfileTaskMappingList.Select(a => a.MasterProfileId).FirstOrDefault()??1;

                var objSPProfileTaskMappingResult = _IProfileTaskMappingInterface.GetAllADProfileTaskMappingWithFunction(_MasterProfileID);

                if(_CompleteOperation==true)
                {
                    return objSPProfileTaskMappingResult.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
