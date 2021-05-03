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
    public class MasterProfilesController : ControllerBase
    {

        private readonly IMasterProfileInterface<MasterProfileResult> _IMasterProfileInterface;

        public MasterProfilesController(IMasterProfileInterface<MasterProfileResult> IMasterProfileInterface)
        {
            _IMasterProfileInterface = IMasterProfileInterface;
        }


        // GET: api/MasterProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProfileResult>>> GetADMasterProfiles()
        {
            try
            {
                return _IMasterProfileInterface.GetAllADMasterProfile().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterProfileResult>> GetADMasterProfile(long id)
        {
            try
            {
                var objADMasterProfile = _IMasterProfileInterface.GetADMasterProfileByID(id);

                if (objADMasterProfile == null)
                {
                    return NotFound();
                }

                return objADMasterProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterProfiles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterProfileResult>> PutADMasterProfile(long id, ADMasterProfile objADMasterProfile)
        {
            if (id != objADMasterProfile.MasterProfileId)
            {
                return BadRequest();
            }
                       

            try
            {
                await _IMasterProfileInterface.UpdateADMasterProfile(objADMasterProfile);
                return _IMasterProfileInterface.GetADMasterProfileByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProfileInterface.ADMasterProfileExists(id))
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

        // POST: api/MasterProfiles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterProfileResult>> PostADMasterProfile(ADMasterProfile objADMasterProfile)
        {
            try
            {
                await _IMasterProfileInterface.InsertADMasterProfile(objADMasterProfile);

                return CreatedAtAction("GetADMasterProfile", new { id = objADMasterProfile.MasterProfileId }, objADMasterProfile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterProfiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterProfileResult>> DeleteADMasterProfile(long id)
        {
            try
            {
                var objMasterProfileResult = _IMasterProfileInterface.GetADMasterProfileByID(id);
                if (objMasterProfileResult == null)
                {
                    return NotFound();
                }

                await _IMasterProfileInterface.DeleteADMasterProfile(id);

                return objMasterProfileResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
