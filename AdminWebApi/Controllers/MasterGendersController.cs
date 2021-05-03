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
    public class MasterGendersController : ControllerBase
    {
        private readonly IMasterGenderInterface<MasterGenderResult> _IMasterGenderInterface;

        public MasterGendersController(IMasterGenderInterface<MasterGenderResult> IMasterGenderInterface)
        {
            _IMasterGenderInterface = IMasterGenderInterface;
        }

        // GET: api/MasterGenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterGenderResult>>> GetADMasterGenders()
        {
            try
            {
                return _IMasterGenderInterface.GetAllADMasterGender().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterGenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterGenderResult>> GetADMasterGender(long id)
        {
            try
            {
                var objMasterGenderResult = _IMasterGenderInterface.GetADMasterGenderByID(id);

                if (objMasterGenderResult == null)
                {
                    return NotFound();
                }

                return objMasterGenderResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterGenders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterGenderResult>> PutADMasterGender(long id, ADMasterGender objADMasterGender)
        {
            if (id != objADMasterGender.MasterGenderId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterGenderInterface.UpdateADMasterGender(objADMasterGender);

                return _IMasterGenderInterface.GetADMasterGenderByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterGenderInterface.ADMasterGenderExists(id))
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

        // POST: api/MasterGenders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterGenderResult>> PostADMasterGender(ADMasterGender objADMasterGender)
        {
            try
            {
                await _IMasterGenderInterface.InsertADMasterGender(objADMasterGender);

                return CreatedAtAction("GetADMasterGender", new { id = objADMasterGender.MasterGenderId }, objADMasterGender);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterGenders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterGenderResult>> DeleteADMasterGender(long id)
        {
            try
            {
                var objMasterGenderResult = _IMasterGenderInterface.GetADMasterGenderByID(id);
                if (objMasterGenderResult == null)
                {
                    return NotFound();
                }

                await _IMasterGenderInterface.DeleteADMasterGender(id);

                return objMasterGenderResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
