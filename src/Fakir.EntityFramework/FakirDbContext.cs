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
            : base("Data Source=localhost;user id=root;password=123;port=3306;Initial Catalog=fakir;Character Set=utf8;Allow User Variables=True;")
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
