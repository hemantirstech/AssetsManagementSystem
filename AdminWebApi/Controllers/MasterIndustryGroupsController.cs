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
    public class MasterIndustryGroupsController : ControllerBase
    {
        private readonly IMasterIndustryGroupInterface<MasterIndustryGroupResult> _IMasterIndustryGroupInterface;

        public MasterIndustryGroupsController(IMasterIndustryGroupInterface<MasterIndustryGroupResult> IMasterIndustryGroupInterface)
        {
            _IMasterIndustryGroupInterface = IMasterIndustryGroupInterface;
        }

        // GET: api/MasterIndustryGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterIndustryGroupResult>>> GetADMasterIndustryGroups()
        {
            try
            {
                return _IMasterIndustryGroupInterface.GetAllADMasterIndustryGroup().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterIndustryGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterIndustryGroupResult>> GetADMasterIndustryGroup(long id)
        {
            try
            {
                var objMasterIndustryGroupResult = _IMasterIndustryGroupInterface.GetADMasterIndustryGroupByID(id);

                if (objMasterIndustryGroupResult == null)
                {
                    return NotFound();
                }

                return objMasterIndustryGroupResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterIndustryGroups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterIndustryGroupResult>> PutADMasterIndustryGroup(long id, ADMasterIndustryGroup objADMasterIndustryGroup)
        {
            if (id != objADMasterIndustryGroup.MasterIndustryGroupId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterIndustryGroupInterface.UpdateADMasterIndustryGroup(objADMasterIndustryGroup);

                return _IMasterIndustryGroupInterface.GetADMasterIndustryGroupByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterIndustryGroupInterface.ADMasterIndustryGroupExists(id))
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

        // POST: api/MasterIndustryGroups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterIndustryGroupResult>> PostADMasterIndustryGroup(ADMasterIndustryGroup objADMasterIndustryGroup)
        {
            try
            {
                await _IMasterIndustryGroupInterface.InsertADMasterIndustryGroup(objADMasterIndustryGroup);

                return CreatedAtAction("GetADMasterIndustryGroup", new { id = objADMasterIndustryGroup.MasterIndustryGroupId }, objADMasterIndustryGroup);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterIndustryGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterIndustryGroupResult>> DeleteADMasterIndustryGroup(long id)
        {
            try
            {
                var objMasterIndustryGroupResult = _IMasterIndustryGroupInterface.GetADMasterIndustryGroupByID(id);
                if (objMasterIndustryGroupResult == null)
                {
                    return NotFound();
                }

                await _IMasterIndustryGroupInterface.DeleteADMasterIndustryGroup(id);

                return objMasterIndustryGroupResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
