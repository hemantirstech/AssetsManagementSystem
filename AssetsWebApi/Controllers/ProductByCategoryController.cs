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
    public class ProductByCategoryController : ControllerBase
    {
        private readonly IMasterProductInterface<MasterProductResult> _IMasterProductInterface;

        public ProductByCategoryController(IMasterProductInterface<MasterProductResult> IMasterProductInterface)
        {
            _IMasterProductInterface = IMasterProductInterface;
        }

        // GET: api/MasterProduct/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductResult>>> GetProductByCategoryId(long MasterCategoryId, long MasterSubCategoryId, long MasterBrandId)
        {
            try
            {
                var objMasterProductResult = _IMasterProductInterface.GetASMasterProductByID(MasterCategoryId, MasterSubCategoryId, MasterBrandId);

                if (objMasterProductResult == null)
                {
                    return NotFound();
                }

                return objMasterProductResult.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
