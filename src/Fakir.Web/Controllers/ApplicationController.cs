using System.Web.Mvc;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Web.Mvc.Authorization;
using Fakir.Admin;
using Fakir.Web.Mvc;
using System.Linq;
using Abp.MultiTenancy;
using Abp.Domain.Repositories;
using Fakir.Admin.Services;
using Fakir.Admin.Dtos.Users;

namespace Fakir.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ApplicationController : FakirController
    {
        private readonly IUserAppService _userService;

        public ApplicationController(IUserAppService userService)
        {
            _userService = userService;
        }

        [DisableAuditing]
        public ActionResult Index()
        {
            //var permissions = PermissionFinder
            //    .GetAllPermissions(new AdminAuthorizationProvider())
            //    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
            //    .Select(a => a.Name)
            //    .ToList();

            //UpdateUserPermissionsInput input = new UpdateUserPermissionsInput()
            //{
            //    Id = AbpSession.UserId.Value,
            //    GrantedPermissionNames = permissions
            //};
            //_userService.UpdateUserPermissions(input);

            return View("~/admin/views/layout/layout.cshtml");
        }
    }
}