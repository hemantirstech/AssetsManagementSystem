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
    public class MasterRegionsController : ControllerBase
    {
        private readonly IMasterRegionInterface<MasterRegionResult> _IMasterRegionInterface;

        public MasterRegionsController(IMasterRegionInterface<MasterRegionResult> IMasterRegionInterface)
        {
            _IMasterRegionInterface = IMasterRegionInterface;
        }

        // GET: api/MasterRegions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterRegionResult>>> GetADMasterRegions()
        {
            try
            {
                return _IMasterRegionInterface.GetAllADMasterRegion().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterRegions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterRegionResult>> GetADMasterRegion(long id)
        {
            try
            {
                var objMasterRegionResult = _IMasterRegionInterface.GetADMasterRegionByID(id);

                if (objMasterRegionResult == null)
                {
                    return NotFound();
                }

                return objMasterRegionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterRegions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterRegionResult>> PutADMasterRegion(long id, ADMasterRegion objADMasterRegion)
        {
            if (id != objADMasterRegion.MasterRegionId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterRegionInterface.UpdateADMasterRegion(objADMasterRegion);

                return _IMasterRegionInterface.GetADMasterRegionByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterRegionInterface.ADMasterRegionExists(id))
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

        // POST: api/MasterRegions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterRegionResult>> PostADMasterRegion(ADMasterRegion objADMasterRegion)
        {
            try
            {
                await _IMasterRegionInterface.InsertADMasterRegion(objADMasterRegion);

                return CreatedAtAction("GetADMasterRegion", new { id = objADMasterRegion.MasterRegionId }, objADMasterRegion);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterRegions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterRegionResult>> DeleteADMasterRegion(long id)
        {
            try
            {
                var objMasterRegionResult = _IMasterRegionInterface.GetADMasterRegionByID(id);
                if (objMasterRegionResult == null)
                {
                    return NotFound();
                }

                await _IMasterRegionInterface.DeleteADMasterRegion(id);

                return objMasterRegionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
