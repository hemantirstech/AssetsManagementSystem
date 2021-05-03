using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterSalutation;
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
    public class MasterSalutationController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterSalutationController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterSalutationViewModel objIndexMasterSalutationViewModel = new IndexMasterSalutationViewModel();
                IEnumerable<MasterSalutationViewModel> objMasterSalutationViewModelList = null;

                string endpoint = apiBaseUrl + "MasterSalutations";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterSalutationViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterSalutationViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterSalutationViewModelList = Enumerable.Empty<MasterSalutationViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterSalutationViewModel.MasterSalutationList = objMasterSalutationViewModelList.OrderBy(a => a.SalutationTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterSalutation/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterSalutationViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterSalutationViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterSalutationViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterSalutationViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterSalutation/Index.cshtml", objIndexMasterSalutationViewModel);
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


        public IActionResult AddMasterSalutation()
        {
            try
            {
                AddMasterSalutationViewModel objAddMasterSalutationViewModel = new AddMasterSalutationViewModel();
                objAddMasterSalutationViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterSalutationViewModel.IsActive = true;
                objAddMasterSalutationViewModel.MasterSalutationId = CommonFunction.NextMasterId("ADMasterSalutations", apiBaseUrl);
                objAddMasterSalutationViewModel.MasterSalutationId = 0;
                objAddMasterSalutationViewModel.SalutationTitle = "";
                objAddMasterSalutationViewModel.SalutationCode = "";               
                objAddMasterSalutationViewModel.EnterById = 0;
                objAddMasterSalutationViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterSalutation/AddMasterSalutation.cshtml", objAddMasterSalutationViewModel);
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

        public IActionResult ViewMasterSalutation(long MasterSalutationId)
        {
            try
            {
                AddMasterSalutationViewModel objAddMasterSalutationViewModel = null;
                string endpoint = apiBaseUrl + "MasterSalutations/" + MasterSalutationId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterSalutationViewModel = JsonConvert.DeserializeObject<AddMasterSalutationViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterSalutationViewModel = new AddMasterSalutationViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterSalutationViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterSalutation/AddMasterSalutation.cshtml", objAddMasterSalutationViewModel);
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
        public IActionResult SaveMasterSalutation(AddMasterSalutationViewModel objAddMasterSalutationViewModel)
        {
            try
            {
                objAddMasterSalutationViewModel.EnterById = 1;
                objAddMasterSalutationViewModel.EnterDate = DateTime.Now;
                objAddMasterSalutationViewModel.ModifiedById = 1;
                objAddMasterSalutationViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterSalutationViewModel.IsActive == null)
                {
                    objAddMasterSalutationViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterSalutations";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterSalutationViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterSalutationViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Salutation", "Master Salutation Insert Successfully!", ""));
                    }
                    else if (objAddMasterSalutationViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterSalutations/" + objAddMasterSalutationViewModel.MasterSalutationId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterSalutationViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Salutation", "Master Salutation Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterSalutationViewModel = JsonConvert.DeserializeObject<AddMasterSalutationViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterSalutation/AddMasterSalutation.cshtml", objAddMasterSalutationViewModel);
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
                    return View("~/Views/Master/MasterSalutation/AddMasterSalutation.cshtml", objAddMasterSalutationViewModel);
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
        public IActionResult DeleteMasterSalutation(long[] DeleteMasterSalutationIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterSalutations";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterSalutationIds != null && DeleteMasterSalutationIds.Length > 0)
                {
                    foreach (long DeleteMasterSalutationId in DeleteMasterSalutationIds)
                    {
                        endpoint = apiBaseUrl + "MasterSalutations/" + DeleteMasterSalutationId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Salutation", "Master Salutation Delete Successfully!", ""));

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

        public IActionResult DeleteMasterSalutationById(long[] MasterSalutationId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterSalutations";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterSalutations/" + MasterSalutationId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Salutation", "Master Salutation Delete Successfully!", ""));

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
