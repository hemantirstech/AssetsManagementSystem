using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels.MasterPassword;
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

namespace CPanelManager.Controllers.Assets
{
    [Authorize]
    public class MasterPasswordController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MasterPasswordController(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MasterProductController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");

            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                IndexMasterProductChildViewModel objIndexMasterProductChildViewModel = new IndexMasterProductChildViewModel();
                IEnumerable<MasterProductAssignChildResult> objMasterProductAssignChildResultList = null;

                string endpoint = assetsApiBaseUrl + "MasterProductChild";
                Task<string> HttpGetResponse = CommonFunction.GetWebAPI(endpoint);

                if (HttpGetResponse != null)
                {
                    objMasterProductAssignChildResultList = JsonConvert.DeserializeObject<IEnumerable<MasterProductAssignChildResult>>(HttpGetResponse.Result);
                }
                else
                {
                    objMasterProductAssignChildResultList = Enumerable.Empty<MasterProductAssignChildResult>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                objIndexMasterProductChildViewModel.ProductAssignChildList = objMasterProductAssignChildResultList.Where(a=>a.ServicePassword!="" && a.ServicePassword != null).ToList();
                //objIndexMasterProductChildViewModel.ProductAssignChildList = objIndexMasterProductChildViewModel.ProductAssignChildList.Where(a => a.ServicePassword != null && a.ServiceUserName != null).ToList();
                return View("~/Views/Assets/MasterPassword/Index.cshtml", objIndexMasterProductChildViewModel);
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
