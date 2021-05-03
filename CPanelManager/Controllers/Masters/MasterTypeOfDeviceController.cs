using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterTypeOfDevice;
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
    public class MasterTypeOfDeviceController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterTypeOfDeviceController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterTypeOfDeviceViewModel objIndexMasterTypeOfDeviceViewModel = new IndexMasterTypeOfDeviceViewModel();
                IEnumerable<MasterTypeOfDeviceViewModel> objMasterTypeOfDeviceViewModelList = null;

                string endpoint = apiBaseUrl + "MasterTypeOfDevices";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterTypeOfDeviceViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterTypeOfDeviceViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterTypeOfDeviceViewModelList = Enumerable.Empty<MasterTypeOfDeviceViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterTypeOfDeviceViewModel.MasterTypeOfDeviceList = objMasterTypeOfDeviceViewModelList.OrderBy(a => a.TypeOfDeviceTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterTypeOfDevice/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterTypeOfDeviceViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterTypeOfDeviceViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterTypeOfDeviceViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterTypeOfDeviceViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterTypeOfDevice/Index.cshtml", objIndexMasterTypeOfDeviceViewModel);
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

        public IActionResult AddMasterTypeOfDevice()
        {
            try
            {
                AddMasterTypeOfDeviceViewModel objAddMasterTypeOfDeviceViewModel = new AddMasterTypeOfDeviceViewModel();
                objAddMasterTypeOfDeviceViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterTypeOfDeviceViewModel.IsActive = true;
                objAddMasterTypeOfDeviceViewModel.MasterTypeOfDeviceId = CommonFunction.NextMasterId("ADMasterTypeOfDevice", apiBaseUrl);
                objAddMasterTypeOfDeviceViewModel.MasterTypeOfDeviceId = 0;
                objAddMasterTypeOfDeviceViewModel.TypeOfDeviceTitle = "";
                objAddMasterTypeOfDeviceViewModel.TypeOfDeviceName = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterTypeOfDevice/AddMasterTypeOfDevice.cshtml", objAddMasterTypeOfDeviceViewModel);
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
        public IActionResult ViewMasterTypeOfDevice(long MasterTypeOfDeviceId)
        {
            try
            {
                AddMasterTypeOfDeviceViewModel objAddMasterTypeOfDeviceViewModel = null;
                string endpoint = apiBaseUrl + "MasterTypeOfDevices/" + MasterTypeOfDeviceId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterTypeOfDeviceViewModel = JsonConvert.DeserializeObject<AddMasterTypeOfDeviceViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterTypeOfDeviceViewModel = new AddMasterTypeOfDeviceViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterTypeOfDeviceViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterTypeOfDevice/AddMasterTypeOfDevice.cshtml", objAddMasterTypeOfDeviceViewModel);
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
        public IActionResult SaveMasterTypeOfDevice(AddMasterTypeOfDeviceViewModel objAddMasterTypeOfDeviceViewModel)
        {
            try
            {
                objAddMasterTypeOfDeviceViewModel.EnterById = 1;
                objAddMasterTypeOfDeviceViewModel.EnterDate = DateTime.Now;
                objAddMasterTypeOfDeviceViewModel.ModifiedById = 1;
                objAddMasterTypeOfDeviceViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterTypeOfDeviceViewModel.IsActive == null)
                {
                    objAddMasterTypeOfDeviceViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterTypeOfDevices";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterTypeOfDeviceViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterTypeOfDeviceViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master TypeOfDevice", "Master TypeOfDevice Insert Successfully!", ""));
                    }
                    else if (objAddMasterTypeOfDeviceViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterTypeOfDevices/" + objAddMasterTypeOfDeviceViewModel.MasterTypeOfDeviceId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterTypeOfDeviceViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master TypeOfDevice", "Master TypeOfDevice Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterTypeOfDeviceViewModel = JsonConvert.DeserializeObject<AddMasterTypeOfDeviceViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterTypeOfDevice/AddMasterTypeOfDevice.cshtml", objAddMasterTypeOfDeviceViewModel);
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
                    return View("~/Views/Master/MasterTypeOfDevice/AddMasterTypeOfDevice.cshtml", objAddMasterTypeOfDeviceViewModel);
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
        public IActionResult DeleteMasterTypeOfDevice(int[] DeleteMasterTypeOfDeviceIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterTypeOfDevices";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterTypeOfDeviceIds != null && DeleteMasterTypeOfDeviceIds.Length > 0)
                {
                    foreach (long DeleteMasterTypeOfDeviceId in DeleteMasterTypeOfDeviceIds)
                    {
                        endpoint = apiBaseUrl + "MasterTypeOfDevices/" + DeleteMasterTypeOfDeviceId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master TypeOfDevice", "Master TypeOfDevice Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterTypeOfDeviceViewModel)); ;
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
