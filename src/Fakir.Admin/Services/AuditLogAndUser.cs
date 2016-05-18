using Abp.Auditing;
using Fakir.Authorization.Users;

namespace Fakir.Admin.Services
{
    public class AuditLogAndUser
    {
        public AuditLog AuditLog { get; set; }

        public User User { get; set; }
    }
}