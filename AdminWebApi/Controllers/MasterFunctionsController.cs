using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterFunctionsController : ControllerBase
    {
        private readonly IMasterFunctionInterface<MasterFunctionResult> _IMasterFunctionInterface;

        public MasterFunctionsController(IMasterFunctionInterface<MasterFunctionResult> IMasterFunctionInterface)
        {
            _IMasterFunctionInterface = IMasterFunctionInterface;
        }

        // GET: api/MasterFunctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterFunctionResult>>> GetADMasterFunctions()
        {
            try
            {
                return _IMasterFunctionInterface.GetAllADMasterFunction().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterFunctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterFunctionResult>> GetADMasterFunction(long id)
        {
            try
            {
                var objMasterFunctionResult = _IMasterFunctionInterface.GetADMasterFunctionByID(id);

                if (objMasterFunctionResult == null)
                {
                    return NotFound();
                }

                return objMasterFunctionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterFunctions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterFunctionResult>> PutADMasterFunction(long id, ADMasterFunction objADMasterFunction)
        {
            if (id != objADMasterFunction.MasterFunctionId)
            {
                return BadRequest();
            }
                       
            try
            {
                await _IMasterFunctionInterface.UpdateADMasterFunction(objADMasterFunction);
                return _IMasterFunctionInterface.GetADMasterFunctionByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterFunctionInterface.ADMasterFunctionExists(id))
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

        // POST: api/MasterFunctions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterFunctionResult>> PostADMasterFunction(ADMasterFunction objADMasterFunction)
        {
            try
            {
                await _IMasterFunctionInterface.InsertADMasterFunction(objADMasterFunction);

                return CreatedAtAction("GetADMasterFunction", new { id = objADMasterFunction.MasterFunctionId }, objADMasterFunction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterFunctions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterFunctionResult>> DeleteADMasterFunction(long id)
        {
            try
            {
                var objMasterFunctionResult = _IMasterFunctionInterface.GetADMasterFunctionByID(id);

                if (objMasterFunctionResult == null)
                {
                    return NotFound();
                }

                await _IMasterFunctionInterface.DeleteADMasterFunction(id);

                return objMasterFunctionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
