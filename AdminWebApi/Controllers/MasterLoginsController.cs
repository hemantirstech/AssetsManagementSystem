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
    public class MasterLoginsController : ControllerBase
    {
        private readonly IMasterLoginInterface<MasterLoginResult> _IMasterLoginInterface;

        public MasterLoginsController(IMasterLoginInterface<MasterLoginResult> IMasterLoginInterface)
        {
            _IMasterLoginInterface = IMasterLoginInterface;
        }

        // GET: api/MasterLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterLoginResult>>> GetADMasterLogins()
        {
            try
            {
                return _IMasterLoginInterface.GetAllADMasterLogin().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterLoginResult>> GetADMasterLogin(long id)
        {
            try
            {
                var objMasterLoginResult = _IMasterLoginInterface.GetADMasterLoginByID(id);

                if (objMasterLoginResult == null)
                {
                    return NotFound();
                }

                return objMasterLoginResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterLogins/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterLoginResult>> PutADMasterLogin(long id, ADMasterLogin objADMasterLogin)
        {
            if (id != objADMasterLogin.MasterLoginId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterLoginInterface.UpdateADMasterLogin(objADMasterLogin);

                return _IMasterLoginInterface.GetADMasterLoginByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterLoginInterface.ADMasterLoginExists(id))
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

        // POST: api/MasterLogins
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterLoginResult>> PostADMasterLogin(ADMasterLogin objADMasterLogin)
        {
            try
            {
                await _IMasterLoginInterface.InsertADMasterLogin(objADMasterLogin);

                return CreatedAtAction("GetADMasterLogin", new { id = objADMasterLogin.MasterLoginId }, objADMasterLogin);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterLogins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterLoginResult>> DeleteADMasterLogin(long id)
        {
            try
            {
                var objMasterLoginResult = _IMasterLoginInterface.GetADMasterLoginByID(id);
                if (objMasterLoginResult == null)
                {
                    return NotFound();
                }

                await _IMasterLoginInterface.DeleteADMasterLogin(id);

                return objMasterLoginResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
