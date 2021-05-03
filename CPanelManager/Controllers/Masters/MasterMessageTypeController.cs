using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterMessageType;
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
    public class MasterMessageTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterMessageTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterMessageTypeViewModel objIndexMasterMessageTypeViewModel = new IndexMasterMessageTypeViewModel();
                IEnumerable<MasterMessageTypeViewModel> objMasterMessageTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterMessageTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterMessageTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterMessageTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterMessageTypeViewModelList = Enumerable.Empty<MasterMessageTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterMessageTypeViewModel.MasterMessageTypeList = objMasterMessageTypeViewModelList.OrderBy(a => a.MessageTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterMessageType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterMessageTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterMessageTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterMessageTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterMessageTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterMessageType/Index.cshtml", objIndexMasterMessageTypeViewModel);
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

        public IActionResult AddMasterMessageType()
        {
            try
            {
                AddMasterMessageTypeViewModel objAddMasterMessageTypeViewModel = new AddMasterMessageTypeViewModel();
                objAddMasterMessageTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterMessageTypeViewModel.IsActive = true;
                objAddMasterMessageTypeViewModel.MasterMessageTypeId = CommonFunction.NextMasterId("ADMasterMessageType", apiBaseUrl);
                objAddMasterMessageTypeViewModel.MasterMessageTypeId = 0;
                objAddMasterMessageTypeViewModel.MessageTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterMessageType/AddMasterMessageType.cshtml", objAddMasterMessageTypeViewModel);
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
        public IActionResult ViewMasterMessageType(long MasterMessageTypeId)
        {
            try
            {
                AddMasterMessageTypeViewModel objAddMasterMessageTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterMessageTypes/" + MasterMessageTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterMessageTypeViewModel = JsonConvert.DeserializeObject<AddMasterMessageTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterMessageTypeViewModel = new AddMasterMessageTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterMessageTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterMessageType/AddMasterMessageType.cshtml", objAddMasterMessageTypeViewModel);
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
        public IActionResult SaveMasterMessageType(AddMasterMessageTypeViewModel objAddMasterMessageTypeViewModel)
        {
            try
            {
                objAddMasterMessageTypeViewModel.EnterById = 1;
                objAddMasterMessageTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterMessageTypeViewModel.ModifiedById = 1;
                objAddMasterMessageTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterMessageTypeViewModel.IsActive == null)
                {
                    objAddMasterMessageTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterMessageTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterMessageTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterMessageTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master MessageType", "Master MessageType Insert Successfully!", ""));
                    }
                    else if (objAddMasterMessageTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterMessageTypes/" + objAddMasterMessageTypeViewModel.MasterMessageTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterMessageTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master MessageType", "Master MessageType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterMessageTypeViewModel = JsonConvert.DeserializeObject<AddMasterMessageTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterMessageType/AddMasterMessageType.cshtml", objAddMasterMessageTypeViewModel);
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
                    return View("~/Views/Master/MasterMessageType/AddMasterMessageType.cshtml", objAddMasterMessageTypeViewModel);
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
        public IActionResult DeleteMasterMessageType(int[] DeleteMasterMessageTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterMessageTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterMessageTypeIds != null && DeleteMasterMessageTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterMessageTypeId in DeleteMasterMessageTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterMessageTypes/" + DeleteMasterMessageTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master MessageType", "Master MessageType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterMessageTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objMessageTypeList = CommonFunction.DropDownFill("ADMasterMessageType", 0, "ALL", apiBaseUrl);
        //    ViewBag.MessageTypeList = new SelectList(objMessageTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
