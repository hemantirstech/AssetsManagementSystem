using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.ProfileTaskMapping;
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
    public class ProfileTaskMappingsController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;

        public ProfileTaskMappingsController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<ProfileTaskMappingsController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            try
            {
                IndexProfileTaskMappingViewModel objIndexProfileTaskMappingViewModel = new IndexProfileTaskMappingViewModel();
                IEnumerable<MasterProfileViewModel> objMasterProfileViewModelList = null;

                string endpoint = apiBaseUrl + "MasterProfiles";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProfileViewModelList = JsonConvert.DeserializeObject<IEnumerable<MasterProfileViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objMasterProfileViewModelList = Enumerable.Empty<MasterProfileViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                objIndexProfileTaskMappingViewModel.MasterProfileViewModelList = objMasterProfileViewModelList.OrderBy(a=> a.ProfileTitle).ToList();

                //############# Profile Maping ###################
                CPanelManager.ViewModels.Account.ValidateAccountViewModel objValidateAccountViewModel = CommonFunction.ActionResultAuthentication(HttpContext, "/ProfileTaskMappings/Index");

                if (objValidateAccountViewModel != null)
                {
                    objIndexProfileTaskMappingViewModel.IsSelect = objValidateAccountViewModel.IsSelect;
                    objIndexProfileTaskMappingViewModel.IsInsert = objValidateAccountViewModel.IsInsert;
                    objIndexProfileTaskMappingViewModel.IsUpdate = objValidateAccountViewModel.IsUpdate;
                    objIndexProfileTaskMappingViewModel.IsDelete = objValidateAccountViewModel.IsDelete;
                }
                //############# Profile Maping End ###################

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/ProfileTaskMappings/Index.cshtml", objIndexProfileTaskMappingViewModel);
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

        

        public IActionResult ViewProfileTaskMapping(long MasterProfileId)
        {
            try
            {                
                IEnumerable<ViewProfileTaskMappingViewModel> objViewProfileTaskMappingViewModelList = null;

                string endpoint = apiBaseUrl + "ProfileTaskMappingWithFunction?MasterProfileId=" + MasterProfileId;
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if(HttpGetResponse != null && MasterProfileId>0)
                {
                    objViewProfileTaskMappingViewModelList = JsonConvert.DeserializeObject<IEnumerable<ViewProfileTaskMappingViewModel>>(HttpGetResponse.Result).ToList();
                }
                else
                {
                    objViewProfileTaskMappingViewModelList = Enumerable.Empty<ViewProfileTaskMappingViewModel>().ToList();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                List<ViewProfileTaskMappingViewModel> objProfileTaskMappingViewModelList = new List<ViewProfileTaskMappingViewModel>();
                if (objViewProfileTaskMappingViewModelList != null)
                {
                    foreach(var Item in objViewProfileTaskMappingViewModelList.OrderBy(a=>a.ParentMasterFunctionId))
                    {
                        ViewProfileTaskMappingViewModel objViewProfileTaskMappingViewModel = new ViewProfileTaskMappingViewModel();

                        objViewProfileTaskMappingViewModel.MasterProfileTaskMappingId = Item.MasterProfileTaskMappingId;
                        objViewProfileTaskMappingViewModel.MasterProfileId = Item.MasterProfileId;
                        objViewProfileTaskMappingViewModel.ProfileTitle = Item.ProfileTitle;
                        objViewProfileTaskMappingViewModel.ProfileDescription = Item.ProfileDescription;
                        objViewProfileTaskMappingViewModel.ProfileCode = Item.ProfileCode;

                        if(objViewProfileTaskMappingViewModel.MasterProfileTaskMappingId==null || objViewProfileTaskMappingViewModel.MasterProfileTaskMappingId==0)
                        {
                            objViewProfileTaskMappingViewModel.MasterProfileTaskMappingId = 0;
                            objViewProfileTaskMappingViewModel.Mode = CommonFunction.Mode.SAVE;
                        }
                        else
                        {
                            objViewProfileTaskMappingViewModel.Mode = CommonFunction.Mode.UPDATE;
                        }

                        objViewProfileTaskMappingViewModel.MasterFunctionId = Item.MasterFunctionId;
                        objViewProfileTaskMappingViewModel.FunctionTitle = Item.FunctionTitle;
                        objViewProfileTaskMappingViewModel.FunctionLink = Item.FunctionLink;
                        objViewProfileTaskMappingViewModel.FunctionIconColour = Item.FunctionIconColour;
                        objViewProfileTaskMappingViewModel.FunctionIcon = Item.FunctionIcon;
                        objViewProfileTaskMappingViewModel.ParentMasterFunctionId = Item.ParentMasterFunctionId;
                        objViewProfileTaskMappingViewModel.ParentFunctionTitle = (Item.ParentFunctionTitle!=null? Item.ParentFunctionTitle:"PARENT MENU");
                        objViewProfileTaskMappingViewModel.Sequence = Item.Sequence;

                        objViewProfileTaskMappingViewModel.IsDelete = (Item.IsDelete!=null? Item.IsDelete:false);
                        objViewProfileTaskMappingViewModel.IsInsert = (Item.IsInsert != null? Item.IsInsert : false);
                        objViewProfileTaskMappingViewModel.IsUpdate = (Item.IsUpdate != null? Item.IsUpdate : false);
                        objViewProfileTaskMappingViewModel.IsSelect = (Item.IsSelect != null? Item.IsSelect : false);
                        objViewProfileTaskMappingViewModel.IsActive = (Item.IsActive != null? Item.IsActive : false);

                        if (objViewProfileTaskMappingViewModel.IsDelete == true)
                        {
                            objViewProfileTaskMappingViewModel.DeleteColor = "green";
                            objViewProfileTaskMappingViewModel.DeleteIcon = "glyphicon glyphicon-ok";
                        }

                        if (objViewProfileTaskMappingViewModel.IsInsert == true)
                        {
                            objViewProfileTaskMappingViewModel.InsertColor = "green";
                            objViewProfileTaskMappingViewModel.InsertIcon = "glyphicon glyphicon-ok";
                        }

                        if (objViewProfileTaskMappingViewModel.IsUpdate == true)
                        {
                            objViewProfileTaskMappingViewModel.UpdateColor = "green";
                            objViewProfileTaskMappingViewModel.UpdateIcon = "glyphicon glyphicon-ok";
                        }

                        if (objViewProfileTaskMappingViewModel.IsSelect == true)
                        {
                            objViewProfileTaskMappingViewModel.SelectColor = "green";
                            objViewProfileTaskMappingViewModel.SelectIcon = "glyphicon glyphicon-ok";
                        }

                        if (objViewProfileTaskMappingViewModel.IsActive == true)
                        {
                            objViewProfileTaskMappingViewModel.ActiveColor = "green";
                            objViewProfileTaskMappingViewModel.ActiveIcon = "glyphicon glyphicon-ok";
                        }

                        objProfileTaskMappingViewModelList.Add(objViewProfileTaskMappingViewModel);
                    }
                }

                IndexProfileTaskMappingViewModel objIndexProfileTaskMappingViewModel = new IndexProfileTaskMappingViewModel();
                objIndexProfileTaskMappingViewModel.ViewProfileTaskMappingList = objProfileTaskMappingViewModelList;

                

                //objIndexProfileTaskMappingViewModel.Mode = CommonFunction.Mode.UPDATE;

                //Return View doesn't make a new requests, it just renders the view
                return View("~/Views/Administrator/ProfileTaskMappings/ViewProfileTaskMapping.cshtml", objIndexProfileTaskMappingViewModel);
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
        public IActionResult SaveProfileTaskMapping(IndexProfileTaskMappingViewModel objIndexProfileTaskMappingViewModel)
        {
            try
            {
                long _MasterProfileId = objIndexProfileTaskMappingViewModel.ViewProfileTaskMappingList.Select(a => a.MasterProfileId).FirstOrDefault()??1;

                string endpoint = apiBaseUrl + "ProfileTaskMappings";
                Task<string> HttpPostResponse = null;

                HttpPostResponse = CommonFunction.PostWebAPI(endpoint, objIndexProfileTaskMappingViewModel.ViewProfileTaskMappingList);
                //Notification Message
                //Session is used to store object
                HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 2, 4, "Profile Task Mapping", "Profile Task Mapping Successfully!", ""));

                if (HttpPostResponse != null)
                {
                    var objViewProfileTaskMappingViewModelList = JsonConvert.DeserializeObject<IEnumerable<ViewProfileTaskMappingViewModel>>(HttpPostResponse.Result).ToList();
                    objIndexProfileTaskMappingViewModel.ViewProfileTaskMappingList = objViewProfileTaskMappingViewModelList.ToList();
                    _logger.LogInformation("Database Insert/Update: ");//+ JsonConvert.SerializeObject(objAddMasterProfileViewModel)); ;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return RedirectToAction("Index");
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

        

        
    }
}
