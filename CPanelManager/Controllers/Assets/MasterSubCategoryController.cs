using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterSubCategory;
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
using Rotativa.AspNetCore;

namespace CPanelManager.Controllers.Assets
{
    [Authorize]
    public class MasterSubCategoryController : Controller
    {

        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterSubCategoryController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterSubCategoryController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterSubCategoryViewModel objIndexMasterSubCategoryViewModel = new IndexMasterSubCategoryViewModel();
                IEnumerable<MasterSubCategoryViewModel> objMasterSubCategoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterSubCategory";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterSubCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterSubCategoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterSubCategoryViewModelList = Enumerable.Empty<MasterSubCategoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterSubCategoryViewModel.MasterSubCategoryList = objMasterSubCategoryViewModelList.OrderBy(a => a.SubCategoryTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterSubCategory/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterSubCategoryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterSubCategoryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterSubCategoryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterSubCategoryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterSubCategory/Index.cshtml", objIndexMasterSubCategoryViewModel);
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


        public IActionResult AddMasterSubCategory()
        {
            try
            {
                AddMasterSubCategoryViewModel objAddMasterSubCategoryViewModel = new AddMasterSubCategoryViewModel();
                objAddMasterSubCategoryViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterSubCategoryViewModel.IsActive = true;
                objAddMasterSubCategoryViewModel.MasterSubCategoryId = CommonFunction.NextMasterIdAssets("ASMasterSubCategory", assetsApiBaseUrl);
                objAddMasterSubCategoryViewModel.Sequence = objAddMasterSubCategoryViewModel.MasterSubCategoryId;
                objAddMasterSubCategoryViewModel.MasterSubCategoryId = 0;
                objAddMasterSubCategoryViewModel.SubCategoryTitle = "";
                objAddMasterSubCategoryViewModel.SubCategoryCode = "";
                objAddMasterSubCategoryViewModel.SubCategoryDescription = "";
                objAddMasterSubCategoryViewModel.SubCategoryImage = "";
                objAddMasterSubCategoryViewModel.MasterCategoryId = 0;               
                objAddMasterSubCategoryViewModel.CategoryTitle = "";
                objAddMasterSubCategoryViewModel.EnterById = 0;
                objAddMasterSubCategoryViewModel.EnterDate = DateTime.Now;

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterSubCategory/AddMasterSubCategory.cshtml", objAddMasterSubCategoryViewModel);
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

        public IActionResult ViewMasterSubCategory(long MasterSubCategoryId)
        {
            try
            {
                AddMasterSubCategoryViewModel objAddMasterSubCategoryViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterSubCategory/" + MasterSubCategoryId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterSubCategoryViewModel = JsonConvert.DeserializeObject<AddMasterSubCategoryViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterSubCategoryViewModel = new AddMasterSubCategoryViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterSubCategoryViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterSubCategory/AddMasterSubCategory.cshtml", objAddMasterSubCategoryViewModel);
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
        public IActionResult SaveMasterSubCategory(AddMasterSubCategoryViewModel objAddMasterSubCategoryViewModel)
        {
            try
            {
                objAddMasterSubCategoryViewModel.EnterById = 1;
                objAddMasterSubCategoryViewModel.EnterDate = DateTime.Now;
                objAddMasterSubCategoryViewModel.ModifiedById = 1;
                objAddMasterSubCategoryViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterSubCategoryViewModel.IsActive == null)
                {
                    objAddMasterSubCategoryViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterSubCategory";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterSubCategoryViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterSubCategoryViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master SubCategory", "Master SubCategory Insert Successfully!", ""));
                    }
                    else if (objAddMasterSubCategoryViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterSubCategory/" + objAddMasterSubCategoryViewModel.MasterSubCategoryId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterSubCategoryViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master SubCategory", "Master SubCategory Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterSubCategoryViewModel = JsonConvert.DeserializeObject<AddMasterSubCategoryViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterSubCategory/AddMasterSubCategory.cshtml", objAddMasterSubCategoryViewModel);
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
                    return View("~/Views/Assets/MasterSubCategory/AddMasterSubCategory.cshtml", objAddMasterSubCategoryViewModel);
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
        public IActionResult DeleteMasterSubCategory(long[] DeleteMasterSubCategoryIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterSubCategory";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterSubCategoryIds != null && DeleteMasterSubCategoryIds.Length > 0)
                {
                    foreach (long DeleteMasterSubCategoryId in DeleteMasterSubCategoryIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterSubCategory/" + DeleteMasterSubCategoryId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master SubCategory", "Master SubCategory Delete Successfully!", ""));

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


        public IActionResult DeleteMasterSubCategoryById(long MasterSubCategoryId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterSubCategory";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterSubCategory/" + MasterSubCategoryId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master SubCategory", "Master SubCategory Delete Successfully!", ""));

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

        public IActionResult PrintMasterSubCategory()
        {
            try
            {
                IndexMasterSubCategoryViewModel objIndexMasterSubCategoryViewModel = new IndexMasterSubCategoryViewModel();
                IEnumerable<MasterSubCategoryViewModel> objMasterSubCategoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterSubCategory";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterSubCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterSubCategoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterSubCategoryViewModelList = Enumerable.Empty<MasterSubCategoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterSubCategoryViewModel.MasterSubCategoryList = objMasterSubCategoryViewModelList.OrderBy(a => a.SubCategoryTitle).ToList();

                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterSubCategory.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterSubCategory/PrintMasterSubCategory.cshtml", objIndexMasterSubCategoryViewModel)
                {
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName =  "MasterSubCategory.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12",
                    CustomSwitches = "--footer-center \"  Created Date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Page: [page]/[toPage]\"" +
                                    " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,

                };
                return report;
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
        public void DropDownFillMethod()
        {
            List<ViewModels.DropDownFill> objCategoryList = CommonFunction.DropDownFillAssets("ASMasterCategory", 0, "ALL", assetsApiBaseUrl);
            ViewBag.CategoryList = new SelectList(objCategoryList, "MasterId", "MasterName", "----select----");

        }


    }
}
