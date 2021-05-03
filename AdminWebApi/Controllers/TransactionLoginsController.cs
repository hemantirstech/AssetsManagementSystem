using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminDAL;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionLoginsController : ControllerBase
    {
        private readonly AdminContext _context;

        public TransactionLoginsController(AdminContext context)
        {
            _context = context;
        }

        // GET: api/TransactionLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ADTransactionLogin>>> GetADTransactionLogins()
        {
            try
            {
                return await _context.ADTransactionLogins.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/TransactionLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ADTransactionLogin>> GetADTransactionLogin(long id)
        {
            try
            {
                var aDTransactionLogin = await _context.ADTransactionLogins.FindAsync(id);

                if (aDTransactionLogin == null)
                {
                    return NotFound();
                }

                return aDTransactionLogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/TransactionLogins/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutADTransactionLogin(long id, ADTransactionLogin aDTransactionLogin)
        {
            if (id != aDTransactionLogin.TransactionLoginId)
            {
                return BadRequest();
            }

            _context.Entry(aDTransactionLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ADTransactionLoginExists(id))
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

        // POST: api/TransactionLogins
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ADTransactionLogin>> PostADTransactionLogin(ADTransactionLogin aDTransactionLogin)
        {
            try
            {
                _context.ADTransactionLogins.Add(aDTransactionLogin);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetADTransactionLogin", new { id = aDTransactionLogin.TransactionLoginId }, aDTransactionLogin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/TransactionLogins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ADTransactionLogin>> DeleteADTransactionLogin(long id)
        {
            try
            {
                var aDTransactionLogin = await _context.ADTransactionLogins.FindAsync(id);
                if (aDTransactionLogin == null)
                {
                    return NotFound();
                }

                _context.ADTransactionLogins.Remove(aDTransactionLogin);
                await _context.SaveChangesAsync();

                return aDTransactionLogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ADTransactionLoginExists(long id)
        {
            try
            {
                return _context.ADTransactionLogins.Any(e => e.TransactionLoginId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
