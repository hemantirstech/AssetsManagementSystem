using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterBankAccountType;
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
    public class MasterBankAccountTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterBankAccountTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterBankAccountTypeViewModel objIndexMasterBankAccountTypeViewModel = new IndexMasterBankAccountTypeViewModel();
                IEnumerable<MasterBankAccountTypeViewModel> objMasterBankAccountTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterBankAccountTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterBankAccountTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterBankAccountTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterBankAccountTypeViewModelList = Enumerable.Empty<MasterBankAccountTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterBankAccountTypeViewModel.MasterBankAccountTypeList = objMasterBankAccountTypeViewModelList.OrderBy(a => a.MasterBankAccountTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterBankAccountType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterBankAccountTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterBankAccountTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterBankAccountTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterBankAccountTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBankAccountType/Index.cshtml", objIndexMasterBankAccountTypeViewModel);
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

        public IActionResult AddMasterBankAccountType()
        {
            try
            {
                AddMasterBankAccountTypeViewModel objAddMasterBankAccountTypeViewModel = new AddMasterBankAccountTypeViewModel();
                objAddMasterBankAccountTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterBankAccountTypeViewModel.IsActive = true;
                objAddMasterBankAccountTypeViewModel.MasterBankAccountTypeId = CommonFunction.NextMasterId("ADMasterBankAccountType", apiBaseUrl);
                objAddMasterBankAccountTypeViewModel.MasterBankAccountTypeId = 0;
                objAddMasterBankAccountTypeViewModel.MasterBankAccountTypeTitle = "";
                objAddMasterBankAccountTypeViewModel.MasterBankAccountCode = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBankAccountType/AddMasterBankAccountType.cshtml", objAddMasterBankAccountTypeViewModel);
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
        public IActionResult ViewMasterBankAccountType(long MasterBankAccountTypeId)
        {
            try
            {
                AddMasterBankAccountTypeViewModel objAddMasterBankAccountTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterBankAccountTypes/" + MasterBankAccountTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterBankAccountTypeViewModel = JsonConvert.DeserializeObject<AddMasterBankAccountTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterBankAccountTypeViewModel = new AddMasterBankAccountTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterBankAccountTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterBankAccountType/AddMasterBankAccountType.cshtml", objAddMasterBankAccountTypeViewModel);
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
        public IActionResult SaveMasterBankAccountType(AddMasterBankAccountTypeViewModel objAddMasterBankAccountTypeViewModel)
        {
            try
            {
                objAddMasterBankAccountTypeViewModel.EnterById = 1;
                objAddMasterBankAccountTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterBankAccountTypeViewModel.ModifiedById = 1;
                objAddMasterBankAccountTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterBankAccountTypeViewModel.IsActive == null)
                {
                    objAddMasterBankAccountTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterBankAccountTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterBankAccountTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterBankAccountTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master BankAccountType", "Master BankAccountType Insert Successfully!", ""));
                    }
                    else if (objAddMasterBankAccountTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterBankAccountTypes/" + objAddMasterBankAccountTypeViewModel.MasterBankAccountTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterBankAccountTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master BankAccountType", "Master BankAccountType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterBankAccountTypeViewModel = JsonConvert.DeserializeObject<AddMasterBankAccountTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterBankAccountTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterBankAccountType/AddMasterBankAccountType.cshtml", objAddMasterBankAccountTypeViewModel);
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
                    return View("~/Views/Master/MasterBankAccountType/AddMasterBankAccountType.cshtml", objAddMasterBankAccountTypeViewModel);
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
        public IActionResult DeleteMasterBankAccountType(int[] DeleteMasterBankAccountTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterBankAccountTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterBankAccountTypeIds != null && DeleteMasterBankAccountTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterBankAccountTypeId in DeleteMasterBankAccountTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterBankAccountTypes/" + DeleteMasterBankAccountTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master BankAccountType", "Master BankAccountType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterBankAccountTypeViewModel)); ;
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
