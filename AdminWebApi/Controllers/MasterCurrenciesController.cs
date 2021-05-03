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
    public class MasterCurrenciesController : ControllerBase
    {
        private readonly IMasterCurrencyInterface<MasterCurrencyResult> _IMasterCurrencyInterface;

        public MasterCurrenciesController(IMasterCurrencyInterface<MasterCurrencyResult> IMasterCurrencyInterface)
        {
            _IMasterCurrencyInterface = IMasterCurrencyInterface;
        }

        // GET: api/MasterCurrencys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCurrencyResult>>> GetADMasterCurrencys()
        {
            try
            {
                return _IMasterCurrencyInterface.GetAllADMasterCurrency().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCurrencys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCurrencyResult>> GetADMasterCurrency(long id)
        {
            try
            {
                var objMasterCurrencyResult = _IMasterCurrencyInterface.GetADMasterCurrencyByID(id);

                if (objMasterCurrencyResult == null)
                {
                    return NotFound();
                }

                return objMasterCurrencyResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCurrencys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCurrencyResult>> PutADMasterCurrency(long id, ADMasterCurrency objADMasterCurrency)
        {
            if (id != objADMasterCurrency.MasterCurrencyId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterCurrencyInterface.UpdateADMasterCurrency(objADMasterCurrency);

                return _IMasterCurrencyInterface.GetADMasterCurrencyByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterCurrencyInterface.ADMasterCurrencyExists(id))
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

        // POST: api/MasterCurrencys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCurrencyResult>> PostADMasterCurrency(ADMasterCurrency objADMasterCurrency)
        {
            try
            {
                await _IMasterCurrencyInterface.InsertADMasterCurrency(objADMasterCurrency);

                return CreatedAtAction("GetADMasterCurrency", new { id = objADMasterCurrency.MasterCurrencyId }, objADMasterCurrency);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCurrencys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCurrencyResult>> DeleteADMasterCurrency(long id)
        {
            try
            {
                var objMasterCurrencyResult = _IMasterCurrencyInterface.GetADMasterCurrencyByID(id);
                if (objMasterCurrencyResult == null)
                {
                    return NotFound();
                }

                await _IMasterCurrencyInterface.DeleteADMasterCurrency(id);

                return objMasterCurrencyResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
