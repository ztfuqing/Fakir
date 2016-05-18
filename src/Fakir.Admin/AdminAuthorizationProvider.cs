using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Fakir.Admin
{
    public class AdminAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            var pages = context.GetPermissionOrNull(AdminPermissions.Pages) ?? context.CreatePermission(AdminPermissions.Pages, FL("功能"));

            var administration = pages.CreateChildPermission(AdminPermissions.Pages_Administration, FL("系统管理"));

            var roles = administration.CreateChildPermission(AdminPermissions.Pages_Administration_Roles, FL("角色管理"));
            roles.CreateChildPermission(AdminPermissions.Pages_Administration_Roles_Create, FL("创建角色"));
            roles.CreateChildPermission(AdminPermissions.Pages_Administration_Roles_Edit, FL("编辑角色"));
            roles.CreateChildPermission(AdminPermissions.Pages_Administration_Roles_Delete, FL("删除角色"));

            var users = administration.CreateChildPermission(AdminPermissions.Pages_Administration_Users, FL("用户管理"));
            users.CreateChildPermission(AdminPermissions.Pages_Administration_Users_Create, FL("创建用户"));
            users.CreateChildPermission(AdminPermissions.Pages_Administration_Users_Edit, FL("编辑用户"));
            users.CreateChildPermission(AdminPermissions.Pages_Administration_Users_Delete, FL("删除用户"));
            users.CreateChildPermission(AdminPermissions.Pages_Administration_Users_ChangePermissions, FL("用户授权"));


            administration.CreateChildPermission(AdminPermissions.Pages_Administration_AuditLogs, FL("审计日志"));

            var organizationUnits = administration.CreateChildPermission(AdminPermissions.Pages_Administration_OrganizationUnits, FL("组织机构管理"));
            organizationUnits.CreateChildPermission(AdminPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, FL("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AdminPermissions.Pages_Administration_OrganizationUnits_ManageMembers, FL("ManagingMembers"));


            pages.CreateChildPermission(AdminPermissions.Pages_Dashboard, FL("工作台"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FakirConsts.LocalizationSourceName);
        }

        private static ILocalizableString FL(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
