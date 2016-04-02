using Abp.Authorization.Users;
using Abp.Extensions;
using Fakir.Authorization.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;

namespace Fakir.Authorization.Users
{
    public class User : AbpUser<Tenant, User>
    {
        public const int MinPlainPasswordLength = 6;

        public virtual Guid? ProfilePictureId { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual long? UserLinkId { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }

        public virtual string Mobile { get; set; }
    }
}
