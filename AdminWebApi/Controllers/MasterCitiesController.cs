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
    public class MasterCitiesController : ControllerBase
    {
        private readonly IMasterCityInterface<MasterCityResult> _IMasterCityInterface;

        public MasterCitiesController(IMasterCityInterface<MasterCityResult> IMasterCityInterface)
        {
            _IMasterCityInterface = IMasterCityInterface;
        }

        // GET: api/MasterCitys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCityResult>>> GetADMasterCitys()
        {
            try
            {
                return _IMasterCityInterface.GetAllADMasterCity().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCitys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCityResult>> GetADMasterCity(long id)
        {
            try
            {
                var objMasterCityResult = _IMasterCityInterface.GetADMasterCityByID(id);

                if (objMasterCityResult == null)
                {
                    return NotFound();
                }

                return objMasterCityResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCitys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCityResult>> PutADMasterCity(long id, ADMasterCity objADMasterCity)
        {
            if (id != objADMasterCity.MasterCityId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterCityInterface.UpdateADMasterCity(objADMasterCity);

                return _IMasterCityInterface.GetADMasterCityByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterCityInterface.ADMasterCityExists(id))
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

        // POST: api/MasterCitys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCityResult>> PostADMasterCity(ADMasterCity objADMasterCity)
        {
            try
            {
                await _IMasterCityInterface.InsertADMasterCity(objADMasterCity);

                return CreatedAtAction("GetADMasterCity", new { id = objADMasterCity.MasterCityId }, objADMasterCity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCitys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCityResult>> DeleteADMasterCity(long id)
        {
            try
            {
                var objMasterCityResult = _IMasterCityInterface.GetADMasterCityByID(id);
                if (objMasterCityResult == null)
                {
                    return NotFound();
                }

                await _IMasterCityInterface.DeleteADMasterCity(id);

                return objMasterCityResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
