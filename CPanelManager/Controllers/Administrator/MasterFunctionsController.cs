using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterFunction;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class MasterFunctionsController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;

        public MasterFunctionsController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<MasterFunctionsController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterFunctionViewModel objIndexMasterFunctionViewModel = new IndexMasterFunctionViewModel();
                IEnumerable<MasterFunctionViewModel> objMasterFunctionViewModelList = null;

                string endpoint = apiBaseUrl + "MasterFunctions";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterFunctionViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterFunctionViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterFunctionViewModelList = Enumerable.Empty<MasterFunctionViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexMasterFunctionViewModel.MasterFunctionList = objMasterFunctionViewModelList.OrderBy(a=> a.ParentMasterFunctionId).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/MasterFunctions/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexMasterFunctionViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexMasterFunctionViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexMasterFunctionViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexMasterFunctionViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterFunctions/Index.cshtml", objIndexMasterFunctionViewModel);
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

        public IActionResult AddMasterFunction()
        {
            try
            {
                //List<BusinessEntity.SP_CGDropDownFill_Result> objCGMasterMenuDropdownList = CGCommonFunction.DropDownFill("CGMasterMenu", 0, "PID").ToList();

                DropDownFillMethod();

                AddMasterFunctionViewModel objAddMasterFunctionViewModel = new AddMasterFunctionViewModel();
                objAddMasterFunctionViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddMasterFunctionViewModel.IsActive = true;
                objAddMasterFunctionViewModel.MasterFunctionId = CommonFunction.NextMasterId("ADMasterFunction", apiBaseUrl);
                objAddMasterFunctionViewModel.Sequence = objAddMasterFunctionViewModel.MasterFunctionId;
                objAddMasterFunctionViewModel.MasterFunctionId = 0;
                objAddMasterFunctionViewModel.ParentMasterFunctionId = 0;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterFunctions/AddMasterFunction.cshtml", objAddMasterFunctionViewModel);
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


        public IActionResult ViewMasterFunction(long MasterFunctionId)
        {
            try
            {
                AddMasterFunctionViewModel objAddMasterFunctionViewModel = null;
                string endpoint = apiBaseUrl + "MasterFunctions/" + MasterFunctionId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddMasterFunctionViewModel = JsonConvert.DeserializeObject<AddMasterFunctionViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddMasterFunctionViewModel = new AddMasterFunctionViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                DropDownFillMethod();

                objAddMasterFunctionViewModel.Mode = CommonFunction.Mode.UPDATE;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/MasterFunctions/AddMasterFunction.cshtml", objAddMasterFunctionViewModel);
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
        public IActionResult SaveMasterFunction(AddMasterFunctionViewModel objAddMasterFunctionViewModel)
        {
            try
            {
                objAddMasterFunctionViewModel.EnterById = CommonFunction.UserAuthentication(this.HttpContext);
                objAddMasterFunctionViewModel.EnterDate = DateTime.Now;
                objAddMasterFunctionViewModel.ModifiedById = CommonFunction.UserAuthentication(this.HttpContext);
                objAddMasterFunctionViewModel.ModifiedDate = DateTime.Now;

                if (objAddMasterFunctionViewModel.IsActive == null)
                {
                    objAddMasterFunctionViewModel.IsActive = false;
                }

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

                if ((ModelState.IsValid) && (!MasterFunctionExists(objAddMasterFunctionViewModel.FunctionTitle) || objAddMasterFunctionViewModel.Mode == CommonFunction.Mode.UPDATE))
                {
                    string endpoint = apiBaseUrl + "MasterFunctions";

                    Task<string> HttpPostResponse = null;

                    if (objAddMasterFunctionViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddMasterFunctionViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Function", "Master Function Insert Successfully!", ""));
                    }
                    else if (objAddMasterFunctionViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "MasterFunctions/" + objAddMasterFunctionViewModel.MasterFunctionId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddMasterFunctionViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Master Function", "Master Function Update Successfully!", ""));
                    }

                    if (HttpPostResponse != null)
                    {
                        objAddMasterFunctionViewModel = JsonConvert.DeserializeObject<AddMasterFunctionViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterFunctionViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DropDownFillMethod();
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Administrator/MasterFunctions/AddMasterFunction.cshtml", objAddMasterFunctionViewModel);
                    }
                }
                else
                {

                    ModelState.Clear();
                    if (ModelState.IsValid == false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }
                    else if (!MasterFunctionExists(objAddMasterFunctionViewModel.FunctionTitle) || objAddMasterFunctionViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        ModelState.AddModelError(string.Empty, "GenCode Type Allready Exist!");
                    }

                    DropDownFillMethod();                    

                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Administrator/MasterFunctions/AddMasterFunction.cshtml", objAddMasterFunctionViewModel);
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
        }

        [HttpPost]
        public IActionResult DeleteMasterFunction(long[] DeleteMasterFunctionIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterFunctions";

                Task<string> HttpPostResponse = null;

                if (DeleteMasterFunctionIds != null && DeleteMasterFunctionIds.Length > 0)
                {
                    foreach (int DeleteMasterFunctionId in DeleteMasterFunctionIds)
                    {
                        endpoint = apiBaseUrl + "MasterFunctions/" + DeleteMasterFunctionId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Function", "Master Function Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterFunctionViewModel)); ;
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

       
        public IActionResult DeleteMasterFunctionById(long MasterFunctionId)
        {
            try
            {
                string endpoint = apiBaseUrl + "MasterFunctions";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "MasterFunctions/" + MasterFunctionId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "Master Function", "Master Function Delete Successfully!", ""));

                    _logger.LogInformation("Database Deleted: ");//+ JsonConvert.SerializeObject(objAddMasterFunctionViewModel)); ;
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

        [AllowAnonymous]
        private bool MasterFunctionExists(string FunctionTitle)
        {
            IEnumerable<AddMasterFunctionViewModel> objAddMasterFunctionViewModellList = null;

            string endpoint = apiBaseUrl + "MasterFunctions";

            if (CommonFunction.GetWebAPI(endpoint) != null)
            {
                objAddMasterFunctionViewModellList = JsonConvert.DeserializeObject<IEnumerable<AddMasterFunctionViewModel>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
            }
            else
            {
                objAddMasterFunctionViewModellList = Enumerable.Empty<AddMasterFunctionViewModel>().ToList();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return objAddMasterFunctionViewModellList.Any(e => e.FunctionTitle.Trim().ToUpper() == FunctionTitle.Trim().ToUpper());
        }

        public void DropDownFillMethod()
        {
            List<ViewModels.DropDownFill> objParentFunctionList = CommonFunction.DropDownFill("ADMasterFunction", 0, "PID", apiBaseUrl);
            ViewBag.ParentFunctionList = new SelectList(objParentFunctionList, "MasterId", "MasterName", "----select----");
        }
    }
}
