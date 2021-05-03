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
    public class TransactionProductHistoryController : ControllerBase
    {
        private readonly ITransactionProductHistoryInterface<TransactionProductHistoryResult> _ITransactionProductHistoryInterface;


        public TransactionProductHistoryController(ITransactionProductHistoryInterface<TransactionProductHistoryResult> ITransactionProductHistoryInterface)
        {
            _ITransactionProductHistoryInterface = ITransactionProductHistoryInterface;
        }


        // GET: api/TransactionProductHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionProductHistoryResult>>> GetASTransactionProductHistorys()
        {
            try
            {
                return _ITransactionProductHistoryInterface.GetAllASTransactionProductHistory().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/TransactionProductHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionProductHistoryResult>> GetASTransactionProductHistory(long id)
        {
            try
            {
                var objTransactionProductHistoryResult = _ITransactionProductHistoryInterface.GetASTransactionProductHistoryByID(id);

                if (objTransactionProductHistoryResult == null)
                {
                    return NotFound();
                }

                return objTransactionProductHistoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/TransactionProductHistory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionProductHistoryResult>> PutASTransactionProductHistory(long id, ASTransactionProductHistory objADTransactionProductHistory)
        {
            if (id != objADTransactionProductHistory.TransactionProductHistoryId)
            {
                return BadRequest();
            }


            try
            {
                await _ITransactionProductHistoryInterface.UpdateASTransactionProductHistory(objADTransactionProductHistory);

                return _ITransactionProductHistoryInterface.GetASTransactionProductHistoryByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ITransactionProductHistoryInterface.ASTransactionProductHistoryExists(id))
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

        // POST: api/TransactionProductHistory
        // To protect from overposting attacks, enable the specific properties you want to bind to, forTransactionProductHistory
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TransactionProductHistoryResult>> PostASTransactionProductHistory(ASTransactionProductHistory objADTransactionProductHistory)
        {
            try
            {
                await _ITransactionProductHistoryInterface.InsertASTransactionProductHistory(objADTransactionProductHistory);

                return CreatedAtAction("GetASTransactionProductHistory", new { id = objADTransactionProductHistory.TransactionProductHistoryId }, objADTransactionProductHistory);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/TransactionProductHistory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionProductHistoryResult>> DeleteASTransactionProductHistory(long id)
        {
            try
            {
                var objTransactionProductHistoryResult = _ITransactionProductHistoryInterface.GetASTransactionProductHistoryByID(id);
                if (objTransactionProductHistoryResult == null)
                {
                    return NotFound();
                }

                await _ITransactionProductHistoryInterface.DeleteASTransactionProductHistory(id);

                return objTransactionProductHistoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
