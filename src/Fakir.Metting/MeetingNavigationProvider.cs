using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Localization;

namespace Fakir.Metting
{
    public class MeetingNavigationProvider : NavigationProvider
    {
        public const string MenuName = "AdminMenu";

        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                    "meeting",
                    FL("会议管理"),
                    icon: "icon-home",
                    order: 3
                    //requiredPermissionName: AdminPermissions.Pages_Dashboard
                    ).AddItem(new MenuItemDefinition(
                       "meetmanager",
                       FL("会议管理"),
                       url: "meeting",
                       icon: "icon-layers"
                       //requiredPermissionName: AdminPermissions.Pages_Administration_OrganizationUnits
                       )
                   ).AddItem(new MenuItemDefinition(
                       "agenda",
                       FL("议程管理"),
                       url: "agenda",
                       icon: "icon-layers"
                       //requiredPermissionName: AdminPermissions.Pages_Administration_OrganizationUnits
                       )
                   ).AddItem(new MenuItemDefinition(
                       "summary",
                       FL("纪要诀要"),
                       url: "summary",
                       icon: "icon-layers"
                       //requiredPermissionName: AdminPermissions.Pages_Administration_OrganizationUnits
                       )
                   )
            );
        }

        private static ILocalizableString FL(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
