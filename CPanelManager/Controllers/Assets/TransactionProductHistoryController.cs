using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.TransactionProductHistory;
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

namespace CPanelManager.Controllers.Assets
{
    [Authorize]
    public class TransactionProductHistoryController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TransactionProductHistoryController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<TransactionProductHistoryController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexTransactionProductHistoryViewModel objIndexTransactionProductHistoryViewModel = new IndexTransactionProductHistoryViewModel();
                IEnumerable<TransactionProductHistoryViewModel> objTransactionProductHistoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "TransactionProductHistory";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objTransactionProductHistoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<TransactionProductHistoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objTransactionProductHistoryViewModelList = Enumerable.Empty<TransactionProductHistoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexTransactionProductHistoryViewModel.TransactionProductHistoryList = objTransactionProductHistoryViewModelList.OrderBy(a => a.TransactionProductHistoryId).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/TransactionProductHistory/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexTransactionProductHistoryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexTransactionProductHistoryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexTransactionProductHistoryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexTransactionProductHistoryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/TransactionProductHistory/Index.cshtml", objIndexTransactionProductHistoryViewModel);
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


        public IActionResult AddTransactionProductHistory()
        {
            try
            {
                AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel = new AddTransactionProductHistoryViewModel();

                objAddTransactionProductHistoryViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddTransactionProductHistoryViewModel.IsActive = true;
                objAddTransactionProductHistoryViewModel.TransactionProductHistoryId = CommonFunction.NextMasterIdAssets("ASTransactionProductHistory", assetsApiBaseUrl);
                objAddTransactionProductHistoryViewModel.MasterProductChildId = 1;
               // objAddTransactionProductHistoryViewModel.ASMasterProductChild = "";
                objAddTransactionProductHistoryViewModel.MasterSubscriptionTypeId = 1;
                objAddTransactionProductHistoryViewModel.SubscriptionPrice = 1;
                objAddTransactionProductHistoryViewModel.MasterSubscriptionVendorId = 1;
                objAddTransactionProductHistoryViewModel.SubscriptionDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.SubscriptionStartDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.SubscriptionExpiryDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.UploadInvoice = "";
                objAddTransactionProductHistoryViewModel.UploadDocument = "";
                objAddTransactionProductHistoryViewModel.UploadWarretyCard = "";
               

                objAddTransactionProductHistoryViewModel.Sequence = objAddTransactionProductHistoryViewModel.TransactionProductHistoryId;
                objAddTransactionProductHistoryViewModel.TransactionProductHistoryId = 0;
               

                objAddTransactionProductHistoryViewModel.EnterById = 0;
                objAddTransactionProductHistoryViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/TransactionProductHistory/AddTransactionProductHistory.cshtml", objAddTransactionProductHistoryViewModel);
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

        public IActionResult ViewTransactionProductHistory(long TransactionProductHistoryId)
        {
            try
            {
                AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel = null;
                string endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + TransactionProductHistoryId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddTransactionProductHistoryViewModel = JsonConvert.DeserializeObject<AddTransactionProductHistoryViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddTransactionProductHistoryViewModel = new AddTransactionProductHistoryViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddTransactionProductHistoryViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/TransactionProductHistory/AddTransactionProductHistory.cshtml", objAddTransactionProductHistoryViewModel);
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
        public IActionResult SaveTransactionProductHistory(AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel)
        {
            try
            {
                objAddTransactionProductHistoryViewModel.EnterById = 1;
                objAddTransactionProductHistoryViewModel.EnterDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.ModifiedById = 1;
                objAddTransactionProductHistoryViewModel.ModifiedDate = DateTime.Now;

                if (objAddTransactionProductHistoryViewModel.IsActive == null)
                {
                    objAddTransactionProductHistoryViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "TransactionProductHistory";

                    Task<string> HttpPostResponse = null;

                    if (objAddTransactionProductHistoryViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddTransactionProductHistoryViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Brand", "Master Brand Insert Successfully!", ""));
                    }
                    else if (objAddTransactionProductHistoryViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + objAddTransactionProductHistoryViewModel.TransactionProductHistoryId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddTransactionProductHistoryViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Brand", "Master Brand Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddTransactionProductHistoryViewModel = JsonConvert.DeserializeObject<AddTransactionProductHistoryViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/TransactionProductHistory/AddTransactionProductHistory.cshtml", objAddTransactionProductHistoryViewModel);
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
                    return View("~/Views/Assets/TransactionProductHistory/AddTransactionProductHistory.cshtml", objAddTransactionProductHistoryViewModel);
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
        public IActionResult DeleteTransactionProductHistory(long[] DeleteTransactionProductHistoryIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "TransactionProductHistory";

                Task<string> HttpPostResponse = null;

                if (DeleteTransactionProductHistoryIds != null && DeleteTransactionProductHistoryIds.Length > 0)
                {
                    foreach (long DeleteTransactionProductHistoryId in DeleteTransactionProductHistoryIds)
                    {
                        endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + DeleteTransactionProductHistoryId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Brand", "Master Brand Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
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

        public IActionResult DeleteTransactionProductHistoryById(long TransactionProductHistoryId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "TransactionProductHistory";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + TransactionProductHistoryId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Brand", "Master Brand Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
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

        }
    }
}
