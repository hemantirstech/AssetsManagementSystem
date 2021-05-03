using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterCategory;
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
using System.Data;
using System.Text;
using Rotativa.AspNetCore;

namespace CPanelManager.Controllers.Assets
{
    [Authorize]
    public class MasterCategoryController : Controller
    {


        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterCategoryController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterCategoryController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterCategoryViewModel objIndexMasterCategoryViewModel = new IndexMasterCategoryViewModel();
                IEnumerable<MasterCategoryViewModel> objMasterCategoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterCategory";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCategoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCategoryViewModelList = Enumerable.Empty<MasterCategoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCategoryViewModel.MasterCategoryList = objMasterCategoryViewModelList.OrderBy(a => a.CategoryTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterCategory/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterCategoryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterCategoryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterCategoryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterCategoryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterCategory/Index.cshtml", objIndexMasterCategoryViewModel);
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


        public IActionResult AddMasterCategory()
        {
            try
            {
                AddMasterCategoryViewModel objAddMasterCategoryViewModel = new AddMasterCategoryViewModel();
                objAddMasterCategoryViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterCategoryViewModel.IsActive = true;
                objAddMasterCategoryViewModel.MasterCategoryId = CommonFunction.NextMasterIdAssets("ASMasterCategory", assetsApiBaseUrl);
                objAddMasterCategoryViewModel.Sequence = objAddMasterCategoryViewModel.MasterCategoryId;
                objAddMasterCategoryViewModel.MasterCategoryId = 0;
                objAddMasterCategoryViewModel.CategoryTitle = "";
                objAddMasterCategoryViewModel.CategoryCode = "";
                objAddMasterCategoryViewModel.MasterCategoryType = 0;
                objAddMasterCategoryViewModel.CategoryImage = "";
                objAddMasterCategoryViewModel.EnterById = 0;
                objAddMasterCategoryViewModel.EnterDate = DateTime.Now;


                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterCategory/AddMasterCategory.cshtml", objAddMasterCategoryViewModel);
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

        public IActionResult ViewMasterCategory(long MasterCategoryId)
        {
            try
            {
                AddMasterCategoryViewModel objAddMasterCategoryViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterCategory/" + MasterCategoryId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterCategoryViewModel = JsonConvert.DeserializeObject<AddMasterCategoryViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterCategoryViewModel = new AddMasterCategoryViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterCategoryViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterCategory/AddMasterCategory.cshtml", objAddMasterCategoryViewModel);
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
        public IActionResult SaveMasterCategory(AddMasterCategoryViewModel objAddMasterCategoryViewModel)
        {
            try
            {
                objAddMasterCategoryViewModel.EnterById = 1;
                objAddMasterCategoryViewModel.EnterDate = DateTime.Now;
                objAddMasterCategoryViewModel.ModifiedById = 1;
                objAddMasterCategoryViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterCategoryViewModel.MasterCategoryType == null)
                {
                    objAddMasterCategoryViewModel.MasterCategoryType = 2;
                }

                if (objAddMasterCategoryViewModel.IsActive == null)
                {
                    objAddMasterCategoryViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterCategory";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterCategoryViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterCategoryViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Category", "Master Category Insert Successfully!", ""));
                    }
                    else if (objAddMasterCategoryViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterCategory/" + objAddMasterCategoryViewModel.MasterCategoryId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterCategoryViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Category", "Master Category Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterCategoryViewModel = JsonConvert.DeserializeObject<AddMasterCategoryViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterCategory/AddMasterCategory.cshtml", objAddMasterCategoryViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }

                    

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Assets/MasterCategory/AddMasterCategory.cshtml", objAddMasterCategoryViewModel);
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
        public IActionResult DeleteMasterCategory(long[] DeleteMasterCategoryIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterCategory";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterCategoryIds != null && DeleteMasterCategoryIds.Length > 0)
                {
                    foreach (long DeleteMasterCategoryId in DeleteMasterCategoryIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterCategory/" + DeleteMasterCategoryId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Category", "Master Category Delete Successfully!", ""));

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

        public IActionResult DeleteMasterCategoryById(long MasterCategoryId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterCategory";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterCategory/" + MasterCategoryId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Category", "Master Category Delete Successfully!", ""));

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

        public IActionResult PrintMasterCategory()
        {
            try
            {
                IndexMasterCategoryViewModel objIndexMasterCategoryViewModel = new IndexMasterCategoryViewModel();
                IEnumerable<MasterCategoryViewModel> objMasterCategoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterCategory";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCategoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterCategoryViewModelList = Enumerable.Empty<MasterCategoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterCategoryViewModel.MasterCategoryList = objMasterCategoryViewModelList.OrderBy(a => a.CategoryTitle).ToList();
                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterCategory.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterCategory/PrintMasterCategory.cshtml", objIndexMasterCategoryViewModel)
                {
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName =  "MasterCategory.pdf",
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
        }


        public IActionResult ShowMasterSubCategory(long MasterCategoryId)
        {
            try
            {
                ViewModels.MasterSubCategory.IndexMasterSubCategoryViewModel objIndexMasterSubCategoryViewModel = new ViewModels.MasterSubCategory.IndexMasterSubCategoryViewModel();
                IEnumerable<ViewModels.MasterSubCategory.MasterSubCategoryViewModel> objMasterSubCategoryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterSubCategory";               
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterSubCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<ViewModels.MasterSubCategory.MasterSubCategoryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterSubCategoryViewModelList = Enumerable.Empty<ViewModels.MasterSubCategory.MasterSubCategoryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterSubCategoryViewModel.MasterSubCategoryList = objMasterSubCategoryViewModelList.Where(a=>a.MasterCategoryId == MasterCategoryId).ToList();              
               
                return View("~/Views/Assets/MasterCategory/ShowMasterSubCategory.cshtml", objIndexMasterSubCategoryViewModel);
                
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



        ////RDLC to PDF
        //public IActionResult PrintMasterCategory()
        //{
        //    try
        //    {
        //        IndexMasterCategoryViewModel objIndexMasterCategoryViewModel = new IndexMasterCategoryViewModel();
        //        IEnumerable<MasterCategoryViewModel> objMasterCategoryViewModelList = null;

        //        string endpoint = assetsApiBaseUrl + "MasterCategory";
        //        Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

        //        if (HttpGetResponse != null)
        //        {
        //            objMasterCategoryViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterCategoryViewModel>>(HttpGetResponse.Result).ToList();
        //        }
        //        else
        //        {
        //            objMasterCategoryViewModelList = Enumerable.Empty<MasterCategoryViewModel>().ToList();

        //            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //        }

        //        //////////////////////////////////////////////////////////////////////////////////////////////////////

        //        DataTable dsMasterCategory = CommonFunction.GetDataTableFromObjects(objMasterCategoryViewModelList.ToArray());

        //        string mimtype = "";
        //        int extension = 1;
        //        //var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Reports\\MasterCategory.rdlc");
        //        var path = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterCategory.rdlc";
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        parameters.Add("RP1", "ASP.NET CORE RDLC Report");
        //        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //        Encoding.GetEncoding("windows-1252");

        //        LocalReport localReport = new LocalReport(path);
        //        //localReport.AddDataSource("dsMasterCategory", dsMasterCategory);

        //        var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

        //        //Save the Byte Array as File.
        //        string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterCategory.pdf";
        //        System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

        //        if (DelFile.Exists)
        //            DelFile.Delete();

        //        //File(result.MainStream, "application/pdf", filePath);
        //        System.IO.File.WriteAllBytes(filePath, result.MainStream);

        //        //return File(result.MainStream, "application/pdf");
        //        return View("~/Views/Assets/MasterCategory/PrintMasterCategory.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
        //        string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
        //        string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

        //        _logger.LogError(ErrorMessage);
        //        return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
        //    }
        //}

    }
}
