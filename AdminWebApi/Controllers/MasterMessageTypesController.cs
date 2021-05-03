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
    public class MasterMessageTypesController : ControllerBase
    {
        private readonly IMasterMessageTypeInterface<MasterMessageTypeResult> _IMasterMessageTypeInterface;

        public MasterMessageTypesController(IMasterMessageTypeInterface<MasterMessageTypeResult> IMasterMessageTypeInterface)
        {
            _IMasterMessageTypeInterface = IMasterMessageTypeInterface;
        }

        // GET: api/MasterMessageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterMessageTypeResult>>> GetADMasterMessageTypes()
        {
            try
            {
                return _IMasterMessageTypeInterface.GetAllADMasterMessageType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterMessageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterMessageTypeResult>> GetADMasterMessageType(long id)
        {
            try
            {
                var objMasterMessageTypeResult = _IMasterMessageTypeInterface.GetADMasterMessageTypeByID(id);

                if (objMasterMessageTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterMessageTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterMessageTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterMessageTypeResult>> PutADMasterMessageType(long id, ADMasterMessageType objADMasterMessageType)
        {
            if (id != objADMasterMessageType.MasterMessageTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterMessageTypeInterface.UpdateADMasterMessageType(objADMasterMessageType);

                return _IMasterMessageTypeInterface.GetADMasterMessageTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterMessageTypeInterface.ADMasterMessageTypeExists(id))
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

        // POST: api/MasterMessageTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterMessageTypeResult>> PostADMasterMessageType(ADMasterMessageType objADMasterMessageType)
        {
            try
            {
                await _IMasterMessageTypeInterface.InsertADMasterMessageType(objADMasterMessageType);

                return CreatedAtAction("GetADMasterMessageType", new { id = objADMasterMessageType.MasterMessageTypeId }, objADMasterMessageType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterMessageTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterMessageTypeResult>> DeleteADMasterMessageType(long id)
        {
            try
            {
                var objMasterMessageTypeResult = _IMasterMessageTypeInterface.GetADMasterMessageTypeByID(id);
                if (objMasterMessageTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterMessageTypeInterface.DeleteADMasterMessageType(id);

                return objMasterMessageTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
