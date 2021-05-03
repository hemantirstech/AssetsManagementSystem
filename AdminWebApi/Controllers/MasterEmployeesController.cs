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
    public class MasterEmployeesController : ControllerBase
    {
        private readonly IMasterEmployeeInterface<MasterEmployeeResult> _IMasterEmployeeInterface;

        public MasterEmployeesController(IMasterEmployeeInterface<MasterEmployeeResult> IMasterEmployeeInterface)
        {
            _IMasterEmployeeInterface = IMasterEmployeeInterface;
        }

        // GET: api/MasterCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterEmployeeResult>>> GetADMasterCompanies()
        {
            try
            {
                return _IMasterEmployeeInterface.GetAllADMasterEmployee().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterEmployeeResult>> GetADMasterEmployee(long id)
        {
            try
            {
                var objMasterEmployeeResult = _IMasterEmployeeInterface.GetADMasterEmployeeByID(id);

                if (objMasterEmployeeResult == null)
                {
                    return NotFound();
                }

                return objMasterEmployeeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCompanies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterEmployeeResult>> PutADMasterEmployee(long id, ADMasterEmployee objADMasterEmployee)
        {
            if (id != objADMasterEmployee.MasterEmployeeId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterEmployeeInterface.UpdateADMasterEmployee(objADMasterEmployee);

                //MasterEmployeeResult objMasterEmployeeResult = new MasterEmployeeResult();

                return _IMasterEmployeeInterface.GetADMasterEmployeeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterEmployeeInterface.ADMasterEmployeeExists(id))
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

        // POST: api/MasterCompanies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterEmployeeResult>> PostADMasterEmployee(ADMasterEmployee objADMasterEmployee)
        {
            try
            {
                await _IMasterEmployeeInterface.InsertADMasterEmployee(objADMasterEmployee);

                return CreatedAtAction("GetADMasterEmployee", new { id = objADMasterEmployee.MasterEmployeeId }, objADMasterEmployee);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterEmployeeResult>> DeleteADMasterEmployee(long id)
        {
            try
            {
                var objMasterEmployeeResult = _IMasterEmployeeInterface.GetADMasterEmployeeByID(id);
                if (objMasterEmployeeResult == null)
                {
                    return NotFound();
                }

                await _IMasterEmployeeInterface.DeleteADMasterEmployee(id);

                return objMasterEmployeeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
