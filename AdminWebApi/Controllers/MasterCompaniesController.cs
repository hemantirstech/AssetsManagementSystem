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
    public class MasterCompaniesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMasterCompanyInterface<MasterCompanyResult> _IMasterCompanyInterface;

        public MasterCompaniesController(ILoggerFactory loggerFactory, IMasterCompanyInterface<MasterCompanyResult> IMasterCompanyInterface)
        {
            _logger = loggerFactory.CreateLogger<MasterCompaniesController>();
            _IMasterCompanyInterface = IMasterCompanyInterface;
        }

        // GET: api/MasterCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCompanyResult>>> GetADMasterCompanies()
        {
            try
            {
                return _IMasterCompanyInterface.GetAllADMasterCompany().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                throw new Exception(ex.Message);
            }

        }

        // GET: api/MasterCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCompanyResult>> GetADMasterCompany(long id)
        {
            try
            {
                var objMasterCompanyResult = _IMasterCompanyInterface.GetADMasterCompanyByID(id);

                if (objMasterCompanyResult == null)
                {
                    return NotFound();
                }

                return objMasterCompanyResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCompanyResult>> PutADMasterCompany(long id, ADMasterCompany objADMasterCompany)
        {
            if (id != objADMasterCompany.MasterCompanyId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterCompanyInterface.UpdateADMasterCompany(objADMasterCompany);
                return _IMasterCompanyInterface.GetADMasterCompanyByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterCompanyInterface.ADMasterCompanyExists(id))
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

        // POST: api/MasterCompanies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCompanyResult>> PostADMasterCompany(ADMasterCompany objADMasterCompany)
        {
            try
            {
                await _IMasterCompanyInterface.InsertADMasterCompany(objADMasterCompany);

                return CreatedAtAction("GetADMasterCompany", new { id = objADMasterCompany.MasterCompanyId }, objADMasterCompany);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCompanyResult>> DeleteADMasterCompany(long id)
        {
            try
            {
                var objMasterCompanyResult = _IMasterCompanyInterface.GetADMasterCompanyByID(id);
                if (objMasterCompanyResult == null)
                {
                    return NotFound();
                }

                await _IMasterCompanyInterface.DeleteADMasterCompany(id);

                return objMasterCompanyResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
