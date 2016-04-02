using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Users;

namespace Fakir.Authorization.Roles
{
    public class RoleStore : AbpRoleStore<Tenant, Role, User>
    {
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}
