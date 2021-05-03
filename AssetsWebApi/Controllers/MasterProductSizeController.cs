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
    public class MasterProductSizeController : ControllerBase
    {
        private readonly IMasterProductSizeInterface<MasterProductSizeResult> _IMasterProductSizeInterface;


        public MasterProductSizeController(IMasterProductSizeInterface<MasterProductSizeResult> IMasterProductSizeInterface)
        {
            _IMasterProductSizeInterface = IMasterProductSizeInterface;
        }


        // GET: api/MasterProductSize
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductSizeResult>>> GetASMasterProductSizes()
        {
            try
            {
                return _IMasterProductSizeInterface.GetAllASMasterProductSize().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProductSize/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterProductSizeResult>> GetASMasterProductSize(long id)
        {
            try
            {
                var objMasterProductSizeResult = _IMasterProductSizeInterface.GetASMasterProductSizeByID(id);

                if (objMasterProductSizeResult == null)
                {
                    return NotFound();
                }

                return objMasterProductSizeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterProductSize/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterProductSizeResult>> PutASMasterProductSize(long id, ASMasterProductSize objADMasterProductSize)
        {
            if (id != objADMasterProductSize.MasterProductSizeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterProductSizeInterface.UpdateASMasterProductSize(objADMasterProductSize);

                return _IMasterProductSizeInterface.GetASMasterProductSizeByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProductSizeInterface.ASMasterProductSizeExists(id))
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

        // POST: api/MasterProductSize
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterProductSize
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterProductSizeResult>> PostASMasterProductSize(ASMasterProductSize objADMasterProductSize)
        {
            try
            {
                await _IMasterProductSizeInterface.InsertASMasterProductSize(objADMasterProductSize);

                return CreatedAtAction("GetASMasterProductSize", new { id = objADMasterProductSize.MasterProductSizeId }, objADMasterProductSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterProductSize/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterProductSizeResult>> DeleteASMasterProductSize(long id)
        {
            try
            {
                var objMasterProductSizeResult = _IMasterProductSizeInterface.GetASMasterProductSizeByID(id);
                if (objMasterProductSizeResult == null)
                {
                    return NotFound();
                }

                await _IMasterProductSizeInterface.DeleteASMasterProductSize(id);

                return objMasterProductSizeResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
