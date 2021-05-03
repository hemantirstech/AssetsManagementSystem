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
    public class MasterVendorController : ControllerBase
    {
        private readonly IMasterVendorInterface<MasterVendorResult> _IMasterVendorInterface;


        public MasterVendorController(IMasterVendorInterface<MasterVendorResult> IMasterVendorInterface)
        {
            _IMasterVendorInterface = IMasterVendorInterface;
        }

        // GET: api/MasterVendor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterVendorResult>>> GetADMasterVendors()
        {
            try
            {
                return _IMasterVendorInterface.GetAllADMasterVendor().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterVendor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterVendorResult>> GetADMasterVendor(long id)
        {
            try
            {
                var objMasterVendorResult = _IMasterVendorInterface.GetADMasterVendorByID(id);

                if (objMasterVendorResult == null)
                {
                    return NotFound();
                }

                return objMasterVendorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterVendor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterVendorResult>> PutADMasterVendor(long id, ADMasterVendor objADMasterVendor)
        {
            if (id != objADMasterVendor.MasterVendorId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterVendorInterface.UpdateADMasterVendor(objADMasterVendor);

                return _IMasterVendorInterface.GetADMasterVendorByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterVendorInterface.ADMasterVendorExists(id))
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

        // POST: api/MasterVendor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterVendorResult>> PostADMasterVendor(ADMasterVendor objADMasterVendor)
        {
            try
            {
                await _IMasterVendorInterface.InsertADMasterVendor(objADMasterVendor);

                return CreatedAtAction("GetADMasterVendor", new { id = objADMasterVendor.MasterVendorId }, objADMasterVendor);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterVendor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterVendorResult>> DeleteADMasterVendor(long id)
        {
            try
            {
                var objMasterVendorResult = _IMasterVendorInterface.GetADMasterVendorByID(id);
                if (objMasterVendorResult == null)
                {
                    return NotFound();
                }

                await _IMasterVendorInterface.DeleteADMasterVendor(id);

                return objMasterVendorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
