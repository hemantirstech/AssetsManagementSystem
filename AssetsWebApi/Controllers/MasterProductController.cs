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
    public class MasterProductController : ControllerBase
    {

        private readonly IMasterProductInterface<MasterProductResult> _IMasterProductInterface;


        public MasterProductController(IMasterProductInterface<MasterProductResult> IMasterProductInterface)
        {
            _IMasterProductInterface = IMasterProductInterface;
        }


        // GET: api/MasterProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductResult>>> GetASMasterProducts()
        {
            try
            {
                return _IMasterProductInterface.GetAllASMasterProduct().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterProductResult>> GetASMasterProduct(long id)
        {
            try
            {
                var objMasterProductResult = _IMasterProductInterface.GetASMasterProductByID(id);

                if (objMasterProductResult == null)
                {
                    return NotFound();
                }

                return objMasterProductResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        // PUT: api/MasterProduct/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterProductResult>> PutASMasterProduct(long id, ASMasterProduct objASMasterProduct)
        {
            if (id != objASMasterProduct.MasterProductId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterProductInterface.UpdateASMasterProduct(objASMasterProduct);

                return _IMasterProductInterface.GetASMasterProductByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProductInterface.ASMasterProductExists(id))
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

        // POST: api/MasterProduct
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterProduct
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterProductResult>> PostASMasterProduct(ASMasterProductMerge objASMasterProductMerge)
        {
            try
            {
                await _IMasterProductInterface.InsertASMasterProduct(objASMasterProductMerge);

                ASMasterProduct objASMasterProduct = objASMasterProductMerge.ASMasterProduct;

                return CreatedAtAction("GetASMasterProduct", new { id = objASMasterProduct.MasterProductId }, objASMasterProduct);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterProduct/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterProductResult>> DeleteASMasterProduct(long id)
        {
            try
            {
                var objMasterProductResult = _IMasterProductInterface.GetASMasterProductByID(id);
                if (objMasterProductResult == null)
                {
                    return NotFound();
                }

                await _IMasterProductInterface.DeleteASMasterProduct(id);

                return objMasterProductResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
