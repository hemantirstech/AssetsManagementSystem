using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCompany;
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
    public class MasterCompanyController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCompanyController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterCompanyViewModel objIndexMasterCompanyViewModel = new IndexMasterCompanyViewModel();
                IEnumerable<MasterCompanyViewModel> objMasterCompanyViewModellList = null;
                string endpoint = apiBaseUrl + "MasterCompanies";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCompanyViewModellList = JsonConvert.DeserializeObject<IEnumerable<MasterCompanyViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCompanyViewModellList = Enumerable.Empty<MasterCompanyViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCompanyViewModel.MasterCompanyList = objMasterCompanyViewModellList.ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCompany/Index");

                if(objValidateAccountViewModel!=null)
                {
                    objIndexMasterCompanyViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCompanyViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCompanyViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCompanyViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################
                
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCompany/Index.cshtml", objIndexMasterCompanyViewModel);
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

        public IActionResult AddMasterCompany()
        {
            try
            {
                AddMasterCompanyViewModel objAddMasterCompanyViewModel = new AddMasterCompanyViewModel();
                objAddMasterCompanyViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCompanyViewModel.IsActive = true;
                objAddMasterCompanyViewModel.MasterDesignationId = 1;
                objAddMasterCompanyViewModel.MasterCompanyTypeId = 1;
                objAddMasterCompanyViewModel.MasterTimeZoneId = 91;
                objAddMasterCompanyViewModel.MasterCurrencyId = 47;
                objAddMasterCompanyViewModel.MasterCountryId = 101;
                objAddMasterCompanyViewModel.MasterCompanyId = CommonFunction.NextMasterId("ADMasterCompany", apiBaseUrl);
                objAddMasterCompanyViewModel.MasterCompanyId = 0;
                objAddMasterCompanyViewModel.DateofRegistration = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCompany/AddMasterCompany.cshtml", objAddMasterCompanyViewModel);
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
        public IActionResult ViewMasterCompany(long MasterCompanyId)
        {
            try
            {
                AddMasterCompanyViewModel objAddMasterCompanyViewModel = null;
                string endpoint = apiBaseUrl + "MasterCompanies/" + MasterCompanyId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCompanyViewModel = JsonConvert.DeserializeObject<AddMasterCompanyViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCompanyViewModel = new AddMasterCompanyViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();

                objAddMasterCompanyViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterCompany/AddMasterCompany.cshtml", objAddMasterCompanyViewModel);
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
        public IActionResult SaveMasterCompany(AddMasterCompanyViewModel objAddMasterCompanyViewModel, IFormFile _CompanyLogo)
        {
            try
            {
                //for main image Logo
                if (_CompanyLogo != null)
                    objAddMasterCompanyViewModel.CompanyLogo = UploadedImageFile(_CompanyLogo, objAddMasterCompanyViewModel.CompanyLogo);

                objAddMasterCompanyViewModel.EnterById = 1;
                objAddMasterCompanyViewModel.EnterDate = DateTime.Now;
                objAddMasterCompanyViewModel.ModifiedById = 1;
                objAddMasterCompanyViewModel.ModifiedDate = DateTime.Now;
                objAddMasterCompanyViewModel.MasterAddressTypeId = 1;
                objAddMasterCompanyViewModel.IsActive = true;

                if (objAddMasterCompanyViewModel.MasterAddressTypeId == null)
                {
                    objAddMasterCompanyViewModel.MasterAddressTypeId = 2;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCompanies";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCompanyViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCompanyViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Company", "Master Company Insert Successfully!", ""));
                    }
                    else if (objAddMasterCompanyViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCompanies/" + objAddMasterCompanyViewModel.MasterCompanyId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCompanyViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Company", "Master Company Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCompanyViewModel = JsonConvert.DeserializeObject<AddMasterCompanyViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterCompany/AddMasterCompany.cshtml", objAddMasterCompanyViewModel);
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
                    return View("~/Views/Master/MasterCompany/AddMasterCompany.cshtml", objAddMasterCompanyViewModel);
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

        [HttpPost]
        public IActionResult DeleteMasterCompany(long[] DeleteMasterCompanyIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCompanys";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCompanyIds != null && DeleteMasterCompanyIds.Length > 0)
                {
                    foreach (int DeleteMasterCompanyId in DeleteMasterCompanyIds)
                    {
                        endpoint = apiBaseUrl + "MasterCompanies/" + DeleteMasterCompanyId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "GenCode Type", "Gencode Type Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyViewModel)); ;
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

        public IActionResult DeleteMasterCompanyById(long MasterCompanyId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCompanys";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterCompanies/" + MasterCompanyId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "GenCode Type", "Gencode Type Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyViewModel)); ;
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
            ViewBag.CurrencyList = new SelectList(objCurrencyList.OrderBy(a=>a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objTimeZoneList = CommonFunction.DropDownFill("ADMasterTimeZone", 0, "ALL", apiBaseUrl);
            ViewBag.TimeZoneList = new SelectList(objTimeZoneList, "MasterId", "MasterName", "----select----");
        }

        private string UploadedImageFile(IFormFile _HttpFile, string OldFile)
        {
            string uniqueFileName = null;
            string DelMainImage = OldFile;

            if (_HttpFile != null)
            {
                var allowedExtensions = new[] { ".jpeg", ".png", ".gif", ".jpg" };
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
