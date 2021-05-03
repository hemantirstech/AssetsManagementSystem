using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterAddressTypesController : ControllerBase
    {
        private readonly IMasterAddressTypeInterface<MasterAddressTypeResult> _IMasterAddressTypeInterface;


        public MasterAddressTypesController(IMasterAddressTypeInterface<MasterAddressTypeResult> IMasterAddressTypeInterface)
        {
            _IMasterAddressTypeInterface = IMasterAddressTypeInterface;
        }

        // GET: api/MasterAddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterAddressTypeResult>>> GetADMasterAddressTypes()
        {
            try
            {
                return _IMasterAddressTypeInterface.GetAllADMasterAddressType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterAddressTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterAddressTypeResult>> GetADMasterAddressType(long id)
        {
            try
            {
                var objMasterAddressTypeResult = _IMasterAddressTypeInterface.GetADMasterAddressTypeByID(id);

                if (objMasterAddressTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterAddressTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/GMasterAddressTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterAddressTypeResult>> PutADMasterAddressType(long id, ADMasterAddressType objADMasterAddressType)
        {
            if (id != objADMasterAddressType.MasterAddressTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterAddressTypeInterface.UpdateADMasterAddressType(objADMasterAddressType);

                return _IMasterAddressTypeInterface.GetADMasterAddressTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterAddressTypeInterface.ADMasterAddressTypeExists(id))
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

        // POST: api/GenCodeMasterss
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterAddressTypeResult>> PostADMasterAddressType(ADMasterAddressType objADMasterAddressType)
        {
            try
            {
                await _IMasterAddressTypeInterface.InsertADMasterAddressType(objADMasterAddressType);

                return CreatedAtAction("GetADMasterAddressType", new { id = objADMasterAddressType.MasterAddressTypeId }, objADMasterAddressType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/GenCodeMasterss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterAddressTypeResult>> DeleteADMasterAddressType(long id)
        {
            try
            {
                var objMasterAddressTypeResult = _IMasterAddressTypeInterface.GetADMasterAddressTypeByID(id);
                if (objMasterAddressTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterAddressTypeInterface.DeleteADMasterAddressType(id);

                return objMasterAddressTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
