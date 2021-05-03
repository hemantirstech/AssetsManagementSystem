using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MessageNotification;
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
    public class MessageNotificationController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MessageNotificationController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMessageNotificationViewModel objIndexMessageNotificationViewModel = new IndexMessageNotificationViewModel();
                IEnumerable<MessageNotificationViewModel> objMessageNotificationViewModelList = null;

                string endpoint = apiBaseUrl + "MessageNotifications";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMessageNotificationViewModelList = JsonConvert.DeserializeObject<IEnumerable<MessageNotificationViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMessageNotificationViewModelList = Enumerable.Empty<MessageNotificationViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMessageNotificationViewModel.MessageNotificationList = objMessageNotificationViewModelList.OrderBy(a => a.MessageTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MessageNotification/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMessageNotificationViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMessageNotificationViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMessageNotificationViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMessageNotificationViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MessageNotification/Index.cshtml", objIndexMessageNotificationViewModel);
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

        public IActionResult AddMessageNotification()
        {
            try
            {
                AddMessageNotificationViewModel objAddMessageNotificationViewModel = new AddMessageNotificationViewModel();
                objAddMessageNotificationViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMessageNotificationViewModel.IsActive = true;
                objAddMessageNotificationViewModel.MasterMessageNotificationId = CommonFunction.NextMasterId("ADMessageNotification", apiBaseUrl);
                objAddMessageNotificationViewModel.MasterMessageNotificationId = 0;
                objAddMessageNotificationViewModel.MessageDate =DateTime.Now ;
                objAddMessageNotificationViewModel.MasterMessageTypeId = 0;
                objAddMessageNotificationViewModel.MessageFrom = 0;
                objAddMessageNotificationViewModel.MessageTo = 0;
                objAddMessageNotificationViewModel.MessageTitle = "";
                objAddMessageNotificationViewModel.MessageDescription = "";
                objAddMessageNotificationViewModel.IsRead = true;
                objAddMessageNotificationViewModel.IsSend = true;
                objAddMessageNotificationViewModel.IsDelete = true;
                objAddMessageNotificationViewModel.ShareTo = "";
                objAddMessageNotificationViewModel.MasterCompanyId = 0;
                objAddMessageNotificationViewModel.MasterBranchId = 0;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MessageNotification/AddMessageNotification.cshtml", objAddMessageNotificationViewModel);
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
        public IActionResult ViewMessageNotification(long MessageNotificationId)
        {
            try
            {
                AddMessageNotificationViewModel objAddMessageNotificationViewModel = null;
                string endpoint = apiBaseUrl + "MessageNotifications/" + MessageNotificationId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMessageNotificationViewModel = JsonConvert.DeserializeObject<AddMessageNotificationViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMessageNotificationViewModel = new AddMessageNotificationViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMessageNotificationViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MessageNotification/AddMessageNotification.cshtml", objAddMessageNotificationViewModel);
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
        public IActionResult SaveMessageNotification(AddMessageNotificationViewModel objAddMessageNotificationViewModel)
        {
            try
            {
                objAddMessageNotificationViewModel.EnterById = 1;
                objAddMessageNotificationViewModel.EnterDate = DateTime.Now;
                objAddMessageNotificationViewModel.ModifiedById = 1;
                objAddMessageNotificationViewModel.ModifiedDate = DateTime.Now;

                if (objAddMessageNotificationViewModel.IsActive == null)
                {
                    objAddMessageNotificationViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MessageNotifications";

                    Task<string> HttpPostResponse = null;

                    if (objAddMessageNotificationViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMessageNotificationViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "MessageNotification", "MessageNotification Insert Successfully!", ""));
                    }
                    else if (objAddMessageNotificationViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MessageNotifications/" + objAddMessageNotificationViewModel.MasterMessageNotificationId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMessageNotificationViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "MessageNotification", "MessageNotification Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMessageNotificationViewModel = JsonConvert.DeserializeObject<AddMessageNotificationViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MessageNotification/AddMessageNotification.cshtml", objAddMessageNotificationViewModel);
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
                    return View("~/Views/Master/MessageNotification/AddMessageNotification.cshtml", objAddMessageNotificationViewModel);
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
        public IActionResult DeleteMessageNotification(int[] DeleteMessageNotificationIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MessageNotifications";

                Task<string> HttpPostResponse = null;

                if (DeleteMessageNotificationIds != null && DeleteMessageNotificationIds.Length > 0)
                {
                    foreach (long DeleteMessageNotificationId in DeleteMessageNotificationIds)
                    {
                        endpoint = apiBaseUrl + "MessageNotifications/" + DeleteMessageNotificationId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "MessageNotification", "MessageNotification Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMessageNotificationViewModel)); ;
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
            List<ViewModels.DropDownFill> objMessageTypeList = CommonFunction.DropDownFill("ADMasterMessageType", 0, "ALL", apiBaseUrl);
            ViewBag.MessageTypeList = new SelectList(objMessageTypeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "ALL", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");
        }
    }
}
