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
using Microsoft.Extensions.Logging;


namespace AssetsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterCategoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMasterCategoryInterface<MasterCategoryResult> _IMasterCategoryInterface;


        public MasterCategoryController(ILoggerFactory loggerFactory, IMasterCategoryInterface<MasterCategoryResult> IMasterCategoryInterface)
        {
            _IMasterCategoryInterface = IMasterCategoryInterface;
            _logger = loggerFactory.CreateLogger<MasterCategoryController>();
        }


        // GET: api/MasterCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterCategoryResult>>> GetASMasterCategorys()
        {
            try
            {
                return _IMasterCategoryInterface.GetAllASMasterCategory().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterCategoryResult>> GetASMasterCategory(long id)
        {
            try
            {
                var objMasterCategoryResult = _IMasterCategoryInterface.GetASMasterCategoryByID(id);

                if (objMasterCategoryResult == null)
                {
                    return NotFound();
                }

                return objMasterCategoryResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterCategoryResult>> PutASMasterCategory(long id, ASMasterCategory objADMasterCategory)
        {
            _logger.LogError("Done PutASMasterCategory -- Aniruddha Pakhale");

            if (id != objADMasterCategory.MasterCategoryId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterCategoryInterface.UpdateASMasterCategory(objADMasterCategory);

                return _IMasterCategoryInterface.GetASMasterCategoryByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                

                if (!_IMasterCategoryInterface.ASMasterCategoryExists(id))
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
                
                _logger.LogError(ex.Message);

                throw new Exception(ex.Message);
            }

            return NoContent();
        }

        // POST: api/MasterCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterCategory
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterCategoryResult>> PostASMasterCategory(ASMasterCategory objADMasterCategory)
        {
            try
            {
                await _IMasterCategoryInterface.InsertASMasterCategory(objADMasterCategory);

                return CreatedAtAction("GetASMasterCategory", new { id = objADMasterCategory.MasterCategoryId }, objADMasterCategory);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterCategoryResult>> DeleteASMasterCategory(long id)
        {
            try
            {
                var objMasterCategoryResult = _IMasterCategoryInterface.GetASMasterCategoryByID(id);
                if (objMasterCategoryResult == null)
                {
                    return NotFound();
                }

                await _IMasterCategoryInterface.DeleteASMasterCategory(id);

                return objMasterCategoryResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw new Exception(ex.Message);
            }
        }

    }
}
