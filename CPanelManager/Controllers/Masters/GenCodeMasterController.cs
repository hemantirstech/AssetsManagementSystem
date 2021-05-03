using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.GenCodeMaster;
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
    public class GenCodeMasterController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GenCodeMasterController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexGenCodeMasterViewModel objIndexGenCodeMasterViewModel = new IndexGenCodeMasterViewModel();
                IEnumerable<GenCodeMasterViewModel> objGenCodeMasterViewModelList = null;

                string endpoint = apiBaseUrl + "GenCodeMasters";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objGenCodeMasterViewModelList = JsonConvert.DeserializeObject<IEnumerable<GenCodeMasterViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objGenCodeMasterViewModelList = Enumerable.Empty<GenCodeMasterViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexGenCodeMasterViewModel.GenCodeMasterList = objGenCodeMasterViewModelList.OrderBy(a => a.GenCodeMasterTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/GenCodeMaster/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexGenCodeMasterViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexGenCodeMasterViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexGenCodeMasterViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexGenCodeMasterViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/GenCodeMaster/Index.cshtml", objIndexGenCodeMasterViewModel);
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

        public IActionResult AddGenCodeMaster()
        {
            try
            {
                AddGenCodeMasterViewModel objAddGenCodeMasterViewModel = new AddGenCodeMasterViewModel();
                objAddGenCodeMasterViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddGenCodeMasterViewModel.IsActive = true;
                objAddGenCodeMasterViewModel.GenCodeMasterId = CommonFunction.NextMasterId("ADGenCodeMaster", apiBaseUrl);
                objAddGenCodeMasterViewModel.GenCodeMasterId = 0;
                objAddGenCodeMasterViewModel.GenCodeMasterTitle = "";
                objAddGenCodeMasterViewModel.PrintDesc = "";
                objAddGenCodeMasterViewModel.GenCodeTypeId = 1;
                objAddGenCodeMasterViewModel.Sequence = 1;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/GenCodeMaster/AddGenCodeMaster.cshtml", objAddGenCodeMasterViewModel);
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
        public IActionResult ViewGenCodeMaster(long GenCodeMasterId)
        {
            try
            {
                AddGenCodeMasterViewModel objAddGenCodeMasterViewModel = null;
                string endpoint = apiBaseUrl + "GenCodeMasters/" + GenCodeMasterId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddGenCodeMasterViewModel = JsonConvert.DeserializeObject<AddGenCodeMasterViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddGenCodeMasterViewModel = new AddGenCodeMasterViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddGenCodeMasterViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/GenCodeMaster/AddGenCodeMaster.cshtml", objAddGenCodeMasterViewModel);
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
        public IActionResult SaveGenCodeMaster(AddGenCodeMasterViewModel objAddGenCodeMasterViewModel)
        {
            try
            {
                objAddGenCodeMasterViewModel.EnterById = 1;
                objAddGenCodeMasterViewModel.EnterDate = DateTime.Now;
                objAddGenCodeMasterViewModel.ModifiedById = 1;
                objAddGenCodeMasterViewModel.ModifiedDate = DateTime.Now;

                if (objAddGenCodeMasterViewModel.IsActive == null)
                {
                    objAddGenCodeMasterViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "GenCodeMasters";

                    Task<string> HttpPostResponse = null;

                    if (objAddGenCodeMasterViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddGenCodeMasterViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "GenCode Master", "GenCode Master Insert Successfully!", ""));
                    }
                    else if (objAddGenCodeMasterViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "GenCodeMasters/" + objAddGenCodeMasterViewModel.GenCodeMasterId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddGenCodeMasterViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "GenCode Master", "GenCode Master Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddGenCodeMasterViewModel = JsonConvert.DeserializeObject<AddGenCodeMasterViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/GenCodeMaster/AddGenCodeMaster.cshtml", objAddGenCodeMasterViewModel);
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
                    return View("~/Views/Master/GenCodeMaster/AddGenCodeMaster.cshtml", objAddGenCodeMasterViewModel);
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
        public IActionResult DeleteGenCodeMaster(int[] DeleteGenCodeMasterIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "GenCodeMasters";

                Task<string> HttpPostResponse = null;

                if (DeleteGenCodeMasterIds != null && DeleteGenCodeMasterIds.Length > 0)
                {
                    foreach (long DeleteGenCodeMasterId in DeleteGenCodeMasterIds)
                    {
                        endpoint = apiBaseUrl + "GenCodeMasters/" + DeleteGenCodeMasterId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "GenCode Master", "GenCode Master Delete Successfully!", ""));

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
            List<ViewModels.DropDownFill> objGenCodeTypeList = CommonFunction.DropDownFill("CGGenCodeType", 0, "ALL", apiBaseUrl);
            ViewBag.GenCodeTypeList = new SelectList(objGenCodeTypeList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        }
    }
}
