using Abp.Zero.EntityFramework;
using Fakir.Authorization.MultiTenancy;
using Fakir.Authorization.Roles;
using Fakir.Authorization.Users;
using Fakir.Storage;
using System.Data.Common;
using System.Data.Entity;

namespace Fakir.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class FakirDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public FakirDbContext()
            : base("Default")
        {

        }

        public FakirDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public FakirDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
        }
    }
}
