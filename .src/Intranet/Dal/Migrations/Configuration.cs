using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using Intranet.Model;

namespace Intranet.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IntranetContext>
    {
        #region Ctor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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

            //  var admin = new Role { Name = "Administrator" };
            var admin = new Role { Name = "Everyone" };
            var laboruser = new Role { Name = "LaborUser" };
            var laborviewer = new Role { Name = "LaborViewer" };

            context.Roles.AddOrUpdate( m => m.Name, admin, laboruser, laborviewer );
            var roles = new Collection<Role> { admin, laboruser, laborviewer };
            var rolesadmin = new Collection<Role> { admin };

            var labor = new MainModule { Name = "Labor", Description = "Labor QS", ActionName = "Index", ControllerName = "LaborHome", Visible = true, Roles = roles };
            var adminshell = new MainModule
            {
                Name = "Einstellungen",
                Description = "Einstellungen für die Shell",
                ActionName = "Index",
                ControllerName = "SettingHome",
                Visible = true,
                Roles = rolesadmin
            };

            context.MainModules.AddOrUpdate( m => m.Name, labor, adminshell );

            var creator = new SubModule
            {
                Description = "Labor QS Creator",
                MainModule = labor,
                Name = "LaborCreator",
                ControllerName = "LaborCreatorHome",
                ActionName = "Index",
                Roles = roles
            };
            var dashboard = new SubModule
            {
                Description = "Labor QS Dashboard",
                MainModule = labor,
                Name = "LaborDashboard",
                ControllerName = "LaborDashboardHome",
                ActionName = "Index",
                Roles = roles
            };

            context.SubModules.AddOrUpdate( m => m.Name, creator, dashboard );
        }
    }
}