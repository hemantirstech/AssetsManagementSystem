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
    public class MasterMailTemplatesController : ControllerBase
    {
        private readonly IMasterMailTemplateInterface<MasterMailTemplateResult> _IMasterMailTemplateInterface;

        public MasterMailTemplatesController(IMasterMailTemplateInterface<MasterMailTemplateResult> IMasterMailTemplateInterface)
        {
            _IMasterMailTemplateInterface = IMasterMailTemplateInterface;
        }

        // GET: api/MasterMailTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterMailTemplateResult>>> GetADMasterMailTemplates()
        {
            try
            {
                return _IMasterMailTemplateInterface.GetAllADMasterMailTemplate().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterMailTemplates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterMailTemplateResult>> GetADMasterMailTemplate(long id)
        {
            try
            {
                var objMasterMailTemplateResult = _IMasterMailTemplateInterface.GetADMasterMailTemplateByID(id);

                if (objMasterMailTemplateResult == null)
                {
                    return NotFound();
                }

                return objMasterMailTemplateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterMailTemplates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterMailTemplateResult>> PutADMasterMailTemplate(long id, ADMasterMailTemplate objADMasterMailTemplate)
        {
            if (id != objADMasterMailTemplate.MasterMailTemplateId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterMailTemplateInterface.UpdateADMasterMailTemplate(objADMasterMailTemplate);

                return _IMasterMailTemplateInterface.GetADMasterMailTemplateByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterMailTemplateInterface.ADMasterMailTemplateExists(id))
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

        // POST: api/MasterMailTemplates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterMailTemplateResult>> PostADMasterMailTemplate(ADMasterMailTemplate objADMasterMailTemplate)
        {
            try
            {
                await _IMasterMailTemplateInterface.InsertADMasterMailTemplate(objADMasterMailTemplate);

                return CreatedAtAction("GetADMasterMailTemplate", new { id = objADMasterMailTemplate.MasterMailTemplateId }, objADMasterMailTemplate);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterMailTemplates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterMailTemplateResult>> DeleteADMasterMailTemplate(long id)
        {
            try
            {
                var objMasterMailTemplateResult = _IMasterMailTemplateInterface.GetADMasterMailTemplateByID(id);
                if (objMasterMailTemplateResult == null)
                {
                    return NotFound();
                }

                await _IMasterMailTemplateInterface.DeleteADMasterMailTemplate(id);

                return objMasterMailTemplateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
