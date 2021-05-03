using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterFinancialYear;
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
    public class MasterFinancialYearController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterFinancialYearController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterFinancialYearViewModel objIndexMasterFinancialYearViewModel = new IndexMasterFinancialYearViewModel();
                IEnumerable<MasterFinancialYearViewModel> objMasterFinancialYearViewModelList = null;

                string endpoint = apiBaseUrl + "MasterFinancialYears";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterFinancialYearViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterFinancialYearViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterFinancialYearViewModelList = Enumerable.Empty<MasterFinancialYearViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterFinancialYearViewModel.MasterFinancialYearList = objMasterFinancialYearViewModelList.OrderBy(a => a.FinancialYearDescription).ToList();


                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterFinancialYear/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterFinancialYearViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterFinancialYearViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterFinancialYearViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterFinancialYearViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterFinancialYear/Index.cshtml", objIndexMasterFinancialYearViewModel);
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

        public IActionResult AddMasterFinancialYear()
        {
            try
            {
                AddMasterFinancialYearViewModel objAddMasterFinancialYearViewModel = new AddMasterFinancialYearViewModel();
                objAddMasterFinancialYearViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterFinancialYearViewModel.IsActive = true;
                objAddMasterFinancialYearViewModel.MasterFinancialYearId = CommonFunction.NextMasterId("ADMasterFinancialYear", apiBaseUrl);
                objAddMasterFinancialYearViewModel.MasterFinancialYearId = 0;
                objAddMasterFinancialYearViewModel.FinancialYearDescription ="";
                objAddMasterFinancialYearViewModel.YearStartDate = DateTime.Now;
                objAddMasterFinancialYearViewModel.YearEndDate = DateTime.Now;
                objAddMasterFinancialYearViewModel.CashBook = "";
                objAddMasterFinancialYearViewModel.YearLocked = true;
                objAddMasterFinancialYearViewModel.CurrentYear = true;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterFinancialYear/AddMasterFinancialYear.cshtml", objAddMasterFinancialYearViewModel);
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
        public IActionResult ViewMasterFinancialYear(long MasterFinancialYearId)
        {
            try
            {
                AddMasterFinancialYearViewModel objAddMasterFinancialYearViewModel = null;
                string endpoint = apiBaseUrl + "MasterFinancialYears/" + MasterFinancialYearId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterFinancialYearViewModel = JsonConvert.DeserializeObject<AddMasterFinancialYearViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterFinancialYearViewModel = new AddMasterFinancialYearViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objAddMasterFinancialYearViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterFinancialYear/AddMasterFinancialYear.cshtml", objAddMasterFinancialYearViewModel);
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
        public IActionResult SaveMasterFinancialYear(AddMasterFinancialYearViewModel objAddMasterFinancialYearViewModel)
        {
            try
            {
                objAddMasterFinancialYearViewModel.EnterById = 1;
                objAddMasterFinancialYearViewModel.EnterDate = DateTime.Now;
                objAddMasterFinancialYearViewModel.ModifiedById = 1;
                objAddMasterFinancialYearViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterFinancialYearViewModel.IsActive == null)
                {
                    objAddMasterFinancialYearViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid) 
                {
                    string endpoint = apiBaseUrl + "MasterFinancialYears";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterFinancialYearViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterFinancialYearViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master FinancialYear", "Master FinancialYear Insert Successfully!", ""));
                    }
                    else if (objAddMasterFinancialYearViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterFinancialYears/" + objAddMasterFinancialYearViewModel.MasterFinancialYearId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterFinancialYearViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master FinancialYear", "Master FinancialYear Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterFinancialYearViewModel = JsonConvert.DeserializeObject<AddMasterFinancialYearViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterFinancialYear/AddMasterFinancialYear.cshtml", objAddMasterFinancialYearViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Master/MasterFinancialYear/AddMasterFinancialYear.cshtml", objAddMasterFinancialYearViewModel);
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
        public IActionResult DeleteMasterFinancialYear(int[] DeleteMasterFinancialYearIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterFinancialYears";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterFinancialYearIds != null && DeleteMasterFinancialYearIds.Length > 0)
                {
                    foreach (long DeleteMasterFinancialYearId in DeleteMasterFinancialYearIds)
                    {
                        endpoint = apiBaseUrl + "MasterFinancialYears/" + DeleteMasterFinancialYearId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master FinancialYear", "Master FinancialYear Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
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
