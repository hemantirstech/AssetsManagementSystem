using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterEmployeeStatus;
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
    public class MasterEmployeeStatusController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterEmployeeStatusController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterEmployeeStatusViewModel objIndexMasterEmployeeStatusViewModel = new IndexMasterEmployeeStatusViewModel();
                IEnumerable<MasterEmployeeStatusViewModel> objMasterEmployeeStatusViewModelList = null;

                string endpoint = apiBaseUrl + "MasterEmployeeStatus";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterEmployeeStatusViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterEmployeeStatusViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterEmployeeStatusViewModelList = Enumerable.Empty<MasterEmployeeStatusViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterEmployeeStatusViewModel.MasterEmployeeStatusList = objMasterEmployeeStatusViewModelList.OrderBy(a => a.EmployeeStatusTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterEmployeeStatus/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterEmployeeStatusViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterEmployeeStatusViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterEmployeeStatusViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterEmployeeStatusViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployeeStatus/Index.cshtml", objIndexMasterEmployeeStatusViewModel);
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

        public IActionResult AddMasterEmployeeStatus()
        {
            try
            {
                AddMasterEmployeeStatusViewModel objAddMasterEmployeeStatusViewModel = new AddMasterEmployeeStatusViewModel();
                objAddMasterEmployeeStatusViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterEmployeeStatusViewModel.IsActive = true;
                objAddMasterEmployeeStatusViewModel.MasterEmployeeStatusId = CommonFunction.NextMasterId("ADMasterEmployeeStatus", apiBaseUrl);
                objAddMasterEmployeeStatusViewModel.MasterEmployeeStatusId = 0;
                objAddMasterEmployeeStatusViewModel.EmployeeStatusTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployeeStatus/AddMasterEmployeeStatus.cshtml", objAddMasterEmployeeStatusViewModel);
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
        public IActionResult ViewMasterEmployeeStatus(long MasterEmployeeStatusId)
        {
            try
            {
                AddMasterEmployeeStatusViewModel objAddMasterEmployeeStatusViewModel = null;
                string endpoint = apiBaseUrl + "MasterEmployeeStatus/" + MasterEmployeeStatusId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterEmployeeStatusViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeStatusViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterEmployeeStatusViewModel = new AddMasterEmployeeStatusViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterEmployeeStatusViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterEmployeeStatus/AddMasterEmployeeStatus.cshtml", objAddMasterEmployeeStatusViewModel);
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
        public IActionResult SaveMasterEmployeeStatus(AddMasterEmployeeStatusViewModel objAddMasterEmployeeStatusViewModel)
        {
            try
            {
                objAddMasterEmployeeStatusViewModel.EnterById = 1;
                objAddMasterEmployeeStatusViewModel.EnterDate = DateTime.Now;
                objAddMasterEmployeeStatusViewModel.ModifiedById = 1;
                objAddMasterEmployeeStatusViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterEmployeeStatusViewModel.IsActive == null)
                {
                    objAddMasterEmployeeStatusViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterEmployeeStatus";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterEmployeeStatusViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterEmployeeStatusViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master EmployeeStatus", "Master EmployeeStatus Insert Successfully!", ""));
                    }
                    else if (objAddMasterEmployeeStatusViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterEmployeeStatus/" + objAddMasterEmployeeStatusViewModel.MasterEmployeeStatusId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterEmployeeStatusViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master EmployeeStatus", "Master EmployeeStatus Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterEmployeeStatusViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeStatusViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeStatusViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterEmployeeStatus/AddMasterEmployeeStatus.cshtml", objAddMasterEmployeeStatusViewModel);
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
                    return View("~/Views/Master/MasterEmployeeStatus/AddMasterEmployeeStatus.cshtml", objAddMasterEmployeeStatusViewModel);
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
        public IActionResult DeleteMasterEmployeeStatus(int[] DeleteMasterEmployeeStatusIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterEmployeeStatus";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterEmployeeStatusIds != null && DeleteMasterEmployeeStatusIds.Length > 0)
                {
                    foreach (long DeleteMasterEmployeeStatusId in DeleteMasterEmployeeStatusIds)
                    {
                        endpoint = apiBaseUrl + "MasterEmployeeStatus/" + DeleteMasterEmployeeStatusId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master EmployeeStatus", "Master EmployeeStatus Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeStatusViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objEmployeeStatusList = CommonFunction.DropDownFill("ADMasterEmployeeStatus", 0, "ALL", apiBaseUrl);
        //    ViewBag.EmployeeStatusList = new SelectList(objEmployeeStatusList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
