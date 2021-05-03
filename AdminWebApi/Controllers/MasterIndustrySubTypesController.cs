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
    public class MasterIndustrySubTypesController : ControllerBase
    {
        private readonly IMasterIndustrySubTypeInterface<MasterIndustrySubTypeResult> _IMasterIndustrySubTypeInterface;

        public MasterIndustrySubTypesController(IMasterIndustrySubTypeInterface <MasterIndustrySubTypeResult>IMasterIndustrySubTypeInterface)
        {
            _IMasterIndustrySubTypeInterface = IMasterIndustrySubTypeInterface;
        }

        // GET: api/MasterIndustrySubTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterIndustrySubTypeResult>>> GetADMasterIndustrySubTypes()
        {
            try
            {
                return  _IMasterIndustrySubTypeInterface.GetAllADMasterIndustrySubType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterIndustrySubTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterIndustrySubTypeResult>> GetADMasterIndustrySubType(long id)
        {
            try
            {
                var objMasterIndustrySubTypeResult = _IMasterIndustrySubTypeInterface.GetADMasterIndustrySubTypeByID(id);

                if (objMasterIndustrySubTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterIndustrySubTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterIndustrySubTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterIndustrySubTypeResult>> PutADMasterIndustrySubType(long id, ADMasterIndustrySubType objADMasterIndustrySubType)
        {
            if (id != objADMasterIndustrySubType.MasterIndustrySubTypeId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterIndustrySubTypeInterface.UpdateADMasterIndustrySubType(objADMasterIndustrySubType);

                return _IMasterIndustrySubTypeInterface.GetADMasterIndustrySubTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterIndustrySubTypeInterface.ADMasterIndustrySubTypeExists(id))
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

        // POST: api/MasterIndustrySubTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterIndustrySubTypeResult>> PostADMasterIndustrySubType(ADMasterIndustrySubType objADMasterIndustrySubType)
        {
            try
            {
                await _IMasterIndustrySubTypeInterface.InsertADMasterIndustrySubType(objADMasterIndustrySubType);

                return CreatedAtAction("GetADMasterIndustrySubType", new { id = objADMasterIndustrySubType.MasterIndustrySubTypeId }, objADMasterIndustrySubType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterIndustrySubTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterIndustrySubTypeResult>> DeleteADMasterIndustrySubType(long id)
        {
            try
            {
                var objMasterIndustrySubTypeResult = _IMasterIndustrySubTypeInterface.GetADMasterIndustrySubTypeByID(id);
                if (objMasterIndustrySubTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterIndustrySubTypeInterface.DeleteADMasterIndustrySubType(id);

                return objMasterIndustrySubTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
