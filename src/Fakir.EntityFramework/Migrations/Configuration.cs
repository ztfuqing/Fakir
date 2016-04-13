namespace Fakir.EntityFramework.Migrations
{
    using SeedData;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Fakir.EntityFramework.FakirDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Fakir";
        }

        protected override void Seed(Fakir.EntityFramework.FakirDbContext context)
        {
            new InitialDataBuilder(context).Build();
        }
    }
}
