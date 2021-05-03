using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterDepartment;
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
    [Authorize]
    public class MasterDepartmentController : Controller
    {

        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterDepartmentController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterDepartmentViewModel objIndexMasterDepartmentViewModel = new IndexMasterDepartmentViewModel();
                IEnumerable<MasterDepartmentViewModel> objMasterDepartmentViewModelList = null;

                string endpoint = apiBaseUrl + "MasterDepartments";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterDepartmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterDepartmentViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterDepartmentViewModelList = Enumerable.Empty<MasterDepartmentViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterDepartmentViewModel.MasterDepartmentList = objMasterDepartmentViewModelList.OrderBy(a => a.DepartmentTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterDepartment/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterDepartmentViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterDepartmentViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterDepartmentViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterDepartmentViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterDepartment/Index.cshtml", objIndexMasterDepartmentViewModel);
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


        public IActionResult AddMasterDepartment()
        {
            try
            {
                AddMasterDepartmentViewModel objAddMasterDepartmentViewModel = new AddMasterDepartmentViewModel();
                objAddMasterDepartmentViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterDepartmentViewModel.IsActive = true;
                objAddMasterDepartmentViewModel.MasterDepartmentId = CommonFunction.NextMasterId("ADMasterDepartments", apiBaseUrl);
                objAddMasterDepartmentViewModel.MasterDepartmentId = 0;
                objAddMasterDepartmentViewModel.DepartmentTitle = "";
                objAddMasterDepartmentViewModel.DepartmentCode = "";
                objAddMasterDepartmentViewModel.DepartmentDescription = "";
                objAddMasterDepartmentViewModel.EnterById = 0;              
                objAddMasterDepartmentViewModel.EnterDate = DateTime.Now;
                
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterDepartment/AddMasterDepartment.cshtml", objAddMasterDepartmentViewModel);
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

        public IActionResult ViewMasterDepartment(long MasterDepartmentId)
        {
            try
            {
                AddMasterDepartmentViewModel objAddMasterDepartmentViewModel = null;
                string endpoint = apiBaseUrl + "MasterDepartments/" + MasterDepartmentId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterDepartmentViewModel = JsonConvert.DeserializeObject<AddMasterDepartmentViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterDepartmentViewModel = new AddMasterDepartmentViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterDepartmentViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterDepartment/AddMasterDepartment.cshtml", objAddMasterDepartmentViewModel);
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
        public IActionResult SaveMasterDepartment(AddMasterDepartmentViewModel objAddMasterDepartmentViewModel)
        {
            try
            {
                objAddMasterDepartmentViewModel.EnterById = 1;
                objAddMasterDepartmentViewModel.EnterDate = DateTime.Now;
                objAddMasterDepartmentViewModel.ModifiedById = 1;
                objAddMasterDepartmentViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterDepartmentViewModel.IsActive == null)
                {
                    objAddMasterDepartmentViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterDepartments";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterDepartmentViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterDepartmentViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Department", "Master Department Insert Successfully!", ""));
                    }
                    else if (objAddMasterDepartmentViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterDepartments/" + objAddMasterDepartmentViewModel.MasterDepartmentId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterDepartmentViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Department", "Master Department Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterDepartmentViewModel = JsonConvert.DeserializeObject<AddMasterDepartmentViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterDepartment/AddMasterDepartment.cshtml", objAddMasterDepartmentViewModel);
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
                    return View("~/Views/Master/MasterDepartment/AddMasterDepartment.cshtml", objAddMasterDepartmentViewModel);
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
        public IActionResult DeleteMasterDepartment(long[] DeleteMasterDepartmentIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterDepartments";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterDepartmentIds != null && DeleteMasterDepartmentIds.Length > 0)
                {
                    foreach (long DeleteMasterDepartmentId in DeleteMasterDepartmentIds)
                    {
                        endpoint = apiBaseUrl + "MasterDepartments/" + DeleteMasterDepartmentId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Department", "Master Department Delete Successfully!", ""));

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


        public IActionResult DeleteMasterDepartmentById(long MasterDepartmentId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterDepartments";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterDepartments/" + MasterDepartmentId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Department", "Master Department Delete Successfully!", ""));

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



        public void DropDownFillMethod()
        {
          

            List<ViewModels.DropDownFill> objDesignationList = CommonFunction.DropDownFill("ADMasterDesignation", 0, "ALL", apiBaseUrl);
            ViewBag.DesignationList = new SelectList(objDesignationList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        }


    }
}
