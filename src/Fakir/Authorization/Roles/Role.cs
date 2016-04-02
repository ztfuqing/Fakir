using Abp.Authorization.Roles;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Users;

namespace Fakir.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
