using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterReportingHead;
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
    public class MasterReportingHeadController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterReportingHeadController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterReportingHeadViewModel objIndexMasterReportingHeadViewModel = new IndexMasterReportingHeadViewModel();
                IEnumerable<MasterReportingHeadViewModel> objMasterReportingHeadViewModelList = null;

                string endpoint = apiBaseUrl + "MasterReportingHeads";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterReportingHeadViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterReportingHeadViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterReportingHeadViewModelList = Enumerable.Empty<MasterReportingHeadViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterReportingHeadViewModel.MasterReportingHeadList = objMasterReportingHeadViewModelList.OrderBy(a => a.ReportingHeadTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterReportingHead/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterReportingHeadViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterReportingHeadViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterReportingHeadViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterReportingHeadViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterReportingHead/Index.cshtml", objIndexMasterReportingHeadViewModel);
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

        public IActionResult AddMasterReportingHead()
        {
            try
            {
                AddMasterReportingHeadViewModel objAddMasterReportingHeadViewModel = new AddMasterReportingHeadViewModel();
                objAddMasterReportingHeadViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterReportingHeadViewModel.IsActive = true;
                objAddMasterReportingHeadViewModel.MasterReportingHeadId = CommonFunction.NextMasterId("ADMasterReportingHead", apiBaseUrl);
                objAddMasterReportingHeadViewModel.MasterReportingHeadId = 0;
                objAddMasterReportingHeadViewModel.ReportingHeadTitle = "";
                objAddMasterReportingHeadViewModel.ReportingDescription = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterReportingHead/AddMasterReportingHead.cshtml", objAddMasterReportingHeadViewModel);
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
        public IActionResult ViewMasterReportingHead(long MasterReportingHeadId)
        {
            try
            {
                AddMasterReportingHeadViewModel objAddMasterReportingHeadViewModel = null;
                string endpoint = apiBaseUrl + "MasterReportingHeads/" + MasterReportingHeadId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterReportingHeadViewModel = JsonConvert.DeserializeObject<AddMasterReportingHeadViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterReportingHeadViewModel = new AddMasterReportingHeadViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterReportingHeadViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterReportingHead/AddMasterReportingHead.cshtml", objAddMasterReportingHeadViewModel);
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
        public IActionResult SaveMasterReportingHead(AddMasterReportingHeadViewModel objAddMasterReportingHeadViewModel)
        {
            try
            {
                objAddMasterReportingHeadViewModel.EnterById = 1;
                objAddMasterReportingHeadViewModel.EnterDate = DateTime.Now;
                objAddMasterReportingHeadViewModel.ModifiedById = 1;
                objAddMasterReportingHeadViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterReportingHeadViewModel.IsActive == null)
                {
                    objAddMasterReportingHeadViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterReportingHeads";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterReportingHeadViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterReportingHeadViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ReportingHead", "Master ReportingHead Insert Successfully!", ""));
                    }
                    else if (objAddMasterReportingHeadViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterReportingHeads/" + objAddMasterReportingHeadViewModel.MasterReportingHeadId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterReportingHeadViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ReportingHead", "Master ReportingHead Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterReportingHeadViewModel = JsonConvert.DeserializeObject<AddMasterReportingHeadViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterReportingHeadViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterReportingHead/AddMasterReportingHead.cshtml", objAddMasterReportingHeadViewModel);
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
                    return View("~/Views/Master/MasterReportingHead/AddMasterReportingHead.cshtml", objAddMasterReportingHeadViewModel);
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
        public IActionResult DeleteMasterReportingHead(int[] DeleteMasterReportingHeadIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterReportingHeads";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterReportingHeadIds != null && DeleteMasterReportingHeadIds.Length > 0)
                {
                    foreach (long DeleteMasterReportingHeadId in DeleteMasterReportingHeadIds)
                    {
                        endpoint = apiBaseUrl + "MasterReportingHeads/" + DeleteMasterReportingHeadId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master ReportingHead", "Master ReportingHead Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterReportingHeadViewModel)); ;
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
