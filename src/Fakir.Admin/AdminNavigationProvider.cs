using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Admin
{
    public class AdminNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Admin";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var adminMenu = new MenuDefinition(MenuName, new FixedLocalizableString("Admin"));
            context.Manager.Menus[MenuName] = adminMenu;

            adminMenu
                .AddItem(new MenuItemDefinition(
                    "dashboard",
                    FL("首页"),
                    url: "dashboard",
                    icon: "icon-globe"
                    //requiredPermissionName: AdminPermissions.Dashboard
                    )
                ).AddItem(new MenuItemDefinition(
                    "system",
                    FL("系统管理"),
                    url: "system",
                    icon: "icon-globe"
                    //requiredPermissionName: AdminPermissions.System
                    ).AddItem(new MenuItemDefinition(
                        "users",
                        L("用户管理"),
                        url: "system.users",
                        icon: "icon-settings"
                        //requiredPermissionName: AdminPermissions.System_Users
                        )
                    ).AddItem(new MenuItemDefinition(
                        "roles",
                        L("角色管理"),
                        url: "system.roles",
                        icon: "icon-settings"
                        //requiredPermissionName: AdminPermissions.System_Roles
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
