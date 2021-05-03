using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MasterCompanyController : ControllerBase
    {
        private readonly AdminContext _context;
        public MasterCompanyController(AdminContext context)
        {
            _context = context;
        }

        // GET: api/MasterCompanys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ADMasterCompany>>> GetMasterCompany()
        {
            try
            {
                return await _context.ADMasterCompanies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterCompanys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ADMasterCompany>> GetMasterCompany(long MasterCompanyId)
        {
            try
            {
                var MasterCompany = await _context.ADMasterCompanies.FindAsync(MasterCompanyId);

                if (MasterCompany == null)
                {
                    return NotFound();
                }

                return MasterCompany;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool MasterCompanyExists(string CompanyTitle)
        {
            

            try
            {
                return _context.ADMasterCompanies.Any(e => e.CompanyTitle.Trim().ToUpper() == CompanyTitle.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        // POST api/<MasterCompanyController>
        [HttpPost]
        public async Task<ActionResult<ADMasterCompany>> PostMasterCompany(ADMasterCompany objADMasterCompany)
        {
            
            try
            {
                if (!MasterCompanyExists(objADMasterCompany.CompanyTitle))
                {
                    _context.ADMasterCompanies.Add(objADMasterCompany);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetMasterCompany", new { MasterCompanyId = objADMasterCompany.MasterCompanyId }, objADMasterCompany);
                }
                else
                {
                    return UnprocessableEntity();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<MasterCompanyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //_context.Update<ADMasterCompany>(stud);
            //_context.SaveChanges();
        }

        // DELETE api/<MasterCompanyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
