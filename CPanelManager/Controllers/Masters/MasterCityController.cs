using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCity;
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
    public class MasterCityController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCityController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterCityViewModel objIndexMasterCityViewModel = new IndexMasterCityViewModel();
                IEnumerable<MasterCityViewModel> objMasterCityViewModelList = null;

                string endpoint = apiBaseUrl + "MasterCities";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCityViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCityViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCityViewModelList = Enumerable.Empty<MasterCityViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCityViewModel.MasterCityList = objMasterCityViewModelList.OrderBy(a => a.CityTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCity/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterCityViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCityViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCityViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCityViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCity/Index.cshtml", objIndexMasterCityViewModel);
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

        public IActionResult AddMasterCity()
        {
            try
            {
                AddMasterCityViewModel objAddMasterCityViewModel = new AddMasterCityViewModel();
                objAddMasterCityViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCityViewModel.IsActive = true;
                objAddMasterCityViewModel.MasterCityId = CommonFunction.NextMasterId("ADMasterCity", apiBaseUrl);
                objAddMasterCityViewModel.MasterCityId = 0;
                objAddMasterCityViewModel.CityTitle = "";
                objAddMasterCityViewModel.MasterStateId = 0;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterCity/AddMasterCity.cshtml", objAddMasterCityViewModel);
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
        public IActionResult ViewMasterCity(long MasterCityId)
        {
            try
            {
                AddMasterCityViewModel objAddMasterCityViewModel = null;
                string endpoint = apiBaseUrl + "MasterCities/" + MasterCityId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCityViewModel = JsonConvert.DeserializeObject<AddMasterCityViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCityViewModel = new AddMasterCityViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterCityViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterCity/AddMasterCity.cshtml", objAddMasterCityViewModel);
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
        public IActionResult SaveMasterCity(AddMasterCityViewModel objAddMasterCityViewModel)
        {
            try
            {
                objAddMasterCityViewModel.EnterById = 1;
                objAddMasterCityViewModel.EnterDate = DateTime.Now;
                objAddMasterCityViewModel.ModifiedById = 1;
                objAddMasterCityViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterCityViewModel.IsActive == null)
                {
                    objAddMasterCityViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterCities";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCityViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCityViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master City", "Master City Insert Successfully!", ""));
                    }
                    else if (objAddMasterCityViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterCities/" + objAddMasterCityViewModel.MasterCityId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCityViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master City", "Master City Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCityViewModel = JsonConvert.DeserializeObject<AddMasterCityViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterCity/AddMasterCity.cshtml", objAddMasterCityViewModel);
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
                    return View("~/Views/Master/MasterCity/AddMasterCity.cshtml", objAddMasterCityViewModel);
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
        public IActionResult DeleteMasterCity(int[] DeleteMasterCityIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterCities";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCityIds != null && DeleteMasterCityIds.Length > 0)
                {
                    foreach (long DeleteMasterCityId in DeleteMasterCityIds)
                    {
                        endpoint = apiBaseUrl + "MasterCities/" + DeleteMasterCityId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master City", "Master City Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterCityViewModel)); ;
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
            //List<ViewModels.DropDownFill> objCityList = CommonFunction.DropDownFill("ADMasterCity", 0, "ALL", apiBaseUrl);
            //ViewBag.CityList = new SelectList(objCityList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ALL", apiBaseUrl);
            ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        }
    }
}
