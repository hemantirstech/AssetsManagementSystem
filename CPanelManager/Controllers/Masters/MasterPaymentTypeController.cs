using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterPaymentType;
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
    public class MasterPaymentTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterPaymentTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterPaymentTypeViewModel objIndexMasterPaymentTypeViewModel = new IndexMasterPaymentTypeViewModel();
                IEnumerable<MasterPaymentTypeViewModel> objMasterPaymentTypeViewModelList = null;

                string endpoint = apiBaseUrl + "MasterPaymentTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterPaymentTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterPaymentTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterPaymentTypeViewModelList = Enumerable.Empty<MasterPaymentTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterPaymentTypeViewModel.MasterPaymentTypeList = objMasterPaymentTypeViewModelList.OrderBy(a => a.MasterPaymentTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterPaymentType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterPaymentTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterPaymentTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterPaymentTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterPaymentTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterPaymentType/Index.cshtml", objIndexMasterPaymentTypeViewModel);
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

        public IActionResult AddMasterPaymentType()
        {
            try
            {
                AddMasterPaymentTypeViewModel objAddMasterPaymentTypeViewModel = new AddMasterPaymentTypeViewModel();
                objAddMasterPaymentTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterPaymentTypeViewModel.IsActive = true;
                objAddMasterPaymentTypeViewModel.MasterPaymentTypeId = CommonFunction.NextMasterId("ADMasterPaymentType", apiBaseUrl);
                objAddMasterPaymentTypeViewModel.MasterPaymentTypeId = 0;
                objAddMasterPaymentTypeViewModel.MasterPaymentTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterPaymentType/AddMasterPaymentType.cshtml", objAddMasterPaymentTypeViewModel);
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
        public IActionResult ViewMasterPaymentType(long MasterPaymentTypeId)
        {
            try
            {
                AddMasterPaymentTypeViewModel objAddMasterPaymentTypeViewModel = null;
                string endpoint = apiBaseUrl + "MasterPaymentTypes/" + MasterPaymentTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterPaymentTypeViewModel = JsonConvert.DeserializeObject<AddMasterPaymentTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterPaymentTypeViewModel = new AddMasterPaymentTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterPaymentTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterPaymentType/AddMasterPaymentType.cshtml", objAddMasterPaymentTypeViewModel);
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
        public IActionResult SaveMasterPaymentType(AddMasterPaymentTypeViewModel objAddMasterPaymentTypeViewModel)
        {
            try
            {
                objAddMasterPaymentTypeViewModel.EnterById = 1;
                objAddMasterPaymentTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterPaymentTypeViewModel.ModifiedById = 1;
                objAddMasterPaymentTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterPaymentTypeViewModel.IsActive == null)
                {
                    objAddMasterPaymentTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterPaymentTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterPaymentTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterPaymentTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master PaymentType", "Master PaymentType Insert Successfully!", ""));
                    }
                    else if (objAddMasterPaymentTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterPaymentTypes/" + objAddMasterPaymentTypeViewModel.MasterPaymentTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterPaymentTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master PaymentType", "Master PaymentType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterPaymentTypeViewModel = JsonConvert.DeserializeObject<AddMasterPaymentTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterPaymentType/AddMasterPaymentType.cshtml", objAddMasterPaymentTypeViewModel);
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
                    return View("~/Views/Master/MasterPaymentType/AddMasterPaymentType.cshtml", objAddMasterPaymentTypeViewModel);
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
        public IActionResult DeleteMasterPaymentType(int[] DeleteMasterPaymentTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterPaymentTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterPaymentTypeIds != null && DeleteMasterPaymentTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterPaymentTypeId in DeleteMasterPaymentTypeIds)
                    {
                        endpoint = apiBaseUrl + "MasterPaymentTypes/" + DeleteMasterPaymentTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master PaymentType", "Master PaymentType Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterPaymentTypeViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objPaymentTypeList = CommonFunction.DropDownFill("ADMasterPaymentType", 0, "ALL", apiBaseUrl);
        //    ViewBag.PaymentTypeList = new SelectList(objPaymentTypeList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
