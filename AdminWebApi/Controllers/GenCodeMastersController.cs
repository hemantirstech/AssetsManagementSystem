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
    public class GenCodeMastersController : ControllerBase
    {
        private readonly IGenCodeMasterInterface<GenCodeMasterResult> _IGenCodeMastersInterface;


        public GenCodeMastersController(IGenCodeMasterInterface<GenCodeMasterResult> IGenCodeMastersInterface)
        {
            _IGenCodeMastersInterface = IGenCodeMastersInterface;
        }

        // GET: api/GenCodeMasterss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenCodeMasterResult>>> GetADGenCodeMasters()
        {
            try
            {
                return _IGenCodeMastersInterface.GetAllADGenCodeMaster().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/GenCodeMasterss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenCodeMasterResult>> GetADGenCodeMaster(long id)
        {
            try
            {
                var objGenCodeMasterResult = _IGenCodeMastersInterface.GetADGenCodeMasterByID(id);

                if (objGenCodeMasterResult == null)
                {
                    return NotFound();
                }

                return objGenCodeMasterResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/GenCodeMasterss/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<GenCodeMasterResult>> PutADGenCodeMasters(long id, ADGenCodeMaster objADGenCodeMasters)
        {
            if (id != objADGenCodeMasters.GenCodeMasterId)
            {
                return BadRequest();
            }


            try
            {
                await _IGenCodeMastersInterface.UpdateADGenCodeMaster(objADGenCodeMasters);

                return _IGenCodeMastersInterface.GetADGenCodeMasterByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IGenCodeMastersInterface.ADGenCodeMasterExists(id))
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

        // POST: api/GenCodeMasterss
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GenCodeMasterResult>> PostADGenCodeMasters(ADGenCodeMaster objADGenCodeMasters)
        {
            try
            {
                await _IGenCodeMastersInterface.InsertADGenCodeMaster(objADGenCodeMasters);

                return CreatedAtAction("GetADGenCodeMasters", new { id = objADGenCodeMasters.GenCodeMasterId }, objADGenCodeMasters);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/GenCodeMasterss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenCodeMasterResult>> DeleteADGenCodeMaster(long id)
        {
            try
            {
                var objGenCodeMasterResult = _IGenCodeMastersInterface.GetADGenCodeMasterByID(id);
                if (objGenCodeMasterResult == null)
                {
                    return NotFound();
                }

                await _IGenCodeMastersInterface.DeleteADGenCodeMaster(id);

                return objGenCodeMasterResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
