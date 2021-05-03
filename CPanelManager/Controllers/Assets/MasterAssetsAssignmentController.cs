using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterAssetsAssignment;
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
    public class MasterAssetsAssignmentController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterAssetsAssignmentController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterAssetsAssignmentViewModel objIndexMasterAssetsAssignmentViewModel = new IndexMasterAssetsAssignmentViewModel();
                //Generate List Of Employees
                IEnumerable<AssetsAssignEmployeeViewModel> objAssetsAssignEmployeeViewModelList = null;
                string endpoint = apiBaseUrl + "MasterEmployees";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsAssignEmployeeViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsAssignEmployeeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsAssignEmployeeViewModelList = Enumerable.Empty<AssetsAssignEmployeeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                ////Generate List Of AssetsAssignment
                //IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModelList = null;
                //endpoint = apiBaseUrl + "MasterAssetsAssignment";
                //Task<string> HttpGetResponseNew = CommonFunction.GetWebAPI(endpoint);

                //if (HttpGetResponseNew != null)
                //{
                //    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result).ToList();
                //}
                //else
                //{
                //    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();

                //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //}

                

                objIndexMasterAssetsAssignmentViewModel.AssetsAssignEmployeeList = objAssetsAssignEmployeeViewModelList.ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterEmployee/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterAssetsAssignmentViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterAssetsAssignmentViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterAssetsAssignmentViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterAssetsAssignmentViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/Index.cshtml", objIndexMasterAssetsAssignmentViewModel);
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

        public IActionResult PrintEmployeeAssetsAssignment()
        {
            try
            {
                IndexMasterAssetsAssignmentViewModel objIndexMasterAssetsAssignmentViewModel = new IndexMasterAssetsAssignmentViewModel();
                //Generate List Of Employees
                IEnumerable<AssetsAssignEmployeeViewModel> objAssetsAssignEmployeeViewModelList = null;
                string endpoint = apiBaseUrl + "MasterEmployees";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsAssignEmployeeViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsAssignEmployeeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsAssignEmployeeViewModelList = Enumerable.Empty<AssetsAssignEmployeeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objIndexMasterAssetsAssignmentViewModel.AssetsAssignEmployeeList = objAssetsAssignEmployeeViewModelList.ToList();

                //Generate List Of AssetsAssignment
                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModelList = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/0";
                Task<string> HttpGetResponseNew = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponseNew != null)
                {
                    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponseNew.Result).ToList();
                }
                else
                {
                    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objIndexMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModelList.ToList();

                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\EmployeeAssetsAssignment.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterAssetsAssignment/PrintEmployeeAssetsAssignment.cshtml", objIndexMasterAssetsAssignmentViewModel)
                {
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName = filePath,//"EmployeeAssetsAssignment.pdf",
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


        public IActionResult AddMasterAssetsAssignment()
        {
            try
            {
                AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel = new AddMasterAssetsAssignmentViewModel();
                objAddMasterAssetsAssignmentViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterAssetsAssignmentViewModel.MasterBranchId = 1;
                objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = 0;
                objAddMasterAssetsAssignmentViewModel.MasterCategoryId = 0;
                objAddMasterAssetsAssignmentViewModel.MasterSubCategoryId = 0;

                List<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModelList = new List<MasterAssetsAssignmentViewModel>();
                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModelList;
                objAddMasterAssetsAssignmentViewModel.MasterAssetsNotAssignmentList = objMasterAssetsAssignmentViewModelList;

                DropDownFillMethod();
               
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/AddMasterAssetsAssignment.cshtml", objAddMasterAssetsAssignmentViewModel);
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

        public IActionResult ViewMasterAssetsAssignment(long MasterEmployeeId,long MasterBranchId)
        {
            try
            {
                AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel = new AddMasterAssetsAssignmentViewModel();
                objAddMasterAssetsAssignmentViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterAssetsAssignmentViewModel.MasterBranchId = MasterBranchId;
                objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = MasterEmployeeId;
                objAddMasterAssetsAssignmentViewModel.MasterCategoryId = 0;
                objAddMasterAssetsAssignmentViewModel.MasterSubCategoryId = 0;

                
                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModel = null; 
                string endpoint = assetsApiBaseUrl + "MasterAssetsAssignment?MasterCategoryId=0&MasterSubCategoryId=0&MasterBranchId=" + objAddMasterAssetsAssignmentViewModel.MasterBranchId;
                Task<string> HttpGetResponseNotAssign = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponseNotAssign != null)
                {
                    objMasterAssetsAssignmentViewModel = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponseNotAssign.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModel = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsNotAssignmentList = objMasterAssetsAssignmentViewModel.ToList();

                //Assets Assign To Employee
                objMasterAssetsAssignmentViewModel = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAssetsAssignmentViewModel = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModel = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModel.ToList();

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/AddMasterAssetsAssignment.cshtml", objAddMasterAssetsAssignmentViewModel);
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

        public IActionResult PrintMasterAssetsAssignment(long MasterEmployeeId)
        {
            try
            {
                PrintMasterAssetsAssignmentViewModel objPrintMasterAssetsAssignmentViewModel = new PrintMasterAssetsAssignmentViewModel();
                objPrintMasterAssetsAssignmentViewModel.Mode = CommonFunction.Mode.SAVE;
                objPrintMasterAssetsAssignmentViewModel.MasterEmployeeId = MasterEmployeeId;
                objPrintMasterAssetsAssignmentViewModel.MasterCategoryId = 0;
                objPrintMasterAssetsAssignmentViewModel.MasterSubCategoryId = 0;

                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModel = null;

                //Assets Assign To Employee
                objMasterAssetsAssignmentViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objPrintMasterAssetsAssignmentViewModel.MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAssetsAssignmentViewModel = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModel = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objPrintMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModel.ToList();
                //
                //Get Employee List
                AssetsAssignEmployeeViewModel objAssetsAssignEmployeeViewModel = null;
                endpoint = apiBaseUrl + "MasterEmployees/" + MasterEmployeeId;
                HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsAssignEmployeeViewModel = JsonConvert.DeserializeObject<AssetsAssignEmployeeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAssetsAssignEmployeeViewModel = new AssetsAssignEmployeeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objPrintMasterAssetsAssignmentViewModel.AssetsAssignEmployee = objAssetsAssignEmployeeViewModel;


                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\AssetsAssignment.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterAssetsAssignment/PrintMasterAssetsAssignment.cshtml", objPrintMasterAssetsAssignmentViewModel)
                {
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName = filePath,//"AssetsAssignment.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12",
                    CustomSwitches = "--footer-center \"  Created Date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Page: [page]/[toPage]\"" +
                                    " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,

                };
                
                return report;
                //var objReportViewModel = new ViewModels.ReportViewModel();
                //objReportViewModel.ReportPath = "AssetsAssignment.pdf";
                //return View("~/Views/Report/PrintReport.cshtml", objReportViewModel);
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

        public IActionResult AssignMasterAssetsAssignment(long MasterEmployeeId, long MasterBranchId, long MasterProductChildId)
        {
            try
            {
                AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel = new AddMasterAssetsAssignmentViewModel();
                objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = MasterEmployeeId;
                objAddMasterAssetsAssignmentViewModel.MasterBranchId = MasterBranchId;
                objAddMasterAssetsAssignmentViewModel.MasterProductChildId = MasterProductChildId;
                //objAddMasterProductChildViewModel.ManufacturingDate = DateTime.Now;
                
                Task<string> HttpPostResponse = null;
                string endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterAssetsAssignmentViewModel);

                //Notification Message                       
                //Session is used to store object
                HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Assets Assign Successfully!", ""));


                //Assets Not Assign To Employee
                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModelList = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment?MasterCategoryId=0&MasterSubCategoryId=0&MasterBranchId=" + objAddMasterAssetsAssignmentViewModel.MasterBranchId;
                Task<string> HttpGetResponseNotAssign = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponseNotAssign != null)
                {
                    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponseNotAssign.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsNotAssignmentList = objMasterAssetsAssignmentViewModelList.ToList();

                //Assets Assign To Employee
                objMasterAssetsAssignmentViewModelList = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModelList.ToList();

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/AddMasterAssetsAssignment.cshtml", objAddMasterAssetsAssignmentViewModel);
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

        public IActionResult DeAssignMasterAssetsAssignment(long MasterEmployeeId, long MasterBranchId, long MasterProductChildId,int MasterAssetsAssignmentId)
        {
            try
            {
                AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel = new AddMasterAssetsAssignmentViewModel();
                objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = 0;
                objAddMasterAssetsAssignmentViewModel.MasterBranchId = MasterBranchId;
                objAddMasterAssetsAssignmentViewModel.MasterProductChildId = MasterProductChildId;
                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentId = MasterAssetsAssignmentId;
                //objAddMasterProductChildViewModel.ManufacturingDate = DateTime.Now;

                Task<string> HttpPostResponse = null;
                string endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterAssetsAssignmentViewModel);

                //Notification Message                       
                //Session is used to store object
                HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Assets", "Assets De-Assign Successfully!", ""));


                //Assets Not Assign To Employee
                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModelList = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment?MasterCategoryId=0&MasterSubCategoryId=0&MasterBranchId=" + objAddMasterAssetsAssignmentViewModel.MasterBranchId;
                Task<string> HttpGetResponseNotAssign = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponseNotAssign != null)
                {
                    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponseNotAssign.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsNotAssignmentList = objMasterAssetsAssignmentViewModelList.ToList();


                //Assets Assign To Employee
                objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = MasterEmployeeId;
                objMasterAssetsAssignmentViewModelList = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAssetsAssignmentViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterAssetsAssignmentViewModelList = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModelList.ToList();

                DropDownFillMethod();
                
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/AddMasterAssetsAssignment.cshtml", objAddMasterAssetsAssignmentViewModel);
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
        public IActionResult SearchMasterAssetsAssignment(AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel)
        {
            try
            {
                //Assets Not Assign To Employee
                IEnumerable<MasterAssetsAssignmentViewModel> objMasterAssetsAssignmentViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterAssetsAssignment?MasterCategoryId=" + objAddMasterAssetsAssignmentViewModel.MasterCategoryId + "&MasterSubCategoryId=" + objAddMasterAssetsAssignmentViewModel.MasterSubCategoryId + "&MasterBranchId=" + objAddMasterAssetsAssignmentViewModel.MasterBranchId;
                Task<string> HttpGetResponseNotAssign = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponseNotAssign != null)
                {
                    objMasterAssetsAssignmentViewModel = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponseNotAssign.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModel = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsNotAssignmentList = objMasterAssetsAssignmentViewModel.ToList();

                //Assets Assign To Employee
                objMasterAssetsAssignmentViewModel = null;
                endpoint = assetsApiBaseUrl + "MasterAssetsAssignment/" + objAddMasterAssetsAssignmentViewModel.MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterAssetsAssignmentViewModel = JsonConvert.DeserializeObject<IEnumerable<MasterAssetsAssignmentViewModel>>(HttpGetResponse.Result);
                }
                else
                {
                    objMasterAssetsAssignmentViewModel = Enumerable.Empty<MasterAssetsAssignmentViewModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterAssetsAssignmentViewModel.MasterAssetsAssignmentList = objMasterAssetsAssignmentViewModel.ToList();

                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterAssetsAssignment/AddMasterAssetsAssignment.cshtml", objAddMasterAssetsAssignmentViewModel);
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

            List<ViewModels.DropDownFill> objSubCategoryList = CommonFunction.DropDownFillAssets("ASMasterSubCategory", 0, "CID", assetsApiBaseUrl);
            ViewBag.SubCategoryList = new SelectList(objSubCategoryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBrandList = CommonFunction.DropDownFillAssets("ASMasterBrand", 0, "ALL", assetsApiBaseUrl);
            ViewBag.BrandList = new SelectList(objBrandList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "ALL", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeList = CommonFunction.DropDownFill("ADMasterEmployee", 0, "BID", apiBaseUrl);
            ViewBag.EmployeeList = new SelectList(objEmployeeList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

        }
    }
}
