using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterProfile;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using System.Net;

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterProfilesController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;

        public MasterProfilesController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<MasterProfilesController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterProfileViewModel objIndexMasterProfileViewModel = new IndexMasterProfileViewModel();
                IEnumerable<MasterProfileViewModel> objMasterProfileViewModelList = null;

                string endpoint = apiBaseUrl + "MasterProfiles";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProfileViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProfileViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProfileViewModelList = Enumerable.Empty<MasterProfileViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProfileViewModel.MasterProfileList = objMasterProfileViewModelList.OrderBy(a=> a.ProfileTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProfiles/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterProfileViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterProfileViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterProfileViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterProfileViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterProfiles/Index.cshtml", objIndexMasterProfileViewModel);
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

        public IActionResult AddMasterProfile()
        {
            try
            {
                AddMasterProfileViewModel objAddMasterProfileViewModel = new AddMasterProfileViewModel();
                objAddMasterProfileViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterProfileViewModel.IsSelect = true;
                objAddMasterProfileViewModel.IsInsert = true;
                objAddMasterProfileViewModel.IsUpdate = true;
                objAddMasterProfileViewModel.IsDelete = true;
                objAddMasterProfileViewModel.IsActive = true;
                objAddMasterProfileViewModel.MasterProfileId = CommonFunction.NextMasterId("ADMasterProfile", apiBaseUrl);
                objAddMasterProfileViewModel.MasterProfileId = 0;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterProfiles/AddMasterProfile.cshtml", objAddMasterProfileViewModel);
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


        public IActionResult ViewMasterProfile(long MasterProfileId)
        {
            try
            {
                AddMasterProfileViewModel objAddMasterProfileViewModel = null;
                string endpoint = apiBaseUrl + "MasterProfiles/" + MasterProfileId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProfileViewModel = JsonConvert.DeserializeObject<AddMasterProfileViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProfileViewModel = new AddMasterProfileViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterProfileViewModel.Mode = CommonFunction.Mode.UPDATE;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterProfiles/AddMasterProfile.cshtml", objAddMasterProfileViewModel);
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
        public IActionResult SaveMasterProfile(AddMasterProfileViewModel objAddMasterProfileViewModel)
        {
            try
            {
                objAddMasterProfileViewModel.EnterById = 1;
                objAddMasterProfileViewModel.EnterDate = DateTime.Now;
                objAddMasterProfileViewModel.ModifiedById = 1;
                objAddMasterProfileViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterProfileViewModel.IsSelect == null)
                {
                    objAddMasterProfileViewModel.IsSelect = false;
                }

                if (objAddMasterProfileViewModel.IsInsert == null)
                {
                    objAddMasterProfileViewModel.IsInsert = false;
                }

                if (objAddMasterProfileViewModel.IsUpdate == null)
                {
                    objAddMasterProfileViewModel.IsUpdate = false;
                }

                if (objAddMasterProfileViewModel.IsDelete == null)
                {
                    objAddMasterProfileViewModel.IsDelete = false;
                }

                if (objAddMasterProfileViewModel.IsActive == null)
                {
                    objAddMasterProfileViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if ((ModelState.IsValid) && (!MasterProfileExists(objAddMasterProfileViewModel.ProfileTitle) || objAddMasterProfileViewModel.Mode == CommonFunction.Mode.UPDATE))
                {
                    string endpoint = apiBaseUrl + "MasterProfiles";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterProfileViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterProfileViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Profile", "Master Profile Insert Successfully!", ""));
                    }
                    else if (objAddMasterProfileViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterProfiles/" + objAddMasterProfileViewModel.MasterProfileId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterProfileViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Profile", "Master Profile Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterProfileViewModel = JsonConvert.DeserializeObject<AddMasterProfileViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterProfileViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Administrator/MasterProfiles/AddMasterProfile.cshtml", objAddMasterProfileViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }
                    else if (!MasterProfileExists(objAddMasterProfileViewModel.ProfileTitle) || objAddMasterProfileViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        ModelState.AddModelError(string.Empty, "GenCode Type Allready Exist!");
                    }

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Administrator/MasterProfiles/AddMasterProfile.cshtml", objAddMasterProfileViewModel);
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
        public IActionResult DeleteMasterProfile(long[] DeleteMasterProfileIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterProfiles";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterProfileIds != null && DeleteMasterProfileIds.Length > 0)
                {
                    foreach (int DeleteMasterProfileId in DeleteMasterProfileIds)
                    {
                        endpoint = apiBaseUrl + "MasterProfiles/" + DeleteMasterProfileId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Profile", "Master Profile Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterProfileViewModel)); ;
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

        [HttpPost]
        public IActionResult DeleteMasterProfileById(long MasterProfileId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterProfiles";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterProfiles/" + MasterProfileId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Profile", "Master Profile Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterProfileViewModel)); ;
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
        private bool MasterProfileExists(string ProfileTitle)
        {
            IEnumerable<AddMasterProfileViewModel> objAddMasterProfileViewModellList = null;

            string endpoint = apiBaseUrl + "MasterProfiles";

            if (CommonFunction.GetWebAPI(endpoint) != null)
            {
                objAddMasterProfileViewModellList = JsonConvert.DeserializeObject<IEnumerable<AddMasterProfileViewModel>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
            }
            else
            {
                objAddMasterProfileViewModellList = Enumerable.Empty<AddMasterProfileViewModel>().ToList();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return objAddMasterProfileViewModellList.Any(e => e.ProfileTitle.Trim().ToUpper() == ProfileTitle.Trim().ToUpper());
        }
    }
}
