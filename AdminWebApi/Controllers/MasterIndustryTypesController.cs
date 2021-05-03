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
    public class MasterIndustryTypesController : ControllerBase
    {
        private readonly IMasterIndustryTypeInterface<MasterIndustryTypeResult> _IMasterIndustryTypeInterface;

        public MasterIndustryTypesController(IMasterIndustryTypeInterface <MasterIndustryTypeResult>IMasterIndustryTypeInterface)
        {
            _IMasterIndustryTypeInterface = IMasterIndustryTypeInterface;
        }

        // GET: api/MasterIndustryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterIndustryTypeResult>>> GetADMasterIndustryTypes()
        {
            try
            {
                return _IMasterIndustryTypeInterface.GetAllADMasterIndustryType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterIndustryTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterIndustryTypeResult>> GetADMasterIndustryType(long id)
        {
            try
            {
                var objMasterIndustryTypeResult = _IMasterIndustryTypeInterface.GetADMasterIndustryTypeByID(id);

                if (objMasterIndustryTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterIndustryTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterIndustryTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterIndustryTypeResult>> PutADMasterIndustryType(long id, ADMasterIndustryType objADMasterIndustryType)
        {
            if (id != objADMasterIndustryType.MasterIndustryTypeId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterIndustryTypeInterface.UpdateADMasterIndustryType(objADMasterIndustryType);

                return _IMasterIndustryTypeInterface.GetADMasterIndustryTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterIndustryTypeInterface.ADMasterIndustryTypeExists(id))
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

        // POST: api/MasterIndustryTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterIndustryTypeResult>> PostADMasterIndustryType(ADMasterIndustryType objADMasterIndustryType)
        {
            try
            {
                await _IMasterIndustryTypeInterface.InsertADMasterIndustryType(objADMasterIndustryType);

                return CreatedAtAction("GetADMasterIndustryType", new { id = objADMasterIndustryType.MasterIndustryGroupId }, objADMasterIndustryType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterIndustryTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterIndustryTypeResult>> DeleteADMasterIndustryType(long id)
        {

            try
            {
                var objMasterIndustryTypeResult = _IMasterIndustryTypeInterface.GetADMasterIndustryTypeByID(id);
                if (objMasterIndustryTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterIndustryTypeInterface.DeleteADMasterIndustryType(id);

                return objMasterIndustryTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
