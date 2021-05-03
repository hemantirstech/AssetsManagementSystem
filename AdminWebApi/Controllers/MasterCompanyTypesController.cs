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
    public class MasterCompanyTypesController : ControllerBase
    {
        private readonly IMasterCompanyTypeInterface<MasterCompanyTypeResult> _IMasterCompanyTypeInterface;

        public MasterCompanyTypesController(IMasterCompanyTypeInterface<MasterCompanyTypeResult> IMasterCompanyTypeInterface)
        {
            _IMasterCompanyTypeInterface = IMasterCompanyTypeInterface;
        }

        // GET: api/MasterCompanyTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCompanyTypeResult>>> GetADMasterCompanyTypes()
        {
            try
            {
                return _IMasterCompanyTypeInterface.GetAllADMasterCompanyType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCompanyTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCompanyTypeResult>> GetADMasterCompanyType(long id)
        {
            try
            {
                var objMasterCompanyTypeResult = _IMasterCompanyTypeInterface.GetADMasterCompanyTypeByID(id);

                if (objMasterCompanyTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterCompanyTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCompanyTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCompanyTypeResult>> PutADMasterCompanyType(long id, ADMasterCompanyType objADMasterCompanyType)
        {
            if (id != objADMasterCompanyType.MasterCompanyTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterCompanyTypeInterface.UpdateADMasterCompanyType(objADMasterCompanyType);

                return _IMasterCompanyTypeInterface.GetADMasterCompanyTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterCompanyTypeInterface.ADMasterCompanyTypeExists(id))
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

        // POST: api/MasterCompanyTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCompanyTypeResult>> PostADMasterCompanyType(ADMasterCompanyType objADMasterCompanyType)
        {
            try
            {
                await _IMasterCompanyTypeInterface.InsertADMasterCompanyType(objADMasterCompanyType);

                return CreatedAtAction("GetADMasterCompanyType", new { id = objADMasterCompanyType.MasterCompanyTypeId }, objADMasterCompanyType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCompanyTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCompanyTypeResult>> DeleteADMasterCompanyType(long id)
        {
            try
            {
                var objMasterCompanyTypeResult = _IMasterCompanyTypeInterface.GetADMasterCompanyTypeByID(id);
                if (objMasterCompanyTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterCompanyTypeInterface.DeleteADMasterCompanyType(id);

                return objMasterCompanyTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
