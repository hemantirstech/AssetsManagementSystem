using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterEmployee;
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

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterEmployeeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterEmployeeController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
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
                IndexMasterEmployeeViewModel objIndexMasterEmployeeViewModel = new IndexMasterEmployeeViewModel();
                IEnumerable<MasterEmployeeViewModel> objMasterEmployeeViewModellList = null;
                string endpoint = apiBaseUrl + "MasterEmployees";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterEmployeeViewModellList = JsonConvert.DeserializeObject<IEnumerable<MasterEmployeeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterEmployeeViewModellList = Enumerable.Empty<MasterEmployeeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterEmployeeViewModel.MasterEmployeeList = objMasterEmployeeViewModellList.ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterEmployee/Index");

                if(objValidateAccountViewModel!=null)
                {
                    objIndexMasterEmployeeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterEmployeeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterEmployeeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterEmployeeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################
                
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployee/Index.cshtml", objIndexMasterEmployeeViewModel);
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

        public IActionResult AddMasterEmployee()
        {
            try
            {
                AddMasterEmployeeViewModel objAddMasterEmployeeViewModel = new AddMasterEmployeeViewModel();
                objAddMasterEmployeeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterEmployeeViewModel.IsActive = true;
                objAddMasterEmployeeViewModel.MasterSalutationId = 1;
                objAddMasterEmployeeViewModel.Gender = 1;
                objAddMasterEmployeeViewModel.MasterDesignationId = 1;
                objAddMasterEmployeeViewModel.MasterDepartmentId = 1;
                objAddMasterEmployeeViewModel.MasterEmployeeTypeId = 1;
                objAddMasterEmployeeViewModel.MasterEmployeeStatusId = 2;
                objAddMasterEmployeeViewModel.MasterTimeZoneId = 91;
                objAddMasterEmployeeViewModel.MasterPaymentTypeId = 1;
                objAddMasterEmployeeViewModel.MasterBankAccountTypeId = 1;
                objAddMasterEmployeeViewModel.MasterCountryId = 101;
                objAddMasterEmployeeViewModel.MasterBranchId = 1;
                objAddMasterEmployeeViewModel.MasterCountryId = 1;
                objAddMasterEmployeeViewModel.DateOfJoining = DateTime.Now;
                objAddMasterEmployeeViewModel.DateOfBirth = DateTime.Now;
                objAddMasterEmployeeViewModel.MasterEmployeeId = CommonFunction.NextMasterId("ADMasterEmployee", apiBaseUrl);
                objAddMasterEmployeeViewModel.MasterEmployeeId = 0;
                objAddMasterEmployeeViewModel.EmployeeCode = CommonFunction.Encrypt("P@ssword",true);
                objAddMasterEmployeeViewModel.EmployeeCode = CommonFunction.RamdomCode("EMPLOYEE", apiBaseUrl);
                objAddMasterEmployeeViewModel.CostOfAssetsAssign = "0";

                DropDownFillMethod();
                objAddMasterEmployeeViewModel.ProductDetailResultList = new List<ProductDetailResult>();
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Master/MasterEmployee/AddMasterEmployee.cshtml", objAddMasterEmployeeViewModel);
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
        public IActionResult ViewMasterEmployee(long MasterEmployeeId)
        {
            try
            {
                AddMasterEmployeeViewModel objAddMasterEmployeeViewModel = null;
                string endpoint = apiBaseUrl + "MasterEmployees/" + MasterEmployeeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterEmployeeViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterEmployeeViewModel = new AddMasterEmployeeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();
                objAddMasterEmployeeViewModel.CostOfAssetsAssign = "2,15,000";
                objAddMasterEmployeeViewModel.Mode = CommonFunction.Mode.UPDATE;

                IEnumerable<ProductDetailResult> objProductDetailResultList = null;
                endpoint = assetsApiBaseUrl + "Dashboard/" + MasterEmployeeId;
                Task<string> HttpGetResponseProduct = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objProductDetailResultList = JsonConvert.DeserializeObject<IEnumerable<ProductDetailResult>>(HttpGetResponseProduct.Result).ToList();
                }
                else
                {
                    objProductDetailResultList = Enumerable.Empty<ProductDetailResult>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddMasterEmployeeViewModel.ProductDetailResultList = objProductDetailResultList.ToList();
                objAddMasterEmployeeViewModel.CostOfAssetsAssign = objProductDetailResultList.Sum(a=>a.TotalAssetsCost).ToString();
                return View("~/Views/Master/MasterEmployee/AddMasterEmployee.cshtml", objAddMasterEmployeeViewModel);
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
        public IActionResult SaveMasterEmployee(AddMasterEmployeeViewModel objAddMasterEmployeeViewModel, IFormFile _EmployeeLogo)
        {
            try
            {
                //for main image Logo
                if (_EmployeeLogo != null)
                    objAddMasterEmployeeViewModel.EmployeeLogo = UploadedImageFile(_EmployeeLogo, objAddMasterEmployeeViewModel.EmployeeLogo);

                objAddMasterEmployeeViewModel.EnterById = 1;
                objAddMasterEmployeeViewModel.EnterDate = DateTime.Now;
                objAddMasterEmployeeViewModel.ModifiedById = 1;
                objAddMasterEmployeeViewModel.ModifiedDate = DateTime.Now;
                objAddMasterEmployeeViewModel.IsActive = true;

                if (objAddMasterEmployeeViewModel.Gender == null)
                {
                    objAddMasterEmployeeViewModel.Gender = 2;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                if (ModelState.IsValid)
                {
                    string endpoint = apiBaseUrl + "MasterEmployees";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterEmployeeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterEmployeeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Employee", "Master Employee Insert Successfully!", ""));
                    }
                    else if (objAddMasterEmployeeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterEmployees/" + objAddMasterEmployeeViewModel.MasterEmployeeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterEmployeeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Employee", "Master Employee Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterEmployeeViewModel = JsonConvert.DeserializeObject<AddMasterEmployeeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Master/MasterEmployee/AddMasterEmployee.cshtml", objAddMasterEmployeeViewModel);
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
                    return View("~/Views/Master/MasterEmployee/AddMasterEmployee.cshtml", objAddMasterEmployeeViewModel);
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
        public IActionResult DeleteMasterEmployee(long DeleteMasterEmployeeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterEmployees";

                Task<string> HttpPostResponse = null;

                //if (DeleteMasterEmployeeIds != null && DeleteMasterEmployeeIds.Length > 0)
                //{
                //    foreach (int DeleteMasterEmployeeId in DeleteMasterEmployeeIds)
                //    {
                //        endpoint = apiBaseUrl + "MasterEmployees/" + DeleteMasterEmployeeId;
                //        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                //    }
                //}

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Employee", "Master Employee Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeViewModel)); ;
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


        public IActionResult DeleteMasterEmployeeById(long MasterEmployeeId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterEmployees";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterEmployees/" + MasterEmployeeId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Employee", "Master Employee Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterEmployeeViewModel)); ;
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
            List<ViewModels.DropDownFill> objSalutationList = CommonFunction.DropDownFill("ADMasterSalutation", 0, "ALL", apiBaseUrl);
            ViewBag.SalutationList = new SelectList(objSalutationList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objDesignationList = CommonFunction.DropDownFill("ADMasterDesignation", 0, "ALL", apiBaseUrl);
            ViewBag.DesignationList = new SelectList(objDesignationList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objDepartmentList = CommonFunction.DropDownFill("ADMasterDepartment", 0, "ALL", apiBaseUrl);
            ViewBag.DepartmentList = new SelectList(objDepartmentList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeHeadList = CommonFunction.DropDownFill("ADMasterEmployeeHead", 0, "ALL", apiBaseUrl);
            ViewBag.EmployeeHeadList = new SelectList(objEmployeeHeadList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCountryList = CommonFunction.DropDownFill("ADMasterCountry", 0, "ALL", apiBaseUrl);
            ViewBag.CountryList = new SelectList(objCountryList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objStateList = CommonFunction.DropDownFill("ADMasterState", 0, "ID", apiBaseUrl);
            ViewBag.StateList = new SelectList(objStateList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeTypeList = CommonFunction.DropDownFill("ADMasterEmployeeType", 0, "ALL", apiBaseUrl);
            ViewBag.EmployeeTypeList = new SelectList(objEmployeeTypeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objEmployeeStatusList = CommonFunction.DropDownFill("ADMasterEmployeeStatus", 0, "ALL", apiBaseUrl);
            ViewBag.EmployeeStatusList = new SelectList(objEmployeeStatusList.OrderBy(a=>a.MasterName).ToList(), "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objTimeZoneList = CommonFunction.DropDownFill("ADMasterTimeZone", 0, "ALL", apiBaseUrl);
            ViewBag.TimeZoneList = new SelectList(objTimeZoneList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBankAccountTypeList = CommonFunction.DropDownFill("ADMasterBankAccountType", 0, "ALL", apiBaseUrl);
            ViewBag.BankAccountTypeList = new SelectList(objBankAccountTypeList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objCompanyList = CommonFunction.DropDownFill("ADMasterCompany", 0, "ALL", apiBaseUrl);
            ViewBag.CompanyList = new SelectList(objCompanyList, "MasterId", "MasterName", "----select----");

            List<ViewModels.DropDownFill> objBranchList = CommonFunction.DropDownFill("ADMasterBranch", 0, "CID", apiBaseUrl);
            ViewBag.BranchList = new SelectList(objBranchList, "MasterId", "MasterName", "----select----");
        }

        private string UploadedImageFile(IFormFile _HttpFile, string OldFile)
        {
            string uniqueFileName = null;
            string DelMainImage = OldFile;

            if (_HttpFile != null)
            {
                var allowedExtensions = new[] { ".jpeg", ".png", ".gif", ".jpg" };
                string picExtenction = System.IO.Path.GetExtension(_HttpFile.FileName.ToLower());

                string uploadsFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "img/app-images");
                uniqueFileName = "EmpImage_" + Guid.NewGuid().ToString() + picExtenction;
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


    }
}
