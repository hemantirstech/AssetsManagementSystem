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
    public class MasterBusinessVerticlesController : ControllerBase
    {
        private readonly IMasterBusinessVerticleInterface<MasterBusinessVerticleResult> _IMasterBusinessVerticleInterface;

        public MasterBusinessVerticlesController(IMasterBusinessVerticleInterface<MasterBusinessVerticleResult> IMasterBusinessVerticleInterface)
        {
            _IMasterBusinessVerticleInterface = IMasterBusinessVerticleInterface;
        }

        // GET: api/MasterBusinessVerticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterBusinessVerticleResult>>> GetADMasterBusinessVerticles()
        {
            try
            {
                return _IMasterBusinessVerticleInterface.GetAllADMasterBusinessVerticle().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterBusinessVerticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterBusinessVerticleResult>> GetADMasterBusinessVerticle(long id)
        {
            try
            {
                var objMasterBusinessVerticleResult = _IMasterBusinessVerticleInterface.GetADMasterBusinessVerticleByID(id);

                if (objMasterBusinessVerticleResult == null)
                {
                    return NotFound();
                }

                return objMasterBusinessVerticleResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterBusinessVerticles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterBusinessVerticleResult>> PutADMasterBusinessVerticle(long id, ADMasterBusinessVerticle objADMasterBusinessVerticle)
        {
            if (id != objADMasterBusinessVerticle.MasterBusinessVerticleId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterBusinessVerticleInterface.UpdateADMasterBusinessVerticle(objADMasterBusinessVerticle);

                return _IMasterBusinessVerticleInterface.GetADMasterBusinessVerticleByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterBusinessVerticleInterface.ADMasterBusinessVerticleExists(id))
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

        // POST: api/MasterBusinessVerticles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterBusinessVerticleResult>> PostADMasterBusinessVerticle(ADMasterBusinessVerticle objADMasterBusinessVerticle)
        {
            try
            {
                await _IMasterBusinessVerticleInterface.InsertADMasterBusinessVerticle(objADMasterBusinessVerticle);

                return CreatedAtAction("GetADMasterBusinessVerticle", new { id = objADMasterBusinessVerticle.MasterBusinessVerticleId }, objADMasterBusinessVerticle);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterBusinessVerticles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterBusinessVerticleResult>> DeleteADMasterBusinessVerticle(long id)
        {
            try
            {
                var objMasterBusinessVerticleResult = _IMasterBusinessVerticleInterface.GetADMasterBusinessVerticleByID(id);
                if (objMasterBusinessVerticleResult == null)
                {
                    return NotFound();
                }

                await _IMasterBusinessVerticleInterface.DeleteADMasterBusinessVerticle(id);

                return objMasterBusinessVerticleResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
