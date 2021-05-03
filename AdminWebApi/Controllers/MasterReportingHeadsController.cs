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
    public class MasterReportingHeadsController : ControllerBase
    {
        private readonly IMasterReportingHeadInterface<MasterReportingHeadResult> _IMasterReportingHeadInterface;

        public MasterReportingHeadsController(IMasterReportingHeadInterface<MasterReportingHeadResult> IMasterReportingHeadInterface)
        {
            _IMasterReportingHeadInterface = IMasterReportingHeadInterface;
        }

        // GET: api/MasterReportingHeads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterReportingHeadResult>>> GetADMasterReportingHeads()
        {
            try
            {
                return _IMasterReportingHeadInterface.GetAllADMasterReportingHead().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterReportingHeads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterReportingHeadResult>> GetADMasterReportingHead(long id)
        {
            try
            {
                var objMasterReportingHeadResult = _IMasterReportingHeadInterface.GetADMasterReportingHeadByID(id);

                if (objMasterReportingHeadResult == null)
                {
                    return NotFound();
                }

                return objMasterReportingHeadResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterReportingHeads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterReportingHeadResult>> PutADMasterReportingHead(long id, ADMasterReportingHead objADMasterReportingHead)
        {
            if (id != objADMasterReportingHead.MasterReportingHeadId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterReportingHeadInterface.UpdateADMasterReportingHead(objADMasterReportingHead);

                return _IMasterReportingHeadInterface.GetADMasterReportingHeadByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterReportingHeadInterface.ADMasterReportingHeadExists(id))
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

        // POST: api/MasterReportingHeads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterReportingHeadResult>> PostADMasterReportingHead(ADMasterReportingHead objADMasterReportingHead)
        {
            try
            {
                await _IMasterReportingHeadInterface.InsertADMasterReportingHead(objADMasterReportingHead);

                return CreatedAtAction("GetADMasterReportingHead", new { id = objADMasterReportingHead.MasterReportingHeadId }, objADMasterReportingHead);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterReportingHeads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterReportingHeadResult>> DeleteADMasterReportingHead(long id)
        {
            try
            {
                var objMasterReportingHeadResult = _IMasterReportingHeadInterface.GetADMasterReportingHeadByID(id);
                if (objMasterReportingHeadResult == null)
                {
                    return NotFound();
                }

                await _IMasterReportingHeadInterface.DeleteADMasterReportingHead(id);

                return objMasterReportingHeadResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
