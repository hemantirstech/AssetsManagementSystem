using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterColor;
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
    public class MasterColorController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterColorController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterColorViewModel objIndexMasterColorViewModel = new IndexMasterColorViewModel();
                IEnumerable<MasterColorViewModel> objMasterColorViewModelList = null;

                string endpoint = apiBaseUrl + "MasterColors";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterColorViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterColorViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterColorViewModelList = Enumerable.Empty<MasterColorViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterColorViewModel.MasterColorList = objMasterColorViewModelList.OrderBy(a => a.ColorTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterColor/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterColorViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterColorViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterColorViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterColorViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterColor/Index.cshtml", objIndexMasterColorViewModel);
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

        public IActionResult AddMasterColor()
        {
            try
            {
                AddMasterColorViewModel objAddMasterColorViewModel = new AddMasterColorViewModel();
                objAddMasterColorViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterColorViewModel.IsActive = true;
                objAddMasterColorViewModel.MasterColorId = CommonFunction.NextMasterId("ADMasterColor", apiBaseUrl);
                objAddMasterColorViewModel.MasterColorId = 0;
                objAddMasterColorViewModel.ColorTitle = "";
                objAddMasterColorViewModel.ColorCode = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterColor/AddMasterColor.cshtml", objAddMasterColorViewModel);
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
        public IActionResult ViewMasterColor(long MasterColorId)
        {
            try
            {
                AddMasterColorViewModel objAddMasterColorViewModel = null;
                string endpoint = apiBaseUrl + "MasterColors/" + MasterColorId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterColorViewModel = JsonConvert.DeserializeObject<AddMasterColorViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterColorViewModel = new AddMasterColorViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterColorViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterColor/AddMasterColor.cshtml", objAddMasterColorViewModel);
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
        public IActionResult SaveMasterColor(AddMasterColorViewModel objAddMasterColorViewModel)
        {
            try
            {
                objAddMasterColorViewModel.EnterById = 1;
                objAddMasterColorViewModel.EnterDate = DateTime.Now;
                objAddMasterColorViewModel.ModifiedById = 1;
                objAddMasterColorViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterColorViewModel.IsActive == null)
                {
                    objAddMasterColorViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterColors";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterColorViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterColorViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Color", "Master Color Insert Successfully!", ""));
                    }
                    else if (objAddMasterColorViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterColors/" + objAddMasterColorViewModel.MasterColorId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterColorViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Color", "Master Color Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterColorViewModel = JsonConvert.DeserializeObject<AddMasterColorViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterColorViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterColor/AddMasterColor.cshtml", objAddMasterColorViewModel);
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
                    return View("~/Views/Master/MasterColor/AddMasterColor.cshtml", objAddMasterColorViewModel);
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
        public IActionResult DeleteMasterColor(int[] DeleteMasterColorIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterColors";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterColorIds != null && DeleteMasterColorIds.Length > 0)
                {
                    foreach (long DeleteMasterColorId in DeleteMasterColorIds)
                    {
                        endpoint = apiBaseUrl + "MasterColors/" + DeleteMasterColorId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Color", "Master Color Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterColorViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objColorList = CommonFunction.DropDownFill("ADMasterColor", 0, "ALL", apiBaseUrl);
        //    ViewBag.ColorList = new SelectList(objColorList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
