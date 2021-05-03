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
    public class MasterTimeZonesController : ControllerBase
    {
        private readonly IMasterTimeZoneInterface<MasterTimeZoneResult> _IMasterTimeZoneInterface;

        public MasterTimeZonesController(IMasterTimeZoneInterface<MasterTimeZoneResult> IMasterTimeZoneInterface)
        {
            _IMasterTimeZoneInterface = IMasterTimeZoneInterface;
        }

        // GET: api/MasterTimeZones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterTimeZoneResult>>> GetADMasterTimeZones()
        {
            try
            {
                return _IMasterTimeZoneInterface.GetAllADMasterTimeZone().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterTimeZones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterTimeZoneResult>> GetADMasterTimeZone(long id)
        {
            try
            {
                var objMasterTimeZoneResult = _IMasterTimeZoneInterface.GetADMasterTimeZoneByID(id);

                if (objMasterTimeZoneResult == null)
                {
                    return NotFound();
                }

                return objMasterTimeZoneResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterTimeZones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterTimeZoneResult>> PutADMasterTimeZone(long id, ADMasterTimeZone objADMasterTimeZone)
        {
            if (id != objADMasterTimeZone.MasterTimeZoneId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterTimeZoneInterface.UpdateADMasterTimeZone(objADMasterTimeZone);

                return _IMasterTimeZoneInterface.GetADMasterTimeZoneByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterTimeZoneInterface.ADMasterTimeZoneExists(id))
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

        // POST: api/MasterTimeZones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterTimeZoneResult>> PostADMasterTimeZone(ADMasterTimeZone objADMasterTimeZone)
        {
            try
            {
                await _IMasterTimeZoneInterface.InsertADMasterTimeZone(objADMasterTimeZone);

                return CreatedAtAction("GetADMasterTimeZone", new { id = objADMasterTimeZone.MasterTimeZoneId }, objADMasterTimeZone);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterTimeZones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterTimeZoneResult>> DeleteADMasterTimeZone(long id)
        {
            try
            {
                var objMasterTimeZoneResult = _IMasterTimeZoneInterface.GetADMasterTimeZoneByID(id);
                if (objMasterTimeZoneResult == null)
                {
                    return NotFound();
                }

                await _IMasterTimeZoneInterface.DeleteADMasterTimeZone(id);

                return objMasterTimeZoneResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
