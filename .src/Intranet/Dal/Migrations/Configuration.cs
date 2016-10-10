using System.Data.Entity.Migrations;

namespace Intranet.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IntranetContext>
    {
        #region Ctor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        protected override void Seed( IntranetContext context )
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}