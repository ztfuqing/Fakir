using Abp.Application.Navigation;
using Abp.Localization;

namespace Fakir.Admin
{
    public class AdminNavigationProvider : NavigationProvider
    {
        public const string MenuName = "AdminMenu";

        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                   AdminPageNames.Dashboard,
                   FL("工作台"),
                   url: "dashboard",
                   icon: "icon-home",
                   order:0,
                   requiredPermissionName: AdminPermissions.Pages_Dashboard
                   )
               ).AddItem(new MenuItemDefinition(
                   AdminPageNames.Administration,
                   FL("系统管理"),
                   icon: "icon-wrench",
                   order:1
                   ).AddItem(new MenuItemDefinition(
                       AdminPageNames.OrganizationUnits,
                       FL("组织机构管理"),
                       url: "organizationUnits",
                       icon: "icon-layers",
                       requiredPermissionName: AdminPermissions.Pages_Administration_OrganizationUnits
                       )
                   ).AddItem(new MenuItemDefinition(
                       AdminPageNames.Roles,
                       FL("角色管理"),
                       url: "roles",
                       icon: "icon-briefcase",
                       requiredPermissionName: AdminPermissions.Pages_Administration_Roles
                       )
                   ).AddItem(new MenuItemDefinition(
                       AdminPageNames.Users,
                       FL("用户管理"),
                       url: "users",
                       icon: "icon-users",
                       requiredPermissionName: AdminPermissions.Pages_Administration_Users
                       )
                   ).AddItem(new MenuItemDefinition(
                       AdminPageNames.AuditLogs,
                       FL("审计日志"),
                       url: "auditLogs",
                       icon: "icon-lock",
                       requiredPermissionName: AdminPermissions.Pages_Administration_AuditLogs
                       )
                   )
               );
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
