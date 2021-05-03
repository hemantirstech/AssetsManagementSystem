using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterIndustryType;
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
    public class MasterIndustryTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterIndustryTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterIndustryTypeViewModel objIndexMasterIndustryTypeViewModel = new IndexMasterIndustryTypeViewModel();
                IEnumerable<MasterIndustryTypeViewModel> objMasterIndustryTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterIndustryTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterIndustryTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterIndustryTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterIndustryTypeViewModelList = Enumerable.Empty<MasterIndustryTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterIndustryTypeViewModel.MasterIndustryTypeList = objMasterIndustryTypeViewModelList.OrderBy(a => a.IndustryTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterIndustryType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterIndustryTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterIndustryTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterIndustryTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterIndustryTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustryType/Index.cshtml", objIndexMasterIndustryTypeViewModel);
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

        public IActionResult AddMasterIndustryType()
        {
            try
            {
                AddMasterIndustryTypeViewModel objAddMasterIndustryTypeViewModel = new AddMasterIndustryTypeViewModel();
                objAddMasterIndustryTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterIndustryTypeViewModel.IsActive = true;
                objAddMasterIndustryTypeViewModel.MasterIndustryTypeId = CommonFunction.NextMasterId("ADMasterIndustryType", apiBaseUrl);
                objAddMasterIndustryTypeViewModel.MasterIndustryTypeId = 0;
                objAddMasterIndustryTypeViewModel.MasterIndustryGroupId = 1;
                objAddMasterIndustryTypeViewModel.IndustryTypeTitle = "";
                objAddMasterIndustryTypeViewModel.IndustryTypeCode = "";
                objAddMasterIndustryTypeViewModel.IndustryTypeDescription = "";
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustryType/AddMasterIndustryType.cshtml", objAddMasterIndustryTypeViewModel);
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
        public IActionResult ViewMasterIndustryType(long MasterIndustryTypeId)
        {
            try
            {
                AddMasterIndustryTypeViewModel objAddMasterIndustryTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterIndustryTypes/" + MasterIndustryTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterIndustryTypeViewModel = JsonConvert.DeserializeObject<AddMasterIndustryTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterIndustryTypeViewModel = new AddMasterIndustryTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterIndustryTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterIndustryType/AddMasterIndustryType.cshtml", objAddMasterIndustryTypeViewModel);
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
        public IActionResult SaveMasterIndustryType(AddMasterIndustryTypeViewModel objAddMasterIndustryTypeViewModel)
        {
            try
            {
                objAddMasterIndustryTypeViewModel.EnterById = 1;
                objAddMasterIndustryTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterIndustryTypeViewModel.ModifiedById = 1;
                objAddMasterIndustryTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterIndustryTypeViewModel.IsActive == null)
                {
                    objAddMasterIndustryTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterIndustryTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterIndustryTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterIndustryTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustryType", "Master IndustryType Insert Successfully!", ""));
                    }
                    else if (objAddMasterIndustryTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterIndustryTypes/" + objAddMasterIndustryTypeViewModel.MasterIndustryTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterIndustryTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustryType", "Master IndustryType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterIndustryTypeViewModel = JsonConvert.DeserializeObject<AddMasterIndustryTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterIndustryType/AddMasterIndustryType.cshtml", objAddMasterIndustryTypeViewModel);
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
                    return View("~/Views/Master/MasterIndustryType/AddMasterIndustryType.cshtml", objAddMasterIndustryTypeViewModel);
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
        public IActionResult DeleteMasterIndustryType(int[] DeleteMasterIndustryTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterIndustryTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterIndustryTypeIds != null && DeleteMasterIndustryTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterIndustryTypeId in DeleteMasterIndustryTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterIndustryTypes/" + DeleteMasterIndustryTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master IndustryType", "Master IndustryType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterIndustryTypeViewModel)); ;
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
            List<ViewModels.DropDownFill> objIndustryGroupList = CommonFunction.DropDownFill("ADMasterIndustryGroup", 0, "ALL", apiBaseUrl);
            ViewBag.IndustryGroupList = new SelectList(objIndustryGroupList, "MasterId", "MasterName", "----select----");
        }
    }
}
