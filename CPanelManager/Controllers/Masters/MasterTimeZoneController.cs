using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterTimeZone;
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
    public class MasterTimeZoneController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterTimeZoneController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterTimeZoneViewModel objIndexMasterTimeZoneViewModel = new IndexMasterTimeZoneViewModel();
                IEnumerable<MasterTimeZoneViewModel> objMasterTimeZoneViewModelList = null;

                string endpoint = apiBaseUrl + "MasterTimeZones";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterTimeZoneViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterTimeZoneViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterTimeZoneViewModelList = Enumerable.Empty<MasterTimeZoneViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterTimeZoneViewModel.MasterTimeZoneList = objMasterTimeZoneViewModelList.OrderBy(a => a.TimeZoneTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterTimeZone/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterTimeZoneViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterTimeZoneViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterTimeZoneViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterTimeZoneViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterTimeZone/Index.cshtml", objIndexMasterTimeZoneViewModel);
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

        public IActionResult AddMasterTimeZone()
        {
            try
            {
                AddMasterTimeZoneViewModel objAddMasterTimeZoneViewModel = new AddMasterTimeZoneViewModel();
                objAddMasterTimeZoneViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterTimeZoneViewModel.IsActive = true;
                objAddMasterTimeZoneViewModel.MasterTimeZoneId = CommonFunction.NextMasterId("ADMasterTimeZone", apiBaseUrl);
                objAddMasterTimeZoneViewModel.MasterTimeZoneId = 0;
                objAddMasterTimeZoneViewModel.TimeZoneTitle = "";
                objAddMasterTimeZoneViewModel.TimeZoneOffset = "";
                objAddMasterTimeZoneViewModel.HasDst = true;
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterTimeZone/AddMasterTimeZone.cshtml", objAddMasterTimeZoneViewModel);
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
        public IActionResult ViewMasterTimeZone(long MasterTimeZoneId)
        {
            try
            {
                AddMasterTimeZoneViewModel objAddMasterTimeZoneViewModel = null;
                string endpoint = apiBaseUrl + "MasterTimeZones/" + MasterTimeZoneId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterTimeZoneViewModel = JsonConvert.DeserializeObject<AddMasterTimeZoneViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterTimeZoneViewModel = new AddMasterTimeZoneViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterTimeZoneViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterTimeZone/AddMasterTimeZone.cshtml", objAddMasterTimeZoneViewModel);
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
        public IActionResult SaveMasterTimeZone(AddMasterTimeZoneViewModel objAddMasterTimeZoneViewModel)
        {
            try
            {
                objAddMasterTimeZoneViewModel.EnterById = 1;
                objAddMasterTimeZoneViewModel.EnterDate = DateTime.Now;
                objAddMasterTimeZoneViewModel.ModifiedById = 1;
                objAddMasterTimeZoneViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterTimeZoneViewModel.IsActive == null)
                {
                    objAddMasterTimeZoneViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterTimeZones";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterTimeZoneViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterTimeZoneViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master TimeZone", "Master TimeZone Insert Successfully!", ""));
                    }
                    else if (objAddMasterTimeZoneViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterTimeZones/" + objAddMasterTimeZoneViewModel.MasterTimeZoneId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterTimeZoneViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master TimeZone", "Master TimeZone Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterTimeZoneViewModel = JsonConvert.DeserializeObject<AddMasterTimeZoneViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterTimeZone/AddMasterTimeZone.cshtml", objAddMasterTimeZoneViewModel);
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
                    return View("~/Views/Master/MasterTimeZone/AddMasterTimeZone.cshtml", objAddMasterTimeZoneViewModel);
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
        public IActionResult DeleteMasterTimeZone(int[] DeleteMasterTimeZoneIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterTimeZones";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterTimeZoneIds != null && DeleteMasterTimeZoneIds.Length > 0)
                {
                    foreach (long DeleteMasterTimeZoneId in DeleteMasterTimeZoneIds)
                    {
                        endpoint = apiBaseUrl + "MasterTimeZones/" + DeleteMasterTimeZoneId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master TimeZone", "Master TimeZone Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterTimeZoneViewModel)); ;
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
    }
}
