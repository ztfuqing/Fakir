using Abp.Application.Features;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Roles;
using Fakir.Authorization.Users;

namespace Fakir.Authorization.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}
