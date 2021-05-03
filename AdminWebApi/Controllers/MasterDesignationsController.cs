using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminDAL;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;


namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDesignationsController : ControllerBase
    {
        private readonly IMasterDesignationInterface<MasterDesignationResult> _IMasterDesignationInterface;

        public MasterDesignationsController(IMasterDesignationInterface<MasterDesignationResult> IMasterDesignationInterface)
        {
            _IMasterDesignationInterface = IMasterDesignationInterface;
        }

        // GET: api/MasterDesignations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterDesignationResult>>> GetADMasterDesignations()
        {
            try
            {
                return _IMasterDesignationInterface.GetAllADMasterDesignation().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterDesignations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterDesignationResult>> GetADMasterDesignation(long id)
        {
            try
            {
                var objMasterDesignationResult = _IMasterDesignationInterface.GetADMasterDesignationByID(id);

                if (objMasterDesignationResult == null)
                {
                    return NotFound();
                }

                return objMasterDesignationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterDesignations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterDesignationResult>> PutADMasterDesignation(long id, ADMasterDesignation objADMasterDesignation)
        {
            if (id != objADMasterDesignation.MasterDesignationId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterDesignationInterface.UpdateADMasterDesignation(objADMasterDesignation);

                return _IMasterDesignationInterface.GetADMasterDesignationByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterDesignationInterface.ADMasterDesignationExists(id))
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

        // POST: api/MasterDesignations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterDesignationResult>> PostADMasterDesignation(ADMasterDesignation objADMasterDesignation)
        {
            try
            {
                await _IMasterDesignationInterface.InsertADMasterDesignation(objADMasterDesignation);

                return CreatedAtAction("GetADMasterDesignation", new { id = objADMasterDesignation.MasterDesignationId }, objADMasterDesignation);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterDesignations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterDesignationResult>> DeleteADMasterDesignation(long id)
        {
            try
            {
                var objMasterDesignationResult = _IMasterDesignationInterface.GetADMasterDesignationByID(id);
                if (objMasterDesignationResult == null)
                {
                    return NotFound();
                }

                await _IMasterDesignationInterface.DeleteADMasterDesignation(id);

                return objMasterDesignationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
