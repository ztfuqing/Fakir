using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Fakir.Authorization.Editions;
using Fakir.Authorization.Roles;
using Fakir.Authorization.Users;

namespace Fakir.Authorization.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager
            )
        {
        }
    }
}
