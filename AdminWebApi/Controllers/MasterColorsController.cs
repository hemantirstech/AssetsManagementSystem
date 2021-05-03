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
    public class MasterColorsController : ControllerBase
    {
        private readonly IMasterColorInterface<MasterColorResult> _IMasterColorInterface;

        public MasterColorsController(IMasterColorInterface<MasterColorResult> IMasterColorInterface)
        {
            _IMasterColorInterface = IMasterColorInterface;
        }

        // GET: api/MasterColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterColorResult>>> GetADMasterColors()
        {
            try
            {
                return _IMasterColorInterface.GetAllADMasterColor().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterColorResult>> GetADMasterColor(long id)
        {
            try
            {
                var objMasterColorResult = _IMasterColorInterface.GetADMasterColorByID(id);

                if (objMasterColorResult == null)
                {
                    return NotFound();
                }

                return objMasterColorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterColors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterColorResult>> PutADMasterColor(long id, ADMasterColor objADMasterColor)
        {
            if (id != objADMasterColor.MasterColorId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterColorInterface.UpdateADMasterColor(objADMasterColor);

                return _IMasterColorInterface.GetADMasterColorByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterColorInterface.ADMasterColorExists(id))
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

        // POST: api/MasterColors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterColorResult>> PostADMasterColor(ADMasterColor objADMasterColor)
        {
            try
            {
                await _IMasterColorInterface.InsertADMasterColor(objADMasterColor);

                return CreatedAtAction("GetADMasterColor", new { id = objADMasterColor.MasterColorId }, objADMasterColor);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterColors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterColorResult>> DeleteADMasterColor(long id)
        {
            try
            {
                var objMasterColorResult = _IMasterColorInterface.GetADMasterColorByID(id);
                if (objMasterColorResult == null)
                {
                    return NotFound();
                }

                await _IMasterColorInterface.DeleteADMasterColor(id);

                return objMasterColorResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
