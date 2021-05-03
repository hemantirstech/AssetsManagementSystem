using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProductTypeController : ControllerBase
    {
        private readonly IMasterProductTypeInterface<MasterProductTypeResult> _IMasterProductTypeInterface;


        public MasterProductTypeController(IMasterProductTypeInterface<MasterProductTypeResult> IMasterProductTypeInterface)
        {
            _IMasterProductTypeInterface = IMasterProductTypeInterface;
        }


        // GET: api/MasterProductType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductTypeResult>>> GetASMasterProductTypes()
        {
            try
            {
                return _IMasterProductTypeInterface.GetAllASMasterProductType().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProductType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterProductTypeResult>> GetASMasterProductType(long id)
        {
            try
            {
                var objMasterProductTypeResult = _IMasterProductTypeInterface.GetASMasterProductTypeByID(id);

                if (objMasterProductTypeResult == null)
                {
                    return NotFound();
                }

                return objMasterProductTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterProductType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterProductTypeResult>> PutASMasterProductType(long id, ASMasterProductType objADMasterProductType)
        {
            if (id != objADMasterProductType.MasterProductTypeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterProductTypeInterface.UpdateASMasterProductType(objADMasterProductType);

                return _IMasterProductTypeInterface.GetASMasterProductTypeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProductTypeInterface.ASMasterProductTypeExists(id))
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

        // POST: api/MasterProductType
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterProductType
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterProductTypeResult>> PostASMasterProductType(ASMasterProductType objADMasterProductType)
        {
            try
            {
                await _IMasterProductTypeInterface.InsertASMasterProductType(objADMasterProductType);

                return CreatedAtAction("GetASMasterProductType", new { id = objADMasterProductType.MasterProductTypeId }, objADMasterProductType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterProductType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterProductTypeResult>> DeleteASMasterProductType(long id)
        {
            try
            {
                var objMasterProductTypeResult = _IMasterProductTypeInterface.GetASMasterProductTypeByID(id);
                if (objMasterProductTypeResult == null)
                {
                    return NotFound();
                }

                await _IMasterProductTypeInterface.DeleteASMasterProductType(id);

                return objMasterProductTypeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
