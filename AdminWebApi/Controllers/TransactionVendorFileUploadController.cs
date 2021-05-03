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
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Route("api/file")]

    public class TransactionVendorFileUploadController : ControllerBase
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        //private readonly ITransactionVendorFileUploadInterface<TransactionVendorFileUploadResult> _ITransactionVendorFileUploadInterface;


        //public TransactionVendorFileUploadController(ITransactionVendorFileUploadInterface<TransactionVendorFileUploadResult> ITransactionVendorFileUploadInterface)
        //{
        //    _ITransactionVendorFileUploadInterface = ITransactionVendorFileUploadInterface;
        //}

        private IWebHostEnvironment iwebHostEnvironment;

        public TransactionVendorFileUploadController(IWebHostEnvironment _iwebHostEnvironment)
        {
            this.webHostEnvironment = _iwebHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                var result = new List<FileUploadResult>();
                foreach (var file in files)
                {
                    //var path = Path.Combine(this.iwebHostEnvironment.WebRootPath, "images", file.FileName);
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "img/app-images", file.FileName);

                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    result.Add(new FileUploadResult() { Name = file.FileName, Length = file.Length });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost, DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {



        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();

        //        // var file = Request.Form.Files[0];
        //        var folderName = Path.Combine(webHostEnvironment.WebRootPath, "img/app-images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}



        ////// POST: api/TransactionVendorFileUpload
        ////// To protect from overposting attacks, enable the specific properties you want to bind to, for
        ////// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        ////[HttpPost]
        ////public async Task<ActionResult<TransactionVendorFileUploadResult>> PostADTransactionVendorFileUpload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload)
        ////{
        ////    try
        ////    {
        ////        await _ITransactionVendorFileUploadInterface.InsertADTransactionVendorFileUpload(objADTransactionVendorFileUpload);

        ////        return CreatedAtAction("GetADTransactionVendorFileUpload", new { id = objADTransactionVendorFileUpload.TransactionVendorFileUploadId }, objADTransactionVendorFileUpload);

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception(ex.Message);
        ////    }
        ////}



        ////// GET: api/TransactionVendorFileUpload
        ////[HttpGet]
        ////public async Task<ActionResult<IEnumerable<TransactionVendorFileUploadResult>>> GetADTransactionVendorFileUploads()
        ////{
        ////    try
        ////    {
        ////        return _ITransactionVendorFileUploadInterface.GetAllADTransactionVendorFileUpload().ToList();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception(ex.Message);
        ////    }
        ////}

        ////// GET: api/TransactionVendorFileUpload/5
        ////[HttpGet("{id}")]
        ////public async Task<ActionResult<TransactionVendorFileUploadResult>> GetADTransactionVendorFileUpload(long id)
        ////{
        ////    try
        ////    {
        ////        var objTransactionVendorFileUploadResult = _ITransactionVendorFileUploadInterface.GetADTransactionVendorFileUploadByID(id);

        ////        if (objTransactionVendorFileUploadResult == null)
        ////        {
        ////            return NotFound();
        ////        }

        ////        return objTransactionVendorFileUploadResult;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception(ex.Message);
        ////    }
        ////}

        ////// PUT: api/TransactionVendorFileUpload/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to, for
        ////// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        ////[HttpPut("{id}")]
        ////public async Task<ActionResult<TransactionVendorFileUploadResult>> PutADTransactionVendorFileUpload(long id, ADTransactionVendorFileUpload objADTransactionVendorFileUpload)
        ////{
        ////    if (id != objADTransactionVendorFileUpload.TransactionVendorFileUploadId)
        ////    {
        ////        return BadRequest();
        ////    }


        ////    try
        ////    {
        ////        await _ITransactionVendorFileUploadInterface.UpdateADTransactionVendorFileUpload(objADTransactionVendorFileUpload);

        ////        return _ITransactionVendorFileUploadInterface.GetADTransactionVendorFileUploadByID(id);
        ////    }
        ////    catch (DbUpdateConcurrencyException)
        ////    {
        ////        if (!_ITransactionVendorFileUploadInterface.ADTransactionVendorFileUploadExists(id))
        ////        {
        ////            return NotFound();
        ////        }
        ////        else
        ////        {
        ////            throw;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception(ex.Message);
        ////    }

        ////    return NoContent();
        ////}

     
        ////// DELETE: api/TransactionVendorFileUpload/5
        ////[HttpDelete("{id}")]
        ////public async Task<ActionResult<TransactionVendorFileUploadResult>> DeleteADTransactionVendorFileUpload(long id)
        ////{
        ////    try
        ////    {
        ////        var objTransactionVendorFileUploadResult = _ITransactionVendorFileUploadInterface.GetADTransactionVendorFileUploadByID(id);
        ////        if (objTransactionVendorFileUploadResult == null)
        ////        {
        ////            return NotFound();
        ////        }

        ////        await _ITransactionVendorFileUploadInterface.DeleteADTransactionVendorFileUpload(id);

        ////        return objTransactionVendorFileUploadResult;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception(ex.Message);
        ////    }
        ////}

    }
}
