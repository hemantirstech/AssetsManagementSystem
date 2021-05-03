using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CPanelManager.ViewModels;
using CPanelManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CPanelManager.Models;

namespace CPanelManager.Controllers
{
    public class CommonController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        private string assetsApiBaseUrl;

        public CommonController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
            assetsApiBaseUrl = _Configure.GetValue<string>("AssetsWebAPIBaseUrl");
        }
        public JsonResult DropDownFill(string TableName, long MasterId, string Type)
        {
            try
            {
                List<ViewModels.DropDownFill> objDropDownFillList = CommonFunction.DropDownFill(TableName, MasterId, Type, apiBaseUrl);

                string data = JsonConvert.SerializeObject(objDropDownFillList);

                JsonResult objJsonResult = new JsonResult(data);

                if (objDropDownFillList.Count() > 0)
                   return objJsonResult;
            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
            }
            return Json("");
        }

        public JsonResult AssetsDropDownFill(string TableName, long MasterId, string Type)
        {
            try
            {
                List<ViewModels.DropDownFill> objDropDownFillList = CommonFunction.DropDownFillAssets(TableName, MasterId, Type, assetsApiBaseUrl);

                string data = JsonConvert.SerializeObject(objDropDownFillList);

                JsonResult objJsonResult = new JsonResult(data);

                if (objDropDownFillList.Count() > 0)
                    return objJsonResult;
            }
            catch (Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
            }
            return Json("");
        }
    }
}
