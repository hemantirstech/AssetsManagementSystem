using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterVendor;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterVendorController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterVendorController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterVendorViewModel objIndexMasterVendorViewModel = new IndexMasterVendorViewModel();
                IEnumerable<MasterVendorViewModel> objMasterVendorViewModellList = null;
                string endpoint = apiBaseUrl + "MasterVendor";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterVendorViewModellList = JsonConvert.DeserializeObject<IEnumerable<MasterVendorViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterVendorViewModellList = Enumerable.Empty<MasterVendorViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterVendorViewModel.MasterVendorList = objMasterVendorViewModellList.ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterVendor/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterVendorViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterVendorViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterVendorViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterVendorViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterVendor/Index.cshtml", objIndexMasterVendorViewModel);
            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return new EmptyResult();
        }

        public IActionResult AddMasterVendor()
        {
            try
            {
                AddMasterVendorViewModel objAddMasterVendorViewModel = new AddMasterVendorViewModel();
                objAddMasterVendorViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterVendorViewModel.IsActive = true;
                objAddMasterVendorViewModel.MasterDesignationId = 1;
                objAddMasterVendorViewModel.MasterCompanyTypeId = 1;
                objAddMasterVendorViewModel.MasterTimeZoneId = 91;
                objAddMasterVendorViewModel.MasterCurrencyId = 47;
                objAddMasterVendorViewModel.MasterCountryId = 101;
                objAddMasterVendorViewModel.MasterVendorId = CommonFunction.NextMasterId("ADMasterVendor", apiBaseUrl);
                objAddMasterVendorViewModel.MasterVendorId = 0;
                objAddMasterVendorViewModel.DateofRegistration = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return new EmptyResult();
        }
        public IActionResult ViewMasterVendor(long MasterVendorId)
        {
            try
            {
                AddMasterVendorViewModel objAddMasterVendorViewModel = null;
                string endpoint = apiBaseUrl + "MasterVendor/" + MasterVendorId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterVendorViewModel = JsonConvert.DeserializeObject<AddMasterVendorViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterVendorViewModel = new AddMasterVendorViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();

                objAddMasterVendorViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult SaveMasterVendor(AddMasterVendorViewModel objAddMasterVendorViewModel, IFormFile _VendorLogo, string SaveMasterVendor, string AddUploadFile, IFormFile[] UploadFile)
        {
            try
            {
                if (SaveMasterVendor == "Save Master Vendor" && AddUploadFile == null)
                {

                    //for main image Logo
                    if (_VendorLogo != null)
                        objAddMasterVendorViewModel.VendorLogo = UploadedImageFile(_VendorLogo, objAddMasterVendorViewModel.VendorLogo);

                    objAddMasterVendorViewModel.EnterById = 1;
                    objAddMasterVendorViewModel.EnterDate = DateTime.Now;
                    objAddMasterVendorViewModel.ModifiedById = 1;
                    objAddMasterVendorViewModel.ModifiedDate = DateTime.Now;
                    objAddMasterVendorViewModel.MasterAddressTypeId = 1;
                    objAddMasterVendorViewModel.IsActive = true;

                    if (objAddMasterVendorViewModel.MasterAddressTypeId == null)
                    {
                        objAddMasterVendorViewModel.MasterAddressTypeId = 2;
                    }

                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    if (ModelState.IsValid)
                    {
                        string endpoint = apiBaseUrl + "MasterVendor";

                        Task<string> HttpPostResponse = null;

                        if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.SAVE)
                        {
                            HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterVendorViewModel);
                            //Notification Message
                            //Session is used to store object
                            HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Insert Successfully!", ""));
                        }
                        else if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.UPDATE)
                        {
                            endpoint = apiBaseUrl + "MasterVendor/" + objAddMasterVendorViewModel.MasterVendorId;
                            HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterVendorViewModel);

                            //Notification Message                       
                            //Session is used to store object
                            HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Update Successfully!", ""));
                        }

                        if (HttpPostResponse != null)
                        {
                            objAddMasterVendorViewModel = JsonConvert.DeserializeObject<AddMasterVendorViewModel>(HttpPostResponse.Result);
                            _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterVendorViewModel)); ;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            DropDownFillMethod();
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                            return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
                        }
                    }
                    else
                    {
                        ModelState.Clear();
                        if (ModelState.IsValid == false)
                        {
                            ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                        }
                        DropDownFillMethod();

                        //Return View doesn't make a new requests, it just renders the view
                        return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
                    }
                }

                else if (SaveMasterVendor == null && AddUploadFile == "Add Upload File")
                {
                    foreach (IFormFile file in UploadFile)
                    {

                        //Checking file is available to save.  
                        if (file != null)
                        {
                            //objAddMasterVendorViewModel.TransactionVendorFileName = Path.GetFileName(file.FileName);
                            //objAddMasterVendorViewModel.UploadFile = UploadedImageFile(file, objAddMasterVendorViewModel.UploadFile);

                        }
                    }

                    //for main image Logo
                    if (_VendorLogo != null)
                        objAddMasterVendorViewModel.VendorLogo = UploadedImageFile(_VendorLogo, objAddMasterVendorViewModel.VendorLogo);

                    objAddMasterVendorViewModel.EnterById = 1;
                    objAddMasterVendorViewModel.EnterDate = DateTime.Now;
                    objAddMasterVendorViewModel.ModifiedById = 1;
                    objAddMasterVendorViewModel.ModifiedDate = DateTime.Now;
                    objAddMasterVendorViewModel.MasterAddressTypeId = 1;
                    objAddMasterVendorViewModel.IsActive = true;

                    if (objAddMasterVendorViewModel.MasterAddressTypeId == null)
                    {
                        objAddMasterVendorViewModel.MasterAddressTypeId = 2;
                    }

                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                    if (ModelState.IsValid)
                    {
                        string endpoint = apiBaseUrl + "MasterVendor";

                        Task<string> HttpPostResponse = null;

                        if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.SAVE)
                        {
                            HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterVendorViewModel);
                            //Notification Message
                            //Session is used to store object
                            HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Insert Successfully!", ""));
                        }
                        else if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.UPDATE)
                        {
                            endpoint = apiBaseUrl + "MasterVendor/" + objAddMasterVendorViewModel.MasterVendorId;
                            HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterVendorViewModel);

                            //Notification Message                       
                            //Session is used to store object
                            HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Update Successfully!", ""));
                        }

                        if (HttpPostResponse != null)
                        {
                            objAddMasterVendorViewModel = JsonConvert.DeserializeObject<AddMasterVendorViewModel>(HttpPostResponse.Result);
                            _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterVendorViewModel)); ;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            DropDownFillMethod();
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                            return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
                        }
                    }
                    else
                    {
                        ModelState.Clear();
                        if (ModelState.IsValid == false)
                        {
                            ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                        }
                        DropDownFillMethod();

                        //Return View doesn't make a new requests, it just renders the view
                        return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
                    }


                }

            }

            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return new EmptyResult();
        }

       



        //public IActionResult SaveMasterVendor(AddMasterVendorViewModel objAddMasterVendorViewModel, IFormFile _VendorLogo, string SaveMasterVendor, string AddUploadFile, IFormFile _UploadFile)
        //{
        //    try
        //    {
        //        if (SaveMasterVendor == "Save Master Vendor" && AddUploadFile == null)
        //        {

        //            //for main image Logo
        //            if (_VendorLogo != null)
        //                objAddMasterVendorViewModel.VendorLogo = UploadedImageFile(_VendorLogo, objAddMasterVendorViewModel.VendorLogo);

        //            objAddMasterVendorViewModel.EnterById = 1;
        //            objAddMasterVendorViewModel.EnterDate = DateTime.Now;
        //            objAddMasterVendorViewModel.ModifiedById = 1;
        //            objAddMasterVendorViewModel.ModifiedDate = DateTime.Now;
        //            objAddMasterVendorViewModel.MasterAddressTypeId = 1;
        //            objAddMasterVendorViewModel.IsActive = true;

        //            if (objAddMasterVendorViewModel.MasterAddressTypeId == null)
        //            {
        //                objAddMasterVendorViewModel.MasterAddressTypeId = 2;
        //            }

        //            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
        //            if (ModelState.IsValid)
        //            {
        //                string endpoint = apiBaseUrl + "MasterVendor";

        //                Task<string> HttpPostResponse = null;

        //                if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.SAVE)
        //                {
        //                    HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterVendorViewModel);
        //                    //Notification Message
        //                    //Session is used to store object
        //                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Insert Successfully!", ""));
        //                }
        //                else if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.UPDATE)
        //                {
        //                    endpoint = apiBaseUrl + "MasterVendor/" + objAddMasterVendorViewModel.MasterVendorId;
        //                    HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterVendorViewModel);

        //                    //Notification Message                       
        //                    //Session is used to store object
        //                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Vendor", "Master Vendor Update Successfully!", ""));
        //                }

        //                if (HttpPostResponse != null)
        //                {
        //                    objAddMasterVendorViewModel = JsonConvert.DeserializeObject<AddMasterVendorViewModel>(HttpPostResponse.Result);
        //                    _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterVendorViewModel)); ;
        //                    return RedirectToAction("Index");
        //                }
        //                else
        //                {
        //                    DropDownFillMethod();
        //                    ModelState.Clear();
        //                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //                    return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
        //                }
        //            }
        //            else
        //            {
        //                ModelState.Clear();
        //                if (ModelState.IsValid == false)
        //                {
        //                    ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
        //                }
        //                DropDownFillMethod();

        //                //Return View doesn't make a new requests, it just renders the view
        //                return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
        //            }
        //        }

        //        else if (SaveMasterVendor == null && AddUploadFile == "Add Upload File")
        //        {

        //            //// for trasaction view lists
        //            ///
        //            //List<ViewModels.TransactionVendorFileUpload.TransactionVendorFileUploadViewModel> objTransactionVendorFileUploadList = new List<ViewModels.TransactionVendorFileUpload.TransactionVendorFileUploadViewModel>();

        //            //if (objAddMasterVendorViewModel.TransactionVendorFileUploadList != null)
        //            //    objTransactionVendorFileUploadList = objAddMasterVendorViewModel.TransactionVendorFileUploadList;

        //            //ViewModels.TransactionVendorFileUpload.TransactionVendorFileUploadViewModel objTransactionVendorFileUploadViewModel = new ViewModels.TransactionVendorFileUpload.TransactionVendorFileUploadViewModel();

        //            //if (_UploadFile != null)
        //            //    objTransactionVendorFileUploadViewModel.UploadFile = UploadedImageFile(_UploadFile, objAddMasterVendorViewModel.UploadFile);

        //            //objTransactionVendorFileUploadViewModel.TransactionVendorFileName = objAddMasterVendorViewModel.TransactionVendorFileName;
        //            //objTransactionVendorFileUploadViewModel.Sequence = objAddMasterVendorViewModel.Sequence;

        //            //objTransactionVendorFileUploadViewModel.EnterById = 1;
        //            //objTransactionVendorFileUploadViewModel.EnterDate = DateTime.Now;
        //            //objTransactionVendorFileUploadViewModel.ModifiedById = 1;
        //            //objTransactionVendorFileUploadViewModel.ModifiedDate = DateTime.Now;
        //            //objTransactionVendorFileUploadViewModel.IsActive = true;

        //            //objTransactionVendorFileUploadList.Add(objTransactionVendorFileUploadViewModel);
        //            //objAddMasterVendorViewModel.TransactionVendorFileUploadList = objTransactionVendorFileUploadList.ToList();
        //            //// for trasaction view list
        //            ///



        //            //IEnumerable<ViewModels.TransactionVendorFileUpload.ViewTransactionVendorFileUploadViewModel> objViewTransactionVendorFileUploadList = null;

        //            List<ViewModels.TransactionVendorFileUpload.ViewTransactionVendorFileUploadViewModel> ViewTransactionVendorFileUploadViewModelList =new List<ViewModels.TransactionVendorFileUpload.ViewTransactionVendorFileUploadViewModel>();

        //            if (objAddMasterVendorViewModel.ViewTransactionVendorFileUploadList != null)

        //            {
        //                ViewTransactionVendorFileUploadViewModelList = objAddMasterVendorViewModel.ViewTransactionVendorFileUploadList;


        //                ViewModels.TransactionVendorFileUpload.ViewTransactionVendorFileUploadViewModel objViewTransactionVendorFileUploadViewModel = new ViewModels.TransactionVendorFileUpload.ViewTransactionVendorFileUploadViewModel();


        //                if (_UploadFile != null)
        //                    objViewTransactionVendorFileUploadViewModel.UploadFile = UploadedImageFile(_UploadFile, objAddMasterVendorViewModel.UploadFile);

        //                objViewTransactionVendorFileUploadViewModel.TransactionVendorFileName = objAddMasterVendorViewModel.TransactionVendorFileName;
        //                objViewTransactionVendorFileUploadViewModel.Sequence = objAddMasterVendorViewModel.Sequence;

        //                objViewTransactionVendorFileUploadViewModel.EnterById = 1;
        //                objViewTransactionVendorFileUploadViewModel.EnterDate = DateTime.Now;
        //                objViewTransactionVendorFileUploadViewModel.ModifiedById = 1;
        //                objViewTransactionVendorFileUploadViewModel.ModifiedDate = DateTime.Now;
        //                objViewTransactionVendorFileUploadViewModel.IsActive = true;

        //                ViewTransactionVendorFileUploadViewModelList.Add(objViewTransactionVendorFileUploadViewModel);

        //            }




        //            objAddMasterVendorViewModel.ViewTransactionVendorFileUploadList = ViewTransactionVendorFileUploadViewModelList.ToList();

        //            DropDownFillMethod();

        //            return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);



        //            //var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

        //            //if (ModelState.IsValid)
        //            //{
        //            //    string endpoint = apiBaseUrl  + "TransactionVendorFileUpload";

        //            //    Task<string> HttpPostResponse = null;

        //            //    if (objAddMasterVendorViewModel.Mode == CommonFunction.Mode.SAVE)
        //            //    {
        //            //        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterVendorViewModel);
        //            //        //Notification Message
        //            //        //Session is used to store object
        //            //        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Transaction Master Vender", "Transaction Vendor FileUpload Insert Successfully!", ""));
        //            //    }

        //            //    if (HttpPostResponse != null)
        //            //    {
        //            //        objAddMasterVendorViewModel = JsonConvert.DeserializeObject<ViewModels.MasterVendor.AddMasterVendorViewModel>(HttpPostResponse.Result);
        //            //        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyViewModel)); ;

        //            //        return RedirectToAction("AddMasterVendor", objAddMasterVendorViewModel);

        //            //    }
        //            //    else
        //            //    {
        //            //        DropDownFillMethod();
        //            //        ModelState.Clear();
        //            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //            //        return View("~/Views/Master/MasterVendor/AddMasterVendor.cshtml", objAddMasterVendorViewModel);
        //            //    }


        //            //}


        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
        //        string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
        //        string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

        //        _logger.LogError(ErrorMessage);
        //        return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
        //    }
        //    return new EmptyResult();
        //}

        [HttpPost]
        public IActionResult DeleteMasterVendor(long[] DeleteMasterVendorIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterVendors";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterVendorIds != null && DeleteMasterVendorIds.Length > 0)
                {
                    foreach (int DeleteMasterVendorId in DeleteMasterVendorIds)
                    {
                        endpoint = apiBaseUrl + "MasterVendor/" + DeleteMasterVendorId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Vendor Type", "Vendor Type Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterVendorViewModel)); ;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return RedirectToAction("Index");
                }
                // And finally, redirect to the action that lists the books
                // (let's assume it's Index)

            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
        }

        public IActionResult DeleteMasterVendorById(long MasterVendorId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterVendors";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterVendor/" + MasterVendorId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Vendor Type", "Vendor Type Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterVendorViewModel)); ;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return RedirectToAction("Index");
                }
                // And finally, redirect to the action that lists the books
                // (let's assume it's Index)

            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
        }

        public void DropDownFillMethod()
        {
            List<ViewModels.DropDownFill> objDesignationList = CommonFunction.DropDownFill("ADMasterDesignation", 0, "ALL", apiBaseUrl);
            ViewBag.DesignationList = new SelectList(objDesignationList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCompanyTypeList = CommonFunction.DropDownFill("ADMasterCompanyType", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyTypeList = new SelectList(objCompanyTypeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCurrencyList = CommonFunction.DropDownFill("ADMasterCurrency", 0, "ALL", apiBaseUrl);
            ViewBag.CurrencyList = new SelectList(objCurrencyList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objTimeZoneList = CommonFunction.DropDownFill("ADMasterTimeZone", 0, "ALL", apiBaseUrl);
            ViewBag.TimeZoneList = new SelectList(objTimeZoneList, "MasterId", "MasterName", "----select----");
        }

        private string UploadedImageFile(IFormFile _HttpFile, string OldFile)
        {
            string uniqueFileName = null;
            string DelMainImage = OldFile;

            if (_HttpFile != null)
            {
                var allowedExtensions = new[] { ".jpeg", ".png", ".gif", ".jpg",".pdf", ".doc", ".xlsx", ".txt" };
                string picExtenction = System.IO.Path.GetExtension(_HttpFile.FileName.ToLower());

                string uploadsFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "img/app-images");
                uniqueFileName = "Image_" + Guid.NewGuid().ToString() + picExtenction;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (DelMainImage != null)
                {
                    string DelPath = System.IO.Path.Combine(uploadsFolder, DelMainImage);
                    System.IO.FileInfo DelFile = new System.IO.FileInfo(DelPath);
                    if (DelFile.Exists)
                        DelFile.Delete();
                }

                if (allowedExtensions.Contains(picExtenction))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        _HttpFile.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }


    }
}
