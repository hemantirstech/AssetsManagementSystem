using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterFinancialYearsController : ControllerBase
    {
        private readonly IMasterFinancialYearInterface<MasterFinancialYearResult> _IMasterFinancialYearInterface;

        public MasterFinancialYearsController(IMasterFinancialYearInterface<MasterFinancialYearResult> IMasterFinancialYearInterface)
        {
            _IMasterFinancialYearInterface = IMasterFinancialYearInterface;
        }

        // GET: api/MasterFinancialYears
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterFinancialYearResult>>> GetADMasterFinancialYears()
        {
            try
            {
                return _IMasterFinancialYearInterface.GetAllADMasterFinancialYear().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterFinancialYears/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterFinancialYearResult>> GetADMasterFinancialYear(long id)
        {
            try
            {
                var objMasterFinancialYearResult = _IMasterFinancialYearInterface.GetADMasterFinancialYearByID(id);

                if (objMasterFinancialYearResult == null)
                {
                    return NotFound();
                }

                return objMasterFinancialYearResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterFinancialYears/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterFinancialYearResult>> PutADMasterFinancialYear(long id, ADMasterFinancialYear objADMasterFinancialYear)
        {
            if (id != objADMasterFinancialYear.MasterFinancialYearId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterFinancialYearInterface.UpdateADMasterFinancialYear(objADMasterFinancialYear);

                return _IMasterFinancialYearInterface.GetADMasterFinancialYearByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterFinancialYearInterface.ADMasterFinancialYearExists(id))
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

        // POST: api/MasterFinancialYears
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterFinancialYearResult>> PostADMasterFinancialYear(ADMasterFinancialYear objADMasterFinancialYear)
        {
            try
            {
                await _IMasterFinancialYearInterface.InsertADMasterFinancialYear(objADMasterFinancialYear);

                return CreatedAtAction("GetADMasterFinancialYear", new { id = objADMasterFinancialYear.MasterFinancialYearId }, objADMasterFinancialYear);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterFinancialYears/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterFinancialYearResult>> DeleteADMasterFinancialYear(long id)
        {
            try
            {
                var objMasterFinancialYearResult = _IMasterFinancialYearInterface.GetADMasterFinancialYearByID(id);
                if (objMasterFinancialYearResult == null)
                {
                    return NotFound();
                }

                await _IMasterFinancialYearInterface.DeleteADMasterFinancialYear(id);

                return objMasterFinancialYearResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
