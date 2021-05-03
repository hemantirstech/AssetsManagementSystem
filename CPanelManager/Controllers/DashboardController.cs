using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.Dashboard;
using CPanelManager.ViewModels.Account;
using CPanelManager.Helpers;
using CPanelManager.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CPanelManager.ViewModels.AssetsExpiry;
using CPanelManager.Services;

namespace CPanelManager.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IEmailSender _emailSender;

        public DashboardController(ILoggerFactory loggerFactory, IConfiguration configuration, IEmailSender IEmailSender)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
            _emailSender = IEmailSender;
        }

        public IActionResult Index()
        {
            ProfileMenuRightsViewModel objProfileMenuRightsViewModel = HttpContext.Session.GetObjectFromJson<ProfileMenuRightsViewModel>("MenuDetail");


            //DashboardAdminViewModel objDashboardAdminViewModel = new DashboardAdminViewModel();          

            DashboardAdminViewModel objDashboardAdminViewModel = null;
            string endpoint = assetsApiBaseUrl + "Dashboard?MasterSubCategoryId=0&MasterBranchId=0";
            Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

            if (HttpGetResponse != null)
            {
                objDashboardAdminViewModel = JsonConvert.DeserializeObject<DashboardAdminViewModel>(HttpGetResponse.Result);
            }
            else
            {
                objDashboardAdminViewModel = new DashboardAdminViewModel(); ;

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            //OtherDetail

            objDashboardAdminViewModel.CurrentDateTime = DateTime.Now;
            objDashboardAdminViewModel.LastSuccessfullLogin = objProfileMenuRightsViewModel.LastSuccessfullLogin;
            objDashboardAdminViewModel.CompanyMasterName = objProfileMenuRightsViewModel.MasterCompanyName;
            objDashboardAdminViewModel.LoginIP = objProfileMenuRightsViewModel.LoginIP;
            objDashboardAdminViewModel.SessionId = objProfileMenuRightsViewModel.SessionId;

            return View(objDashboardAdminViewModel);
        }

        public IActionResult RemiderExpire()
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
                objIndexAssetsExpiryViewModel.AssetsExpiryViewModelList = objAssetsExpiryViewModelList.ToList();
                
                string HtmlBody = "<div ><b>Dear KR Team</b><br /><br />Following are the list of assets/services will be expire in next 2 days.<br /><br />";

                HtmlBody = HtmlBody + "<table><tr><td>Assets</td><td>Expiry</td><td>Type</td></tr>";
                //.Where(a=>a.WarrantyExpiryDate?.ToString("dd-MM-yyyy")==DateTime.Now.ToString("dd-MM-yyyy") )
                foreach (var Item in objAssetsExpiryViewModelList)
                {
                    HtmlBody = HtmlBody + "<tr><td>" + Item.ProductTitle + " (" + Item.ManufacturerPartNumber + ")</td><td>" + Item.WarrantyExpiryDate?.ToString("dd-MMM-yyyy") + "</td><td>" + (Item.MasterCategoryType == 1 ? "Assets" : "Service") + "</td></tr>";
                }
                HtmlBody = HtmlBody + "</table>";

                //var message = new Message(new string[] { "amit.kubade@knowledgeridge.com", "vivek.patil@knowledgeridge.com","aniruddha.pakhale@gmail.com","aniruddha@irstechnologies.com"}, "Assets/Services Expiry Reminder from www.kritms.com", HtmlBody);
                //_emailSender.SendEmail(message);
                return View(objIndexAssetsExpiryViewModel);
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
