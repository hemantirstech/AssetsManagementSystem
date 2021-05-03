using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCompanyType;
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
    public class MasterCompanyTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCompanyTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterCompanyTypeViewModel objIndexMasterCompanyTypeViewModel = new IndexMasterCompanyTypeViewModel();
                IEnumerable<MasterCompanyTypeViewModel> objMasterCompanyTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterCompanyTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCompanyTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCompanyTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCompanyTypeViewModelList = Enumerable.Empty<MasterCompanyTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCompanyTypeViewModel.MasterCompanyTypeList = objMasterCompanyTypeViewModelList.OrderBy(a => a.CompanyTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCompanyType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterCompanyTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCompanyTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCompanyTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCompanyTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCompanyType/Index.cshtml", objIndexMasterCompanyTypeViewModel);
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

        public IActionResult AddMasterCompanyType()
        {
            try
            {
                AddMasterCompanyTypeViewModel objAddMasterCompanyTypeViewModel = new AddMasterCompanyTypeViewModel();
                objAddMasterCompanyTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCompanyTypeViewModel.IsActive = true;
                objAddMasterCompanyTypeViewModel.MasterCompanyTypeId = CommonFunction.NextMasterId("ADMasterCompanyType", apiBaseUrl);
                objAddMasterCompanyTypeViewModel.MasterCompanyTypeId = 0;
                objAddMasterCompanyTypeViewModel.CompanyTypeTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCompanyType/AddMasterCompanyType.cshtml", objAddMasterCompanyTypeViewModel);
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
        public IActionResult ViewMasterCompanyType(long MasterCompanyTypeId)
        {
            try
            {
                AddMasterCompanyTypeViewModel objAddMasterCompanyTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterCompanyTypes/" + MasterCompanyTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCompanyTypeViewModel = JsonConvert.DeserializeObject<AddMasterCompanyTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCompanyTypeViewModel = new AddMasterCompanyTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterCompanyTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterCompanyType/AddMasterCompanyType.cshtml", objAddMasterCompanyTypeViewModel);
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
        public IActionResult SaveMasterCompanyType(AddMasterCompanyTypeViewModel objAddMasterCompanyTypeViewModel)
        {
            try
            {
                objAddMasterCompanyTypeViewModel.EnterById = 1;
                objAddMasterCompanyTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterCompanyTypeViewModel.ModifiedById = 1;
                objAddMasterCompanyTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterCompanyTypeViewModel.IsActive == null)
                {
                    objAddMasterCompanyTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCompanyTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCompanyTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCompanyTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master CompanyType", "Master CompanyType Insert Successfully!", ""));
                    }
                    else if (objAddMasterCompanyTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCompanyTypes/" + objAddMasterCompanyTypeViewModel.MasterCompanyTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCompanyTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master CompanyType", "Master CompanyType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCompanyTypeViewModel = JsonConvert.DeserializeObject<AddMasterCompanyTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterCompanyType/AddMasterCompanyType.cshtml", objAddMasterCompanyTypeViewModel);
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
                    return View("~/Views/Master/MasterCompanyType/AddMasterCompanyType.cshtml", objAddMasterCompanyTypeViewModel);
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
        public IActionResult DeleteMasterCompanyType(int[] DeleteMasterCompanyTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCompanyTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCompanyTypeIds != null && DeleteMasterCompanyTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterCompanyTypeId in DeleteMasterCompanyTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterCompanyTypes/" + DeleteMasterCompanyTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master CompanyType", "Master CompanyType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objCompanyTypeList = CommonFunction.DropDownFill("ADMasterCompanyType", 0, "ALL", apiBaseUrl);
        //    ViewBag.CompanyTypeList = new SelectList(objCompanyTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
