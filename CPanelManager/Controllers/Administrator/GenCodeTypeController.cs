using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.GenCodeType;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using System.Net;

namespace CPanelManager.Controllers.Administrator
{
    [Authorize]
    public class GenCodeTypeController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;

        public GenCodeTypeController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<GenCodeTypeController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            try
            {      
                IndexGenCodeTypeViewModel objIndexGenCodeTypeViewModel = new IndexGenCodeTypeViewModel();
                IEnumerable<GenCodeTypeViewModel> objGenCodeTypeViewModellList = null;
                string endpoint = apiBaseUrl + "GenCodeTypes";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objGenCodeTypeViewModellList = JsonConvert.DeserializeObject<IEnumerable<GenCodeTypeViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objGenCodeTypeViewModellList = Enumerable.Empty<GenCodeTypeViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexGenCodeTypeViewModel.GenCodeTypeList = objGenCodeTypeViewModellList.ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/GenCodeType/Index");

                if(objValidateAccountViewModel!=null)
                {
                    objIndexGenCodeTypeViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexGenCodeTypeViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexGenCodeTypeViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexGenCodeTypeViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################
                
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/GenCodeType/Index.cshtml", objIndexGenCodeTypeViewModel);
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

        public IActionResult AddGenCodeType()
        {
            try
            {
                AddGenCodeTypeViewModel objAddGenCodeTypeViewModel = new AddGenCodeTypeViewModel();
                objAddGenCodeTypeViewModel.Mode = CommonFunction.Mode.SAVE;
                objAddGenCodeTypeViewModel.IsActive = true;
                objAddGenCodeTypeViewModel.GenCodeTypeId = CommonFunction.NextMasterId("ADGenCodeType", apiBaseUrl) ;
                objAddGenCodeTypeViewModel.Sequence = objAddGenCodeTypeViewModel.GenCodeTypeId;
                objAddGenCodeTypeViewModel.GenCodeTypeId = 0;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/GenCodeType/AddGenCodeType.cshtml", objAddGenCodeTypeViewModel);
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

        
        public IActionResult ViewGenCodeType(long GenCodeTypeId)
        {
            try
            {                
                AddGenCodeTypeViewModel objAddGenCodeTypeViewModel = null;
                string endpoint = apiBaseUrl + "GenCodeTypes/" + GenCodeTypeId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objAddGenCodeTypeViewModel = JsonConvert.DeserializeObject<AddGenCodeTypeViewModel>(HttpGetResponse.Result);
                }
                else
                {
                    objAddGenCodeTypeViewModel = new AddGenCodeTypeViewModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objAddGenCodeTypeViewModel.Mode = CommonFunction.Mode.UPDATE;
                                
                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/GenCodeType/AddGenCodeType.cshtml", objAddGenCodeTypeViewModel);
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
        public IActionResult SaveGenCodeType(AddGenCodeTypeViewModel objAddGenCodeTypeViewModel)
        {
            try
            {
                objAddGenCodeTypeViewModel.EnterById = CommonFunction.UserAuthentication(this.HttpContext);
                objAddGenCodeTypeViewModel.EnterDate = DateTime.Now;
                objAddGenCodeTypeViewModel.ModifiedById = CommonFunction.UserAuthentication(this.HttpContext);
                objAddGenCodeTypeViewModel.ModifiedDate = DateTime.Now;

                if (objAddGenCodeTypeViewModel.IsActive ==null)
                {
                    objAddGenCodeTypeViewModel.IsActive = false;
                }                    

                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                
                if ((ModelState.IsValid) && (!GenCodeTypeExists(objAddGenCodeTypeViewModel.GenCodeTypeTitle) || objAddGenCodeTypeViewModel.Mode == CommonFunction.Mode.UPDATE))
                {
                    string endpoint = apiBaseUrl + "GenCodeTypes";

                    Task<string> HttpPostResponse = null;

                    if (objAddGenCodeTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objAddGenCodeTypeViewModel);
                        //Notification Message
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "GenCode Type", "Gencode Type Insert Successfully!", ""));
                    }                        
                    else if (objAddGenCodeTypeViewModel.Mode == CommonFunction.Mode.UPDATE)
                    {
                        endpoint = apiBaseUrl + "GenCodeTypes/" + objAddGenCodeTypeViewModel.GenCodeTypeId;
                        HttpPostResponse = CommonFunction.PutWebAPI(endpoint, objAddGenCodeTypeViewModel);

                        //Notification Message                       
                        //Session is used to store object
                        HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "GenCode Type", "Gencode Type Update Successfully!", ""));
                    }                        

                    if (HttpPostResponse != null)
                    {
                        objAddGenCodeTypeViewModel = JsonConvert.DeserializeObject<AddGenCodeTypeViewModel>(HttpPostResponse.Result);
                        _logger.LogInformation("Database Insert/Update: ") ;//+ JsonConvert.SerializeObject(objAddGenCodeTypeViewModel)); ;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return View("~/Views/Administrator/GenCodeType/AddGenCodeType.cshtml", objAddGenCodeTypeViewModel);
                    }
                }
                else
                {
                    ModelState.Clear();
                    if (ModelState.IsValid==false)
                    {
                        ModelState.AddModelError(string.Empty, "Validation error. Please contact administrator.");
                    }
                    else if (!GenCodeTypeExists(objAddGenCodeTypeViewModel.GenCodeTypeTitle) || objAddGenCodeTypeViewModel.Mode == CommonFunction.Mode.SAVE)
                    {
                        ModelState.AddModelError(string.Empty, "GenCode Type Allready Exist!");
                    }                    
                    
                    //Return View doesn't make a new requests, it just renders the view
                    return View("~/Views/Administrator/GenCodeType/AddGenCodeType.cshtml", objAddGenCodeTypeViewModel);
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
        public IActionResult DeleteGenCodeType(int[] DeleteGenCodeTypeIds)
        {
            try
            {
                string endpoint = apiBaseUrl + "GenCodeTypes";

                Task<string> HttpPostResponse = null;

                if (DeleteGenCodeTypeIds != null && DeleteGenCodeTypeIds.Length > 0)
                {
                    foreach (int DeleteGenCodeTypeId in DeleteGenCodeTypeIds)
                    {
                        endpoint = apiBaseUrl + "GenCodeTypes/" + DeleteGenCodeTypeId;
                        HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);
                    }                    
                }

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "GenCode Type", "Gencode Type Delete Successfully!", ""));
                    
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

        [HttpPost]
        public IActionResult DeleteGenCodeTypeById(long GenCodeTypeId)
        {
            try
            {
                string endpoint = apiBaseUrl + "GenCodeTypes";

                Task<string> HttpPostResponse = null;

                endpoint = apiBaseUrl + "GenCodeTypes/" + GenCodeTypeId;
                HttpPostResponse = CommonFunction.DeleteWebAPI(endpoint);

                if (HttpPostResponse != null)
                {
                    //Notification Message                       
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 1, 6, "GenCode Type", "Gencode Type Delete Successfully!", ""));

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

        [AllowAnonymous]
        private bool GenCodeTypeExists(string GenCodeTypeTitle)
        {
            IEnumerable<AddGenCodeTypeViewModel> objAddGenCodeTypeViewModellList = null;

            string endpoint = apiBaseUrl + "GenCodeType";

            if (CommonFunction.GetWebAPI(endpoint) != null)
            {
                objAddGenCodeTypeViewModellList = JsonConvert.DeserializeObject<IEnumerable<AddGenCodeTypeViewModel>>(CommonFunction.GetWebAPI(endpoint).Result).ToList();
            }
            else
            {
                objAddGenCodeTypeViewModellList = Enumerable.Empty<AddGenCodeTypeViewModel>().ToList();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }          

            return objAddGenCodeTypeViewModellList.Any(e => e.GenCodeTypeTitle.Trim().ToUpper() == GenCodeTypeTitle.Trim().ToUpper());
        }
        
        
    }
}
