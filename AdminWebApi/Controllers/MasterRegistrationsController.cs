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
    public class MasterRegistrationsController : ControllerBase
    {
        private readonly IMasterRegistrationInterface<MasterRegistrationResult> _IMasterRegistrationInterface;

        public MasterRegistrationsController(IMasterRegistrationInterface<MasterRegistrationResult> IMasterRegistrationInterface)
        {
            _IMasterRegistrationInterface = IMasterRegistrationInterface;
        }

        // GET: api/MasterRegistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterRegistrationResult>>> GetADMasterRegistrations()
        {
            try
            {
                return _IMasterRegistrationInterface.GetAllADMasterRegistration().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterRegistrationResult>> GetADMasterRegistration(long id)
        {
            try
            {
                var objMasterRegistrationResult = _IMasterRegistrationInterface.GetADMasterRegistrationByID(id);

                if (objMasterRegistrationResult == null)
                {
                    return NotFound();
                }

                return objMasterRegistrationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterRegistrations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterRegistrationResult>> PutADMasterRegistration(long id, ADMasterRegistration objADMasterRegistration)
        {
            if (id != objADMasterRegistration.MasterRegistrationId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterRegistrationInterface.UpdateADMasterRegistration(objADMasterRegistration);

                return _IMasterRegistrationInterface.GetADMasterRegistrationByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterRegistrationInterface.ADMasterRegistrationExists(id))
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

        // POST: api/MasterRegistrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterRegistrationResult>> PostADMasterRegistration(ADMasterRegistration objADMasterRegistration)
        {
            try
            {
                await _IMasterRegistrationInterface.InsertADMasterRegistration(objADMasterRegistration);

                return CreatedAtAction("GetADMasterRegistration", new { id = objADMasterRegistration.MasterRegistrationId }, objADMasterRegistration);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterRegistrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterRegistrationResult>> DeleteADMasterRegistration(long id)
        {
            try
            {
                var objMasterRegistrationResult = _IMasterRegistrationInterface.GetADMasterRegistrationByID(id);
                if (objMasterRegistrationResult == null)
                {
                    return NotFound();
                }

                await _IMasterRegistrationInterface.DeleteADMasterRegistration(id);

                return objMasterRegistrationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
