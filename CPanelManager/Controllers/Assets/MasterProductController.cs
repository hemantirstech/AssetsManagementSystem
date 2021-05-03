using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterProduct;
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
    public class MasterProductController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterProductController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterProductController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl= _Configure.GetValue<string>("AssetsWebAPIBaseUrl");

            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterProductViewModel objIndexMasterProductViewModel = new IndexMasterProductViewModel();
                IEnumerable<MasterProductViewModel> objMasterProductViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterProduct";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductViewModelList = Enumerable.Empty<MasterProductViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductViewModel.MasterProductList = objMasterProductViewModelList.OrderBy(a => a.ProductTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProduct/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterProductViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterProductViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterProductViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterProductViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProduct/Index.cshtml", objIndexMasterProductViewModel);
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
        public IActionResult PrintMasterProduct()
        {
            try
            {
                IndexMasterProductViewModel objIndexMasterProductViewModel = new IndexMasterProductViewModel();
                IEnumerable<MasterProductViewModel> objMasterProductViewModelList = null;

                string endpoint = assetsApiBaseUrl + "MasterProduct";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductViewModelList = Enumerable.Empty<MasterProductViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductViewModel.MasterProductList = objMasterProductViewModelList.OrderBy(a => a.ProductTitle).ToList();

                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterProduct.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterProduct/PrintMasterProduct.cshtml", objIndexMasterProductViewModel)
                {
                    
                    //CustomHeaders = ,
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 10 },
                    //FileName =  "MasterProduct.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12",
                    //CustomSwitches = "--footer-center \"  Created Date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Page: [page]/[toPage]\"" +
                    //                " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
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
        public IActionResult AddMasterProduct()
        {
            try
            {
                AddMasterProductViewModel objAddMasterProductViewModel = new AddMasterProductViewModel();
                objAddMasterProductViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterProductViewModel.IsActive = true;
                //objAddMasterProductViewModel.MasterProductId = CommonFunction.NextMasterIdAssets("ADMasterProduct", assetsApiBaseUrl);
                objAddMasterProductViewModel.MasterProductId = 0;
                objAddMasterProductViewModel.DummyMasterProductId = CommonFunction.NextMasterIdAssets("ASMasterProduct", assetsApiBaseUrl);
                objAddMasterProductViewModel.MasterCategoryId = 0;
                objAddMasterProductViewModel.MasterSubCategoryId = 0;
                objAddMasterProductViewModel.MasterBranchId = 0;
                objAddMasterProductViewModel.DepreciatePercentage = 0;
                objAddMasterProductViewModel.ReorderLevel = 0;
                objAddMasterProductViewModel.ProductCurrency = 47;
                objAddMasterProductViewModel.ProductTaxCode = 1;
                objAddMasterProductViewModel.CountryOfOrigin =101;

                DropDownFillMethod();
                
                objAddMasterProductViewModel.MasterProductChildId = 0;
                objAddMasterProductViewModel.MasterBranchId = 1;
                objAddMasterProductViewModel.MasterEmployeeId = 0;
                objAddMasterProductViewModel.ConditionType = 1;
                objAddMasterProductViewModel.PurchaseDate = DateTime.Now;
                objAddMasterProductViewModel.PurchasePrice = 0;
                objAddMasterProductViewModel.DepreciatePrice = 0;
                objAddMasterProductViewModel.IterationOfWarranty = 0;
                objAddMasterProductViewModel.AssetsAssignmentDate = DateTime.Now;
                objAddMasterProductViewModel.WarrantyStartDate = DateTime.Now;
                objAddMasterProductViewModel.WarrantyExpiryDate = DateTime.Now;
                objAddMasterProductViewModel.EnterById = 1;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/MasterProduct/AddMasterProduct.cshtml", objAddMasterProductViewModel);
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
        public IActionResult ViewMasterProduct(long MasterProductId)
        {
            try
            {
                AddMasterProductViewModel objAddMasterProductViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterProduct/" + MasterProductId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProductViewModel = JsonConvert.DeserializeObject<AddMasterProductViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProductViewModel = new AddMasterProductViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objAddMasterProductViewModel.MasterBranchId = objAddMasterProductViewModel.ProductAssignChildList.Select(a => a.MasterBranchId).FirstOrDefault();
                objAddMasterProductViewModel.ManufacturerPartNumber = objAddMasterProductViewModel.ProductAssignChildList.Select(a => a.ManufacturerPartNumber).FirstOrDefault();

                DropDownFillMethod();
                objAddMasterProductViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/MasterProduct/AddMasterProduct.cshtml", objAddMasterProductViewModel);
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
        public IActionResult SaveMasterProduct(AddMasterProductViewModel objAddMasterProductViewModel)
        {
            try
            {
                var _Mode = objAddMasterProductViewModel.Mode;
                var _MasterBranchId = objAddMasterProductViewModel.MasterBranchId;
                var _ManufacturerPartNumber = objAddMasterProductViewModel.ManufacturerPartNumber;

                objAddMasterProductViewModel.EnterById = 1;
                objAddMasterProductViewModel.EnterDate = DateTime.Now;
                objAddMasterProductViewModel.ModifiedById = 1;
                objAddMasterProductViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterProductViewModel.IsActive == null)
                {
                    objAddMasterProductViewModel.IsActive = false;
                }

                if (objAddMasterProductViewModel.ConditionType == null)
                {
                    objAddMasterProductViewModel.ConditionType = 2;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid) 
                {
                    string endpoint = assetsApiBaseUrl + "MasterProduct";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterProductViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        AddMasterProductMergeViewModel objAddMasterProductMergeViewModel = AddMasterProductMergeFunction(objAddMasterProductViewModel);
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterProductMergeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Master Assets Insert Successfully!", ""));
                    }
                    else if (objAddMasterProductViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProduct/" + objAddMasterProductViewModel.MasterProductId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterProductViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Master Assets Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterProductViewModel = JsonConvert.DeserializeObject<AddMasterProductViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterProduct/AddMasterProduct.cshtml", objAddMasterProductViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }

                    string endpoint = assetsApiBaseUrl + "MasterProduct/" + objAddMasterProductViewModel.MasterProductId;
                    Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                    if (HttpGetResponse != null)
                    {
                        objAddMasterProductViewModel = JsonConvert.DeserializeObject<AddMasterProductViewModel>(HttpGetResponse.Result);
                    }
                    else
                    {
                        objAddMasterProductViewModel = new AddMasterProductViewModel();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    objAddMasterProductViewModel.Mode = _Mode;
                    objAddMasterProductViewModel.MasterBranchId = _MasterBranchId;
                    objAddMasterProductViewModel.ManufacturerPartNumber = _ManufacturerPartNumber;

                    DropDownFillMethod();                    

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Assets/MasterProduct/AddMasterProduct.cshtml", objAddMasterProductViewModel);
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
        public IActionResult DeleteMasterProduct(long[] DeleteMasterProductIds)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProducts";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterProductIds != null && DeleteMasterProductIds.Length > 0)
                {
                    foreach (long DeleteMasterProductId in DeleteMasterProductIds)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProduct/" + DeleteMasterProductId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Assets", "Master Assets Delete Successfully!", ""));

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

        public IActionResult DeleteMasterProductById(long MasterProductId)
        {
            try
            {
                string endpoint = assetsApiBaseUrl + "MasterProducts";

                Task<string> HttpPostResponse = null;

                endpoint = assetsApiBaseUrl + "MasterProduct/" + MasterProductId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Assets", "Master Assets Delete Successfully!", ""));

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

        public IActionResult IndexMasterProductChild(long MasterProductId)
        {
            try
            {
                IndexMasterProductChildViewModel objIndexMasterProductChildViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterProduct/" + MasterProductId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objIndexMasterProductChildViewModel = JsonConvert.DeserializeObject<IndexMasterProductChildViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objIndexMasterProductChildViewModel = new IndexMasterProductChildViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                
                return View("~/Views/Assets/MasterProduct/IndexMasterProductChild.cshtml", objIndexMasterProductChildViewModel);
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

        public IActionResult PrintMasterProductChild(long MasterProductId)
        {
            try
            {
                IndexMasterProductChildViewModel objIndexMasterProductChildViewModel = null;
                string endpoint = assetsApiBaseUrl + "MasterProduct/" + MasterProductId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objIndexMasterProductChildViewModel = JsonConvert.DeserializeObject<IndexMasterProductChildViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objIndexMasterProductChildViewModel = new IndexMasterProductChildViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                
                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterProductChild.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterProduct/PrintMasterProductChild.cshtml", objIndexMasterProductChildViewModel)
                {
                    //CustomHeaders = ,
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 10 },
                    //FileName =  "MasterProductChild.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12",
                    //CustomSwitches = "--footer-center \"  Created Date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Page: [page]/[toPage]\"" +
                    //                " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,

                };
                return report;

                //return View("~/Views/Assets/MasterProduct/PrintMasterProductChild.cshtml", objIndexMasterProductChildViewModel);
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

        public IActionResult AddMasterProductChild(long MasterProductId)
        {
            try
            {
                AddMasterProductChildViewModel objAddMasterProductChildViewModel = new AddMasterProductChildViewModel();

                DropDownFillMethodChild();

                string endpoint = assetsApiBaseUrl + "MasterProduct/" + MasterProductId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProductChildViewModel = JsonConvert.DeserializeObject<AddMasterProductChildViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProductChildViewModel = new AddMasterProductChildViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                //List<ViewModels.DropDownFill> objProductSKUDetail = CommonFunction.DropDownFill("ASMasterProductDetail", MasterProductId, "", assetsApiBaseUrl);
                string strProductChildSKU = (objAddMasterProductChildViewModel.MaxProductSKU!=null? objAddMasterProductChildViewModel.MaxProductSKU : objAddMasterProductChildViewModel.ProductSKU );
                //objProductSKUDetail.Select(a=>a.MasterName).FirstOrDefault();

                long _ProductChildSKU = long.Parse(strProductChildSKU);
                _ProductChildSKU++;
                objAddMasterProductChildViewModel.ProductChildSKU = _ProductChildSKU.ToString();

                objAddMasterProductChildViewModel.MasterProductId = MasterProductId;
                objAddMasterProductChildViewModel.MasterProductChildId = 0;
                objAddMasterProductChildViewModel.MasterBranchId = 1;
                objAddMasterProductChildViewModel.MasterEmployeeId = 0;
                objAddMasterProductChildViewModel.ConditionType = 1;
                objAddMasterProductChildViewModel.MasterVendorId = 0;
                objAddMasterProductChildViewModel.PurchaseDate = DateTime.Now;
                objAddMasterProductChildViewModel.PurchasePrice = 0;
                objAddMasterProductChildViewModel.DepreciatePrice = 0;
                objAddMasterProductChildViewModel.IterationOfWarranty = 0;
                objAddMasterProductChildViewModel.AssetsAssignmentDate = DateTime.Now;
                objAddMasterProductChildViewModel.WarrantyStartDate = DateTime.Now;
                objAddMasterProductChildViewModel.WarrantyExpiryDate = DateTime.Now;
                objAddMasterProductChildViewModel.Mode = CommonFunction.Mode.SAVE;
              
                objAddMasterProductChildViewModel.TransactionProductHistoryList = new List<TransactionProductHistoryViewModel>();

                return View("~/Views/Assets/MasterProduct/AddMasterProductChild.cshtml", objAddMasterProductChildViewModel);
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

        public IActionResult ViewMasterProductChild(long MasterProductChildId)
        {
            try
            {
                AddMasterProductChildViewModel objAddMasterProductChildViewModel = new AddMasterProductChildViewModel();

                DropDownFillMethodChild();

                string endpoint = assetsApiBaseUrl + "MasterProductChild/" + MasterProductChildId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterProductChildViewModel = JsonConvert.DeserializeObject<AddMasterProductChildViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterProductChildViewModel = new AddMasterProductChildViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if (objAddMasterProductChildViewModel.AssetsAssignmentDate == null)
                    objAddMasterProductChildViewModel.AssetsAssignmentDate = DateTime.Now;

                objAddMasterProductChildViewModel.Mode = CommonFunction.Mode.UPDATE;

                return View("~/Views/Assets/MasterProduct/AddMasterProductChild.cshtml", objAddMasterProductChildViewModel);
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
        public IActionResult SaveMasterProductChild(AddMasterProductChildViewModel objAddMasterProductChildViewModel)
        {
            try
            {
                long _MasterProductId = objAddMasterProductChildViewModel.MasterProductId;
                objAddMasterProductChildViewModel.EnterById = 1;
                objAddMasterProductChildViewModel.EnterDate = DateTime.Now;
                objAddMasterProductChildViewModel.ModifiedById = 1;
                objAddMasterProductChildViewModel.ModifiedDate = DateTime.Now;

                //Assignment Date is passed through ManufacturingDate
                objAddMasterProductChildViewModel.ManufacturingDate = objAddMasterProductChildViewModel.AssetsAssignmentDate;

                if (objAddMasterProductChildViewModel.ConditionType == null)
                {
                    objAddMasterProductChildViewModel.ConditionType = 2;
                }

                objAddMasterProductChildViewModel.ProductChildTitle = objAddMasterProductChildViewModel.ProductTitle;

                if (objAddMasterProductChildViewModel.ConditionType == null)
                {
                    objAddMasterProductChildViewModel.ConditionType = 2;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "MasterProductChild";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterProductChildViewModel.Mode == CommonFunction.Mode.SAVE)
                    {                        
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterProductChildViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Master Assets Insert Successfully!", ""));
                    }
                    else if (objAddMasterProductChildViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "MasterProductChild/" + objAddMasterProductChildViewModel.MasterProductChildId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterProductChildViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Master Assets Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterProductChildViewModel = JsonConvert.DeserializeObject<AddMasterProductChildViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("IndexMasterProductChild", new { MasterProductId  = _MasterProductId });
                    }
                    else
                    {
                        DropDownFillMethodChild();
                        _logger.LogWarning("Server error. Please contact administrator.");
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/MasterProduct/AddMasterProductChild.cshtml", objAddMasterProductChildViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }

                    DropDownFillMethodChild();

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Assets/MasterProduct/AddMasterProductChild.cshtml", objAddMasterProductChildViewModel);
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
        public IActionResult UpdateMasterChildProduct(long[] IsRepairs, long[] IsDeadAssets, long[] IsSaleProducts,int MasterProductId)
        {
            try
            {
                ASMasterProductChildStatusViewModel objASMasterProductChildStatusViewModel = new ASMasterProductChildStatusViewModel();
                objASMasterProductChildStatusViewModel.IsRepairs = IsRepairs;
                objASMasterProductChildStatusViewModel.IsDeadAssets = IsDeadAssets;
                objASMasterProductChildStatusViewModel.IsSaleProducts = IsSaleProducts;
                objASMasterProductChildStatusViewModel.MasterProductId = MasterProductId;

                Task<string> HttpPostResponse = null;

                string endpoint = assetsApiBaseUrl + "MasterProductChild";
                HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objASMasterProductChildStatusViewModel);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Assets", "Master Assets Updated Successfully!", ""));

                    _logger.LogInformation("Database Updated: ");//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                    return RedirectToAction("IndexMasterProductChild", new { MasterProductId = MasterProductId });
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
            List<ViewModels.DropDownFill> objCategoryList = CommonFunction.DropDownFillAssets("ASMasterCategory", 0, "ALL", assetsApiBaseUrl);
            ViewBag.CategoryList = new SelectList(objCategoryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objSubCategoryList = CommonFunction.DropDownFillAssets("ASMasterSubCategory", 0, "CID", assetsApiBaseUrl);
            ViewBag.SubCategoryList = new SelectList(objSubCategoryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBrandList = CommonFunction.DropDownFillAssets("ASMasterBrand", 0, "ALL", assetsApiBaseUrl);
            ViewBag.BrandList = new SelectList(objBrandList, "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objProductSizeList = CommonFunction.DropDownFillAssets("ASMasterProductSize", 0, "ALL", assetsApiBaseUrl);
            //ViewBag.ProductSizeList = new SelectList(objProductSizeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCurrencyList = CommonFunction.DropDownFill("ADMasterCurrency", 0, "ALL", apiBaseUrl);
            ViewBag.CurrencyList = new SelectList(objCurrencyList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objTaxList = CommonFunction.DropDownFill("ADMasterTax", 0, "ALL", apiBaseUrl);
            ViewBag.TaxList = new SelectList(objTaxList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "ALL", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeList = CommonFunction.DropDownFill("ADMasterEmployee", 0, "BID", apiBaseUrl);
            ViewBag.EmployeeList = new SelectList(objEmployeeList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            //List<ViewModels.DropDownFill> objColorList = CommonFunction.DropDownFill("ADMasterColor", 0, "ALL", apiBaseUrl);
            //ViewBag.ColorList = new SelectList(objColorList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");
           
            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyList = new SelectList(objCompanyList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

        }

        public void DropDownFillMethodChild()
        {
            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyList = new SelectList(objCompanyList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");


            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "ALL", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeList = CommonFunction.DropDownFill("ADMasterEmployee", 0, "BID", apiBaseUrl);
            ViewBag.EmployeeList = new SelectList(objEmployeeList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

        }

        public AddMasterProductMergeViewModel AddMasterProductMergeFunction(AddMasterProductViewModel objAddMasterProductViewModel)
        {
            AddMasterProductMergeViewModel objAddMasterProductMergeViewModel = new AddMasterProductMergeViewModel();
            AddMasterProductViewModel objAddMasterProductNewViewModel = new AddMasterProductViewModel();
            AddMasterProductChildViewModel objAddMasterProductChildViewModel = new AddMasterProductChildViewModel();
            AddMasterAssetsAssignmentViewModel objAddMasterAssetsAssignmentViewModel = new AddMasterAssetsAssignmentViewModel();

            //AddMasterProductViewModel
            objAddMasterProductNewViewModel.MasterProductId = objAddMasterProductViewModel.MasterProductId;
            objAddMasterProductNewViewModel.MasterSubCategoryId = objAddMasterProductViewModel.MasterSubCategoryId;
            objAddMasterProductNewViewModel.MasterBrandId = objAddMasterProductViewModel.MasterBrandId;
            objAddMasterProductNewViewModel.ProductTitle = objAddMasterProductViewModel.ProductTitle;
            objAddMasterProductNewViewModel.ProductSKU = objAddMasterProductViewModel.ProductSKU;
            objAddMasterProductNewViewModel.IsActive = objAddMasterProductViewModel.IsActive;
            objAddMasterProductNewViewModel.ProductTaxCode = objAddMasterProductViewModel.ProductTaxCode;
            objAddMasterProductNewViewModel.DepreciatePercentage = objAddMasterProductViewModel.DepreciatePercentage;
            objAddMasterProductNewViewModel.CountryOfOrigin = objAddMasterProductViewModel.CountryOfOrigin;
            objAddMasterProductNewViewModel.ProductCurrency = objAddMasterProductViewModel.ProductCurrency;
            objAddMasterProductNewViewModel.ProductHSNCode = objAddMasterProductViewModel.ProductHSNCode;
            objAddMasterProductNewViewModel.ReorderLevel = objAddMasterProductViewModel.ReorderLevel;
            objAddMasterProductNewViewModel.ProductModel = objAddMasterProductViewModel.ProductModel;
            objAddMasterProductNewViewModel.Manufacturer = objAddMasterProductViewModel.Manufacturer;
            objAddMasterProductNewViewModel.EnterById = objAddMasterProductViewModel.EnterById;
            objAddMasterProductNewViewModel.EnterDate = objAddMasterProductViewModel.EnterDate;
            objAddMasterProductNewViewModel.ModifiedById = objAddMasterProductViewModel.ModifiedById;
            objAddMasterProductNewViewModel.ModifiedDate = objAddMasterProductViewModel.ModifiedDate;
            //End AddMasterProductViewModel

            //AddMasterProductChildViewModel
            objAddMasterProductChildViewModel.MasterProductChildId = objAddMasterProductViewModel.MasterProductChildId;
            objAddMasterProductChildViewModel.MasterProductId = objAddMasterProductViewModel.MasterProductId;
            objAddMasterProductChildViewModel.MasterBranchId = objAddMasterProductViewModel.MasterBranchId;
            objAddMasterProductChildViewModel.MasterEmployeeId = objAddMasterProductViewModel.MasterEmployeeId;

            long _ProductChildSKU = long.Parse(objAddMasterProductViewModel.ProductSKU);
            _ProductChildSKU++;

            objAddMasterProductChildViewModel.ProductChildSKU = _ProductChildSKU.ToString();
            objAddMasterProductChildViewModel.IsActive = objAddMasterProductViewModel.IsActive;
            objAddMasterProductChildViewModel.ProductTitle = objAddMasterProductViewModel.ProductTitle;
            objAddMasterProductChildViewModel.ManufacturerPartNumber = objAddMasterProductViewModel.ManufacturerPartNumber;
            objAddMasterProductChildViewModel.ConditionType = objAddMasterProductViewModel.ConditionType;
            objAddMasterProductChildViewModel.ManufacturingDate = objAddMasterProductViewModel.ManufacturingDate;
            objAddMasterProductChildViewModel.PurchaseDate = objAddMasterProductViewModel.PurchaseDate;
            objAddMasterProductChildViewModel.PurchasePrice = objAddMasterProductViewModel.PurchasePrice;
            objAddMasterProductChildViewModel.DepreciatePrice = objAddMasterProductViewModel.DepreciatePrice;
            objAddMasterProductChildViewModel.MasterVendorId = objAddMasterProductViewModel.MasterVendorId;
            objAddMasterProductChildViewModel.ProductQty = 1;
            objAddMasterProductChildViewModel.IterationOfWarranty = objAddMasterProductViewModel.IterationOfWarranty;
            objAddMasterProductChildViewModel.WarrantyStartDate = objAddMasterProductViewModel.WarrantyStartDate;
            objAddMasterProductChildViewModel.WarrantyExpiryDate = objAddMasterProductViewModel.WarrantyExpiryDate;
            objAddMasterProductChildViewModel.ServiceURL = objAddMasterProductViewModel.ServiceURL;
            objAddMasterProductChildViewModel.ServiceUserName = objAddMasterProductViewModel.ServiceUserName;
            objAddMasterProductChildViewModel.ServicePassword = objAddMasterProductViewModel.ServicePassword;
            objAddMasterProductChildViewModel.MasterCompanyOwnerId = objAddMasterProductViewModel.MasterCompanyOwnerId;
            objAddMasterProductChildViewModel.CompanyOwnerTitle = objAddMasterProductViewModel.CompanyOwnerTitle;

            objAddMasterProductChildViewModel.IsActive = true;
            objAddMasterProductChildViewModel.EnterById = objAddMasterProductViewModel.EnterById;
            objAddMasterProductChildViewModel.EnterDate = objAddMasterProductViewModel.EnterDate;
            objAddMasterProductChildViewModel.ModifiedById = objAddMasterProductViewModel.ModifiedById;
            objAddMasterProductChildViewModel.ModifiedDate = objAddMasterProductViewModel.ModifiedDate;
            //End AddMasterProductChildViewModel


            //AddMasterAssetsAssignmentViewModel
            objAddMasterAssetsAssignmentViewModel.AssetsAssignmentDate = objAddMasterProductViewModel.AssetsAssignmentDate;
            objAddMasterAssetsAssignmentViewModel.MasterEmployeeId = objAddMasterProductViewModel.MasterEmployeeId;
            objAddMasterAssetsAssignmentViewModel.MasterLocationId = objAddMasterProductViewModel.MasterBranchId;
            objAddMasterAssetsAssignmentViewModel.IsActive = true;
            objAddMasterAssetsAssignmentViewModel.IsAssetsDeAssign = false;
            objAddMasterAssetsAssignmentViewModel.EnterById = objAddMasterProductViewModel.EnterById;
            objAddMasterAssetsAssignmentViewModel.EnterDate = objAddMasterProductViewModel.EnterDate;
            objAddMasterAssetsAssignmentViewModel.ModifiedById = objAddMasterProductViewModel.ModifiedById;
            objAddMasterAssetsAssignmentViewModel.ModifiedDate = objAddMasterProductViewModel.ModifiedDate;

            objAddMasterProductMergeViewModel.ASMasterProduct = objAddMasterProductNewViewModel;
            objAddMasterProductMergeViewModel.ASMasterProductChild = objAddMasterProductChildViewModel;
            objAddMasterProductMergeViewModel.ASMasterAssetsAssignment = objAddMasterAssetsAssignmentViewModel;

            return objAddMasterProductMergeViewModel;
        }

        public IActionResult IndexDetailMasterProductChild(long MasterCategoryId=0,long MasterSubCategoryId=0,long MasterBrandId=0)
        {
            try
            {
                IndexMasterProductViewModel objIndexMasterProductViewModel = new IndexMasterProductViewModel();
                IEnumerable<MasterProductViewModel> objMasterProductViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductByCategory?MasterCategoryId=" + MasterCategoryId + "&MasterSubCategoryId=" + MasterSubCategoryId + "&MasterBrandId=" + MasterBrandId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductViewModelList = Enumerable.Empty<MasterProductViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductViewModel.MasterProductList = objMasterProductViewModelList.OrderBy(a => a.ProductTitle).ToList();
                objIndexMasterProductViewModel.MasterCategoryId = MasterCategoryId;
                objIndexMasterProductViewModel.MasterSubCategoryId = MasterSubCategoryId;
                objIndexMasterProductViewModel.MasterBrandId = MasterBrandId;

                objIndexMasterProductViewModel.CategoryTitle = objIndexMasterProductViewModel.MasterProductList.Select(a=>a.CategoryTitle).FirstOrDefault();
                objIndexMasterProductViewModel.SubCategoryTitle = objIndexMasterProductViewModel.MasterProductList.Select(a => a.SubCategoryTitle).FirstOrDefault(); ;
                objIndexMasterProductViewModel.BrandTitle = objIndexMasterProductViewModel.MasterProductList.Select(a => a.BrandTitle).FirstOrDefault(); ;

                return View("~/Views/Assets/MasterProduct/IndexDetailMasterProductChild.cshtml", objIndexMasterProductViewModel);
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


        public IActionResult PrintDetailMasterProductChild(long MasterCategoryId = 0, long MasterSubCategoryId = 0, long MasterBrandId = 0)
        {
            try
            {
                IndexMasterProductViewModel objIndexMasterProductViewModel = new IndexMasterProductViewModel();
                IEnumerable<MasterProductViewModel> objMasterProductViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductByCategory?MasterCategoryId=" + MasterCategoryId + "&MasterSubCategoryId=" + MasterSubCategoryId + "&MasterBrandId=" + MasterBrandId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProductViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProductViewModelList = Enumerable.Empty<MasterProductViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterProductViewModel.MasterProductList = objMasterProductViewModelList.OrderBy(a => a.ProductTitle).ToList();
                objIndexMasterProductViewModel.MasterCategoryId = MasterCategoryId;
                objIndexMasterProductViewModel.MasterSubCategoryId = MasterSubCategoryId;
                objIndexMasterProductViewModel.MasterBrandId = MasterBrandId;

                objIndexMasterProductViewModel.CategoryTitle = objIndexMasterProductViewModel.MasterProductList.Select(a => a.CategoryTitle).FirstOrDefault();
                objIndexMasterProductViewModel.SubCategoryTitle = objIndexMasterProductViewModel.MasterProductList.Select(a => a.SubCategoryTitle).FirstOrDefault(); ;
                objIndexMasterProductViewModel.BrandTitle = objIndexMasterProductViewModel.MasterProductList.Select(a => a.BrandTitle).FirstOrDefault(); ;

                
                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\MasterDetailProduct.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/MasterProduct/PrintDetailMasterProductChild.cshtml", objIndexMasterProductViewModel)
                {

                    //CustomHeaders = ,
                    MinimumFontSize = 10,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 10 },
                    //FileName =  "MasterDetailProduct.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12",
                    //CustomSwitches = "--footer-center \"  Created Date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Page: [page]/[toPage]\"" +
                    //                " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\"",
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
    }
}
