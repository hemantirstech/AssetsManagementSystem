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
using Microsoft.Extensions.Logging;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterBranchesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMasterBranchInterface<MasterBranchResult> _IMasterBranchInterface;

        public MasterBranchesController(ILoggerFactory loggerFactory, IMasterBranchInterface<MasterBranchResult> IMasterBranchInterface)
        {
            _IMasterBranchInterface = IMasterBranchInterface;
            _logger = loggerFactory.CreateLogger<MasterBranchesController>();
        }

        // GET: api/MasterBranches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterBranchResult>>> GetADMasterBranch()
        {
            try
            {
                return _IMasterBranchInterface.GetAllADMasterBranch().ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
          
        }

        // GET: api/MasterBranches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterBranchResult>> GetADMasterBranch(long id)
        {
            try
            {
                var objMasterBranchResult = _IMasterBranchInterface.GetADMasterBranchByID(id);

                if (objMasterBranchResult == null)
                {
                    return NotFound();
                }

                return objMasterBranchResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterBranches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterBranchResult>> PutADMasterBranch(long id, ADMasterBranch objADMasterBranch)
        {
            if (id != objADMasterBranch.MasterBranchId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterBranchInterface.UpdateADMasterBranch(objADMasterBranch);

                return _IMasterBranchInterface.GetADMasterBranchByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterBranchInterface.ADMasterBranchExists(id))
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
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);

            }

            return NoContent();
        }

        // POST: api/MasterBranches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterBranchResult>> PostADMasterBranch(ADMasterBranch objADMasterBranch)
        {
            try
            {
                await _IMasterBranchInterface.InsertADMasterBranch(objADMasterBranch);

                return CreatedAtAction("GetADMasterBranch", new { id = objADMasterBranch.MasterBranchId }, objADMasterBranch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterBranches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterBranchResult>> DeleteADMasterBranch(long id)
        {
            try
            {
                var objMasterBranchResult = _IMasterBranchInterface.GetADMasterBranchByID(id);
                if (objMasterBranchResult == null)
                {
                    return NotFound();
                }

                await _IMasterBranchInterface.DeleteADMasterBranch(id);

                return objMasterBranchResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
