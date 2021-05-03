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
    public class MasterSalutationsController : ControllerBase
        
    {
        private readonly IMasterSalutationInterface<MasterSalutationResult> _IMasterSalutationInterface;

        public MasterSalutationsController(IMasterSalutationInterface<MasterSalutationResult> IMasterSalutationInterface)
        {
            _IMasterSalutationInterface = IMasterSalutationInterface;
        }

        // GET: api/MasterSalutations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterSalutationResult>>> GetADMasterSalutations()
        {
            try
            {
                return _IMasterSalutationInterface.GetAllADMasterSalutation().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterSalutations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterSalutationResult>> GetADMasterSalutation(long id)
        {
            try
            {
                var objMasterSalutationResult = _IMasterSalutationInterface.GetADMasterSalutationByID(id);

                if (objMasterSalutationResult == null)
                {
                    return NotFound();
                }

                return objMasterSalutationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterSalutations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterSalutationResult>> PutADMasterSalutation(long id, ADMasterSalutation objADMasterSalutation)
        {
            if (id != objADMasterSalutation.MasterSalutationId)
            {
                return BadRequest();
            }


            try
            {
                await  _IMasterSalutationInterface.UpdateADMasterSalutation(objADMasterSalutation);

                return _IMasterSalutationInterface.GetADMasterSalutationByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterSalutationInterface.ADMasterSalutationExists(id))
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

        // POST: api/MasterSalutations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterSalutationResult>> PostADMasterSalutation(ADMasterSalutation objADMasterSalutation)
        {
            try
            {
                await _IMasterSalutationInterface.InsertADMasterSalutation(objADMasterSalutation);

                return CreatedAtAction("GetADMasterSalutation", new { id = objADMasterSalutation.MasterSalutationId }, objADMasterSalutation);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterSalutations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterSalutationResult>> DeleteADMasterSalutation(long id)
        {
            try
            {
                var objMasterSalutationResult = _IMasterSalutationInterface.GetADMasterSalutationByID(id);
                if (objMasterSalutationResult == null)
                {
                    return NotFound();
                }

                await _IMasterSalutationInterface.DeleteADMasterSalutation(id);

                return objMasterSalutationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
