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
    public class MasterRegistrationTypesController : ControllerBase
    {
        private readonly IMasterRegistrationTypeInterface<MasterRegistrationTypeResult> _IMasterRegistrationTypeInterface;

        public MasterRegistrationTypesController(IMasterRegistrationTypeInterface<MasterRegistrationTypeResult> IMasterRegistrationTypeInterface)
        {
            _IMasterRegistrationTypeInterface = IMasterRegistrationTypeInterface;
        }

        // GET: api/MasterRegistrationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterRegistrationTypeResult>>> GetADMasterRegistrationTypes()
        {
            try
            {
                return _IMasterRegistrationTypeInterface.GetAllADMasterRegistrationType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterRegistrationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterRegistrationTypeResult>> GetADMasterRegistrationType(long id)
        {
            try
            {
                var objMasterRegistrationTypeResult = _IMasterRegistrationTypeInterface.GetADMasterRegistrationTypeByID(id);

                if (objMasterRegistrationTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterRegistrationTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterRegistrationTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterRegistrationTypeResult>> PutADMasterRegistrationType(long id, ADMasterRegistrationType objADMasterRegistrationType)
        {
            if (id != objADMasterRegistrationType.MasterRegistrationTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterRegistrationTypeInterface.UpdateADMasterRegistrationType(objADMasterRegistrationType);

                return _IMasterRegistrationTypeInterface.GetADMasterRegistrationTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterRegistrationTypeInterface.ADMasterRegistrationTypeExists(id))
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

        // POST: api/MasterRegistrationTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterRegistrationTypeResult>> PostADMasterRegistrationType(ADMasterRegistrationType objADMasterRegistrationType)
        {
            try
            {
                await _IMasterRegistrationTypeInterface.InsertADMasterRegistrationType(objADMasterRegistrationType);

                return CreatedAtAction("GetADMasterRegistrationType", new { id = objADMasterRegistrationType.MasterRegistrationTypeId }, objADMasterRegistrationType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterRegistrationTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterRegistrationTypeResult>> DeleteADMasterRegistrationType(long id)
        {
            try
            {
                var objMasterRegistrationTypeResult = _IMasterRegistrationTypeInterface.GetADMasterRegistrationTypeByID(id);
                if (objMasterRegistrationTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterRegistrationTypeInterface.DeleteADMasterRegistrationType(id);

                return objMasterRegistrationTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
