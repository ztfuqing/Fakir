using Abp.MultiTenancy;
using Fakir.Authorization.Users;

namespace Fakir.Authorization.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        public Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
