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
    public class MasterConfigurationsController : ControllerBase
    {
        private readonly IMasterConfigurationInterface<MasterConfigurationResult> _IMasterConfigurationInterface;

        public MasterConfigurationsController(IMasterConfigurationInterface<MasterConfigurationResult> IMasterConfigurationInterface)
        {
            _IMasterConfigurationInterface = IMasterConfigurationInterface;
        }

        // GET: api/MasterConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterConfigurationResult>>> GetADMasterConfigurations()
        {
            try
            {
                return _IMasterConfigurationInterface.GetAllADMasterConfiguration().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterConfigurationResult>> GetADMasterConfiguration(long id)
        {
            try
            {
                var objMasterConfigurationResult = _IMasterConfigurationInterface.GetADMasterConfigurationByID(id);

                if (objMasterConfigurationResult == null)
                {
                    return NotFound();
                }

                return objMasterConfigurationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterConfigurations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterConfigurationResult>> PutADMasterConfiguration(long id, ADMasterConfiguration objADMasterConfiguration)
        {
            if (id != objADMasterConfiguration.MasterConfigurationId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterConfigurationInterface.UpdateADMasterConfiguration(objADMasterConfiguration);

                return _IMasterConfigurationInterface.GetADMasterConfigurationByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterConfigurationInterface.ADMasterConfigurationExists(id))
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

        // POST: api/MasterConfigurations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterConfigurationResult>> PostADMasterConfiguration(ADMasterConfiguration objADMasterConfiguration)
        {
            try
            {
                await _IMasterConfigurationInterface.InsertADMasterConfiguration(objADMasterConfiguration);

                return CreatedAtAction("GetADMasterConfiguration", new { id = objADMasterConfiguration.MasterConfigurationId }, objADMasterConfiguration);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterConfigurations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterConfigurationResult>> DeleteADMasterConfiguration(long id)
        {
            try
            {
                var objMasterConfigurationResult = _IMasterConfigurationInterface.GetADMasterConfigurationByID(id);
                if (objMasterConfigurationResult == null)
                {
                    return NotFound();
                }

                await _IMasterConfigurationInterface.DeleteADMasterConfiguration(id);

                return objMasterConfigurationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
