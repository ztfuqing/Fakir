using Abp.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fakir.Authorization.Users;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Roles;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Fakir.Authorization;
using Abp.Notifications;
using Microsoft.Owin.Security;
using Fakir.Web.Models;
using Abp.Auditing;
using System.Threading.Tasks;
using Abp.Web.Mvc.Models;
using Fakir.Tools;
using Fakir.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.UI;

namespace Fakir.Web.Controllers
{
    public class AccountController : FakirController
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManager _cacheManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly IUserLinkManager _userLinkManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public AccountController(
           UserManager userManager,
           IMultiTenancyConfig multiTenancyConfig,
           RoleManager roleManager,
           TenantManager tenantManager,
           IUnitOfWorkManager unitOfWorkManager,
           ICacheManager cacheManager,
           AbpLoginResultTypeHelper abpLoginResultTypeHelper,
           IUserLinkManager userLinkManager,
           INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _roleManager = roleManager;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _cacheManager = cacheManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _userLinkManager = userLinkManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Index", "Application");
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View(
                new LoginFormViewModel
                {
                    TenancyName = Tenant.DefaultTenantName,
                    SuccessMessage = successMessage,
                    UserNameOrEmailAddress = userNameOrEmailAddress
                });
        }

        [HttpPost]
        [UnitOfWork]
        [DisableAuditing]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();
            loginModel.TenancyName = null;
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);

            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, loginModel.TenancyName);

            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                loginResult.User.SetNewPasswordResetCode();

                return Json(new MvcAjaxResponse
                {
                    TargetUrl = Url.Action(
                        "ResetPassword",
                        new ResetPasswordViewModel
                        {
                            UserId = SimpleStringCipher.Encrypt(loginResult.User.Id.ToString()),
                            ResetCode = loginResult.User.PasswordResetCode
                        })
                });
            }

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Index", "Application");
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            return Json(new MvcAjaxResponse { TargetUrl = returnUrl });
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }


        [UnitOfWork]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            CheckModelState();

            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);

            var userId = Convert.ToInt64(SimpleStringCipher.Decrypt(model.UserId));

            var user = await _userManager.GetUserByIdAsync(userId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != model.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordFormViewModel model)
        {
            CheckModelState();

            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);

            var userId = Convert.ToInt64(SimpleStringCipher.Decrypt(model.UserId));

            var user = await _userManager.GetUserByIdAsync(userId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != model.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            user.Password = new PasswordHasher().HashPassword(model.Password);
            user.PasswordResetCode = null;
            user.IsEmailConfirmed = true;
            user.ShouldChangePasswordOnNextLogin = false;

            await _userManager.UpdateAsync(user);

            if (user.IsActive)
            {
                await SignInAsync(user);
            }

            return RedirectToAction("Index", "Application");
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private async Task<AbpUserManager<Tenant, Role, User>.AbpLoginResult> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

    }
}