using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GenCodeTypeController : ControllerBase
    {
        private readonly IGenCodeTypeInterface<GenCodeTypeResult> _IGenCodeTypeInterface;


        public GenCodeTypeController(IGenCodeTypeInterface<GenCodeTypeResult> IGenCodeTypeInterface)
        {
            _IGenCodeTypeInterface = IGenCodeTypeInterface;
        }

        // GET: api/GenCodeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenCodeTypeResult>>> GetADGenCodeType()
        {
            try
            {
                return _IGenCodeTypeInterface.GetAllADGenCodeType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/GenCodeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenCodeTypeResult>> GetADGenCodeType(long id)
        {
            try
            {
                var objGenCodeTypeResult = _IGenCodeTypeInterface.GetADGenCodeTypeByID(id);

                if (objGenCodeTypeResult == null)
                {
                    return NotFound();
                }

                return objGenCodeTypeResult;
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
        public async Task<ActionResult<GenCodeTypeResult>> PutADGenCodeType(long id, ADGenCodeType objADGenCodeType)
        {
            if (id != objADGenCodeType.GenCodeTypeId)
            {
                return BadRequest();
            }

            try
            {
                await _IGenCodeTypeInterface.UpdateADGenCodeType(objADGenCodeType);

                return _IGenCodeTypeInterface.GetADGenCodeTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IGenCodeTypeInterface.ADGenCodeTypeExists(id))
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
        public async Task<ActionResult<GenCodeTypeResult>> PostADGenCodeType(ADGenCodeType objADGenCodeType)
        {
            try
            {
                await _IGenCodeTypeInterface.InsertADGenCodeType(objADGenCodeType);

                return CreatedAtAction("GetADGenCodeType", new { id = objADGenCodeType.GenCodeTypeId }, objADGenCodeType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/GenCodeMasterss/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenCodeTypeResult>> DeleteADGenCodeType(long id)
        {
            try
            {
                var objGenCodeTypeResult = _IGenCodeTypeInterface.GetADGenCodeTypeByID(id);
                if (objGenCodeTypeResult == null)
                {
                    return NotFound();
                }

                await _IGenCodeTypeInterface.DeleteADGenCodeType(id);

                return objGenCodeTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
