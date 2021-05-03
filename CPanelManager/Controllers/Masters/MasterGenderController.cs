using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterGender;
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
    public class MasterGenderController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterGenderController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterGenderViewModel objIndexMasterGenderViewModel = new IndexMasterGenderViewModel();
                IEnumerable<MasterGenderViewModel> objMasterGenderViewModelList = null;

                string endpoint = apiBaseUrl + "MasterGenders";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterGenderViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterGenderViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterGenderViewModelList = Enumerable.Empty<MasterGenderViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterGenderViewModel.MasterGenderList = objMasterGenderViewModelList.OrderBy(a => a.GenderTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterGender/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterGenderViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterGenderViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterGenderViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterGenderViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterGender/Index.cshtml", objIndexMasterGenderViewModel);
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

        public IActionResult AddMasterGender()
        {
            try
            {
                AddMasterGenderViewModel objAddMasterGenderViewModel = new AddMasterGenderViewModel();
                objAddMasterGenderViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterGenderViewModel.IsActive = true;
                objAddMasterGenderViewModel.MasterGenderId = CommonFunction.NextMasterId("ADMasterGender", apiBaseUrl);
                objAddMasterGenderViewModel.MasterGenderId = 0;
                objAddMasterGenderViewModel.GenderTitle = "";
                objAddMasterGenderViewModel.Gendercode = "";
                objAddMasterGenderViewModel.GenderIcon = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterGender/AddMasterGender.cshtml", objAddMasterGenderViewModel);
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
        public IActionResult ViewMasterGender(long MasterGenderId)
        {
            try
            {
                AddMasterGenderViewModel objAddMasterGenderViewModel = null;
                string endpoint = apiBaseUrl + "MasterGenders/" + MasterGenderId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterGenderViewModel = JsonConvert.DeserializeObject<AddMasterGenderViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterGenderViewModel = new AddMasterGenderViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterGenderViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterGender/AddMasterGender.cshtml", objAddMasterGenderViewModel);
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
        public IActionResult SaveMasterGender(AddMasterGenderViewModel objAddMasterGenderViewModel)
        {
            try
            {
                objAddMasterGenderViewModel.EnterById = 1;
                objAddMasterGenderViewModel.EnterDate = DateTime.Now;
                objAddMasterGenderViewModel.ModifiedById = 1;
                objAddMasterGenderViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterGenderViewModel.IsActive == null)
                {
                    objAddMasterGenderViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterGenders";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterGenderViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterGenderViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Gender", "Master Gender Insert Successfully!", ""));
                    }
                    else if (objAddMasterGenderViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterGenders/" + objAddMasterGenderViewModel.MasterGenderId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterGenderViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Gender", "Master Gender Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterGenderViewModel = JsonConvert.DeserializeObject<AddMasterGenderViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterGenderViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterGender/AddMasterGender.cshtml", objAddMasterGenderViewModel);
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
                    return View("~/Views/Master/MasterGender/AddMasterGender.cshtml", objAddMasterGenderViewModel);
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
        public IActionResult DeleteMasterGender(int[] DeleteMasterGenderIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterGenders";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterGenderIds != null && DeleteMasterGenderIds.Length > 0)
                {
                    foreach (long DeleteMasterGenderId in DeleteMasterGenderIds)
                    {
                        endpoint = apiBaseUrl + "MasterGenders/" + DeleteMasterGenderId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Gender", "Master Gender Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterGenderViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objGenderList = CommonFunction.DropDownFill("ADMasterGender", 0, "ALL", apiBaseUrl);
        //    ViewBag.GenderList = new SelectList(objGenderList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
