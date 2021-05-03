using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterRegion;
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
    public class MasterRegionController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterRegionController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterRegionViewModel objIndexMasterRegionViewModel = new IndexMasterRegionViewModel();
                IEnumerable<MasterRegionViewModel> objMasterRegionViewModelList = null;

                string endpoint = apiBaseUrl + "MasterRegions";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterRegionViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterRegionViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterRegionViewModelList = Enumerable.Empty<MasterRegionViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterRegionViewModel.MasterRegionList = objMasterRegionViewModelList.OrderBy(a => a.MasterRegionTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterRegion/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterRegionViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterRegionViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterRegionViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterRegionViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterRegion/Index.cshtml", objIndexMasterRegionViewModel);
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

        public IActionResult AddMasterRegion()
        {
            try
            {
                AddMasterRegionViewModel objAddMasterRegionViewModel = new AddMasterRegionViewModel();
                objAddMasterRegionViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterRegionViewModel.IsActive = true;
                objAddMasterRegionViewModel.MasterRegionId = CommonFunction.NextMasterId("ADMasterRegion", apiBaseUrl);
                objAddMasterRegionViewModel.MasterRegionId = 0;
                objAddMasterRegionViewModel.MasterRegionTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterRegion/AddMasterRegion.cshtml", objAddMasterRegionViewModel);
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
        public IActionResult ViewMasterRegion(long MasterRegionId)
        {
            try
            {
                AddMasterRegionViewModel objAddMasterRegionViewModel = null;
                string endpoint = apiBaseUrl + "MasterRegions/" + MasterRegionId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterRegionViewModel = JsonConvert.DeserializeObject<AddMasterRegionViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterRegionViewModel = new AddMasterRegionViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterRegionViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterRegion/AddMasterRegion.cshtml", objAddMasterRegionViewModel);
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
        public IActionResult SaveMasterRegion(AddMasterRegionViewModel objAddMasterRegionViewModel)
        {
            try
            {
                objAddMasterRegionViewModel.EnterById = 1;
                objAddMasterRegionViewModel.EnterDate = DateTime.Now;
                objAddMasterRegionViewModel.ModifiedById = 1;
                objAddMasterRegionViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterRegionViewModel.IsActive == null)
                {
                    objAddMasterRegionViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterRegions";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterRegionViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterRegionViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Region", "Master Region Insert Successfully!", ""));
                    }
                    else if (objAddMasterRegionViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterRegions/" + objAddMasterRegionViewModel.MasterRegionId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterRegionViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Region", "Master Region Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterRegionViewModel = JsonConvert.DeserializeObject<AddMasterRegionViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterRegion/AddMasterRegion.cshtml", objAddMasterRegionViewModel);
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
                    return View("~/Views/Master/MasterRegion/AddMasterRegion.cshtml", objAddMasterRegionViewModel);
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
        public IActionResult DeleteMasterRegion(int[] DeleteMasterRegionIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterRegions";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterRegionIds != null && DeleteMasterRegionIds.Length > 0)
                {
                    foreach (long DeleteMasterRegionId in DeleteMasterRegionIds)
                    {
                        endpoint = apiBaseUrl + "MasterRegions/" + DeleteMasterRegionId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Region", "Master Region Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterRegionViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objRegionList = CommonFunction.DropDownFill("ADMasterRegion", 0, "ALL", apiBaseUrl);
        //    ViewBag.RegionList = new SelectList(objRegionList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
