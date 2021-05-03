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
    public class MasterCountriesController : ControllerBase
    {
        private readonly IMasterCountryInterface<MasterCountryResult> _IMasterCountryInterface;

        public MasterCountriesController(IMasterCountryInterface<MasterCountryResult> IMasterCountryInterface)
        {
            _IMasterCountryInterface = IMasterCountryInterface;
        }

        // GET: api/MasterCountrys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCountryResult>>> GetADMasterCountrys()
        {
            try
            {
                return _IMasterCountryInterface.GetAllADMasterCountry().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCountrys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCountryResult>> GetADMasterCountry(long id)
        {
            try
            {
                var objMasterCountryResult = _IMasterCountryInterface.GetADMasterCountryByID(id);

                if (objMasterCountryResult == null)
                {
                    return NotFound();
                }

                return objMasterCountryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCountrys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCountryResult>> PutADMasterCountry(long id, ADMasterCountry objADMasterCountry)
        {
            if (id != objADMasterCountry.MasterCountryId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterCountryInterface.UpdateADMasterCountry(objADMasterCountry);

                return _IMasterCountryInterface.GetADMasterCountryByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterCountryInterface.ADMasterCountryExists(id))
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

        // POST: api/MasterCountrys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCountryResult>> PostADMasterCountry(ADMasterCountry objADMasterCountry)
        {
            try
            {
                await _IMasterCountryInterface.InsertADMasterCountry(objADMasterCountry);

                return CreatedAtAction("GetADMasterCountry", new { id = objADMasterCountry.MasterCountryId }, objADMasterCountry);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCountrys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCountryResult>> DeleteADMasterCountry(long id)
        {
            try
            {
                var objMasterCountryResult = _IMasterCountryInterface.GetADMasterCountryByID(id);
                if (objMasterCountryResult == null)
                {
                    return NotFound();
                }

                await _IMasterCountryInterface.DeleteADMasterCountry(id);

                return objMasterCountryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
