using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterBusinessVerticle;
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
    public class MasterBusinessVerticleController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterBusinessVerticleController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterBusinessVerticleViewModel objIndexMasterBusinessVerticleViewModel = new IndexMasterBusinessVerticleViewModel();
                IEnumerable<MasterBusinessVerticleViewModel> objMasterBusinessVerticleViewModelList = null;

                string endpoint = apiBaseUrl + "MasterBusinessVerticles";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterBusinessVerticleViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterBusinessVerticleViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterBusinessVerticleViewModelList = Enumerable.Empty<MasterBusinessVerticleViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterBusinessVerticleViewModel.MasterBusinessVerticleList = objMasterBusinessVerticleViewModelList.OrderBy(a => a.BusinessVerticleTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterBusinessVerticle/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterBusinessVerticleViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterBusinessVerticleViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterBusinessVerticleViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterBusinessVerticleViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBusinessVerticle/Index.cshtml", objIndexMasterBusinessVerticleViewModel);
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

        public IActionResult AddMasterBusinessVerticle()
        {
            try
            {
                AddMasterBusinessVerticleViewModel objAddMasterBusinessVerticleViewModel = new AddMasterBusinessVerticleViewModel();
                objAddMasterBusinessVerticleViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterBusinessVerticleViewModel.IsActive = true;
                objAddMasterBusinessVerticleViewModel.MasterBusinessVerticleId = CommonFunction.NextMasterId("ADMasterBusinessVerticle", apiBaseUrl);
                objAddMasterBusinessVerticleViewModel.MasterBusinessVerticleId = 0;
                objAddMasterBusinessVerticleViewModel.BusinessVerticleTitle = "";
                //DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterBusinessVerticle/AddMasterBusinessVerticle.cshtml", objAddMasterBusinessVerticleViewModel);
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
        public IActionResult ViewMasterBusinessVerticle(long MasterBusinessVerticleId)
        {
            try
            {
                AddMasterBusinessVerticleViewModel objAddMasterBusinessVerticleViewModel = null;
                string endpoint = apiBaseUrl + "MasterBusinessVerticles/" + MasterBusinessVerticleId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterBusinessVerticleViewModel = JsonConvert.DeserializeObject<AddMasterBusinessVerticleViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterBusinessVerticleViewModel = new AddMasterBusinessVerticleViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //DropDownFillMethod();
                objAddMasterBusinessVerticleViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Master/MasterBusinessVerticle/AddMasterBusinessVerticle.cshtml", objAddMasterBusinessVerticleViewModel);
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
        public IActionResult SaveMasterBusinessVerticle(AddMasterBusinessVerticleViewModel objAddMasterBusinessVerticleViewModel)
        {
            try
            {
                objAddMasterBusinessVerticleViewModel.EnterById = 1;
                objAddMasterBusinessVerticleViewModel.EnterDate = DateTime.Now;
                objAddMasterBusinessVerticleViewModel.ModifiedById = 1;
                objAddMasterBusinessVerticleViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterBusinessVerticleViewModel.IsActive == null)
                {
                    objAddMasterBusinessVerticleViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterBusinessVerticles";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterBusinessVerticleViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterBusinessVerticleViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master BusinessVerticle", "Master BusinessVerticle Insert Successfully!", ""));
                    }
                    else if (objAddMasterBusinessVerticleViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterBusinessVerticles/" + objAddMasterBusinessVerticleViewModel.MasterBusinessVerticleId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterBusinessVerticleViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master BusinessVerticle", "Master BusinessVerticle Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterBusinessVerticleViewModel = JsonConvert.DeserializeObject<AddMasterBusinessVerticleViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterBusinessVerticleViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterBusinessVerticle/AddMasterBusinessVerticle.cshtml", objAddMasterBusinessVerticleViewModel);
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
                    return View("~/Views/Master/MasterBusinessVerticle/AddMasterBusinessVerticle.cshtml", objAddMasterBusinessVerticleViewModel);
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
        public IActionResult DeleteMasterBusinessVerticle(int[] DeleteMasterBusinessVerticleIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterBusinessVerticles";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterBusinessVerticleIds != null && DeleteMasterBusinessVerticleIds.Length > 0)
                {
                    foreach (long DeleteMasterBusinessVerticleId in DeleteMasterBusinessVerticleIds)
                    {
                        endpoint = apiBaseUrl + "MasterBusinessVerticles/" + DeleteMasterBusinessVerticleId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master BusinessVerticle", "Master BusinessVerticle Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterBusinessVerticleViewModel)); ;
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
    }
}
