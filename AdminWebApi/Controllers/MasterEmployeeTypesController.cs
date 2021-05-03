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
    public class MasterEmployeeTypesController : ControllerBase
    {
        private readonly IMasterEmployeeTypeInterface<MasterEmployeeTypeResult> _IMasterEmployeeTypeInterface;

        public MasterEmployeeTypesController(IMasterEmployeeTypeInterface<MasterEmployeeTypeResult> IMasterEmployeeTypeInterface)
        {
            _IMasterEmployeeTypeInterface = IMasterEmployeeTypeInterface;
        }

        // GET: api/MasterEmployeeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterEmployeeTypeResult>>> GetADMasterEmployeeTypes()
        {
            try
            {
                return _IMasterEmployeeTypeInterface.GetAllADMasterEmployeeType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterEmployeeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterEmployeeTypeResult>> GetADMasterEmployeeType(long id)
        {
            try
            {
                var objMasterEmployeeTypeResult = _IMasterEmployeeTypeInterface.GetADMasterEmployeeTypeByID(id);

                if (objMasterEmployeeTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterEmployeeTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterEmployeeTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterEmployeeTypeResult>> PutADMasterEmployeeType(long id, ADMasterEmployeeType objADMasterEmployeeType)
        {
            if (id != objADMasterEmployeeType.MasterEmployeeTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterEmployeeTypeInterface.UpdateADMasterEmployeeType(objADMasterEmployeeType);

                return _IMasterEmployeeTypeInterface.GetADMasterEmployeeTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterEmployeeTypeInterface.ADMasterEmployeeTypeExists(id))
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

        // POST: api/MasterEmployeeTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterEmployeeTypeResult>> PostADMasterEmployeeType(ADMasterEmployeeType objADMasterEmployeeType)
        {
            try
            {
                await _IMasterEmployeeTypeInterface.InsertADMasterEmployeeType(objADMasterEmployeeType);

                return CreatedAtAction("GetADMasterEmployeeType", new { id = objADMasterEmployeeType.MasterEmployeeTypeId }, objADMasterEmployeeType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterEmployeeTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterEmployeeTypeResult>> DeleteADMasterEmployeeType(long id)
        {
            try
            {
                var objMasterEmployeeTypeResult = _IMasterEmployeeTypeInterface.GetADMasterEmployeeTypeByID(id);
                if (objMasterEmployeeTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterEmployeeTypeInterface.DeleteADMasterEmployeeType(id);

                return objMasterEmployeeTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
