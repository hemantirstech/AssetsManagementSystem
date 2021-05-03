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
    public class MasterLoginTypesController : ControllerBase
    {
        private readonly IMasterLoginTypeInterface<MasterLoginTypeResult> _IMasterLoginTypeInterface;

        public MasterLoginTypesController(IMasterLoginTypeInterface<MasterLoginTypeResult> IMasterLoginTypeInterface)
        {
            _IMasterLoginTypeInterface = IMasterLoginTypeInterface;
        }

        // GET: api/MasterLoginTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterLoginTypeResult>>> GetADMasterLoginTypes()
        {
            try
            {
                return _IMasterLoginTypeInterface.GetAllADMasterLoginType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterLoginTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterLoginTypeResult>> GetADMasterLoginType(long id)
        {
            try
            {
                var objMasterLoginTypeResult = _IMasterLoginTypeInterface.GetADMasterLoginTypeByID(id);

                if (objMasterLoginTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterLoginTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterLoginTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterLoginTypeResult>> PutADMasterLoginType(long id, ADMasterLoginType objADMasterLoginType)
        {
            if (id != objADMasterLoginType.MasterLoginTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterLoginTypeInterface.UpdateADMasterLoginType(objADMasterLoginType);

                return _IMasterLoginTypeInterface.GetADMasterLoginTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterLoginTypeInterface.ADMasterLoginTypeExists(id))
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

        // POST: api/MasterLoginTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterLoginTypeResult>> PostADMasterLoginType(ADMasterLoginType objADMasterLoginType)
        {
            try
            {
                await _IMasterLoginTypeInterface.InsertADMasterLoginType(objADMasterLoginType);

                return CreatedAtAction("GetADMasterLoginType", new { id = objADMasterLoginType.MasterLoginTypeId }, objADMasterLoginType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterLoginTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterLoginTypeResult>> DeleteADMasterLoginType(long id)
        {
            try
            {
                var objMasterLoginTypeResult = _IMasterLoginTypeInterface.GetADMasterLoginTypeByID(id);
                if (objMasterLoginTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterLoginTypeInterface.DeleteADMasterLoginType(id);

                return objMasterLoginTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
