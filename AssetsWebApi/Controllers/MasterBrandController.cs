using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterBrandController : ControllerBase
    {
        private readonly IMasterBrandInterface<MasterBrandResult> _IMasterBrandInterface;


        public MasterBrandController(IMasterBrandInterface<MasterBrandResult> IMasterBrandInterface)
        {
            _IMasterBrandInterface = IMasterBrandInterface;
        }


        // GET: api/MasterBrand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterBrandResult>>> GetASMasterBrands()
        {
            try
            {
                return _IMasterBrandInterface.GetAllASMasterBrand().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterBrand/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterBrandResult>> GetASMasterBrand(long id)
        {
            try
            {
                var objMasterBrandResult = _IMasterBrandInterface.GetASMasterBrandByID(id);

                if (objMasterBrandResult == null)
                {
                    return NotFound();
                }

                return objMasterBrandResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterBrand/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterBrandResult>> PutASMasterBrand(long id, ASMasterBrand objADMasterBrand)
        {
            if (id != objADMasterBrand.MasterBrandId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterBrandInterface.UpdateASMasterBrand(objADMasterBrand);

                return _IMasterBrandInterface.GetASMasterBrandByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterBrandInterface.ASMasterBrandExists(id))
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

        // POST: api/MasterBrand
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterBrand
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterBrandResult>> PostASMasterBrand(ASMasterBrand objADMasterBrand)
        {
            try
            {
                await _IMasterBrandInterface.InsertASMasterBrand(objADMasterBrand);

                return CreatedAtAction("GetASMasterBrand", new { id = objADMasterBrand.MasterBrandId }, objADMasterBrand);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterBrand/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterBrandResult>> DeleteASMasterBrand(long id)
        {
            try
            {
                var objMasterBrandResult = _IMasterBrandInterface.GetASMasterBrandByID(id);
                if (objMasterBrandResult == null)
                {
                    return NotFound();
                }

                await _IMasterBrandInterface.DeleteASMasterBrand(id);

                return objMasterBrandResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
