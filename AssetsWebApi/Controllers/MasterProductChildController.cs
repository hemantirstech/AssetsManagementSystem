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
    public class MasterProductChildController : ControllerBase
    {
        private readonly IMasterProductChildInterface<MasterProductChildResult> _IMasterProductChildInterface;


        public MasterProductChildController(IMasterProductChildInterface<MasterProductChildResult> IMasterProductChildInterface)
        {
            _IMasterProductChildInterface = IMasterProductChildInterface;
        }


        // GET: api/MasterProductChild
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterProductChildResult>>> GetASMasterProductChilds()
        {
            try
            {
                return _IMasterProductChildInterface.GetAllASMasterProductChild().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterProductChild/5
        [HttpGet("{MasterProductChildid}")]
        public async Task<ActionResult<MasterProductChildResult>> GetASMasterProductChild(long MasterProductChildid)
        {
            try
            {
                var objMasterProductChildResult = _IMasterProductChildInterface.GetASMasterProductChildByID(MasterProductChildid);

                if (objMasterProductChildResult == null)
                {
                    return NotFound();
                }

                return objMasterProductChildResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterProductChild/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{MasterProductChildid}")]
        public async Task<ActionResult<MasterProductChildResult>> PutASMasterProductChild(long MasterProductChildid, ASMasterProductChild objADMasterProductChild)
        {
            if (MasterProductChildid != objADMasterProductChild.MasterProductChildId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterProductChildInterface.UpdateASMasterProductChild(objADMasterProductChild);

                return _IMasterProductChildInterface.GetASMasterProductChildByID(MasterProductChildid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProductChildInterface.ASMasterProductChildExists(MasterProductChildid))
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

        // PUT: api/MasterProductChild/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut()]
        public async Task<ActionResult<MasterProductChildResult>> PutASMasterProductChild(ASMasterProductChildStatus objASMasterProductChildStatus)
        {
            
            try
            {
                await _IMasterProductChildInterface.UpdateASMasterProductChildStatus(objASMasterProductChildStatus);

                return _IMasterProductChildInterface.GetASMasterProductChildByID(objASMasterProductChildStatus.IsDeadAssets.FirstOrDefault());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterProductChildInterface.ASMasterProductChildExists(objASMasterProductChildStatus.IsDeadAssets.FirstOrDefault()))
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

        // POST: api/MasterProductChild
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterProductChild
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterProductChildResult>> PostASMasterProductChild(ASMasterProductChild objADMasterProductChild)
        {
            try
            {
                await _IMasterProductChildInterface.InsertASMasterProductChild(objADMasterProductChild);

                return CreatedAtAction("GetASMasterProductChild", new { id = objADMasterProductChild.MasterProductChildId }, objADMasterProductChild);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterProductChild/5
        [HttpDelete("{MasterProductChildid}")]
        public async Task<ActionResult<MasterProductChildResult>> DeleteASMasterProductChild(long MasterProductChildid)
        {
            try
            {
                var objMasterProductChildResult = _IMasterProductChildInterface.GetASMasterProductChildByID(MasterProductChildid);
                if (objMasterProductChildResult == null)
                {
                    return NotFound();
                }

                await _IMasterProductChildInterface.DeleteASMasterProductChild(MasterProductChildid);

                return objMasterProductChildResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
