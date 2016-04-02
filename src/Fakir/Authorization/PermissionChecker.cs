using Abp.Authorization;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Roles;
using Fakir.Authorization.Users;

namespace Fakir.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
