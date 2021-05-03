using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterIndustryGroup;
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
    public class MasterIndustryGroupController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterIndustryGroupController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterIndustryGroupViewModel objIndexMasterIndustryGroupViewModel = new IndexMasterIndustryGroupViewModel();
                IEnumerable<MasterIndustryGroupViewModel> objMasterIndustryGroupViewModelList = null;

                string endpoint = apiBaseUrl + "MasterIndustryGroups";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterIndustryGroupViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterIndustryGroupViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterIndustryGroupViewModelList = Enumerable.Empty<MasterIndustryGroupViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterIndustryGroupViewModel.MasterIndustryGroupList = objMasterIndustryGroupViewModelList.OrderBy(a => a.IndustryGroupTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterIndustryGroup/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterIndustryGroupViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterIndustryGroupViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterIndustryGroupViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterIndustryGroupViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustryGroup/Index.cshtml", objIndexMasterIndustryGroupViewModel);
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

        public IActionResult AddMasterIndustryGroup()
        {
            try
            {
                AddMasterIndustryGroupViewModel objAddMasterIndustryGroupViewModel = new AddMasterIndustryGroupViewModel();
                objAddMasterIndustryGroupViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterIndustryGroupViewModel.IsActive = true;
                objAddMasterIndustryGroupViewModel.MasterIndustryGroupId = CommonFunction.NextMasterId("ADMasterIndustryGroup", apiBaseUrl);
                objAddMasterIndustryGroupViewModel.MasterIndustryGroupId = 0;
                objAddMasterIndustryGroupViewModel.IndustryGroupTitle = "";
                objAddMasterIndustryGroupViewModel.IndustryGroupCode = "";
                objAddMasterIndustryGroupViewModel.IndustryGroupDescription = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterIndustryGroup/AddMasterIndustryGroup.cshtml", objAddMasterIndustryGroupViewModel);
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
        public IActionResult ViewMasterIndustryGroup(long MasterIndustryGroupId)
        {
            try
            {
                AddMasterIndustryGroupViewModel objAddMasterIndustryGroupViewModel = null;
                string endpoint = apiBaseUrl + "MasterIndustryGroups/" + MasterIndustryGroupId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterIndustryGroupViewModel = JsonConvert.DeserializeObject<AddMasterIndustryGroupViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterIndustryGroupViewModel = new AddMasterIndustryGroupViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterIndustryGroupViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterIndustryGroup/AddMasterIndustryGroup.cshtml", objAddMasterIndustryGroupViewModel);
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
        public IActionResult SaveMasterIndustryGroup(AddMasterIndustryGroupViewModel objAddMasterIndustryGroupViewModel)
        {
            try
            {
                objAddMasterIndustryGroupViewModel.EnterById = 1;
                objAddMasterIndustryGroupViewModel.EnterDate = DateTime.Now;
                objAddMasterIndustryGroupViewModel.ModifiedById = 1;
                objAddMasterIndustryGroupViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterIndustryGroupViewModel.IsActive == null)
                {
                    objAddMasterIndustryGroupViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterIndustryGroups";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterIndustryGroupViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterIndustryGroupViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustryGroup", "Master IndustryGroup Insert Successfully!", ""));
                    }
                    else if (objAddMasterIndustryGroupViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterIndustryGroups/" + objAddMasterIndustryGroupViewModel.MasterIndustryGroupId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterIndustryGroupViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master IndustryGroup", "Master IndustryGroup Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterIndustryGroupViewModel = JsonConvert.DeserializeObject<AddMasterIndustryGroupViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterIndustryGroupViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterIndustryGroup/AddMasterIndustryGroup.cshtml", objAddMasterIndustryGroupViewModel);
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
                    return View("~/Views/Master/MasterIndustryGroup/AddMasterIndustryGroup.cshtml", objAddMasterIndustryGroupViewModel);
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
        public IActionResult DeleteMasterIndustryGroup(int[] DeleteMasterIndustryGroupIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterIndustryGroups";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterIndustryGroupIds != null && DeleteMasterIndustryGroupIds.Length > 0)
                {
                    foreach (long DeleteMasterIndustryGroupId in DeleteMasterIndustryGroupIds)
                    {
                        endpoint = apiBaseUrl + "MasterIndustryGroups/" + DeleteMasterIndustryGroupId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master IndustryGroup", "Master IndustryGroup Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterIndustryGroupViewModel)); ;
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
        //    List<ViewModels.DropDownFill> objIndustryGroupList = CommonFunction.DropDownFill("ADMasterIndustryGroup", 0, "ALL", apiBaseUrl);
        //    ViewBag.IndustryGroupList = new SelectList(objIndustryGroupList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
        //    //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

        //    //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
        //    //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        //}
    }
}
