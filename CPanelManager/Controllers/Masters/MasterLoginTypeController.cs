using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterLoginType;
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
    public class MasterLoginTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterLoginTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterLoginTypeViewModel objIndexMasterLoginTypeViewModel = new IndexMasterLoginTypeViewModel();
                IEnumerable<MasterLoginTypeViewModel> objMasterLoginTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterLoginTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterLoginTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterLoginTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterLoginTypeViewModelList = Enumerable.Empty<MasterLoginTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterLoginTypeViewModel.MasterLoginTypeList = objMasterLoginTypeViewModelList.OrderBy(a => a.MasterLoginTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterLoginType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterLoginTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterLoginTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterLoginTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterLoginTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterLoginType/Index.cshtml", objIndexMasterLoginTypeViewModel);
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

        public IActionResult AddMasterLoginType()
        {
            try
            {
                AddMasterLoginTypeViewModel objAddMasterLoginTypeViewModel = new AddMasterLoginTypeViewModel();
                objAddMasterLoginTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterLoginTypeViewModel.IsActive = true;
                objAddMasterLoginTypeViewModel.MasterLoginTypeId = CommonFunction.NextMasterId("ADMasterLoginType", apiBaseUrl);
                objAddMasterLoginTypeViewModel.MasterLoginTypeId = 0;
                objAddMasterLoginTypeViewModel.MasterLoginTypeTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterLoginType/AddMasterLoginType.cshtml", objAddMasterLoginTypeViewModel);
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
        public IActionResult ViewMasterLoginType(long MasterLoginTypeId)
        {
            try
            {
                AddMasterLoginTypeViewModel objAddMasterLoginTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterLoginTypes/" + MasterLoginTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterLoginTypeViewModel = JsonConvert.DeserializeObject<AddMasterLoginTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterLoginTypeViewModel = new AddMasterLoginTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterLoginTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterLoginType/AddMasterLoginType.cshtml", objAddMasterLoginTypeViewModel);
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
        public IActionResult SaveMasterLoginType(AddMasterLoginTypeViewModel objAddMasterLoginTypeViewModel)
        {
            try
            {
                objAddMasterLoginTypeViewModel.EnterById = 1;
                objAddMasterLoginTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterLoginTypeViewModel.ModifiedById = 1;
                objAddMasterLoginTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterLoginTypeViewModel.IsActive == null)
                {
                    objAddMasterLoginTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterLoginTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterLoginTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterLoginTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master LoginType", "Master LoginType Insert Successfully!", ""));
                    }
                    else if (objAddMasterLoginTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterLoginTypes/" + objAddMasterLoginTypeViewModel.MasterLoginTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterLoginTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master LoginType", "Master LoginType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterLoginTypeViewModel = JsonConvert.DeserializeObject<AddMasterLoginTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterLoginType/AddMasterLoginType.cshtml", objAddMasterLoginTypeViewModel);
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
                    return View("~/Views/Master/MasterLoginType/AddMasterLoginType.cshtml", objAddMasterLoginTypeViewModel);
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
        public IActionResult DeleteMasterLoginType(int[] DeleteMasterLoginTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterLoginTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterLoginTypeIds != null && DeleteMasterLoginTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterLoginTypeId in DeleteMasterLoginTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterLoginTypes/" + DeleteMasterLoginTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master LoginType", "Master LoginType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterLoginTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objLoginTypeList = CommonFunction.DropDownFill("ADMasterLoginType", 0, "ALL", apiBaseUrl);
        //    ViewBag.LoginTypeList = new SelectList(objLoginTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
