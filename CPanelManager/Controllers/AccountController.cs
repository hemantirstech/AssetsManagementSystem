#region Using

using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using CPanelManager.Models;
using CPanelManager.ViewModels.Account;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using CPanelManager.Helpers;

#endregion

namespace CPanelManager.Controllers
{
 
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        

        public AccountController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // We do not want to use any existing identity information
            await EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            var viewModel = new LoginViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                
                if (!ModelState.IsValid)
                    return View(model);

                //Get IP Address for Client Computer:
                //first the IP Address is determined for the Client machine’s which are behind Routers or Proxy Servers and hence the HTTP_X_FORWARDED_FOR server variable is checked.
                //If the IP Address is not found in the HTTP_X_FORWARDED_FOR server variable, it means that it is not using any Proxy Server and hence the IP Address is now checked in the REMOTE_ADDR server variable.
                
                string ipAddress = "";Request.HttpContext.Connection.RemoteIpAddress.ToString();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                }

                string strSessionId = HttpContext.Session.Id;

                // Verify if a user exists with the provided identity information      
                IEnumerable<ValidateAccountViewModel> objValidateAccountViewModelList = null;

                //Authentication & Authorisation
                ClaimsIdentity identity = null;
                bool isAuthenticated = false;

                string endpoint = apiBaseUrl + "ValidateAccount?UserName=" + model.Email + "&Password=" + CommonFunction.Encrypt(model.Password, true) + "&VerificationCode='0'&MacId=" + ipAddress + "&SessionId=" + strSessionId;

                if (CommonFunction.GetWebAPI(endpoint) != null)
                {
                    objValidateAccountViewModelList = JsonConvert.DeserializeObject<IEnumerable<ValidateAccountViewModel>>(CommonFunction.GetWebAPI(endpoint).Result);
                }
                else
                {
                    objValidateAccountViewModelList = Enumerable.Empty<ValidateAccountViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                if (objValidateAccountViewModelList != null && objValidateAccountViewModelList.Count() >0 && objValidateAccountViewModelList.Where(a => a.ValidationCount == 1).Count() > 0 && objValidateAccountViewModelList.Where(a => a.IsFirstLogin == true).Count() == 0)
                {
                    ProfileMenuRightsViewModel objProfileMenuRightsViewModel = new ProfileMenuRightsViewModel();
                    objProfileMenuRightsViewModel.ValidateAccountViewModelList = objValidateAccountViewModelList.ToList();

                    objProfileMenuRightsViewModel.PayCalPeriod = DateTime.Now.ToString("MMyyyy");
                    objProfileMenuRightsViewModel.PayPeriod = DateTime.Now.AddMonths(-1).ToString("MMyyyy");
                    objProfileMenuRightsViewModel.MasterLoginId = objValidateAccountViewModelList.Select(a => a.MasterLoginId).FirstOrDefault();
                    objProfileMenuRightsViewModel.MasterFinancialId = 3;
                    objProfileMenuRightsViewModel.MasterCompanyId = objValidateAccountViewModelList.Select(a => a.MasterCompanyId).FirstOrDefault() ?? 0;
                    objProfileMenuRightsViewModel.MasterCompanyName = "Knowledge Ridge Private Limited";
                    objProfileMenuRightsViewModel.SessionId = strSessionId;
                    objProfileMenuRightsViewModel.LoginIP = ipAddress;
                    objProfileMenuRightsViewModel.LastSuccessfullLogin = objValidateAccountViewModelList.Select(a => a.LastLoginDate).FirstOrDefault().ToString();

                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("MenuDetail", objProfileMenuRightsViewModel);

                    identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, model.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticated = true;
                }

                //Check for Authentication
                if (isAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //Notification Message 
                    //Session is used to store object
                    HttpContext.Session.SetObjectAsJson("GlobalMessage", CommonFunction.GlobalMessage(1, 3, 4, "HI " + objValidateAccountViewModelList.Select(a => a.EmployeeName).FirstOrDefault(), "You have successfully login!", "Your last successfull login was on dated " + objValidateAccountViewModelList.Select(a => a.LastLoginDate).FirstOrDefault().ToString() + "!"));
                    
                    _logger.LogInformation(1, "User " + objValidateAccountViewModelList.Select(a => a.EmployeeName).FirstOrDefault() + " successfully logged in.");

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    _logger.LogWarning(2, "Invalid login attempt by" + objValidateAccountViewModelList.Select(a => a.EmployeeName).FirstOrDefault());

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                    // If we get to this point, something failed, redisplay form and trigger the error summary
                    return View(model);
                }
            }
            catch (System.Exception ex)
            {
                string ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string ErrorMessage = "Controler:" + ControllerName + " , Action:" + ActionName + " , Exception:" + ex.Message;

                _logger.LogError(ErrorMessage);
                return View("~/Views/Shared/Error.cshtml", CommonFunction.HandleErrorInfo(ex, ActionName, ControllerName));
            }
            return new EmptyResult();
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            // We do not want to use any existing identity information
            await EnsureLoggedOut();

            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //var user = new ApplicationUser
            //{
            //    UserName = model.Email,
            //    Email = model.Email
            //};

            //var result = await _userManager.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            //{
            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            //    // Send an email with this link
            //    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //    await _signInManager.SignInAsync(user, false);
            //    _logger.LogInformation(3, "User created a new account with password.");
            //    return RedirectToAction(nameof(HomeController.Index), "Home");
            //}

            //AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /account/error
        [AllowAnonymous]
        public async Task<IActionResult> Error()
        {
            // We do not want to use any existing identity information
            await EnsureLoggedOut();

            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            //await _signInManager.SignOutAsync();
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            // Clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            _logger.LogInformation(4, "User logged out.");

            return RedirectToLocal();
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //    // Request a redirect to the external login provider.
        //    var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new
        //    {
        //        ReturnUrl = returnUrl
        //    });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return new ChallengeResult(provider, properties);
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        //{
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    // Sign in the user with this external login provider if the user already has a login.
        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    if (result.RequiresTwoFactor)
        //    {
        //        return RedirectToAction(nameof(SendCode), new
        //        {
        //            ReturnUrl = returnUrl
        //        });
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        return View("Lockout");
        //    }
        //    // If the user does not have an account, then ask the user to create an account.
        //    ViewData["ReturnUrl"] = returnUrl;
        //    ViewData["LoginProvider"] = info.LoginProvider;
        //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
        //    {
        //        Email = email
        //    });
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction(nameof(HomeController.Index), "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await _signInManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.Email,
        //            Email = model.Email
        //        };
        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await _userManager.AddLoginAsync(user, info);
        //            if (result.Succeeded)
        //            {
        //                await _signInManager.SignInAsync(user, false);
        //                _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View(model);
        //}

        // GET: /Account/ConfirmEmail
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            // We do not want to use any existing identity information
            await EnsureLoggedOut();

            return View();
        }

        //
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(model.Email);
        //        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //        // Send an email with this link
        //        //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //        //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
        //        //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
        //        //return View("ForgotPasswordConfirmation");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await _userManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
        //    }
        //    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(ResetPasswordConfirmation), "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/SendCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        //{
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem
        //    {
        //        Text = purpose,
        //        Value = purpose
        //    }).ToList();
        //    return View(new SendCodeViewModel
        //    {
        //        Providers = factorOptions,
        //        ReturnUrl = returnUrl,
        //        RememberMe = rememberMe
        //    });
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (User.Identity.IsAuthenticated)
                await LogOff();
        }
    }
}