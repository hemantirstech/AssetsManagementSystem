using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterProductType;
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
    public class MasterProductTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterProductTypeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterProductTypeController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterProductTypeViewModel objIndexMasterProductTypeViewModel = new IndexMasterProductTypeViewModel();
                IEnumerable<MasterProductTypeViewModel> objMasterProductTypeViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterProductType";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductTypeViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductTypeViewModelList = Enumerable.Empty<MasterProductTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductTypeViewModel.MasterProductTypeList = objMasterProductTypeViewModelList.OrderBy(a => a.ProductTypeTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProductType/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterProductTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterProductTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterProductTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterProductTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProductType/Index.cshtml", objIndexMasterProductTypeViewModel);
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


        public IActionResult AddMasterProductType()
        {
            try
            {
                AddMasterProductTypeViewModel objAddMasterProductTypeViewModel = new AddMasterProductTypeViewModel();
                objAddMasterProductTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterProductTypeViewModel.IsActive = true;
                objAddMasterProductTypeViewModel.MasterProductTypeId = CommonFunction.NextMasterIdAssets("ASMasterProductType", assetsApiBaseUrl);
                objAddMasterProductTypeViewModel.Sequence = objAddMasterProductTypeViewModel.MasterProductTypeId;
                objAddMasterProductTypeViewModel.MasterProductTypeId = 0;
                objAddMasterProductTypeViewModel.ProductTypeTitle = "";
                objAddMasterProductTypeViewModel.ProductTypeCode = "";
                objAddMasterProductTypeViewModel.ProductTypeDescription = "";
                objAddMasterProductTypeViewModel.ProductTypeImage = "";               
                objAddMasterProductTypeViewModel.SubCategoryTitle = "";
                objAddMasterProductTypeViewModel.EnterById = 0;
                objAddMasterProductTypeViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProductType/AddMasterProductType.cshtml", objAddMasterProductTypeViewModel);
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

        public IActionResult ViewMasterProductType(long MasterProductTypeId)
        {
            try
            {
                AddMasterProductTypeViewModel objAddMasterProductTypeViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterProductType/" + MasterProductTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProductTypeViewModel = JsonConvert.DeserializeObject<AddMasterProductTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProductTypeViewModel = new AddMasterProductTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterProductTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterProductType/AddMasterProductType.cshtml", objAddMasterProductTypeViewModel);
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
        public IActionResult SaveMasterProductType(AddMasterProductTypeViewModel objAddMasterProductTypeViewModel)
        {
            try
            {
                objAddMasterProductTypeViewModel.EnterById = 1;
                objAddMasterProductTypeViewModel.EnterDate = DateTime.Now;
                objAddMasterProductTypeViewModel.ModifiedById = 1;
                objAddMasterProductTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterProductTypeViewModel.IsActive == null)
                {
                    objAddMasterProductTypeViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterProductType";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterProductTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterProductTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ProductType", "Master ProductType Insert Successfully!", ""));
                    }
                    else if (objAddMasterProductTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProductType/" + objAddMasterProductTypeViewModel.MasterProductTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterProductTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master ProductType", "Master ProductType Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterProductTypeViewModel = JsonConvert.DeserializeObject<AddMasterProductTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterProductType/AddMasterProductType.cshtml", objAddMasterProductTypeViewModel);
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
                    return View("~/Views/Assets/MasterProductType/AddMasterProductType.cshtml", objAddMasterProductTypeViewModel);
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
        public IActionResult DeleteMasterProductType(long[] DeleteMasterProductTypeIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProductType";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterProductTypeIds != null && DeleteMasterProductTypeIds.Length > 0)
                {
                    foreach (long DeleteMasterProductTypeId in DeleteMasterProductTypeIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProductType/" + DeleteMasterProductTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master ProductType", "Master ProductType Delete Successfully!", ""));

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

        public IActionResult DeleteMasterProductTypeById(long[] MasterProductTypeId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProductType";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterProductType/" + MasterProductTypeId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master ProductType", "Master ProductType Delete Successfully!", ""));

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

            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFillAssets("ASMasterCategory", 0, "ALL", assetsApiBaseUrl);
            ViewBag.MasterCategoryList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objSubCategoryList = CommonFunction.DropDownFillAssets("ASMasterSubCategory", 0, "CID", assetsApiBaseUrl);
            ViewBag.SubCategoryList = new SelectList(objSubCategoryList, "MasterId", "MasterName", "----select----");
            


        }
    }
}
