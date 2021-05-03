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
    public class MasterTaxesController : ControllerBase
    {
        private readonly IMasterTaxInterface<MasterTaxResult> _IMasterTaxInterface;

        public MasterTaxesController(IMasterTaxInterface<MasterTaxResult> IMasterTaxInterface)
        {
            _IMasterTaxInterface = IMasterTaxInterface;
        }

        // GET: api/MasterTaxs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterTaxResult>>> GetADMasterTaxs()
        {
            try
            {
                return _IMasterTaxInterface.GetAllADMasterTax().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterTaxs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterTaxResult>> GetADMasterTax(long id)
        {
            try
            {
                var objMasterTaxResult = _IMasterTaxInterface.GetADMasterTaxByID(id);

                if (objMasterTaxResult == null)
                {
                    return NotFound();
                }

                return objMasterTaxResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterTaxs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterTaxResult>> PutADMasterTax(long id, ADMasterTax objADMasterTax)
        {
            if (id != objADMasterTax.MasterTaxId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterTaxInterface.UpdateADMasterTax(objADMasterTax);

                return _IMasterTaxInterface.GetADMasterTaxByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterTaxInterface.ADMasterTaxExists(id))
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

        // POST: api/MasterTaxs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterTaxResult>> PostADMasterTax(ADMasterTax objADMasterTax)
        {
            try
            {
                await _IMasterTaxInterface.InsertADMasterTax(objADMasterTax);

                return CreatedAtAction("GetADMasterTax", new { id = objADMasterTax.MasterTaxId }, objADMasterTax);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterTaxs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterTaxResult>> DeleteADMasterTax(long id)
        {
            try
            {
                var objMasterTaxResult = _IMasterTaxInterface.GetADMasterTaxByID(id);
                if (objMasterTaxResult == null)
                {
                    return NotFound();
                }

                await _IMasterTaxInterface.DeleteADMasterTax(id);

                return objMasterTaxResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
