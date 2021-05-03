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
    public class MasterErrorLogsController : ControllerBase
    {
        private readonly IMasterErrorLogInterface<MasterErrorLogResult> _IMasterErrorLogInterface;

        public MasterErrorLogsController(IMasterErrorLogInterface<MasterErrorLogResult> IMasterErrorLogInterface)
        {
            _IMasterErrorLogInterface = IMasterErrorLogInterface;
        }

        // GET: api/MasterErrorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterErrorLogResult>>> GetADMasterErrorLogs()
        {
            try
            {
                return _IMasterErrorLogInterface.GetAllADMasterErrorLog().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterErrorLogResult>> GetADMasterErrorLog(long id)
        {
            try
            {
                var objMasterErrorLogResult = _IMasterErrorLogInterface.GetADMasterErrorLogByID(id);

                if (objMasterErrorLogResult == null)
                {
                    return NotFound();
                }

                return objMasterErrorLogResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterErrorLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterErrorLogResult>> PutADMasterErrorLog(long id, ADMasterErrorLog objADMasterErrorLog)
        {
            if (id != objADMasterErrorLog.MasterErrorLogId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterErrorLogInterface.UpdateADMasterErrorLog(objADMasterErrorLog);

                return _IMasterErrorLogInterface.GetADMasterErrorLogByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterErrorLogInterface.ADMasterErrorLogExists(id))
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

        // POST: api/MasterErrorLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterErrorLogResult>> PostADMasterErrorLog(ADMasterErrorLog objADMasterErrorLog)
        {
            try
            {
                await _IMasterErrorLogInterface.InsertADMasterErrorLog(objADMasterErrorLog);

                return CreatedAtAction("GetADMasterErrorLog", new { id = objADMasterErrorLog.MasterErrorLogId }, objADMasterErrorLog);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterErrorLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterErrorLogResult>> DeleteADMasterErrorLog(long id)
        {
            try
            {
                var objMasterErrorLogResult = _IMasterErrorLogInterface.GetADMasterErrorLogByID(id);
                if (objMasterErrorLogResult == null)
                {
                    return NotFound();
                }

                await _IMasterErrorLogInterface.DeleteADMasterErrorLog(id);

                return objMasterErrorLogResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
