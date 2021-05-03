using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterDesignation;
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
    public class MasterDesignationController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterDesignationController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterDesignationViewModel objIndexMasterDesignationViewModel = new IndexMasterDesignationViewModel();
                IEnumerable<MasterDesignationViewModel> objMasterDesignationViewModelList = null;

                string endpoint = apiBaseUrl + "MasterDesignations";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterDesignationViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterDesignationViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterDesignationViewModelList = Enumerable.Empty<MasterDesignationViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterDesignationViewModel.MasterDesignationList = objMasterDesignationViewModelList.OrderBy(a => a.DesignationTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterDesignation/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterDesignationViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterDesignationViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterDesignationViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterDesignationViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterDesignation/Index.cshtml", objIndexMasterDesignationViewModel);
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


        public IActionResult AddMasterDesignation()
        {
            try
            {
                AddMasterDesignationViewModel objAddMasterDesignationViewModel = new AddMasterDesignationViewModel();
                objAddMasterDesignationViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterDesignationViewModel.IsActive = true;
                objAddMasterDesignationViewModel.MasterDesignationId = CommonFunction.NextMasterId("ADMasterDesignations", apiBaseUrl);
                objAddMasterDesignationViewModel.MasterDesignationId = 0;
                objAddMasterDesignationViewModel.DesignationTitle = "";
                objAddMasterDesignationViewModel.DesignationCode = "";
                objAddMasterDesignationViewModel.DesignationDescription = "";
               // objAddMasterDesignationViewModel.MasterDepartmentId = 0;
                objAddMasterDesignationViewModel.EnterById = 0;
                objAddMasterDesignationViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterDesignation/AddMasterDesignation.cshtml", objAddMasterDesignationViewModel);
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

        public IActionResult ViewMasterDesignation(long MasterDesignationId)
        {
            try
            {
                AddMasterDesignationViewModel objAddMasterDesignationViewModel = null;
                string endpoint = apiBaseUrl + "MasterDesignations/" + MasterDesignationId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterDesignationViewModel = JsonConvert.DeserializeObject<AddMasterDesignationViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterDesignationViewModel = new AddMasterDesignationViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterDesignationViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterDesignation/AddMasterDesignation.cshtml", objAddMasterDesignationViewModel);
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
        public IActionResult SaveMasterDesignation(AddMasterDesignationViewModel objAddMasterDesignationViewModel)
        {
            try
            {
                objAddMasterDesignationViewModel.EnterById = 1;
                objAddMasterDesignationViewModel.EnterDate = DateTime.Now;
                objAddMasterDesignationViewModel.ModifiedById = 1;
                objAddMasterDesignationViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterDesignationViewModel.IsActive == null)
                {
                    objAddMasterDesignationViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterDesignations";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterDesignationViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterDesignationViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Designation", "Master Designation Insert Successfully!", ""));
                    }
                    else if (objAddMasterDesignationViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterDesignations/" + objAddMasterDesignationViewModel.MasterDesignationId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterDesignationViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Designation", "Master Designation Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterDesignationViewModel = JsonConvert.DeserializeObject<AddMasterDesignationViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterDesignation/AddMasterDesignation.cshtml", objAddMasterDesignationViewModel);
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
                    return View("~/Views/Master/MasterDesignation/AddMasterDesignation.cshtml", objAddMasterDesignationViewModel);
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
        public IActionResult DeleteMasterDesignation(long[] DeleteMasterDesignationIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterDesignations";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterDesignationIds != null && DeleteMasterDesignationIds.Length > 0)
                {
                    foreach (long DeleteMasterDesignationId in DeleteMasterDesignationIds)
                    {
                        endpoint = apiBaseUrl + "MasterDesignations/" + DeleteMasterDesignationId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Designation", "Master Designation Delete Successfully!", ""));

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


        public IActionResult DeleteMasterDesignationById(long MasterDesignationId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterDesignations";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterDesignations/" + MasterDesignationId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Designation", "Master Designation Delete Successfully!", ""));

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
            List<ViewModels.DropDownFill> objDepartmentList = CommonFunction.DropDownFill("ADMasterDepartment", 0, "ALL", apiBaseUrl);
            ViewBag.DesignationList = new SelectList(objDepartmentList, "MasterId", "MasterName", "----select----");

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
