using Abp.Authorization;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Admin
{
    public class AdminAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var admin = context.GetPermissionOrNull(AdminPermissions.Admin) ?? context.CreatePermission(AdminPermissions.Admin, L("系统模块"));

            var dashboard = admin.CreateChildPermission(AdminPermissions.Admin_Dashboard, L("首页"));

            var system = admin.CreateChildPermission(AdminPermissions.Admin_System, L("系统管理"));

            var tenants = system.CreateChildPermission(AdminPermissions.Admin_System_Tenant, L("租户管理"));
            var user = system.CreateChildPermission(AdminPermissions.Admin_System_Users, L("用户管理"));
            var roles = system.CreateChildPermission(AdminPermissions.Admin_System_Roles, L("角色管理"));

            var goods = admin.CreateChildPermission(AdminPermissions.Admin_Goods, L("商品管理"));

            var tires = goods.CreateChildPermission(AdminPermissions.Admin_Goods_Tire, L("轮胎管理"));

            var tires_category= tires.CreateChildPermission(AdminPermissions.Admin_Goods_Tire_Category, L("轮胎类别管理"));

            var tires_list = tires.CreateChildPermission(AdminPermissions.Admin_Goods_Tire_List, L("轮胎列表"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FakirConsts.LocalizationSourceName);
        }
    }
}
