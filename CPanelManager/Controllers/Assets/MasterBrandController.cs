using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterBrand;
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

namespace CPanelManager.Controllers.Assets
{
    [Authorize]
    public class MasterBrandController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterBrandController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterBrandController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterBrandViewModel objIndexMasterBrandViewModel = new IndexMasterBrandViewModel();
                IEnumerable<MasterBrandViewModel> objMasterBrandViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterBrand";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterBrandViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterBrandViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterBrandViewModelList = Enumerable.Empty<MasterBrandViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterBrandViewModel.MasterBrandList = objMasterBrandViewModelList.OrderBy(a => a.BrandTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterBrand/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterBrandViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterBrandViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterBrandViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterBrandViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterBrand/Index.cshtml", objIndexMasterBrandViewModel);
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


        public IActionResult AddMasterBrand()
        {
            try
            {
                AddMasterBrandViewModel objAddMasterBrandViewModel = new AddMasterBrandViewModel();
                objAddMasterBrandViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterBrandViewModel.IsActive = true;
                objAddMasterBrandViewModel.MasterBrandId = CommonFunction.NextMasterIdAssets("ASMasterBrand", assetsApiBaseUrl);
                objAddMasterBrandViewModel.Sequence = objAddMasterBrandViewModel.MasterBrandId;
                objAddMasterBrandViewModel.MasterBrandId = 0;
                objAddMasterBrandViewModel.BrandTitle = "";

                objAddMasterBrandViewModel.EnterById = 0;
                objAddMasterBrandViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterBrand/AddMasterBrand.cshtml", objAddMasterBrandViewModel);
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

        public IActionResult ViewMasterBrand(long MasterBrandId)
        {
            try
            {
                AddMasterBrandViewModel objAddMasterBrandViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterBrand/" + MasterBrandId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterBrandViewModel = JsonConvert.DeserializeObject<AddMasterBrandViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterBrandViewModel = new AddMasterBrandViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterBrandViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterBrand/AddMasterBrand.cshtml", objAddMasterBrandViewModel);
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
        public IActionResult SaveMasterBrand(AddMasterBrandViewModel objAddMasterBrandViewModel)
        {
            try
            {
                objAddMasterBrandViewModel.EnterById = 1;
                objAddMasterBrandViewModel.EnterDate = DateTime.Now;
                objAddMasterBrandViewModel.ModifiedById = 1;
                objAddMasterBrandViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterBrandViewModel.IsActive == null)
                {
                    objAddMasterBrandViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterBrand";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterBrandViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterBrandViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Brand", "Master Brand Insert Successfully!", ""));
                    }
                    else if (objAddMasterBrandViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterBrand/" + objAddMasterBrandViewModel.MasterBrandId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterBrandViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Brand", "Master Brand Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterBrandViewModel = JsonConvert.DeserializeObject<AddMasterBrandViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterBrand/AddMasterBrand.cshtml", objAddMasterBrandViewModel);
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
                    return View("~/Views/Assets/MasterBrand/AddMasterBrand.cshtml", objAddMasterBrandViewModel);
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
        public IActionResult DeleteMasterBrand(long[] DeleteMasterBrandIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterBrand";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterBrandIds != null && DeleteMasterBrandIds.Length > 0)
                {
                    foreach (long DeleteMasterBrandId in DeleteMasterBrandIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterBrand/" + DeleteMasterBrandId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Brand", "Master Brand Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
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

        public IActionResult DeleteMasterBrandById(long MasterBrandId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterBrand";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterBrand/" + MasterBrandId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Brand", "Master Brand Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
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

        }

    }
}
