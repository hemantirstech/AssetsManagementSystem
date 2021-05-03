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
    public class MasterStatusController : ControllerBase
    {
        private readonly IMasterStatusInterface<MasterStatusResult> _IMasterStatusInterface;

        public MasterStatusController(IMasterStatusInterface<MasterStatusResult> IMasterStatusInterface)
        {
            _IMasterStatusInterface = IMasterStatusInterface;
        }

        // GET: api/MasterStatuss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterStatusResult>>> GetADMasterStatuss()
        {
            try
            {
                return _IMasterStatusInterface.GetAllADMasterStatus().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterStatuss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterStatusResult>> GetADMasterStatus(long id)
        {
            try
            {
                var objMasterStatusResult = _IMasterStatusInterface.GetADMasterStatusByID(id);

                if (objMasterStatusResult == null)
                {
                    return NotFound();
                }

                return objMasterStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterStatuss/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterStatusResult>> PutADMasterStatus(long id, ADMasterStatus objADMasterStatus)
        {
            if (id != objADMasterStatus.MasterStatusId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterStatusInterface.UpdateADMasterStatus(objADMasterStatus);

                return _IMasterStatusInterface.GetADMasterStatusByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterStatusInterface.ADMasterStatusExists(id))
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

        // POST: api/MasterStatuss
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterStatusResult>> PostADMasterStatus(ADMasterStatus objADMasterStatus)
        {
            try
            {
                await _IMasterStatusInterface.InsertADMasterStatus(objADMasterStatus);

                return CreatedAtAction("GetADMasterStatus", new { id = objADMasterStatus.MasterStatusId }, objADMasterStatus);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterStatuss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterStatusResult>> DeleteADMasterStatus(long id)
        {
            try
            {
                var objMasterStatusResult = _IMasterStatusInterface.GetADMasterStatusByID(id);
                if (objMasterStatusResult == null)
                {
                    return NotFound();
                }

                await _IMasterStatusInterface.DeleteADMasterStatus(id);

                return objMasterStatusResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
