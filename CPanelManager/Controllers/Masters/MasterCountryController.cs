using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCountry;
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
    public class MasterCountryController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCountryController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterCountryViewModel objIndexMasterCountryViewModel = new IndexMasterCountryViewModel();
                IEnumerable<MasterCountryViewModel> objMasterCountryViewModelList = null;

                string endpoint = apiBaseUrl + "MasterCountries";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCountryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCountryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCountryViewModelList = Enumerable.Empty<MasterCountryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCountryViewModel.MasterCountryList = objMasterCountryViewModelList.OrderBy(a => a.CountryTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCountry/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterCountryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCountryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCountryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCountryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCountry/Index.cshtml", objIndexMasterCountryViewModel);
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

        public IActionResult AddMasterCountry()
        {
            try
            {
                AddMasterCountryViewModel objAddMasterCountryViewModel = new AddMasterCountryViewModel();
                objAddMasterCountryViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCountryViewModel.IsActive = true;
                objAddMasterCountryViewModel.MasterCountryId = CommonFunction.NextMasterId("ADMasterCountry", apiBaseUrl);
                objAddMasterCountryViewModel.MasterCountryId = 0;
                objAddMasterCountryViewModel.CountryTitle = "";
                objAddMasterCountryViewModel.CountryCode = "";
                objAddMasterCountryViewModel.CountryDialCode = "";
                objAddMasterCountryViewModel.CountryFlag = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCountry/AddMasterCountry.cshtml", objAddMasterCountryViewModel);
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
        public IActionResult ViewMasterCountry(long MasterCountryId)
        {
            try
            {
                AddMasterCountryViewModel objAddMasterCountryViewModel = null;
                string endpoint = apiBaseUrl + "MasterCountries/" + MasterCountryId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCountryViewModel = JsonConvert.DeserializeObject<AddMasterCountryViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCountryViewModel = new AddMasterCountryViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterCountryViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterCountry/AddMasterCountry.cshtml", objAddMasterCountryViewModel);
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
        public IActionResult SaveMasterCountry(AddMasterCountryViewModel objAddMasterCountryViewModel)
        {
            try
            {
                objAddMasterCountryViewModel.EnterById = 1;
                objAddMasterCountryViewModel.EnterDate = DateTime.Now;
                objAddMasterCountryViewModel.ModifiedById = 1;
                objAddMasterCountryViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterCountryViewModel.IsActive == null)
                {
                    objAddMasterCountryViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCountries";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCountryViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCountryViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Country", "Master Country Insert Successfully!", ""));
                    }
                    else if (objAddMasterCountryViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCountries/" + objAddMasterCountryViewModel.MasterCountryId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCountryViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Country", "Master Country Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCountryViewModel = JsonConvert.DeserializeObject<AddMasterCountryViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCountryViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterCountry/AddMasterCountry.cshtml", objAddMasterCountryViewModel);
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
                    return View("~/Views/Master/MasterCountry/AddMasterCountry.cshtml", objAddMasterCountryViewModel);
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
        public IActionResult DeleteMasterCountry(int[] DeleteMasterCountryIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCountries";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCountryIds != null && DeleteMasterCountryIds.Length > 0)
                {
                    foreach (long DeleteMasterCountryId in DeleteMasterCountryIds)
                    {
                        endpoint = apiBaseUrl + "MasterCountries/" + DeleteMasterCountryId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Country", "Master Country Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCountryViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
