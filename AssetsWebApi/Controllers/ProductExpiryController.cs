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
    public class ProductExpiryController : ControllerBase
    {
        private readonly IMasterProductChildInterface<MasterProductChildResult> _IMasterProductChildInterface;


        public ProductExpiryController(IMasterProductChildInterface<MasterProductChildResult> IMasterProductChildInterface)
        {
            _IMasterProductChildInterface = IMasterProductChildInterface;
        }


        // GET: api/MasterProductChild
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductChildResult>>> GetExpiryASMasterProductChilds()
        {
            try
            {
                return _IMasterProductChildInterface.GetExpiryASMasterProductChild().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProductChild/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MasterProductChildResult>>> GetExpiredASMasterProductChilds(int ExpiredProduct)
        {
            try
            {
                var objMasterProductChildResult = _IMasterProductChildInterface.GetExpiredASMasterProductChild();

                if (objMasterProductChildResult == null)
                {
                    return NotFound();
                }

                return objMasterProductChildResult.ToList(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

    }
}
