using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterTypeOfDevicesController : ControllerBase
    {
        private readonly IMasterTypeOfDeviceInterface<MasterTypeOfDeviceResult> _IMasterTypeOfDeviceInterface;

        public MasterTypeOfDevicesController(IMasterTypeOfDeviceInterface<MasterTypeOfDeviceResult> IMasterTypeOfDeviceInterface)
        {
            _IMasterTypeOfDeviceInterface = IMasterTypeOfDeviceInterface;
        }

        // GET: api/MasterTypeOfDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterTypeOfDeviceResult>>> GetADMasterTypeOfDevices()
        {
            try
            {
                return _IMasterTypeOfDeviceInterface.GetAllADMasterTypeOfDevice().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterTypeOfDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterTypeOfDeviceResult>> GetADMasterTypeOfDevice(long id)
        {
            try
            {
                var objMasterTypeOfDeviceResult = _IMasterTypeOfDeviceInterface.GetADMasterTypeOfDeviceByID(id);

                if (objMasterTypeOfDeviceResult == null)
                {
                    return NotFound();
                }

                return objMasterTypeOfDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/MasterTypeOfDevices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterTypeOfDeviceResult>> PutADMasterTypeOfDevice(long id, ADMasterTypeOfDevice objADMasterTypeOfDevice)
        {
            if (id != objADMasterTypeOfDevice.MasterTypeOfDeviceId)
            {
                return BadRequest();
            }

            try
            {
                await _IMasterTypeOfDeviceInterface.UpdateADMasterTypeOfDevice(objADMasterTypeOfDevice);

                return _IMasterTypeOfDeviceInterface.GetADMasterTypeOfDeviceByID(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterTypeOfDeviceInterface.ADMasterTypeOfDeviceExists(id))
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

        // POST: api/MasterTypeOfDevices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterTypeOfDeviceResult>> PostADMasterTypeOfDevice(ADMasterTypeOfDevice objADMasterTypeOfDevice)
        {
            try
            {
                await _IMasterTypeOfDeviceInterface.InsertADMasterTypeOfDevice(objADMasterTypeOfDevice);

                return CreatedAtAction("GetADMasterTypeOfDevice", new { id = objADMasterTypeOfDevice.MasterTypeOfDeviceId }, objADMasterTypeOfDevice);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterTypeOfDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterTypeOfDeviceResult>> DeleteADMasterTypeOfDevice(long id)
        {
            try
            {
                var objADMasterTypeOfDeviceResult = _IMasterTypeOfDeviceInterface.GetADMasterTypeOfDeviceByID(id);
                if (objADMasterTypeOfDeviceResult == null)
                {
                    return NotFound();
                }

                await _IMasterTypeOfDeviceInterface.DeleteADMasterTypeOfDevice(id);

                return objADMasterTypeOfDeviceResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
