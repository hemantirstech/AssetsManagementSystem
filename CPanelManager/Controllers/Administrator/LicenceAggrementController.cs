using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.LicenceAggrement;
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
    public class LicenceAggrementController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LicenceAggrementController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewLicence()
        {
            try
            {
                ViewLicenceAggrementViewModel objViewLicenceAggrementViewModel = null;
                string endpoint = apiBaseUrl + "MasterCompanies/1";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objViewLicenceAggrementViewModel = JsonConvert.DeserializeObject<ViewLicenceAggrementViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objViewLicenceAggrementViewModel = new ViewLicenceAggrementViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objViewLicenceAggrementViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Administrator/LicenceAggrement/ViewLicence.cshtml", objViewLicenceAggrementViewModel);
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

        public IActionResult SaveLicence(ViewLicenceAggrementViewModel objViewLicenceAggrementViewModel, IFormFile _CompanyLogo)
        {
            try
            {
                //for main image Logo
                if (_CompanyLogo != null)
                    objViewLicenceAggrementViewModel.CompanyLogo = UploadedImageFile(_CompanyLogo, objViewLicenceAggrementViewModel.CompanyLogo);

                objViewLicenceAggrementViewModel.EnterById = CommonFunction.UserAuthentication(this.HttpContext);
                objViewLicenceAggrementViewModel.EnterDate = DateTime.Now;
                objViewLicenceAggrementViewModel.ModifiedById = CommonFunction.UserAuthentication(this.HttpContext);
                objViewLicenceAggrementViewModel.ModifiedDate = DateTime.Now;
                objViewLicenceAggrementViewModel.MasterAddressTypeId = 1;
                objViewLicenceAggrementViewModel.IsActive = true;

                if (objViewLicenceAggrementViewModel.MasterAddressTypeId == null)
                {
                    objViewLicenceAggrementViewModel.MasterAddressTypeId = 2;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCompanies";

                    Task<string> HttpPostResponse = null;

                    if (objViewLicenceAggrementViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCompanies/" + objViewLicenceAggrementViewModel.MasterCompanyId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objViewLicenceAggrementViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Licence Aggrement", "Licence Aggrement Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objViewLicenceAggrementViewModel = JsonConvert.DeserializeObject<ViewLicenceAggrementViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("ViewLicence");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Administrator/LicenceAggrement/ViewLicence.cshtml", objViewLicenceAggrementViewModel);
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
                    return View("~/Views/Administrator/LicenceAggrement/ViewLicence.cshtml", objViewLicenceAggrementViewModel);
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
