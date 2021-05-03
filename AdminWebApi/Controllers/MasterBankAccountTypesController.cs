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
    public class MasterBankAccountTypesController : ControllerBase
    {
        private readonly IMasterBankAccountTypeInterface<MasterBankAccountTypeResult> _IMasterBankAccountTypeInterface;

        public MasterBankAccountTypesController(IMasterBankAccountTypeInterface<MasterBankAccountTypeResult> IMasterBankAccountTypeInterface)
        {
            _IMasterBankAccountTypeInterface = IMasterBankAccountTypeInterface;
        }

        // GET: api/MasterBankAccountTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterBankAccountTypeResult>>> GetADMasterBankAccountTypes()
        {
            try
            {
                return _IMasterBankAccountTypeInterface.GetAllADMasterBankAccountType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterBankAccountTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterBankAccountTypeResult>> GetADMasterBankAccountType(long id)
        {
            try
            {
                var objMasterBankAccountTypeResult = _IMasterBankAccountTypeInterface.GetADMasterBankAccountTypeByID(id);

                if (objMasterBankAccountTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterBankAccountTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterBankAccountTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterBankAccountTypeResult>> PutADMasterBankAccountType(long id, ADMasterBankAccountType objADMasterBankAccountType)
        {
            if (id != objADMasterBankAccountType.MasterBankAccountTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterBankAccountTypeInterface.UpdateADMasterBankAccountType(objADMasterBankAccountType);

                return _IMasterBankAccountTypeInterface.GetADMasterBankAccountTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterBankAccountTypeInterface.ADMasterBankAccountTypeExists(id))
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

        // POST: api/MasterBankAccountTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterBankAccountTypeResult>> PostADMasterBankAccountType(ADMasterBankAccountType objADMasterBankAccountType)
        {
            try
            {
                await _IMasterBankAccountTypeInterface.InsertADMasterBankAccountType(objADMasterBankAccountType);

                return CreatedAtAction("GetADMasterBankAccountType", new { id = objADMasterBankAccountType.MasterBankAccountTypeId }, objADMasterBankAccountType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterBankAccountTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterBankAccountTypeResult>> DeleteADMasterBankAccountType(long id)
        {
            try
            {
                var objMasterBankAccountTypeResult = _IMasterBankAccountTypeInterface.GetADMasterBankAccountTypeByID(id);
                if (objMasterBankAccountTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterBankAccountTypeInterface.DeleteADMasterBankAccountType(id);

                return objMasterBankAccountTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
