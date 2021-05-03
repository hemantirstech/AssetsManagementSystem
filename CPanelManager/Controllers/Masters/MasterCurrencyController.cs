using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCurrency;
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
    public class MasterCurrencyController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCurrencyController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterCurrencyViewModel objIndexMasterCurrencyViewModel = new IndexMasterCurrencyViewModel();
                IEnumerable<MasterCurrencyViewModel> objMasterCurrencyViewModelList = null;

                string endpoint = apiBaseUrl + "MasterCurrencies";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCurrencyViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCurrencyViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCurrencyViewModelList = Enumerable.Empty<MasterCurrencyViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCurrencyViewModel.MasterCurrencyList = objMasterCurrencyViewModelList.OrderBy(a => a.CurrencyTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCurrency/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterCurrencyViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCurrencyViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCurrencyViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCurrencyViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCurrency/Index.cshtml", objIndexMasterCurrencyViewModel);
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

        public IActionResult AddMasterCurrency()
        {
            try
            {
                AddMasterCurrencyViewModel objAddMasterCurrencyViewModel = new AddMasterCurrencyViewModel();
                objAddMasterCurrencyViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCurrencyViewModel.IsActive = true;
                objAddMasterCurrencyViewModel.MasterCurrencyId = CommonFunction.NextMasterId("ADMasterCurrency", apiBaseUrl);
                objAddMasterCurrencyViewModel.MasterCurrencyId = 0;
                objAddMasterCurrencyViewModel.CurrencyTitle = "";
                objAddMasterCurrencyViewModel.CurrencySymbol = "";
                objAddMasterCurrencyViewModel.MasterCountryId = 1;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCurrency/AddMasterCurrency.cshtml", objAddMasterCurrencyViewModel);
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
        public IActionResult ViewMasterCurrency(long MasterCurrencyId)
        {
            try
            {
                AddMasterCurrencyViewModel objAddMasterCurrencyViewModel = null;
                string endpoint = apiBaseUrl + "MasterCurrencies/" + MasterCurrencyId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCurrencyViewModel = JsonConvert.DeserializeObject<AddMasterCurrencyViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCurrencyViewModel = new AddMasterCurrencyViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterCurrencyViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterCurrency/AddMasterCurrency.cshtml", objAddMasterCurrencyViewModel);
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
        public IActionResult SaveMasterCurrency(AddMasterCurrencyViewModel objAddMasterCurrencyViewModel)
        {
            try
            {
                objAddMasterCurrencyViewModel.EnterById = 1;
                objAddMasterCurrencyViewModel.EnterDate = DateTime.Now;
                objAddMasterCurrencyViewModel.ModifiedById = 1;
                objAddMasterCurrencyViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterCurrencyViewModel.IsActive == null)
                {
                    objAddMasterCurrencyViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCurrencies";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCurrencyViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCurrencyViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Currency", "Master Currency Insert Successfully!", ""));
                    }
                    else if (objAddMasterCurrencyViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCurrencies/" + objAddMasterCurrencyViewModel.MasterCurrencyId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCurrencyViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Currency", "Master Currency Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCurrencyViewModel = JsonConvert.DeserializeObject<AddMasterCurrencyViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCurrencyViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterCurrency/AddMasterCurrency.cshtml", objAddMasterCurrencyViewModel);
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
                    return View("~/Views/Master/MasterCurrency/AddMasterCurrency.cshtml", objAddMasterCurrencyViewModel);
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
        public IActionResult DeleteMasterCurrency(int[] DeleteMasterCurrencyIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCurrencies";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCurrencyIds != null && DeleteMasterCurrencyIds.Length > 0)
                {
                    foreach (long DeleteMasterCurrencyId in DeleteMasterCurrencyIds)
                    {
                        endpoint = apiBaseUrl + "MasterCurrencies/" + DeleteMasterCurrencyId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Currency", "Master Currency Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCurrencyViewModel)); ;
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
            //List<ViewModels.DropDownFill> objCurrencyList = CommonFunction.DropDownFill("ADMasterCurrency", 0, "ALL", apiBaseUrl);
            //ViewBag.CurrencyList = new SelectList(objCurrencyList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        }
    }
}
