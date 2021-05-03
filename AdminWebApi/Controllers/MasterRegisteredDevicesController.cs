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
    public class MasterRegisteredDevicesController : ControllerBase
    {
        private readonly IMasterRegisteredDeviceInterface<MasterRegisteredDeviceResult> _IMasterRegisteredDeviceInterface;

        public MasterRegisteredDevicesController(IMasterRegisteredDeviceInterface<MasterRegisteredDeviceResult> IMasterRegisteredDeviceInterface)
        {
            _IMasterRegisteredDeviceInterface = IMasterRegisteredDeviceInterface;
        }

        // GET: api/MasterRegisteredDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterRegisteredDeviceResult>>> GetADMasterRegisteredDevices()
        {
            try
            {
                return _IMasterRegisteredDeviceInterface.GetAllADMasterRegisteredDevice().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterRegisteredDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterRegisteredDeviceResult>> GetADMasterRegisteredDevice(long id)
        {
            try
            {
                var objMasterRegisteredDeviceResult = _IMasterRegisteredDeviceInterface.GetADMasterRegisteredDeviceByID(id);

                if (objMasterRegisteredDeviceResult == null)
                {
                    return NotFound();
                }

                return objMasterRegisteredDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterRegisteredDevices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterRegisteredDeviceResult>> PutADMasterRegisteredDevice(long id, ADMasterRegisteredDevice objADMasterRegisteredDevice)
        {
            if (id != objADMasterRegisteredDevice.MasterRegisteredDeviceId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterRegisteredDeviceInterface.UpdateADMasterRegisteredDevice(objADMasterRegisteredDevice);

                return _IMasterRegisteredDeviceInterface.GetADMasterRegisteredDeviceByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterRegisteredDeviceInterface.ADMasterRegisteredDeviceExists(id))
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

        // POST: api/MasterRegisteredDevices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterRegisteredDeviceResult>> PostADMasterRegisteredDevice(ADMasterRegisteredDevice objADMasterRegisteredDevice)
        {
            try
            {
                await _IMasterRegisteredDeviceInterface.InsertADMasterRegisteredDevice(objADMasterRegisteredDevice);

                return CreatedAtAction("GetADMasterRegisteredDevice", new { id = objADMasterRegisteredDevice.MasterRegisteredDeviceId }, objADMasterRegisteredDevice);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterRegisteredDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterRegisteredDeviceResult>> DeleteADMasterRegisteredDevice(long id)
        {
            try
            {
                var objMasterRegisteredDeviceResult = _IMasterRegisteredDeviceInterface.GetADMasterRegisteredDeviceByID(id);
                if (objMasterRegisteredDeviceResult == null)
                {
                    return NotFound();
                }

                await _IMasterRegisteredDeviceInterface.DeleteADMasterRegisteredDevice(id);

                return objMasterRegisteredDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
