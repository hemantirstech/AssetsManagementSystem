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
    public class MasterDepartmentsController : ControllerBase
    {
        private readonly IMasterDepartmentInterface<MasterDepartmentResult> _IMasterDepartmentInterface;

        public MasterDepartmentsController(IMasterDepartmentInterface<MasterDepartmentResult> IMasterDepartmentInterface)
        {
            _IMasterDepartmentInterface = IMasterDepartmentInterface;
        }

        // GET: api/MasterDepartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterDepartmentResult>>> GetADMasterDepartments()
        {
            try
            {
                return _IMasterDepartmentInterface.GetAllADMasterDepartment().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterDepartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterDepartmentResult>> GetADMasterDepartment(long id)
        {
            try
            {
                var objMasterDepartmentResult = _IMasterDepartmentInterface.GetADMasterDepartmentByID(id);

                if (objMasterDepartmentResult == null)
                {
                    return NotFound();
                }

                return objMasterDepartmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterDepartments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterDepartmentResult>> PutADMasterDepartment(long id, ADMasterDepartment objADMasterDepartment)
        {
            if (id != objADMasterDepartment.MasterDepartmentId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterDepartmentInterface.UpdateADMasterDepartment(objADMasterDepartment);

                return _IMasterDepartmentInterface.GetADMasterDepartmentByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterDepartmentInterface.ADMasterDepartmentExists(id))
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

        // POST: api/MasterDepartments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterDepartmentResult>> PostADMasterDepartment(ADMasterDepartment objADMasterDepartment)
        {
            try
            {
                await _IMasterDepartmentInterface.InsertADMasterDepartment(objADMasterDepartment);

                return CreatedAtAction("GetADMasterDepartment", new { id = objADMasterDepartment.MasterDepartmentId }, objADMasterDepartment);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterDepartments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterDepartmentResult>> DeleteADMasterDepartment(long id)
        {
            try
            {
                var objMasterDepartmentResult = _IMasterDepartmentInterface.GetADMasterDepartmentByID(id);
                if (objMasterDepartmentResult == null)
                {
                    return NotFound();
                }

                await _IMasterDepartmentInterface.DeleteADMasterDepartment(id);

                return objMasterDepartmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
