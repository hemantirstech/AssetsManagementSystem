using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterProductSize;
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
    public class MasterProductSizeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterProductSizeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterProductSizeController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            try
            {
                IndexMasterProductSizeViewModel objIndexMasterProductSizeViewModel = new IndexMasterProductSizeViewModel();
                IEnumerable<MasterProductSizeViewModel> objMasterProductSizeViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterProductSize";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductSizeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductSizeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductSizeViewModelList = Enumerable.Empty<MasterProductSizeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductSizeViewModel.MasterProductSizeList = objMasterProductSizeViewModelList.OrderBy(a => a.ProductSizeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProductSize/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterProductSizeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterProductSizeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterProductSizeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterProductSizeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProductSize/Index.cshtml", objIndexMasterProductSizeViewModel);
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


        public IActionResult AddMasterProductSize()
        {
            try
            {
                AddMasterProductSizeViewModel objAddMasterProductSizeViewModel = new AddMasterProductSizeViewModel();
                objAddMasterProductSizeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterProductSizeViewModel.IsActive = true;
                objAddMasterProductSizeViewModel.MasterProductSizeId = CommonFunction.NextMasterIdAssets("ASMasterProductSize", assetsApiBaseUrl);
                objAddMasterProductSizeViewModel.Sequence = objAddMasterProductSizeViewModel.MasterProductSizeId;
                objAddMasterProductSizeViewModel.MasterProductSizeId = 0;
                objAddMasterProductSizeViewModel.ProductSizeTitle = "";
              
                objAddMasterProductSizeViewModel.EnterById = 0;
                objAddMasterProductSizeViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProductSize/AddMasterProductSize.cshtml", objAddMasterProductSizeViewModel);
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

        public IActionResult ViewMasterProductSize(long MasterProductSizeId)
        {
            try
            {
                AddMasterProductSizeViewModel objAddMasterProductSizeViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterProductSize/" + MasterProductSizeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProductSizeViewModel = JsonConvert.DeserializeObject<AddMasterProductSizeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProductSizeViewModel = new AddMasterProductSizeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterProductSizeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterProductSize/AddMasterProductSize.cshtml", objAddMasterProductSizeViewModel);
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
        public IActionResult SaveMasterProductSize(AddMasterProductSizeViewModel objAddMasterProductSizeViewModel)
        {
            try
            {
                objAddMasterProductSizeViewModel.EnterById = 1;
                objAddMasterProductSizeViewModel.EnterDate = DateTime.Now;
                objAddMasterProductSizeViewModel.ModifiedById = 1;
                objAddMasterProductSizeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterProductSizeViewModel.IsActive == null)
                {
                    objAddMasterProductSizeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterProductSize";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterProductSizeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterProductSizeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ProductSize", "Master ProductSize Insert Successfully!", ""));
                    }
                    else if (objAddMasterProductSizeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProductSize/" + objAddMasterProductSizeViewModel.MasterProductSizeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterProductSizeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ProductSize", "Master ProductSize Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterProductSizeViewModel = JsonConvert.DeserializeObject<AddMasterProductSizeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeSizeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterProductSize/AddMasterProductSize.cshtml", objAddMasterProductSizeViewModel);
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
                    return View("~/Views/Assets/MasterProductSize/AddMasterProductSize.cshtml", objAddMasterProductSizeViewModel);
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
        public IActionResult DeleteMasterProductSize(long[] DeleteMasterProductSizeIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProductSize";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterProductSizeIds != null && DeleteMasterProductSizeIds.Length > 0)
                {
                    foreach (long DeleteMasterProductSizeId in DeleteMasterProductSizeIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProductSize/" + DeleteMasterProductSizeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master ProductSize", "Master ProductSize Delete Successfully!", ""));

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

        public IActionResult DeleteMasterProductSizeById(long[] MasterProductSizeId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProductSize";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterProductSize/" + MasterProductSizeId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master ProductSize", "Master ProductSize Delete Successfully!", ""));

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
