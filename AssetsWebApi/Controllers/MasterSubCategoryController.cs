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
    public class MasterSubCategoryController : ControllerBase
    {
        private readonly IMasterSubCategoryInterface<MasterSubCategoryResult> _IMasterSubCategoryInterface;


        public MasterSubCategoryController(IMasterSubCategoryInterface<MasterSubCategoryResult> IMasterSubCategoryInterface)
        {
            _IMasterSubCategoryInterface = IMasterSubCategoryInterface;
        }


        // GET: api/MasterSubCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterSubCategoryResult>>> GetASMasterSubCategorys()
        {
            try
            {
                return _IMasterSubCategoryInterface.GetAllASMasterSubCategory().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterSubCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterSubCategoryResult>> GetASMasterSubCategory(long id)
        {
            try
            {
                var objMasterSubCategoryResult = _IMasterSubCategoryInterface.GetASMasterSubCategoryByID(id);

                if (objMasterSubCategoryResult == null)
                {
                    return NotFound();
                }

                return objMasterSubCategoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterSubCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterSubCategoryResult>> PutASMasterSubCategory(long id, ASMasterSubCategory objADMasterSubCategory)
        {
            if (id != objADMasterSubCategory.MasterSubCategoryId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterSubCategoryInterface.UpdateASMasterSubCategory(objADMasterSubCategory);

                return _IMasterSubCategoryInterface.GetASMasterSubCategoryByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterSubCategoryInterface.ASMasterSubCategoryExists(id))
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

        // POST: api/MasterSubCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterSubCategory
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterSubCategoryResult>> PostASMasterSubCategory(ASMasterSubCategory objADMasterSubCategory)
        {
            try
            {
                await _IMasterSubCategoryInterface.InsertASMasterSubCategory(objADMasterSubCategory);

                return CreatedAtAction("GetASMasterSubCategory", new { id = objADMasterSubCategory.MasterSubCategoryId }, objADMasterSubCategory);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterSubCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterSubCategoryResult>> DeleteASMasterSubCategory(long id)
        {
            try
            {
                var objMasterSubCategoryResult = _IMasterSubCategoryInterface.GetASMasterSubCategoryByID(id);
                if (objMasterSubCategoryResult == null)
                {
                    return NotFound();
                }

                await _IMasterSubCategoryInterface.DeleteASMasterSubCategory(id);

                return objMasterSubCategoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
