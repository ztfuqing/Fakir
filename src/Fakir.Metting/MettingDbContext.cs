using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using Fakir.Metting.Domain;

namespace Fakir.Metting
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MettingDbContext : AbpDbContext
    {
        public virtual IDbSet<Conference> Conferences { get; set; }

        public virtual IDbSet<Agenda> Agendas { get; set; }

        public virtual IDbSet<AgendaFile> AgendaFiles { get; set; }

        public virtual IDbSet<ConferenceUser> ConferenceUsers { get; set; }

        public MettingDbContext()
            : base("Data Source=localhost;user id=root;password=123;port=3306;Initial Catalog=fakir;Character Set=utf8;Allow User Variables=True;")
        {

        }

        public MettingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public MettingDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
        }
    }
}
