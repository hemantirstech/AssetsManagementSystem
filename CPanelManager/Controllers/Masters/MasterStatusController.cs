using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterStatus;
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
    public class MasterStatusController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterStatusController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterStatusViewModel objIndexMasterStatusViewModel = new IndexMasterStatusViewModel();
                IEnumerable<MasterStatusViewModel> objMasterStatusViewModelList = null;

                string endpoint = apiBaseUrl + "MasterStatus";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterStatusViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterStatusViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterStatusViewModelList = Enumerable.Empty<MasterStatusViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterStatusViewModel.MasterStatusList = objMasterStatusViewModelList.OrderBy(a => a.StatusTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterStatus/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterStatusViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterStatusViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterStatusViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterStatusViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterStatus/Index.cshtml", objIndexMasterStatusViewModel);
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

        public IActionResult AddMasterStatus()
        {
            try
            {
                AddMasterStatusViewModel objAddMasterStatusViewModel = new AddMasterStatusViewModel();
                objAddMasterStatusViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterStatusViewModel.IsActive = true;
                objAddMasterStatusViewModel.MasterStatusId = CommonFunction.NextMasterId("ADMasterStatus", apiBaseUrl);
                objAddMasterStatusViewModel.MasterStatusId = 0;
                objAddMasterStatusViewModel.StatusTitle = "";
                objAddMasterStatusViewModel.StatusCode = "";
                objAddMasterStatusViewModel.MasterColorId = 0;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterStatus/AddMasterStatus.cshtml", objAddMasterStatusViewModel);
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
        public IActionResult ViewMasterStatus(long MasterStatusId)
        {
            try
            {
                AddMasterStatusViewModel objAddMasterStatusViewModel = null;
                string endpoint = apiBaseUrl + "MasterStatus/" + MasterStatusId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterStatusViewModel = JsonConvert.DeserializeObject<AddMasterStatusViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterStatusViewModel = new AddMasterStatusViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterStatusViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterStatus/AddMasterStatus.cshtml", objAddMasterStatusViewModel);
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
        public IActionResult SaveMasterStatus(AddMasterStatusViewModel objAddMasterStatusViewModel)
        {
            try
            {
                objAddMasterStatusViewModel.EnterById = 1;
                objAddMasterStatusViewModel.EnterDate = DateTime.Now;
                objAddMasterStatusViewModel.ModifiedById = 1;
                objAddMasterStatusViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterStatusViewModel.IsActive == null)
                {
                    objAddMasterStatusViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterStatus";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterStatusViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterStatusViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Status", "Master Status Insert Successfully!", ""));
                    }
                    else if (objAddMasterStatusViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterStatus/" + objAddMasterStatusViewModel.MasterStatusId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterStatusViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Status", "Master Status Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterStatusViewModel = JsonConvert.DeserializeObject<AddMasterStatusViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterStatus/AddMasterStatus.cshtml", objAddMasterStatusViewModel);
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
                    return View("~/Views/Master/MasterStatus/AddMasterStatus.cshtml", objAddMasterStatusViewModel);
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
        public IActionResult DeleteMasterStatus(int[] DeleteMasterStatusIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterStatus";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterStatusIds != null && DeleteMasterStatusIds.Length > 0)
                {
                    foreach (long DeleteMasterStatusId in DeleteMasterStatusIds)
                    {
                        endpoint = apiBaseUrl + "MasterStatus/" + DeleteMasterStatusId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Status", "Master Status Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterStatusViewModel)); ;
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
            List<ViewModels.DropDownFill> objStatusList = CommonFunction.DropDownFill("ADMasterColor", 0, "ALL", apiBaseUrl);
            ViewBag.ColorList = new SelectList(objStatusList, "MasterId", "MasterName", "----select----");
        }
    }
}
