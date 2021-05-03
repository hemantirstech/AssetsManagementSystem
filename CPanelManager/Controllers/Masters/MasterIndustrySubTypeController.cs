using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterIndustrySubType;
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
    public class MasterIndustrySubTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterIndustrySubTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterIndustrySubTypeViewModel objIndexMasterIndustrySubTypeViewModel = new IndexMasterIndustrySubTypeViewModel();
                IEnumerable<MasterIndustrySubTypeViewModel> objMasterIndustrySubTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterIndustrySubTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterIndustrySubTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterIndustrySubTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterIndustrySubTypeViewModelList = Enumerable.Empty<MasterIndustrySubTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterIndustrySubTypeViewModel.MasterIndustrySubTypeList = objMasterIndustrySubTypeViewModelList.OrderBy(a => a.IndustrySubTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterIndustrySubType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterIndustrySubTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterIndustrySubTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterIndustrySubTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterIndustrySubTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustrySubType/Index.cshtml", objIndexMasterIndustrySubTypeViewModel);
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

        public IActionResult AddMasterIndustrySubType()
        {
            try
            {
                AddMasterIndustrySubTypeViewModel objAddMasterIndustrySubTypeViewModel = new AddMasterIndustrySubTypeViewModel();
                objAddMasterIndustrySubTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterIndustrySubTypeViewModel.IsActive = true;
                objAddMasterIndustrySubTypeViewModel.MasterIndustrySubTypeId = CommonFunction.NextMasterId("ADMasterIndustrySubType", apiBaseUrl);
                objAddMasterIndustrySubTypeViewModel.MasterIndustrySubTypeId = 0;
                objAddMasterIndustrySubTypeViewModel.MasterIndustryTypeId = 0;
                objAddMasterIndustrySubTypeViewModel.IndustrySubTypeTitle = "";
                objAddMasterIndustrySubTypeViewModel.IndustrySubTypeCode = "";
                objAddMasterIndustrySubTypeViewModel.IndustrySubTypeDescription = "";
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustrySubType/AddMasterIndustrySubType.cshtml", objAddMasterIndustrySubTypeViewModel);
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
        public IActionResult ViewMasterIndustrySubType(long MasterIndustrySubTypeId)
        {
            try
            {
                AddMasterIndustrySubTypeViewModel objAddMasterIndustrySubTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterIndustrySubTypes/" + MasterIndustrySubTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterIndustrySubTypeViewModel = JsonConvert.DeserializeObject<AddMasterIndustrySubTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterIndustrySubTypeViewModel = new AddMasterIndustrySubTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterIndustrySubTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterIndustrySubType/AddMasterIndustrySubType.cshtml", objAddMasterIndustrySubTypeViewModel);
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
        public IActionResult SaveMasterIndustrySubType(AddMasterIndustrySubTypeViewModel objAddMasterIndustrySubTypeViewModel)
        {
            try
            {
                objAddMasterIndustrySubTypeViewModel.EnterById = 1;
                objAddMasterIndustrySubTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterIndustrySubTypeViewModel.ModifiedById = 1;
                objAddMasterIndustrySubTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterIndustrySubTypeViewModel.IsActive == null)
                {
                    objAddMasterIndustrySubTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterIndustrySubTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterIndustrySubTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterIndustrySubTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustrySubType", "Master IndustrySubType Insert Successfully!", ""));
                    }
                    else if (objAddMasterIndustrySubTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterIndustrySubTypes/" + objAddMasterIndustrySubTypeViewModel.MasterIndustrySubTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterIndustrySubTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustrySubType", "Master IndustrySubType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterIndustrySubTypeViewModel = JsonConvert.DeserializeObject<AddMasterIndustrySubTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterIndustrySubType/AddMasterIndustrySubType.cshtml", objAddMasterIndustrySubTypeViewModel);
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
                    return View("~/Views/Master/MasterIndustrySubType/AddMasterIndustrySubType.cshtml", objAddMasterIndustrySubTypeViewModel);
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
        public IActionResult DeleteMasterIndustrySubType(int[] DeleteMasterIndustrySubTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterIndustrySubTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterIndustrySubTypeIds != null && DeleteMasterIndustrySubTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterIndustrySubTypeId in DeleteMasterIndustrySubTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterIndustrySubTypes/" + DeleteMasterIndustrySubTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master IndustrySubType", "Master IndustrySubType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterIndustrySubTypeViewModel)); ;
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
            List<ViewModels.DropDownFill> objIndustryTypeList = CommonFunction.DropDownFill("ADMasterIndustryType", 0, "ALL", apiBaseUrl);
            ViewBag.IndustryTypeList = new SelectList(objIndustryTypeList, "MasterId", "MasterName", "----select----");
        }
    }
}
