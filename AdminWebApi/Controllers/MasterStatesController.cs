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
    public class MasterStatesController : ControllerBase
    {
        private readonly IMasterStateInterface<MasterStateResult> _IMasterStateInterface;

        public MasterStatesController(IMasterStateInterface<MasterStateResult> IMasterStateInterface)
        {
            _IMasterStateInterface = IMasterStateInterface;
        }

        // GET: api/MasterStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterStateResult>>> GetADMasterStates()
        {
            try
            {
                return _IMasterStateInterface.GetAllADMasterState().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterStateResult>> GetADMasterState(long id)
        {
            try
            {
                var objMasterStateResult = _IMasterStateInterface.GetADMasterStateByID(id);

                if (objMasterStateResult == null)
                {
                    return NotFound();
                }

                return objMasterStateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterStates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterStateResult>> PutADMasterState(long id, ADMasterState objADMasterState)
        {
            if (id != objADMasterState.MasterStateId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterStateInterface.UpdateADMasterState(objADMasterState);

                return _IMasterStateInterface.GetADMasterStateByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterStateInterface.ADMasterStateExists(id))
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

        // POST: api/MasterStates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterStateResult>> PostADMasterState(ADMasterState objADMasterState)
        {
            try
            {
                await _IMasterStateInterface.InsertADMasterState(objADMasterState);

                return CreatedAtAction("GetADMasterState", new { id = objADMasterState.MasterStateId }, objADMasterState);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterStates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterStateResult>> DeleteADMasterState(long id)
        {
            try
            {
                var objMasterStateResult = _IMasterStateInterface.GetADMasterStateByID(id);
                if (objMasterStateResult == null)
                {
                    return NotFound();
                }

                await _IMasterStateInterface.DeleteADMasterState(id);

                return objMasterStateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
