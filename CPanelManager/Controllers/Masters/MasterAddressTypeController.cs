using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterAddressType;
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
    public class MasterAddressTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterAddressTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterAddressTypeViewModel objIndexMasterAddressTypeViewModel = new IndexMasterAddressTypeViewModel();
                IEnumerable<MasterAddressTypeViewModel> objMasterAddressTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterAddressTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAddressTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAddressTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterAddressTypeViewModelList = Enumerable.Empty<MasterAddressTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterAddressTypeViewModel.MasterAddressTypeList = objMasterAddressTypeViewModelList.OrderBy(a => a.AddressTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterAddressType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterAddressTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterAddressTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterAddressTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterAddressTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterAddressType/Index.cshtml", objIndexMasterAddressTypeViewModel);
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

        public IActionResult AddMasterAddressType()
        {
            try
            {
                AddMasterAddressTypeViewModel objAddMasterAddressTypeViewModel = new AddMasterAddressTypeViewModel();
                objAddMasterAddressTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterAddressTypeViewModel.IsActive = true;
                objAddMasterAddressTypeViewModel.MasterAddressTypeId = CommonFunction.NextMasterId("ADMasterAddressType", apiBaseUrl);
                objAddMasterAddressTypeViewModel.MasterAddressTypeId = 0;
                objAddMasterAddressTypeViewModel.AddressTypeTitle = "";
                objAddMasterAddressTypeViewModel.AddressTypeCode = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterAddressType/AddMasterAddressType.cshtml", objAddMasterAddressTypeViewModel);
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
        public IActionResult ViewMasterAddressType(long MasterAddressTypeId)
        {
            try
            {
                AddMasterAddressTypeViewModel objAddMasterAddressTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterAddressTypes/" + MasterAddressTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterAddressTypeViewModel = JsonConvert.DeserializeObject<AddMasterAddressTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterAddressTypeViewModel = new AddMasterAddressTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterAddressTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterAddressType/AddMasterAddressType.cshtml", objAddMasterAddressTypeViewModel);
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
        public IActionResult SaveMasterAddressType(AddMasterAddressTypeViewModel objAddMasterAddressTypeViewModel)
        {
            try
            {
                objAddMasterAddressTypeViewModel.EnterById = 1;
                objAddMasterAddressTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterAddressTypeViewModel.ModifiedById = 1;
                objAddMasterAddressTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterAddressTypeViewModel.IsActive == null)
                {
                    objAddMasterAddressTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterAddressTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterAddressTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterAddressTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master AddressType", "Master AddressType Insert Successfully!", ""));
                    }
                    else if (objAddMasterAddressTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterAddressTypes/" + objAddMasterAddressTypeViewModel.MasterAddressTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterAddressTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master AddressType", "Master AddressType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterAddressTypeViewModel = JsonConvert.DeserializeObject<AddMasterAddressTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterAddressType/AddMasterAddressType.cshtml", objAddMasterAddressTypeViewModel);
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
                    return View("~/Views/Master/MasterAddressType/AddMasterAddressType.cshtml", objAddMasterAddressTypeViewModel);
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
        public IActionResult DeleteMasterAddressType(int[] DeleteMasterAddressTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterAddressTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterAddressTypeIds != null && DeleteMasterAddressTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterAddressTypeId in DeleteMasterAddressTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterAddressTypes/" + DeleteMasterAddressTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master AddressType", "Master AddressType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterAddressTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objAddressTypeList = CommonFunction.DropDownFill("ADMasterAddressType", 0, "ALL", apiBaseUrl);
        //    ViewBag.AddressTypeList = new SelectList(objAddressTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
