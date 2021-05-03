using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterState;
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
    public class MasterStateController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterStateController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterStateViewModel objIndexMasterStateViewModel = new IndexMasterStateViewModel();
                IEnumerable<MasterStateViewModel> objMasterStateViewModelList = null;

                string endpoint = apiBaseUrl + "MasterStates";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterStateViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterStateViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterStateViewModelList = Enumerable.Empty<MasterStateViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterStateViewModel.MasterStateList = objMasterStateViewModelList.OrderBy(a => a.StateTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterState/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterStateViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterStateViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterStateViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterStateViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterState/Index.cshtml", objIndexMasterStateViewModel);
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

        public IActionResult AddMasterState()
        {
            try
            {
                AddMasterStateViewModel objAddMasterStateViewModel = new AddMasterStateViewModel();
                objAddMasterStateViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterStateViewModel.IsActive = true;
                objAddMasterStateViewModel.MasterStateId = CommonFunction.NextMasterId("ADMasterState", apiBaseUrl);
                objAddMasterStateViewModel.MasterStateId = 0;
                objAddMasterStateViewModel.StateTitle = "";
                objAddMasterStateViewModel.StateCode = "";
              //  objAddMasterStateViewModel.MasterCountryId = 0;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterState/AddMasterState.cshtml", objAddMasterStateViewModel);
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
        public IActionResult ViewMasterState(long MasterStateId)
        {
            try
            {
                AddMasterStateViewModel objAddMasterStateViewModel = null;
                string endpoint = apiBaseUrl + "MasterStates/" + MasterStateId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterStateViewModel = JsonConvert.DeserializeObject<AddMasterStateViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterStateViewModel = new AddMasterStateViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterStateViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterState/AddMasterState.cshtml", objAddMasterStateViewModel);
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
       public IActionResult SaveMasterState(AddMasterStateViewModel objAddMasterStateViewModel)
       
        {
            try
            {            

                objAddMasterStateViewModel.EnterById = 1;
                objAddMasterStateViewModel.EnterDate = DateTime.Now;
                objAddMasterStateViewModel.ModifiedById = 1;
                objAddMasterStateViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterStateViewModel.IsActive == null)
                {
                    objAddMasterStateViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterStates";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterStateViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterStateViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master State", "Master State Insert Successfully!", ""));
                    }
                    else if (objAddMasterStateViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterStates/" + objAddMasterStateViewModel.MasterStateId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterStateViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master State", "Master State Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterStateViewModel = JsonConvert.DeserializeObject<AddMasterStateViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterState/AddMasterState.cshtml", objAddMasterStateViewModel);
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
                    return View("~/Views/Master/MasterState/AddMasterState.cshtml", objAddMasterStateViewModel);
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
        public IActionResult DeleteMasterState(int[] DeleteMasterStateIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterStates";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterStateIds != null && DeleteMasterStateIds.Length > 0)
                {
                    foreach (long DeleteMasterStateId in DeleteMasterStateIds)
                    {
                        endpoint = apiBaseUrl + "MasterStates/" + DeleteMasterStateId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master State", "Master State Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterStateViewModel)); ;
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
            //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ALL", apiBaseUrl);
            //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            //ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            //ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

        }
    }
}
