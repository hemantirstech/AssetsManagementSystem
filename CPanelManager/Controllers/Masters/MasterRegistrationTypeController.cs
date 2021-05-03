using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterRegistrationType;
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

namespace CPanelManager.Controllers.Masters
{
    public class MasterRegistrationTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterRegistrationTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterRegistrationTypeViewModel objIndexMasterRegistrationTypeViewModel = new IndexMasterRegistrationTypeViewModel();
                IEnumerable<MasterRegistrationTypeViewModel> objMasterRegistrationTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterRegistrationTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterRegistrationTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterRegistrationTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterRegistrationTypeViewModelList = Enumerable.Empty<MasterRegistrationTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterRegistrationTypeViewModel.MasterRegistrationTypeList = objMasterRegistrationTypeViewModelList.OrderBy(a => a.MasterRegistrationTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterRegistrationType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterRegistrationTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterRegistrationTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterRegistrationTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterRegistrationTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterRegistrationType/Index.cshtml", objIndexMasterRegistrationTypeViewModel);
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

        public IActionResult AddMasterRegistrationType()
        {
            try
            {
                AddMasterRegistrationTypeViewModel objAddMasterRegistrationTypeViewModel = new AddMasterRegistrationTypeViewModel();
                objAddMasterRegistrationTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterRegistrationTypeViewModel.IsActive = true;
                objAddMasterRegistrationTypeViewModel.MasterRegistrationTypeId = CommonFunction.NextMasterId("ADMasterRegistrationType", apiBaseUrl);
                objAddMasterRegistrationTypeViewModel.MasterRegistrationTypeId = 0;
                objAddMasterRegistrationTypeViewModel.MasterRegistrationTypeTitle = "";
                objAddMasterRegistrationTypeViewModel.MasterRegistrationCode = "";
                objAddMasterRegistrationTypeViewModel.MasterRegistrationExpertType = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterRegistrationType/AddMasterRegistrationType.cshtml", objAddMasterRegistrationTypeViewModel);
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
        public IActionResult ViewMasterRegistrationType(long MasterRegistrationTypeId)
        {
            try
            {
                AddMasterRegistrationTypeViewModel objAddMasterRegistrationTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterRegistrationTypes/" + MasterRegistrationTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterRegistrationTypeViewModel = JsonConvert.DeserializeObject<AddMasterRegistrationTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterRegistrationTypeViewModel = new AddMasterRegistrationTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterRegistrationTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterRegistrationType/AddMasterRegistrationType.cshtml", objAddMasterRegistrationTypeViewModel);
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
        public IActionResult SaveMasterRegistrationType(AddMasterRegistrationTypeViewModel objAddMasterRegistrationTypeViewModel)
        {
            try
            {
                objAddMasterRegistrationTypeViewModel.EnterById = 1;
                objAddMasterRegistrationTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterRegistrationTypeViewModel.ModifiedById = 1;
                objAddMasterRegistrationTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterRegistrationTypeViewModel.IsActive == null)
                {
                    objAddMasterRegistrationTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterRegistrationTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterRegistrationTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterRegistrationTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master RegistrationType", "Master RegistrationType Insert Successfully!", ""));
                    }
                    else if (objAddMasterRegistrationTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterRegistrationTypes/" + objAddMasterRegistrationTypeViewModel.MasterRegistrationTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterRegistrationTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master RegistrationType", "Master RegistrationType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterRegistrationTypeViewModel = JsonConvert.DeserializeObject<AddMasterRegistrationTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterRegistrationType/AddMasterRegistrationType.cshtml", objAddMasterRegistrationTypeViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }

                    //DropDownFillMethod();

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Master/MasterRegistrationType/AddMasterRegistrationType.cshtml", objAddMasterRegistrationTypeViewModel);
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
        public IActionResult DeleteMasterRegistrationType(int[] DeleteMasterRegistrationTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterRegistrationTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterRegistrationTypeIds != null && DeleteMasterRegistrationTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterRegistrationTypeId in DeleteMasterRegistrationTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterRegistrationTypes/" + DeleteMasterRegistrationTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master RegistrationType", "Master RegistrationType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterRegistrationTypeViewModel)); ;
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

        //public void DropDownFillMethod()
        //{
        //    List<ViewModels.DropDownFill> objRegistrationTypeList = CommonFunction.DropDownFill("ADMasterRegistrationType", 0, "ALL", apiBaseUrl);
        //    ViewBag.RegistrationTypeList = new SelectList(objRegistrationTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
