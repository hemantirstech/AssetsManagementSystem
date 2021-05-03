using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterEmployeeType;
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
    public class MasterEmployeeTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterEmployeeTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterEmployeeTypeViewModel objIndexMasterEmployeeTypeViewModel = new IndexMasterEmployeeTypeViewModel();
                IEnumerable<MasterEmployeeTypeViewModel> objMasterEmployeeTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterEmployeeTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterEmployeeTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterEmployeeTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterEmployeeTypeViewModelList = Enumerable.Empty<MasterEmployeeTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterEmployeeTypeViewModel.MasterEmployeeTypeList = objMasterEmployeeTypeViewModelList.OrderBy(a => a.EmployeeTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterEmployeeType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterEmployeeTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterEmployeeTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterEmployeeTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterEmployeeTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployeeType/Index.cshtml", objIndexMasterEmployeeTypeViewModel);
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

        public IActionResult AddMasterEmployeeType()
        {
            try
            {
                AddMasterEmployeeTypeViewModel objAddMasterEmployeeTypeViewModel = new AddMasterEmployeeTypeViewModel();
                objAddMasterEmployeeTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterEmployeeTypeViewModel.IsActive = true;
                objAddMasterEmployeeTypeViewModel.MasterEmployeeTypeId = CommonFunction.NextMasterId("ADMasterEmployeeType", apiBaseUrl);
                objAddMasterEmployeeTypeViewModel.MasterEmployeeTypeId = 0;
                objAddMasterEmployeeTypeViewModel.EmployeeTypeTitle = "";
                objAddMasterEmployeeTypeViewModel.EmpTypCode = "";
                objAddMasterEmployeeTypeViewModel.Remark = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployeeType/AddMasterEmployeeType.cshtml", objAddMasterEmployeeTypeViewModel);
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
        public IActionResult ViewMasterEmployeeType(long MasterEmployeeTypeId)
        {
            try
            {
                AddMasterEmployeeTypeViewModel objAddMasterEmployeeTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterEmployeeTypes/" + MasterEmployeeTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterEmployeeTypeViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterEmployeeTypeViewModel = new AddMasterEmployeeTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterEmployeeTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterEmployeeType/AddMasterEmployeeType.cshtml", objAddMasterEmployeeTypeViewModel);
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
        public IActionResult SaveMasterEmployeeType(AddMasterEmployeeTypeViewModel objAddMasterEmployeeTypeViewModel)
        {
            try
            {
                objAddMasterEmployeeTypeViewModel.EnterById = 1;
                objAddMasterEmployeeTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterEmployeeTypeViewModel.ModifiedById = 1;
                objAddMasterEmployeeTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterEmployeeTypeViewModel.IsActive == null)
                {
                    objAddMasterEmployeeTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterEmployeeTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterEmployeeTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterEmployeeTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master EmployeeType", "Master EmployeeType Insert Successfully!", ""));
                    }
                    else if (objAddMasterEmployeeTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterEmployeeTypes/" + objAddMasterEmployeeTypeViewModel.MasterEmployeeTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterEmployeeTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master EmployeeType", "Master EmployeeType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterEmployeeTypeViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterEmployeeType/AddMasterEmployeeType.cshtml", objAddMasterEmployeeTypeViewModel);
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
                    return View("~/Views/Master/MasterEmployeeType/AddMasterEmployeeType.cshtml", objAddMasterEmployeeTypeViewModel);
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
        public IActionResult DeleteMasterEmployeeType(int[] DeleteMasterEmployeeTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterEmployeeTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterEmployeeTypeIds != null && DeleteMasterEmployeeTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterEmployeeTypeId in DeleteMasterEmployeeTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterEmployeeTypes/" + DeleteMasterEmployeeTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master EmployeeType", "Master EmployeeType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objEmployeeTypeList = CommonFunction.DropDownFill("ADMasterEmployeeType", 0, "ALL", apiBaseUrl);
        //    ViewBag.EmployeeTypeList = new SelectList(objEmployeeTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
