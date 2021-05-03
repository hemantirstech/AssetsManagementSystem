using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterBranch;
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

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterBranchController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterBranchController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterBranchViewModel objIndexMasterBranchViewModel = new IndexMasterBranchViewModel();
                IEnumerable<MasterBranchViewModel> objMasterBranchViewModelList = null;

                string endpoint = apiBaseUrl + "MasterBranches";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterBranchViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterBranchViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterBranchViewModelList = Enumerable.Empty<MasterBranchViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterBranchViewModel.MasterBranchList = objMasterBranchViewModelList.OrderBy(a => a.BranchTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterBranch/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterBranchViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterBranchViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterBranchViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterBranchViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBranch/Index.cshtml", objIndexMasterBranchViewModel);
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

        public IActionResult AddMasterBranch()
        {
            try
            {
                AddMasterBranchViewModel objAddMasterBranchViewModel = new AddMasterBranchViewModel();
                objAddMasterBranchViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterBranchViewModel.IsActive = true;
                objAddMasterBranchViewModel.MasterBranchId = CommonFunction.NextMasterId("ADMasterBranch", apiBaseUrl);
                objAddMasterBranchViewModel.MasterBranchId = 0;
                objAddMasterBranchViewModel.DateofRegistration = DateTime.Now;
                objAddMasterBranchViewModel.MasterAddressTypeId = 1;
                objAddMasterBranchViewModel.MasterCompanyId = 1;
                objAddMasterBranchViewModel.MasterCountryId =101;
                DropDownFillMethod();


                objAddMasterBranchViewModel.ProductDetailResultList = new List<ProductDetailResult>();
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBranch/AddMasterBranch.cshtml", objAddMasterBranchViewModel);
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
        public IActionResult ViewMasterBranch(long MasterBranchId)
        {
            try
            {
                AddMasterBranchViewModel objAddMasterBranchViewModel = null;
                string endpoint = apiBaseUrl + "MasterBranches/" + MasterBranchId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterBranchViewModel = JsonConvert.DeserializeObject<AddMasterBranchViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterBranchViewModel = new AddMasterBranchViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterBranchViewModel.Mode = CommonFunction.Mode.UPDATE;



                DashboardAdminViewModel objDashboardAdminViewModel = null;
                endpoint = assetsApiBaseUrl + "Dashboard?MasterSubCategoryId=0&MasterBranchId=" + MasterBranchId;
                Task<string> HttpGetResponseProduct = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objDashboardAdminViewModel = JsonConvert.DeserializeObject<DashboardAdminViewModel>(HttpGetResponseProduct.Result);
                }
                else
                {
                    objDashboardAdminViewModel = new DashboardAdminViewModel(); ;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterBranchViewModel.ProductDetailResultList = objDashboardAdminViewModel.AssetsSubCategoryList.ToList();
                return View("~/Views/Master/MasterBranch/AddMasterBranch.cshtml", objAddMasterBranchViewModel);
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
        public IActionResult SaveMasterBranch(AddMasterBranchViewModel objAddMasterBranchViewModel)
        {
            try
            {
                objAddMasterBranchViewModel.EnterById = 1;
                objAddMasterBranchViewModel.EnterDate = DateTime.Now;
                objAddMasterBranchViewModel.ModifiedById = 1;
                objAddMasterBranchViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterBranchViewModel.IsActive == null)
                {
                    objAddMasterBranchViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid) 
                {
                    string endpoint = apiBaseUrl + "MasterBranches";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterBranchViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterBranchViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Branch", "Master Branch Insert Successfully!", ""));
                    }
                    else if (objAddMasterBranchViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterBranches/" + objAddMasterBranchViewModel.MasterBranchId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterBranchViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Branch", "Master Branch Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterBranchViewModel = JsonConvert.DeserializeObject<AddMasterBranchViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterBranch/AddMasterBranch.cshtml", objAddMasterBranchViewModel);
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
                    return View("~/Views/Master/MasterBranch/AddMasterBranch.cshtml", objAddMasterBranchViewModel);
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
        public IActionResult DeleteMasterBranch(long[] DeleteMasterBranchIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterBranches";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterBranchIds != null && DeleteMasterBranchIds.Length > 0)
                {
                    foreach (long DeleteMasterBranchId in DeleteMasterBranchIds)
                    {
                        endpoint = apiBaseUrl + "MasterBranches/" + DeleteMasterBranchId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Branch", "Master Branch Delete Successfully!", ""));

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

        public IActionResult DeleteMasterBranchById(long[] MasterBranchId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterBranches";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterBranches/" + MasterBranchId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Branch", "Master Branch Delete Successfully!", ""));

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
