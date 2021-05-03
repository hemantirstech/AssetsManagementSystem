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
    public class MasterPaymentTypesController : ControllerBase
    {
        private readonly IMasterPaymentTypeInterface<MasterPaymentTypeResult> _IMasterPaymentTypeInterface;

        public MasterPaymentTypesController(IMasterPaymentTypeInterface<MasterPaymentTypeResult> IMasterPaymentTypeInterface)
        {
            _IMasterPaymentTypeInterface = IMasterPaymentTypeInterface;
        }

        // GET: api/MasterPaymentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterPaymentTypeResult>>> GetADMasterPaymentTypes()
        {
            try
            {
                return _IMasterPaymentTypeInterface.GetAllADMasterPaymentType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterPaymentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterPaymentTypeResult>> GetADMasterPaymentType(long id)
        {
            try
            {
                var objMasterPaymentTypeResult = _IMasterPaymentTypeInterface.GetADMasterPaymentTypeByID(id);

                if (objMasterPaymentTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterPaymentTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterPaymentTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterPaymentTypeResult>> PutADMasterPaymentType(long id, ADMasterPaymentType objADMasterPaymentType)
        {
            if (id != objADMasterPaymentType.MasterPaymentTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterPaymentTypeInterface.UpdateADMasterPaymentType(objADMasterPaymentType);

                return _IMasterPaymentTypeInterface.GetADMasterPaymentTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterPaymentTypeInterface.ADMasterPaymentTypeExists(id))
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

        // POST: api/MasterPaymentTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterPaymentTypeResult>> PostADMasterPaymentType(ADMasterPaymentType objADMasterPaymentType)
        {
            try
            {
                await _IMasterPaymentTypeInterface.InsertADMasterPaymentType(objADMasterPaymentType);

                return CreatedAtAction("GetADMasterPaymentType", new { id = objADMasterPaymentType.MasterPaymentTypeId }, objADMasterPaymentType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterPaymentTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterPaymentTypeResult>> DeleteADMasterPaymentType(long id)
        {
            try
            {
                var objMasterPaymentTypeResult = _IMasterPaymentTypeInterface.GetADMasterPaymentTypeByID(id);
                if (objMasterPaymentTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterPaymentTypeInterface.DeleteADMasterPaymentType(id);

                return objMasterPaymentTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
