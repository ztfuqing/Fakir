using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Fakir
{
    public abstract class FakirAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected FakirAppServiceBase()
        {
            LocalizationSourceName = FakirConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
