using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterLogin;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterLoginController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;

        public MasterLoginController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<MasterLoginController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterLoginViewModel objIndexMasterLoginViewModel = new IndexMasterLoginViewModel();
                IEnumerable<MasterLoginViewModel> objMasterLoginViewModelList = null;

                string endpoint = apiBaseUrl + "MasterLogins";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterLoginViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterLoginViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterLoginViewModelList = Enumerable.Empty<MasterLoginViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterLoginViewModel.MasterLoginList = objMasterLoginViewModelList.OrderBy(a=> a.RegistrationTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterLogin/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterLoginViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterLoginViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterLoginViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterLoginViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterLogin/Index.cshtml", objIndexMasterLoginViewModel);
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

        public IActionResult AddMasterLogin()
        {
            try
            {
                //List<BusinessEntity.SP_CGDropDownFill_Result> objCGMasterMenuDropdownList = CGCommonFunction.DropDownFill("CGMasterMenu", 0, "PID").ToList();

                DropDownFillMethod();

                AddMasterLoginViewModel objAddMasterLoginViewModel = new AddMasterLoginViewModel();
                objAddMasterLoginViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterLoginViewModel.IsActive = true;
                objAddMasterLoginViewModel.MasterLoginId = CommonFunction.NextMasterId("ADMasterLogin", apiBaseUrl);
                objAddMasterLoginViewModel.MasterRegistrationTypeId = 2;
                objAddMasterLoginViewModel.MasterLoginId = 0;
                objAddMasterLoginViewModel.MasterProfileId = 2;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterLogin/AddMasterLogin.cshtml", objAddMasterLoginViewModel);
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


        public IActionResult ViewMasterLogin(long MasterLoginId)
        {
            try
            {
                AddMasterLoginViewModel objAddMasterLoginViewModel = null;
                string endpoint = apiBaseUrl + "MasterLogins/" + MasterLoginId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterLoginViewModel = JsonConvert.DeserializeObject<AddMasterLoginViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterLoginViewModel = new AddMasterLoginViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();

                objAddMasterLoginViewModel.Mode = CommonFunction.Mode.UPDATE;
                objAddMasterLoginViewModel.Password = CommonFunction.Decrypt(objAddMasterLoginViewModel.Password, true);

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterLogin/AddMasterLogin.cshtml", objAddMasterLoginViewModel);
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
        public IActionResult SaveMasterLogin(AddMasterLoginViewModel objAddMasterLoginViewModel)
        {
            try
            {
                objAddMasterLoginViewModel.EnterById = 1;
                objAddMasterLoginViewModel.EnterDate = DateTime.Now;
                objAddMasterLoginViewModel.ModifiedById = 1;
                objAddMasterLoginViewModel.ModifiedDate = DateTime.Now;
                objAddMasterLoginViewModel.Password = CommonFunction.Encrypt(objAddMasterLoginViewModel.Password,true); ;
                if (objAddMasterLoginViewModel.IsVerified == null)
                {
                    objAddMasterLoginViewModel.IsVerified = false;
                }

                if (objAddMasterLoginViewModel.IsFirstLogin == null)
                {
                    objAddMasterLoginViewModel.IsFirstLogin = false;
                }

                if (objAddMasterLoginViewModel.IsActive == null)
                {
                    objAddMasterLoginViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if ((ModelState.IsValid) && (!MasterLoginExists(objAddMasterLoginViewModel.UserName) || objAddMasterLoginViewModel.Mode == CommonFunction.Mode.UPDATE))
                {
                    string endpoint = apiBaseUrl + "MasterLogins";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterLoginViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterLoginViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Function", "Master Function Insert Successfully!", ""));
                    }
                    else if (objAddMasterLoginViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterLogins/" + objAddMasterLoginViewModel.MasterLoginId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterLoginViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Function", "Master Function Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterLoginViewModel = JsonConvert.DeserializeObject<AddMasterLoginViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterLoginViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Administrator/MasterLogin/AddMasterLogin.cshtml", objAddMasterLoginViewModel);
                    }
                }
                else
                {

                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }
                    else if (!MasterLoginExists(objAddMasterLoginViewModel.UserName) || objAddMasterLoginViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        ModelState.AddModelError(string.Empty, "UserName Allready Exist!");
                    }

                    DropDownFillMethod();                    

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Administrator/MasterLogin/AddMasterLogin.cshtml", objAddMasterLoginViewModel);
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
        }

        [HttpPost]
        public IActionResult DeleteMasterLogin(long[] DeleteMasterLoginIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterLogins";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterLoginIds != null && DeleteMasterLoginIds.Length > 0)
                {
                    foreach (int DeleteMasterLoginId in DeleteMasterLoginIds)
                    {
                        endpoint = apiBaseUrl + "MasterLogins/" + DeleteMasterLoginId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Function", "Master Function Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterLoginViewModel)); ;
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

        
        public IActionResult DeleteMasterLoginById(long MasterLoginId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterLogins";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterLogins/" + MasterLoginId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Function", "Master Function Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterLoginViewModel)); ;
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

        [AllowAnonymous]
        private bool MasterLoginExists(string UserName)
        {
            IEnumerable<AddMasterLoginViewModel> objAddMasterLoginViewModellList = null;

            string endpoint = apiBaseUrl + "MasterLogins";

            if (CommonFunction.GetWebAPI(endpoint) != null)
            {
                objAddMasterLoginViewModellList = JsonConvert.DeserializeObject<IEnumerable<AddMasterLoginViewModel>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
            }
            else
            {
                objAddMasterLoginViewModellList = Enumerable.Empty<AddMasterLoginViewModel>().ToList();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return objAddMasterLoginViewModellList.Any(e => e.UserName.Trim().ToUpper() == UserName.Trim().ToUpper());
        }

        public void DropDownFillMethod()
        {
            List<ViewModels.DropDownFill> objRegistrationTypeList = CommonFunction.DropDownFill("ADMasterRegistrationType", 0, "ALL", apiBaseUrl);
            ViewBag.RegistrationTypeList = new SelectList(objRegistrationTypeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objMasterProfileList = CommonFunction.DropDownFill("ADMasterProfile", 0, "ALL", apiBaseUrl);
            ViewBag.ProfileList = new SelectList(objMasterProfileList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "ALL", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeList = CommonFunction.DropDownFill("ADMasterEmployee", 0, "BID", apiBaseUrl);
            ViewBag.EmployeeList = new SelectList(objEmployeeList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");
        }
    }
}
