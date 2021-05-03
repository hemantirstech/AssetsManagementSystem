using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.AssetsExpiry;
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
    public class AssetsExpiryController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AssetsExpiryController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl= _Configure.GetValue<string>("AssetsWebAPIBaseUrl");

            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index(int CategoryType=0)
        {
            try
            {
                IndexAssetsExpiryViewModel objIndexAssetsExpiryViewModel = new IndexAssetsExpiryViewModel();
                IEnumerable<AssetsExpiryViewModel> objAssetsExpiryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductExpiry";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsExpiryViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsExpiryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsExpiryViewModelList = Enumerable.Empty<AssetsExpiryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if(CategoryType>0)
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.Where(a=>a.MasterCategoryType== CategoryType).OrderBy(a => a.ProductTitle).ToList();
                }
                else 
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.OrderBy(a => a.ProductTitle).ToList();
                }

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProduct/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexAssetsExpiryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexAssetsExpiryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexAssetsExpiryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexAssetsExpiryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################
                objIndexAssetsExpiryViewModel.MasterCategoryType = CategoryType;
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/AssetsExpiry/Index.cshtml", objIndexAssetsExpiryViewModel);
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

        public IActionResult PrintRenewalReminder(int CategoryType = 0)
        {
            try
            {
                ModelState.Clear();
                IndexAssetsExpiryViewModel objIndexAssetsExpiryViewModel = new IndexAssetsExpiryViewModel();
                IEnumerable<AssetsExpiryViewModel> objAssetsExpiryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductExpiry";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsExpiryViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsExpiryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsExpiryViewModelList = Enumerable.Empty<AssetsExpiryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if (CategoryType > 0)
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.Where(a => a.MasterCategoryType == CategoryType).OrderBy(a => a.ProductTitle).ToList();
                }
                else
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.OrderBy(a => a.ProductTitle).ToList();
                }
                objIndexAssetsExpiryViewModel.MasterCategoryType = CategoryType;

                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\PrintRenewalReminder.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/AssetsExpiry/PrintRenewalReminder.cshtml", objIndexAssetsExpiryViewModel)
                {
                    MinimumFontSize = 8,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName =  "PrintRenewalReminder.pdf",
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


        public IActionResult IndexExpired(int CategoryType = 0)
        {
            try
            {
                IndexAssetsExpiryViewModel objIndexAssetsExpiryViewModel = new IndexAssetsExpiryViewModel();
                IEnumerable<AssetsExpiryViewModel> objAssetsExpiryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductExpiry/0";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsExpiryViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsExpiryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsExpiryViewModelList = Enumerable.Empty<AssetsExpiryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if (CategoryType > 0)
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.Where(a => a.MasterCategoryType == CategoryType).OrderBy(a => a.ProductTitle).ToList();
                }
                else
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.OrderBy(a => a.ProductTitle).ToList();
                }

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterProduct/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexAssetsExpiryViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexAssetsExpiryViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexAssetsExpiryViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexAssetsExpiryViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################
                objIndexAssetsExpiryViewModel.MasterCategoryType = CategoryType;
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/AssetsExpiry/IndexExpired.cshtml", objIndexAssetsExpiryViewModel);
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

        public IActionResult PrintExpired(int CategoryType = 0)
        {
            try
            {
                IndexAssetsExpiryViewModel objIndexAssetsExpiryViewModel = new IndexAssetsExpiryViewModel();
                IEnumerable<AssetsExpiryViewModel> objAssetsExpiryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductExpiry/0";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsExpiryViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsExpiryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsExpiryViewModelList = Enumerable.Empty<AssetsExpiryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if (CategoryType > 0)
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.Where(a => a.MasterCategoryType == CategoryType).OrderBy(a => a.ProductTitle).ToList();
                }
                else
                {
                    objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.OrderBy(a => a.ProductTitle).ToList();
                }
                objIndexAssetsExpiryViewModel.MasterCategoryType = CategoryType;

                string filePath = $"{webHostEnvironment.WebRootPath}\\Reports\\PrintExpired.pdf";
                System.IO.FileInfo DelFile = new System.IO.FileInfo(filePath);

                if (DelFile.Exists)
                    DelFile.Delete();

                var report = new ViewAsPdf("~/Views/Assets/AssetsExpiry/PrintExpired.cshtml", objIndexAssetsExpiryViewModel)
                {
                    MinimumFontSize = 8,
                    PageMargins = { Left = 10, Bottom = 10, Right = 10, Top = 5 },
                    //FileName =  "PrintExpired.pdf",
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

        [AllowAnonymous]
        public ActionResult FillNotification(int MasterRegistrationId)
        {
            try
            {
                IndexAssetsExpiryViewModel objIndexAssetsExpiryViewModel = new IndexAssetsExpiryViewModel();
                IEnumerable<AssetsExpiryViewModel> objAssetsExpiryViewModelList = null;

                string endpoint = assetsApiBaseUrl + "ProductExpiry/0";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAssetsExpiryViewModelList = JsonConvert.DeserializeObject<IEnumerable<AssetsExpiryViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objAssetsExpiryViewModelList = Enumerable.Empty<AssetsExpiryViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                string data = JsonConvert.SerializeObject(objAssetsExpiryViewModelList.Take(10).ToList());

                JsonResult objJsonResult = new JsonResult(data);

                if (objAssetsExpiryViewModelList.Count() > 0)
                    return objJsonResult;

                //BusinessAccessLayer.LMMessageNotificationBAL objLMMessageNotificationBAL = new LMMessageNotificationBAL();
                //BusinessEntity.LMMessageNotification objLMMessageNotification = new BusinessEntity.LMMessageNotification();
                //objLMMessageNotification.SendTo = MasterRegistrationId;

                //List<BusinessEntity.SP_LMMessageNotification_Result> MessageNotificationList = objLMMessageNotificationBAL.LMMessageNotification_Select(objLMMessageNotification, 0, 10, 51, "").ToList();

                //if (MessageNotificationList.Count() > 0)
                //    return Json(MessageNotificationList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return Json("");
        }



        //public FileResult DownLoadFile(int id)
        //{

        //    List<ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel> ObjFiles = new List<ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel>();

        //    var FileById = (from TPH in ObjFiles
        //                    where TPH.TransactionProductHistoryId.Equals(id)
        //                    select new { TPH.ProductTitle, TPH.UploadDocument }).ToList().FirstOrDefault();

        //    return File(FileById.UploadDocument, "application/pdf", FileById.ProductTitle);

        //}

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/img/app-images", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));

        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.ms-excel"},
               // {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        //[HttpGet("download")]
        //public IActionResult GetBlobDownload([FromQuery] string link)
        //{
        //    var net = new System.Net.WebClient();
        //    var data = net.DownloadData("~/wwwroot/img/app-images"+link);
        //    var content = new System.IO.MemoryStream(data);
        //    var contentType = "APPLICATION/octet-stream";
        //    var fileName = "something.bin";
        //    return File(content, contentType, fileName);
        //}
        public IActionResult ViewRenewalProduct(long TransactionProductHistoryId)
        {
            try
            {
                ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel = null;
                string endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + TransactionProductHistoryId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddTransactionProductHistoryViewModel = JsonConvert.DeserializeObject<ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddTransactionProductHistoryViewModel = new ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddTransactionProductHistoryViewModel.Mode = CommonFunction.Mode.UPDATE;
                return View("~/Views/Assets/AssetsExpiry/AddRenewalProduct.cshtml", objAddTransactionProductHistoryViewModel);
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

        public IActionResult AddRenewalProduct(long MasterProductChildId, string ProductTitle,string IndexType)
        {
            try
            {
                ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel = new ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel();

                objAddTransactionProductHistoryViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddTransactionProductHistoryViewModel.IsActive = true;
                objAddTransactionProductHistoryViewModel.TransactionProductHistoryId = CommonFunction.NextMasterIdAssets("TransactionProductHistory", assetsApiBaseUrl);
                objAddTransactionProductHistoryViewModel.MasterProductChildId = MasterProductChildId;
                objAddTransactionProductHistoryViewModel.ProductTitle = ProductTitle;
                objAddTransactionProductHistoryViewModel.MasterSubscriptionTypeId = 1;
                objAddTransactionProductHistoryViewModel.SubscriptionPrice = 0;
                objAddTransactionProductHistoryViewModel.MasterSubscriptionVendorId = 0;
                objAddTransactionProductHistoryViewModel.SubscriptionDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.SubscriptionStartDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.SubscriptionExpiryDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.UploadInvoice = "";
                objAddTransactionProductHistoryViewModel.UploadDocument = "";
                objAddTransactionProductHistoryViewModel.UploadWarretyCard = "";
                objAddTransactionProductHistoryViewModel.VendorTitle = "";
                objAddTransactionProductHistoryViewModel.IndexType = IndexType;
                DropDownFillMethod();

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Assets/AssetsExpiry/AddRenewalProduct.cshtml", objAddTransactionProductHistoryViewModel);
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

        public IActionResult SaveRenewalProduct(ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel objAddTransactionProductHistoryViewModel, IFormFile _UploadInvoice, IFormFile _UploadDocument, IFormFile _UploadWarretyCard)
        {
            try
            {
                //for main image Logo
                if (_UploadInvoice != null)
                    objAddTransactionProductHistoryViewModel.UploadInvoice = UploadedImageFile(_UploadInvoice, objAddTransactionProductHistoryViewModel.UploadInvoice);

                if (_UploadDocument != null)
                    objAddTransactionProductHistoryViewModel.UploadDocument = UploadedImageFile(_UploadDocument, objAddTransactionProductHistoryViewModel.UploadDocument);

                if (_UploadWarretyCard != null)
                    objAddTransactionProductHistoryViewModel.UploadWarretyCard = UploadedImageFile(_UploadWarretyCard, objAddTransactionProductHistoryViewModel.UploadWarretyCard);

                objAddTransactionProductHistoryViewModel.EnterById = 1;
                objAddTransactionProductHistoryViewModel.EnterDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.ModifiedById = 1;
                objAddTransactionProductHistoryViewModel.ModifiedDate = DateTime.Now;
                objAddTransactionProductHistoryViewModel.IsActive = true;

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                if (ModelState.IsValid)
                {
                    string endpoint = assetsApiBaseUrl + "TransactionProductHistory";

                    Task<string> HttpPostResponse = null;

                    if (objAddTransactionProductHistoryViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddTransactionProductHistoryViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Product", "Renewal Product Details Insert Successfully!", ""));
                    }
                    else if (objAddTransactionProductHistoryViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = assetsApiBaseUrl + "TransactionProductHistory/" + objAddTransactionProductHistoryViewModel.TransactionProductHistoryId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddTransactionProductHistoryViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Product", "Renewal Product Details Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddTransactionProductHistoryViewModel = JsonConvert.DeserializeObject<ViewModels.TransactionProductHistory.AddTransactionProductHistoryViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterCompanyViewModel)); ;
                        
                        if(objAddTransactionProductHistoryViewModel.IndexType== "IndexExpired")
                        {
                            return RedirectToAction("IndexExpired");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Assets/AssetsExpiry/AddRenewalProduct.cshtml", objAddTransactionProductHistoryViewModel);
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
                    return View("~/Views/Assets/AssetsExpiry/AddRenewalProduct.cshtml", objAddTransactionProductHistoryViewModel);
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

        private string UploadedImageFile(IFormFile _HttpFile, string OldFile)
        {
            string uniqueFileName = null;
            string DelMainImage = OldFile;

            if (_HttpFile != null)
            {
                var allowedExtensions = new[] { ".jpeg", ".png", ".gif", ".jpg", ".pdf", ".doc", ".xlsx", ".txt" };
                string picExtenction = System.IO.Path.GetExtension(_HttpFile.FileName.ToLower());

                string uploadsFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "img/app-images");
                uniqueFileName = "Image_" + Guid.NewGuid().ToString() + picExtenction;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (DelMainImage != null)
                {
                    string DelPath = System.IO.Path.Combine(uploadsFolder, DelMainImage);
                    System.IO.FileInfo DelFile = new System.IO.FileInfo(DelPath);
                    if (DelFile.Exists)
                        DelFile.Delete();
                }

                if (allowedExtensions.Contains(picExtenction))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        _HttpFile.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }


        public void DropDownFillMethod()
        {
            //List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            //ViewBag.CompanyList = new SelectList(objCompanyList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objVendorList = CommonFunction.DropDownFill("ADMasterVendor", 0, "ALL", apiBaseUrl);
            ViewBag.VendorList = new SelectList(objVendorList.OrderBy(a => a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

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
    }
}
