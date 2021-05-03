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
    public class MasterAssetsAssignmentController : ControllerBase
    {
        private readonly IMasterAssetsAssignmentInterface<MasterAssetsAssignmentResult> _IMasterAssetsAssignmentInterface;


        public MasterAssetsAssignmentController(IMasterAssetsAssignmentInterface<MasterAssetsAssignmentResult> IMasterAssetsAssignmentInterface)
        {
            _IMasterAssetsAssignmentInterface = IMasterAssetsAssignmentInterface;
        }


        // GET: api/MasterAssetsAssignment
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MasterAssetsAssignmentResult>>> GetASMasterAssetsAssignments()
        //{
        //    try
        //    {
        //        return _IMasterAssetsAssignmentInterface.GetAllASMasterAssetsAssignment().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterAssetsAssignmentResult>>> GetASMasterAssetsAssignmentsNotAssign(long MasterCategoryId,long MasterSubCategoryId, long MasterBranchId)
        {
            try
            {
                return _IMasterAssetsAssignmentInterface.GetAllASMasterAssetsAssignmentNotAssign(MasterCategoryId,MasterSubCategoryId,MasterBranchId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/MasterAssetsAssignment/5
        [HttpGet("{MasterEmployeeId}")]
        public async Task<ActionResult<IEnumerable<MasterAssetsAssignmentResult>>> GetASMasterAssetsAssignments(long MasterEmployeeId)
        {
            try
            {
                if(MasterEmployeeId>0)
                {
                    return _IMasterAssetsAssignmentInterface.GetAllASMasterAssetsAssignment(MasterEmployeeId).ToList();
                }
                else
                {
                    //It will print list of all employee
                    return _IMasterAssetsAssignmentInterface.GetAllASMasterAssetsAssignment().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //// GET: api/MasterAssetsAssignment/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MasterAssetsAssignmentResult>> GetASMasterAssetsAssignment(long id)
        //{
        //    try
        //    {
        //        var objMasterAssetsAssignmentResult = _IMasterAssetsAssignmentInterface.GetASMasterAssetsAssignmentByID(id);

        //        if (objMasterAssetsAssignmentResult == null)
        //        {
        //            return NotFound();
        //        }

        //        return objMasterAssetsAssignmentResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        // PUT: api/MasterAssetsAssignment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<MasterAssetsAssignmentResult>> PutASMasterAssetsAssignment(long id, ASMasterAssetsAssignment objASMasterAssetsAssignment)
        {
            if (id != objASMasterAssetsAssignment.MasterEmployeeId)
            {
                return BadRequest();
            }


            try
            {
                await _IMasterAssetsAssignmentInterface.UpdateASMasterAssetsAssignment(objASMasterAssetsAssignment);

                return _IMasterAssetsAssignmentInterface.GetAllASMasterAssetsAssignment(id).FirstOrDefault();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_IMasterAssetsAssignmentInterface.ASMasterAssetsAssignmentExists(id))
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

        // POST: api/MasterAssetsAssignment
        // To protect from overposting attacks, enable the specific properties you want to bind to, forMasterAssetsAssignment
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MasterAssetsAssignmentResult>> PostASMasterAssetsAssignment(ASMasterAssetsAssignment objASMasterAssetsAssignment)
        {
            try
            {
                await _IMasterAssetsAssignmentInterface.InsertASMasterAssetsAssignment(objASMasterAssetsAssignment);

                return CreatedAtAction("GetASMasterAssetsAssignment", new { id = objASMasterAssetsAssignment.MasterAssetsAssignmentId }, objASMasterAssetsAssignment);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/MasterAssetsAssignment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MasterAssetsAssignmentResult>> DeleteASMasterAssetsAssignment(long id)
        {
            try
            {
                var objMasterAssetsAssignmentResult = _IMasterAssetsAssignmentInterface.GetASMasterAssetsAssignmentByID(id);
                if (objMasterAssetsAssignmentResult == null)
                {
                    return NotFound();
                }

                await _IMasterAssetsAssignmentInterface.DeleteASMasterAssetsAssignment(id);

                return objMasterAssetsAssignmentResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
