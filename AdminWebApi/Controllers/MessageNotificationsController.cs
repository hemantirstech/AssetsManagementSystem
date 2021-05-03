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
    public class MessageNotificationsController : ControllerBase
    {
        private readonly IMessageNotificationInterface<MessageNotificationResult> _IMessageNotificationInterface;

        public MessageNotificationsController(IMessageNotificationInterface<MessageNotificationResult> IMessageNotificationInterface)
        {
            _IMessageNotificationInterface = IMessageNotificationInterface;
        }

        // GET: api/MessageNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageNotificationResult>>> GetADMessageNotifications()
        {
            try
            {
                return _IMessageNotificationInterface.GetAllADMessageNotification().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MessageNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageNotificationResult>> GetADMessageNotification(long id)
        {
            try
            {
                var objMessageNotificationResult = _IMessageNotificationInterface.GetADMessageNotificationByID(id);

                if (objMessageNotificationResult == null)
                {
                    return NotFound();
                }

                return objMessageNotificationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MessageNotifications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MessageNotificationResult>> PutADMessageNotification(long id, ADMessageNotification objADMessageNotification)
        {
            if (id != objADMessageNotification.MasterMessageNotificationId)
            {
                return BadRequest();
            }


            try
            {
                await _IMessageNotificationInterface.UpdateADMessageNotification(objADMessageNotification);

                return _IMessageNotificationInterface.GetADMessageNotificationByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMessageNotificationInterface.ADMessageNotificationExists(id))
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

        // POST: api/MessageNotifications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MessageNotificationResult>> PostADMessageNotification(ADMessageNotification objADMessageNotification)
        {
            try
            {
                await _IMessageNotificationInterface.InsertADMessageNotification(objADMessageNotification);

                return CreatedAtAction("GetADMessageNotification", new { id = objADMessageNotification.MasterMessageNotificationId }, objADMessageNotification);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MessageNotifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageNotificationResult>> DeleteADMessageNotification(long id)
        {
            try
            {
                var objMessageNotificationResult = _IMessageNotificationInterface.GetADMessageNotificationByID(id);
                if (objMessageNotificationResult == null)
                {
                    return NotFound();
                }

                await _IMessageNotificationInterface.DeleteADMessageNotification(id);

                return objMessageNotificationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
