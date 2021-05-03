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
    public class MasterEmployeeStatusController : ControllerBase
    {
        private readonly IMasterEmployeeStatusInterface<MasterEmployeeStatusResult> _IMasterEmployeeStatusInterface;

        public MasterEmployeeStatusController(IMasterEmployeeStatusInterface<MasterEmployeeStatusResult> IMasterEmployeeStatusInterface)
        {
            _IMasterEmployeeStatusInterface = IMasterEmployeeStatusInterface;
        }

        // GET: api/MasterEmployeeStatuss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterEmployeeStatusResult>>> GetADMasterEmployeeStatuss()
        {
            try
            {
                return _IMasterEmployeeStatusInterface.GetAllADMasterEmployeeStatus().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterEmployeeStatuss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterEmployeeStatusResult>> GetADMasterEmployeeStatus(long id)
        {
            try
            {
                var objMasterEmployeeStatusResult = _IMasterEmployeeStatusInterface.GetADMasterEmployeeStatusByID(id);

                if (objMasterEmployeeStatusResult == null)
                {
                    return NotFound();
                }

                return objMasterEmployeeStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterEmployeeStatuss/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterEmployeeStatusResult>> PutADMasterEmployeeStatus(long id, ADMasterEmployeeStatus objADMasterEmployeeStatus)
        {
            if (id != objADMasterEmployeeStatus.MasterEmployeeStatusId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterEmployeeStatusInterface.UpdateADMasterEmployeeStatus(objADMasterEmployeeStatus);

                return _IMasterEmployeeStatusInterface.GetADMasterEmployeeStatusByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterEmployeeStatusInterface.ADMasterEmployeeStatusExists(id))
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

        // POST: api/MasterEmployeeStatuss
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterEmployeeStatusResult>> PostADMasterEmployeeStatus(ADMasterEmployeeStatus objADMasterEmployeeStatus)
        {
            try
            {
                await _IMasterEmployeeStatusInterface.InsertADMasterEmployeeStatus(objADMasterEmployeeStatus);

                return CreatedAtAction("GetADMasterEmployeeStatus", new { id = objADMasterEmployeeStatus.MasterEmployeeStatusId }, objADMasterEmployeeStatus);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterEmployeeStatuss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterEmployeeStatusResult>> DeleteADMasterEmployeeStatus(long id)
        {
            try
            {
                var objMasterEmployeeStatusResult = _IMasterEmployeeStatusInterface.GetADMasterEmployeeStatusByID(id);
                if (objMasterEmployeeStatusResult == null)
                {
                    return NotFound();
                }

                await _IMasterEmployeeStatusInterface.DeleteADMasterEmployeeStatus(id);

                return objMasterEmployeeStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
